using System.Collections;
using LibraryApp.Models;

namespace LibraryApp;

/// <summary>
/// Core application logic for the Library Inventory and Lending system.
/// </summary>
public class LibraryService
{
    private readonly List<Book> _books = new();
    private readonly ArrayList _loans = new();
    private readonly List<Member> _members = new();
    private readonly List<Loan> _loanHistory = new();
    private const int MaxLoanHistorySize = 50;
    
    /// <summary>
    /// Initialises the service with optional library data.
    /// </summary>
    public LibraryService(LibraryData? data = null)
    {
        if (data == null) 
        {
            return;
        }
        
        _books.AddRange(data.Books);
        _members.AddRange(data.Members);

        foreach (var loan in data.Loans)
        {
            _loans.Add(loan);
        }
    }

    // -------------------------------------------------------------------------
    // Book operations
    // -------------------------------------------------------------------------

    /// <summary>
    /// Adds a new book to the library.
    /// Returns failure if a book with the same ID already exists.
    /// </summary>
    public ServiceResult AddBook(Book book)
    {
        foreach (Book b in _books)
        {
            if (b.Id == book.Id)
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = $"A book with ID '{book.Id}' already exists."
                };
            }
        }

        _books.Add(book);
        _books.Sort((a, b) => string.Compare(a.Id, b.Id, StringComparison.Ordinal));

        return new ServiceResult { Success = true, Message = "Book added successfully." };
    }

    /// <summary>
    /// Removes a book by ID.
    /// Returns failure if the book does not exist or is currently borrowed.
    /// </summary>
    public ServiceResult RemoveBook(string id)
    {
        Book? found = null;
        foreach (Book b in _books)
        {
            if (b.Id == id)
            {
                found = b;
                break;
            }
        }

        if (found == null)
        {
            return new ServiceResult { Success = false, Message = $"Book with ID '{id}' not found." };
        }

        if (found.IsBorrowed)
        {
            return new ServiceResult { Success = false, Message = $"Cannot remove '{found.Title}' because it is currently borrowed." };
        }

        _books.Remove(found);
        return new ServiceResult { Success = true, Message = $"Book '{found.Title}' removed successfully." };
    }

    /// <summary>
    /// Finds a book by its exact ID.
    /// Returns <c>null</c> when no book with that ID exists.
    /// </summary>
    public Book? FindBookById(string id)
    {
        foreach (Book b in _books)
        {
            if (b.Id == id)
            {
                return b;
            }
        }

        return null;
    }

    /// <summary>
    /// Returns all books sorted by title (ascending, case-insensitive).
    /// </summary>
    public List<Book> ListAllBooks()
    {
        var result = new List<Book>(_books);
        result.Sort((a, b) => string.Compare(a.Title, b.Title, StringComparison.OrdinalIgnoreCase));
        return result;
    }

    /// <summary>
    /// Returns all books whose title contains <paramref name="substring"/>
    /// (case-insensitive), sorted by title ascending.
    /// </summary>
    public List<Book> SearchBooksByTitle(string substring)
    {
        var result = new List<Book>();
        var allBooks = _books.ToList();

        foreach (Book b in allBooks)
        {
            if (b.Title.Contains(substring, StringComparison.OrdinalIgnoreCase))
            {
                result.Add(b);
            }
        }

        result.Sort((a, b) => string.Compare(a.Title, b.Title, StringComparison.OrdinalIgnoreCase));
        return result;
    }

    // -------------------------------------------------------------------------
    // Member operations
    // -------------------------------------------------------------------------

    /// <summary>
    /// Registers a new library member.
    /// Returns failure if a member with the same ID already exists.
    /// </summary>
    public ServiceResult RegisterMember(Member member)
    {
        foreach (Member m in _members)
        {
            if (m.Id == member.Id)
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = $"A member with ID '{member.Id}' already exists."
                };
            }
        }

        _members.Add(member);
        return new ServiceResult { Success = true, Message = "Member registered successfully." };
    }

    /// <summary>
    /// Finds a member by their exact ID.
    /// Returns <c>null</c> when no member with that ID exists.
    /// </summary>
    public Member? FindMemberById(string id)
    {
        foreach (Member m in _members)
        {
            if (m.Id == id)
            {
                return m;
            }
        }

        return null;
    }

    // -------------------------------------------------------------------------
    // Lending operations
    // -------------------------------------------------------------------------

    /// <summary>
    /// Records a loan of <paramref name="bookId"/> to <paramref name="memberId"/>.
    /// Returns failure if the book or member does not exist, or if the book is
    /// already borrowed.
    /// </summary>
    public ServiceResult BorrowBook(string bookId, string memberId)
    {
        Book? book = null;
        foreach (Book b in _books)
        {
            if (b.Id == bookId)
            {
                book = b;
                break;
            }
        }

        if (book == null)
        {
            return new ServiceResult { Success = false, Message = $"Book with ID '{bookId}' not found." };
        }

        if (book.IsBorrowed)
        {
            return new ServiceResult { Success = false, Message = $"'{book.Title}' is already borrowed." };
        }

        Member? member = null;
        foreach (Member m in _members)
        {
            if (m.Id == memberId)
            {
                member = m;
                break;
            }
        }

        if (member == null)
        {
            return new ServiceResult { Success = false, Message = $"Member with ID '{memberId}' not found." };
        }

        book.IsBorrowed = true;

        var loan = new Loan { BookId = bookId, MemberId = memberId, LoanDate = DateTime.Now };
        _loans.Add(loan);

        return new ServiceResult { Success = true, Message = $"'{book.Title}' has been borrowed by {member.Name}." };
    }

    /// <summary>
    /// Records the return of a previously borrowed book.
    /// Returns failure if the book does not exist or is not currently borrowed.
    /// </summary>
    public ServiceResult ReturnBook(string bookId)
    {
        Book? book = null;
        foreach (Book b in _books)
        {
            if (b.Id == bookId)
            {
                book = b;
                break;
            }
        }

        if (book == null)
        {
            return new ServiceResult { Success = false, Message = $"Book with ID '{bookId}' not found." };
        }

        if (!book.IsBorrowed)
        {
            return new ServiceResult { Success = false, Message = $"'{book.Title}' is not currently borrowed." };
        }

        Loan? activeLoan = null;
        foreach (object obj in _loans)
        {
            var loan = (Loan)obj;
            if (loan.BookId == bookId)
            {
                activeLoan = loan;
                break;
            }
        }

        if (activeLoan != null)
        {
            _loans.Remove(activeLoan);

            _loanHistory.Add(activeLoan);
            while (_loanHistory.Count > MaxLoanHistorySize)
            {
                _loanHistory.RemoveAt(0);
            }
        }

        book.IsBorrowed = false;
        return new ServiceResult { Success = true, Message = $"'{book.Title}' has been returned successfully." };
    }

    /// <summary>Returns all books that are currently borrowed.</summary>
    public List<Book> ListBorrowedBooks()
    {
        var result = new List<Book>();
        foreach (Book b in _books)
        {
            if (b.IsBorrowed)
            {
                result.Add(b);
            }
        }
        return result;
    }

    /// <summary>
    /// Returns a copy of the completed-loan history, ordered oldest to newest.
    /// The history is capped at 50 entries; the oldest entry is dropped when the
    /// cap is exceeded.
    /// </summary>
    public List<Loan> GetLoanHistory()
    {
        return new List<Loan>(_loanHistory);
    }
}
