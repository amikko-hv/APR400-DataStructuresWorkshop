using LibraryApp;
using LibraryApp.Models;

namespace LibraryApp.Tests;

/// <summary>
/// Behavioural tests for <see cref="LibraryService"/>.
/// </summary>
public class LibraryServiceTests
{
    // =========================================================================
    // AddBook
    // =========================================================================

    [Fact]
    public void AddBook_WhenBookIsNew_ReturnsSuccess()
    {
        // Arrange
        var service = new LibraryService();
        var book = new Book { Id = "X001", Title = "Clean Code", Author = "Robert C. Martin", Year = 2008 };

        // Act
        var result = service.AddBook(book);

        // Assert
        Assert.True(result.Success);
    }

    [Fact]
    public void AddBook_WhenBookIsNew_BookAppearsInList()
    {
        // Arrange
        var service = new LibraryService();
        var book = new Book { Id = "X001", Title = "Clean Code", Author = "Robert C. Martin", Year = 2008 };

        // Act
        service.AddBook(book);

        // Assert
        Assert.Contains(service.ListAllBooks(), b => b.Id == "X001");
    }

    [Fact]
    public void AddBook_WhenDuplicateId_ReturnsFailure()
    {
        // Arrange
        var data = new LibraryData
        {
            Books = new List<Book>
            {
                new Book { Id = "X001", Title = "Clean Code", Author = "Robert C. Martin", Year = 2008 }
            }
        };
        var service = new LibraryService(data);

        // Act
        var result = service.AddBook(new Book { Id = "X001", Title = "Other Book", Author = "Someone", Year = 2020 });

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public void AddBook_WhenDuplicateId_DoesNotDuplicateBook()
    {
        // Arrange
        var data = new LibraryData
        {
            Books = new List<Book>
            {
                new Book { Id = "X001", Title = "Clean Code", Author = "Robert C. Martin", Year = 2008 }
            }
        };
        var service = new LibraryService(data);

        // Act
        service.AddBook(new Book { Id = "X001", Title = "Other Book", Author = "Someone", Year = 2020 });

        // Assert
        Assert.Equal(1, service.ListAllBooks().Count(b => b.Id == "X001"));
    }

    // =========================================================================
    // RemoveBook
    // =========================================================================

    [Fact]
    public void RemoveBook_WhenBookExists_ReturnsSuccess()
    {
        // Arrange
        var data = new LibraryData
        {
            Books = new List<Book>
            {
                new Book { Id = "X001", Title = "Clean Code", Author = "Robert C. Martin", Year = 2008 }
            }
        };
        var service = new LibraryService(data);

        // Act
        var result = service.RemoveBook("X001");

        // Assert
        Assert.True(result.Success);
    }

    [Fact]
    public void RemoveBook_WhenBookExists_BookNoLongerInList()
    {
        // Arrange
        var data = new LibraryData
        {
            Books = new List<Book>
            {
                new Book { Id = "X001", Title = "Clean Code", Author = "Robert C. Martin", Year = 2008 }
            }
        };
        var service = new LibraryService(data);

        // Act
        service.RemoveBook("X001");

        // Assert
        Assert.DoesNotContain(service.ListAllBooks(), b => b.Id == "X001");
    }

    [Fact]
    public void RemoveBook_WhenBookDoesNotExist_ReturnsFailure()
    {
        // Arrange
        var service = new LibraryService();

        // Act
        var result = service.RemoveBook("DOES_NOT_EXIST");

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public void RemoveBook_WhenBookIsBorrowed_ReturnsFailure()
    {
        // Arrange
        var data = new LibraryData
        {
            Books = new List<Book>
            {
                new Book { Id = "X001", Title = "Clean Code", Author = "Robert C. Martin", Year = 2008, IsBorrowed = true }
            },
            Members = new List<Member>
            {
                new Member { Id = "M001", Name = "Alice Johnson" }
            },
            Loans = new List<Loan>
            {
                new Loan { BookId = "X001", MemberId = "M001", LoanDate = new DateTime(2026, 1, 1) }
            }
        };
        var service = new LibraryService(data);

        // Act
        var result = service.RemoveBook("X001");

        // Assert
        Assert.False(result.Success);
    }

    // =========================================================================
    // FindBookById
    // =========================================================================

    [Fact]
    public void FindBookById_WhenBookExists_ReturnsBook()
    {
        // Arrange
        var data = new LibraryData
        {
            Books = new List<Book>
            {
                new Book { Id = "X001", Title = "Clean Code", Author = "Robert C. Martin", Year = 2008 }
            }
        };
        var service = new LibraryService(data);

        // Act
        var book = service.FindBookById("X001");

        // Assert
        Assert.NotNull(book);
        Assert.Equal("X001", book.Id);
    }

    [Fact]
    public void FindBookById_WhenIdDoesNotExist_ReturnsNull()
    {
        // Arrange
        var service = new LibraryService();

        // Act
        var book = service.FindBookById("DOES_NOT_EXIST");

        // Assert
        Assert.Null(book);
    }

    [Fact]
    public void FindBookById_WhenMultipleBooksExist_ReturnsCorrectBook()
    {
        // Arrange
        var data = new LibraryData
        {
            Books = new List<Book>
            {
                new Book { Id = "X001", Title = "Book Alpha", Author = "Author A", Year = 2000 },
                new Book { Id = "X002", Title = "Book Beta",  Author = "Author B", Year = 2001 },
                new Book { Id = "X003", Title = "Book Gamma", Author = "Author C", Year = 2002 }
            }
        };
        var service = new LibraryService(data);

        // Act
        var book = service.FindBookById("X002");

        // Assert
        Assert.NotNull(book);
        Assert.Equal("Book Beta", book.Title);
    }

    // =========================================================================
    // ListAllBooks
    // =========================================================================

    [Fact]
    public void ListAllBooks_WhenNoBooks_ReturnsEmptyList()
    {
        // Arrange
        var service = new LibraryService();

        // Act
        var books = service.ListAllBooks();

        // Assert
        Assert.Empty(books);
    }

    [Fact]
    public void ListAllBooks_WhenBooksExist_ReturnsSortedByTitleAscending()
    {
        // Arrange
        var data = new LibraryData
        {
            Books = new List<Book>
            {
                new Book { Id = "X003", Title = "Zebra Stories",  Author = "Author C", Year = 2000 },
                new Book { Id = "X001", Title = "Apple Orchards", Author = "Author A", Year = 2001 },
                new Book { Id = "X002", Title = "Mango Dreams",   Author = "Author B", Year = 2002 }
            }
        };
        var service = new LibraryService(data);

        // Act
        var books = service.ListAllBooks();

        // Assert
        Assert.Equal("Apple Orchards", books[0].Title);
        Assert.Equal("Mango Dreams",   books[1].Title);
        Assert.Equal("Zebra Stories",  books[2].Title);
    }

    [Fact]
    public void ListAllBooks_AfterAddingBook_IncludesNewBook()
    {
        // Arrange
        var service = new LibraryService();

        // Act
        service.AddBook(new Book { Id = "X001", Title = "New Book", Author = "Author", Year = 2024 });

        // Assert
        Assert.Single(service.ListAllBooks());
        Assert.Equal("X001", service.ListAllBooks()[0].Id);
    }

    // =========================================================================
    // SearchBooksByTitle
    // =========================================================================

    [Fact]
    public void SearchBooksByTitle_WhenTermMatchesExactTitle_ReturnsBook()
    {
        // Arrange
        var data = new LibraryData
        {
            Books = new List<Book>
            {
                new Book { Id = "X001", Title = "Clean Code", Author = "Robert C. Martin", Year = 2008 }
            }
        };
        var service = new LibraryService(data);

        // Act
        var results = service.SearchBooksByTitle("Clean Code");

        // Assert
        Assert.Single(results);
        Assert.Equal("X001", results[0].Id);
    }

    [Fact]
    public void SearchBooksByTitle_WhenTermIsPartialMatch_ReturnsMatchingBooks()
    {
        // Arrange
        var data = new LibraryData
        {
            Books = new List<Book>
            {
                new Book { Id = "X001", Title = "Clean Code",       Author = "Robert C. Martin", Year = 2008 },
                new Book { Id = "X002", Title = "The Clean Coder",  Author = "Robert C. Martin", Year = 2011 },
                new Book { Id = "X003", Title = "Refactoring",      Author = "Martin Fowler",    Year = 1999 }
            }
        };
        var service = new LibraryService(data);

        // Act
        var results = service.SearchBooksByTitle("clean");

        // Assert
        Assert.Equal(2, results.Count);
    }

    [Theory]
    [InlineData("clean code")]
    [InlineData("CLEAN CODE")]
    [InlineData("Clean Code")]
    [InlineData("cLeAn cOdE")]
    public void SearchBooksByTitle_WhenSearchTermHasDifferentCase_StillReturnsBook(string searchTerm)
    {
        // Arrange
        var data = new LibraryData
        {
            Books = new List<Book>
            {
                new Book { Id = "X001", Title = "Clean Code", Author = "Robert C. Martin", Year = 2008 }
            }
        };
        var service = new LibraryService(data);

        // Act
        var results = service.SearchBooksByTitle(searchTerm);

        // Assert
        Assert.Single(results);
    }

    [Fact]
    public void SearchBooksByTitle_WhenTermDoesNotMatch_ReturnsEmptyList()
    {
        // Arrange
        var data = new LibraryData
        {
            Books = new List<Book>
            {
                new Book { Id = "X001", Title = "Clean Code", Author = "Robert C. Martin", Year = 2008 }
            }
        };
        var service = new LibraryService(data);

        // Act
        var results = service.SearchBooksByTitle("xyzzy_no_match");

        // Assert
        Assert.Empty(results);
    }

    [Fact]
    public void SearchBooksByTitle_WhenResultsExist_ReturnsSortedByTitleAscending()
    {
        // Arrange
        var data = new LibraryData
        {
            Books = new List<Book>
            {
                new Book { Id = "X003", Title = "The Guide to C#",    Author = "Author C", Year = 2020 },
                new Book { Id = "X001", Title = "Advanced C# Guide",  Author = "Author A", Year = 2021 },
                new Book { Id = "X002", Title = "Beginning C# Guide", Author = "Author B", Year = 2022 }
            }
        };
        var service = new LibraryService(data);

        // Act
        var results = service.SearchBooksByTitle("guide");

        // Assert
        Assert.Equal("Advanced C# Guide",  results[0].Title);
        Assert.Equal("Beginning C# Guide", results[1].Title);
        Assert.Equal("The Guide to C#",    results[2].Title);
    }

    // =========================================================================
    // RegisterMember
    // =========================================================================

    [Fact]
    public void RegisterMember_WhenMemberIsNew_ReturnsSuccess()
    {
        // Arrange
        var service = new LibraryService();

        // Act
        var result = service.RegisterMember(new Member { Id = "M001", Name = "Alice Johnson" });

        // Assert
        Assert.True(result.Success);
    }

    [Fact]
    public void RegisterMember_WhenDuplicateId_ReturnsFailure()
    {
        // Arrange
        var data = new LibraryData
        {
            Members = new List<Member>
            {
                new Member { Id = "M001", Name = "Alice Johnson" }
            }
        };
        var service = new LibraryService(data);

        // Act
        var result = service.RegisterMember(new Member { Id = "M001", Name = "Different Name" });

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public void RegisterMember_WhenDuplicateId_DoesNotCreateExtraMember()
    {
        // Arrange
        var data = new LibraryData
        {
            Members = new List<Member>
            {
                new Member { Id = "M001", Name = "Alice Johnson" }
            }
        };
        var service = new LibraryService(data);

        // Act
        service.RegisterMember(new Member { Id = "M001", Name = "Different Name" });

        // Assert — original member name is unchanged
        Assert.Equal("Alice Johnson", service.FindMemberById("M001")?.Name);
    }

    // =========================================================================
    // FindMemberById
    // =========================================================================

    [Fact]
    public void FindMemberById_WhenMemberExists_ReturnsMember()
    {
        // Arrange
        var data = new LibraryData
        {
            Members = new List<Member>
            {
                new Member { Id = "M001", Name = "Alice Johnson" }
            }
        };
        var service = new LibraryService(data);

        // Act
        var member = service.FindMemberById("M001");

        // Assert
        Assert.NotNull(member);
        Assert.Equal("Alice Johnson", member.Name);
    }

    [Fact]
    public void FindMemberById_WhenMemberDoesNotExist_ReturnsNull()
    {
        // Arrange
        var service = new LibraryService();

        // Act
        var member = service.FindMemberById("DOES_NOT_EXIST");

        // Assert
        Assert.Null(member);
    }

    // =========================================================================
    // BorrowBook
    // =========================================================================

    [Fact]
    public void BorrowBook_WhenBookExistsAndAvailable_ReturnsSuccess()
    {
        // Arrange
        var data = new LibraryData
        {
            Books   = new List<Book>   { new Book   { Id = "X001", Title = "Clean Code", Author = "Robert C. Martin", Year = 2008 } },
            Members = new List<Member> { new Member { Id = "M001", Name = "Alice Johnson" } }
        };
        var service = new LibraryService(data);

        // Act
        var result = service.BorrowBook("X001", "M001");

        // Assert
        Assert.True(result.Success);
    }

    [Fact]
    public void BorrowBook_WhenBookExistsAndAvailable_BookIsMarkedBorrowed()
    {
        // Arrange
        var data = new LibraryData
        {
            Books   = new List<Book>   { new Book   { Id = "X001", Title = "Clean Code", Author = "Robert C. Martin", Year = 2008 } },
            Members = new List<Member> { new Member { Id = "M001", Name = "Alice Johnson" } }
        };
        var service = new LibraryService(data);

        // Act
        service.BorrowBook("X001", "M001");

        // Assert
        Assert.True(service.FindBookById("X001")!.IsBorrowed);
    }

    [Fact]
    public void BorrowBook_WhenBookIsAlreadyBorrowed_ReturnsFailure()
    {
        // Arrange — book pre-loaded in a borrowed state
        var data = new LibraryData
        {
            Books   = new List<Book>   { new Book   { Id = "X001", Title = "Clean Code", Author = "Robert C. Martin", Year = 2008, IsBorrowed = true } },
            Members = new List<Member> { new Member { Id = "M002", Name = "Bob Smith" } },
            Loans   = new List<Loan>   { new Loan   { BookId = "X001", MemberId = "M001", LoanDate = new DateTime(2026, 1, 1) } }
        };
        var service = new LibraryService(data);

        // Act
        var result = service.BorrowBook("X001", "M002");

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public void BorrowBook_WhenBookDoesNotExist_ReturnsFailure()
    {
        // Arrange
        var data = new LibraryData
        {
            Members = new List<Member> { new Member { Id = "M001", Name = "Alice Johnson" } }
        };
        var service = new LibraryService(data);

        // Act
        var result = service.BorrowBook("DOES_NOT_EXIST", "M001");

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public void BorrowBook_WhenMemberDoesNotExist_ReturnsFailure()
    {
        // Arrange
        var data = new LibraryData
        {
            Books = new List<Book> { new Book { Id = "X001", Title = "Clean Code", Author = "Robert C. Martin", Year = 2008 } }
        };
        var service = new LibraryService(data);

        // Act
        var result = service.BorrowBook("X001", "DOES_NOT_EXIST");

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public void BorrowBook_WhenSuccessful_BookAppearsInBorrowedList()
    {
        // Arrange
        var data = new LibraryData
        {
            Books   = new List<Book>   { new Book   { Id = "X001", Title = "Clean Code", Author = "Robert C. Martin", Year = 2008 } },
            Members = new List<Member> { new Member { Id = "M001", Name = "Alice Johnson" } }
        };
        var service = new LibraryService(data);

        // Act
        service.BorrowBook("X001", "M001");

        // Assert
        Assert.Contains(service.ListBorrowedBooks(), b => b.Id == "X001");
    }

    // =========================================================================
    // ReturnBook
    // =========================================================================

    [Fact]
    public void ReturnBook_WhenBookIsBorrowed_ReturnsSuccess()
    {
        // Arrange — book pre-loaded in a borrowed state with a matching active loan
        var data = new LibraryData
        {
            Books   = new List<Book>   { new Book   { Id = "X001", Title = "Clean Code", Author = "Robert C. Martin", Year = 2008, IsBorrowed = true } },
            Members = new List<Member> { new Member { Id = "M001", Name = "Alice Johnson" } },
            Loans   = new List<Loan>   { new Loan   { BookId = "X001", MemberId = "M001", LoanDate = new DateTime(2026, 1, 1) } }
        };
        var service = new LibraryService(data);

        // Act
        var result = service.ReturnBook("X001");

        // Assert
        Assert.True(result.Success);
    }

    [Fact]
    public void ReturnBook_WhenBookIsReturned_BookIsMarkedNotBorrowed()
    {
        // Arrange
        var data = new LibraryData
        {
            Books   = new List<Book>   { new Book   { Id = "X001", Title = "Clean Code", Author = "Robert C. Martin", Year = 2008, IsBorrowed = true } },
            Members = new List<Member> { new Member { Id = "M001", Name = "Alice Johnson" } },
            Loans   = new List<Loan>   { new Loan   { BookId = "X001", MemberId = "M001", LoanDate = new DateTime(2026, 1, 1) } }
        };
        var service = new LibraryService(data);

        // Act
        service.ReturnBook("X001");

        // Assert
        Assert.False(service.FindBookById("X001")!.IsBorrowed);
    }

    [Fact]
    public void ReturnBook_WhenBookIsNotBorrowed_ReturnsFailure()
    {
        // Arrange
        var data = new LibraryData
        {
            Books = new List<Book> { new Book { Id = "X001", Title = "Clean Code", Author = "Robert C. Martin", Year = 2008 } }
        };
        var service = new LibraryService(data);

        // Act
        var result = service.ReturnBook("X001");

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public void ReturnBook_WhenBookDoesNotExist_ReturnsFailure()
    {
        // Arrange
        var service = new LibraryService();

        // Act
        var result = service.ReturnBook("DOES_NOT_EXIST");

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public void ReturnBook_WhenSuccessful_BookNoLongerInBorrowedList()
    {
        // Arrange
        var data = new LibraryData
        {
            Books   = new List<Book>   { new Book   { Id = "X001", Title = "Clean Code", Author = "Robert C. Martin", Year = 2008, IsBorrowed = true } },
            Members = new List<Member> { new Member { Id = "M001", Name = "Alice Johnson" } },
            Loans   = new List<Loan>   { new Loan   { BookId = "X001", MemberId = "M001", LoanDate = new DateTime(2026, 1, 1) } }
        };
        var service = new LibraryService(data);

        // Act
        service.ReturnBook("X001");

        // Assert
        Assert.DoesNotContain(service.ListBorrowedBooks(), b => b.Id == "X001");
    }

    // =========================================================================
    // GetLoanHistory
    // =========================================================================

    [Fact]
    public void GetLoanHistory_WhenNoBooksHaveBeenReturned_ReturnsEmptyList()
    {
        // Arrange
        var service = new LibraryService(new LibraryData());

        // Act
        var history = service.GetLoanHistory();

        // Assert
        Assert.Empty(history);
    }

    [Fact]
    public void GetLoanHistory_AfterReturningBook_ContainsOneEntry()
    {
        // Arrange
        var data = new LibraryData
        {
            Books   = new List<Book>   { new Book   { Id = "X001", Title = "Clean Code", Author = "Robert C. Martin", Year = 2008, IsBorrowed = true } },
            Members = new List<Member> { new Member { Id = "M001", Name = "Alice Johnson" } },
            Loans   = new List<Loan>   { new Loan   { BookId = "X001", MemberId = "M001", LoanDate = new DateTime(2026, 1, 1) } }
        };
        var service = new LibraryService(data);

        // Act
        service.ReturnBook("X001");

        // Assert
        Assert.Single(service.GetLoanHistory());
    }

    [Fact]
    public void GetLoanHistory_AfterReturningBook_EntryHasCorrectBookId()
    {
        // Arrange
        var data = new LibraryData
        {
            Books   = new List<Book>   { new Book   { Id = "X001", Title = "Clean Code", Author = "Robert C. Martin", Year = 2008, IsBorrowed = true } },
            Members = new List<Member> { new Member { Id = "M001", Name = "Alice Johnson" } },
            Loans   = new List<Loan>   { new Loan   { BookId = "X001", MemberId = "M001", LoanDate = new DateTime(2026, 1, 1) } }
        };
        var service = new LibraryService(data);

        // Act
        service.ReturnBook("X001");

        // Assert
        Assert.Equal("X001", service.GetLoanHistory()[0].BookId);
    }

    [Fact]
    public void GetLoanHistory_AfterReturningBook_EntryHasCorrectMemberId()
    {
        // Arrange
        var data = new LibraryData
        {
            Books   = new List<Book>   { new Book   { Id = "X001", Title = "Clean Code", Author = "Robert C. Martin", Year = 2008, IsBorrowed = true } },
            Members = new List<Member> { new Member { Id = "M001", Name = "Alice Johnson" } },
            Loans   = new List<Loan>   { new Loan   { BookId = "X001", MemberId = "M001", LoanDate = new DateTime(2026, 1, 1) } }
        };
        var service = new LibraryService(data);

        // Act
        service.ReturnBook("X001");

        // Assert
        Assert.Equal("M001", service.GetLoanHistory()[0].MemberId);
    }

    [Fact]
    public void GetLoanHistory_AfterReturningMultipleBooks_ContainsAllEntries()
    {
        // Arrange
        var data = new LibraryData
        {
            Books = new List<Book>
            {
                new Book { Id = "X001", Title = "Clean Code",          Author = "Robert C. Martin", Year = 2008, IsBorrowed = true },
                new Book { Id = "X002", Title = "The Pragmatic Programmer", Author = "David Thomas", Year = 1999, IsBorrowed = true },
                new Book { Id = "X003", Title = "Refactoring",         Author = "Martin Fowler",    Year = 1999, IsBorrowed = true }
            },
            Members = new List<Member> { new Member { Id = "M001", Name = "Alice Johnson" } },
            Loans = new List<Loan>
            {
                new Loan { BookId = "X001", MemberId = "M001", LoanDate = new DateTime(2026, 1, 1) },
                new Loan { BookId = "X002", MemberId = "M001", LoanDate = new DateTime(2026, 1, 2) },
                new Loan { BookId = "X003", MemberId = "M001", LoanDate = new DateTime(2026, 1, 3) }
            }
        };
        var service = new LibraryService(data);

        // Act
        service.ReturnBook("X001");
        service.ReturnBook("X002");
        service.ReturnBook("X003");

        // Assert
        Assert.Equal(3, service.GetLoanHistory().Count);
    }

    [Fact]
    public void GetLoanHistory_WhenHistoryExceedsCapacity_OldestEntryIsEvicted()
    {
        // Arrange — borrow and return 51 books; the history cap is 50
        const int overCapacity = 51;
        var books   = new List<Book>();
        var loans   = new List<Loan>();
        var members = new List<Member> { new Member { Id = "M001", Name = "Alice Johnson" } };

        for (int i = 1; i <= overCapacity; i++)
        {
            var id = $"B{i:D3}";
            books.Add(new Book { Id = id, Title = $"Book {i}", Author = "Author", Year = 2000, IsBorrowed = true });
            loans.Add(new Loan { BookId = id, MemberId = "M001", LoanDate = new DateTime(2026, 1, 1).AddDays(i - 1) });
        }

        var service = new LibraryService(new LibraryData { Books = books, Members = members, Loans = loans });

        // Act
        for (int i = 1; i <= overCapacity; i++)
        {
            service.ReturnBook($"B{i:D3}");
        }

        var history = service.GetLoanHistory();

        // Assert — oldest entry (B001) has been evicted; only 50 entries remain
        Assert.Equal(50, history.Count);
        Assert.DoesNotContain(history, l => l.BookId == "B001");
        Assert.Contains(history, l => l.BookId == "B002");
    }

    // =========================================================================
    // ListBorrowedBooks
    // =========================================================================

    [Fact]
    public void ListBorrowedBooks_WhenNoBooksAreBorrowed_ReturnsEmptyList()
    {
        // Arrange
        var data = new LibraryData
        {
            Books = new List<Book> { new Book { Id = "X001", Title = "Clean Code", Author = "Robert C. Martin", Year = 2008 } }
        };
        var service = new LibraryService(data);

        // Act
        var borrowed = service.ListBorrowedBooks();

        // Assert
        Assert.Empty(borrowed);
    }

    [Fact]
    public void ListBorrowedBooks_WhenSomeBooksAreBorrowed_ReturnsOnlyBorrowedBooks()
    {
        // Arrange
        var data = new LibraryData
        {
            Books = new List<Book>
            {
                new Book { Id = "X001", Title = "Clean Code",      Author = "Robert C. Martin", Year = 2008, IsBorrowed = true },
                new Book { Id = "X002", Title = "The Pragmatic Programmer", Author = "David Thomas", Year = 1999 }
            },
            Members = new List<Member> { new Member { Id = "M001", Name = "Alice Johnson" } },
            Loans   = new List<Loan>   { new Loan { BookId = "X001", MemberId = "M001", LoanDate = new DateTime(2026, 1, 1) } }
        };
        var service = new LibraryService(data);

        // Act
        var borrowed = service.ListBorrowedBooks();

        // Assert
        Assert.Single(borrowed);
        Assert.Equal("X001", borrowed[0].Id);
    }

    [Fact]
    public void ListBorrowedBooks_AfterReturningBook_DoesNotContainReturnedBook()
    {
        // Arrange
        var data = new LibraryData
        {
            Books   = new List<Book>   { new Book   { Id = "X001", Title = "Clean Code", Author = "Robert C. Martin", Year = 2008, IsBorrowed = true } },
            Members = new List<Member> { new Member { Id = "M001", Name = "Alice Johnson" } },
            Loans   = new List<Loan>   { new Loan   { BookId = "X001", MemberId = "M001", LoanDate = new DateTime(2026, 1, 1) } }
        };
        var service = new LibraryService(data);

        // Act
        service.ReturnBook("X001");

        // Assert
        Assert.Empty(service.ListBorrowedBooks());
    }

}

