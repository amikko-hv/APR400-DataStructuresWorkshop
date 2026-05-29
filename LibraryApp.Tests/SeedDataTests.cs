using LibraryApp;
using LibraryApp.Models;

namespace LibraryApp.Tests;

/// <summary>
/// Tests for <see cref="SeedData.GetSeedData"/>.
/// These tests verify the structure and integrity of the seed dataset
/// directly, without going through <see cref="LibraryService"/>.
/// </summary>
public class SeedDataTests
{
    // Cached result so each test works on the same object without re-computing.
    private readonly LibraryData _data = SeedData.GetSeedData();

    [Fact]
    public void GetSeedData_ReturnsAtLeastOneHundredBooks()
    {
        Assert.True(_data.Books.Count >= 100);
    }

    [Fact]
    public void GetSeedData_ReturnsAtLeastFiveMembers()
    {
        Assert.True(_data.Members.Count >= 5);
    }

    [Fact]
    public void GetSeedData_ReturnsAtLeastTwoLoans()
    {
        Assert.True(_data.Loans.Count >= 2);
    }

    [Fact]
    public void GetSeedData_AllBookIdsAreUnique()
    {
        var ids = _data.Books.Select(b => b.Id).ToList();
        Assert.Equal(ids.Count, ids.Distinct().Count());
    }

    [Fact]
    public void GetSeedData_AllMemberIdsAreUnique()
    {
        var ids = _data.Members.Select(m => m.Id).ToList();
        Assert.Equal(ids.Count, ids.Distinct().Count());
    }

    [Theory]
    [InlineData("B001")]
    [InlineData("B002")]
    public void GetSeedData_PreBorrowedBookIsMarkedBorrowed(string bookId)
    {
        // Arrange
        var book = _data.Books.FirstOrDefault(b => b.Id == bookId);

        // Assert
        Assert.NotNull(book);
        Assert.True(book.IsBorrowed);
    }

    [Theory]
    [InlineData("B001")]
    [InlineData("B002")]
    public void GetSeedData_PreBorrowedBookHasMatchingActiveLoan(string bookId)
    {
        // Arrange
        var loan = _data.Loans.FirstOrDefault(l => l.BookId == bookId);

        // Assert
        Assert.NotNull(loan);
    }

    [Fact]
    public void GetSeedData_EveryActiveLoanReferencesAnExistingBook()
    {
        // Arrange
        var bookIds = _data.Books.Select(b => b.Id).ToHashSet();

        // Assert
        Assert.All(_data.Loans, loan => Assert.Contains(loan.BookId, bookIds));
    }

    [Fact]
    public void GetSeedData_EveryActiveLoanReferencesAnExistingMember()
    {
        // Arrange
        var memberIds = _data.Members.Select(m => m.Id).ToHashSet();

        // Assert
        Assert.All(_data.Loans, loan => Assert.Contains(loan.MemberId, memberIds));
    }

    [Fact]
    public void GetSeedData_EachBorrowedBookHasAtLeastOneLoan()
    {
        // Arrange
        var borrowedBooks  = _data.Books.Where(b => b.IsBorrowed).Select(b => b.Id).ToHashSet();
        var loanedBookIds  = _data.Loans.Select(l => l.BookId).ToHashSet();

        // Assert — every book flagged as borrowed must have a loan record
        Assert.All(borrowedBooks, id => Assert.Contains(id, loanedBookIds));
    }
}

