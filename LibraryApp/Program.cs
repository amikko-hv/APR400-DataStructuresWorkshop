using LibraryApp;
using LibraryApp.Models;

// Initialize LibraryService and seed it with mock data.
var data = SeedData.GetSeedData();
var service = new LibraryService(data);

Console.WriteLine("╔══════════════════════════════════════╗");
Console.WriteLine("║       Library Management System      ║");
Console.WriteLine("╚══════════════════════════════════════╝");

// Main application loop
while (true)
{
    PrintMenu();

    var input = Console.ReadLine()?.Trim();

    switch (input)
    {
        case "1":  AddBook();          break;
        case "2":  RemoveBook();       break;
        case "3":  ListAllBooks();     break;
        case "4":  FindBook();         break;
        case "5":  SearchBooks();      break;
        case "6":  RegisterMember();   break;
        case "7":  BorrowBook();       break;
        case "8":  ReturnBook();       break;
        case "9":  ListBorrowedBooks(); break;
        case "10": ListLoanHistory();   break;
        case "11":
            Console.WriteLine("\nGoodbye!");
            return;
        default:
            Console.WriteLine("\n  [!] Invalid option. Please enter a number between 1 and 11.");
            break;
    }
    
    if (!Continue())
    {
        Console.WriteLine("\nGoodbye!");
        return;
    }
}

// ---------------------------------------------------------------------------
// Menu
// ---------------------------------------------------------------------------

static void PrintMenu()
{
    Console.WriteLine();
    Console.WriteLine("  ── Books ─────────────────────────────");
    Console.WriteLine("   1. Add book");
    Console.WriteLine("   2. Remove book by ID");
    Console.WriteLine("   3. List all books");
    Console.WriteLine("   4. Find book by ID");
    Console.WriteLine("   5. Search books by title");
    Console.WriteLine("  ── Members ───────────────────────────");
    Console.WriteLine("   6. Register member");
    Console.WriteLine("  ── Lending ───────────────────────────");
    Console.WriteLine("   7. Borrow book");
    Console.WriteLine("   8. Return book");
    Console.WriteLine("   9. List borrowed books");
    Console.WriteLine("  10. Loan history");
    Console.WriteLine("  ──────────────────────────────────────");
    Console.WriteLine("  11. Exit");
    Console.WriteLine();
    Console.Write("  Choose an option: ");
}

// ---------------------------------------------------------------------------
// Book operations
// ---------------------------------------------------------------------------

void AddBook()
{
    Console.WriteLine("\n── Add Book ──────────────────────────────");
    var id     = Prompt("  Book ID    : ");
    var title  = Prompt("  Title      : ");
    var author = Prompt("  Author     : ");

    if (!int.TryParse(Prompt("  Year       : "), out int year))
    {
        Console.WriteLine("  [!] Year must be a number.");
        return;
    }

    var book   = new Book { Id = id, Title = title, Author = author, Year = year };
    var result = service.AddBook(book);
    PrintResult(result);
}

void RemoveBook()
{
    Console.WriteLine("\n── Remove Book ───────────────────────────");
    var id = Prompt("  Book ID: ");
    PrintResult(service.RemoveBook(id));
}

void ListAllBooks()
{
    Console.WriteLine("\n── All Books (sorted by title) ───────────");
    var books = service.ListAllBooks();
    if (books.Count == 0)
    {
        Console.WriteLine("  No books in the library.");
        return;
    }
    PrintBookTable(books);
    Console.WriteLine($"\n  Total: {books.Count} book(s).");
}

void FindBook()
{
    Console.WriteLine("\n── Find Book by ID ───────────────────────");
    var id   = Prompt("  Book ID: ");
    var book = service.FindBookById(id);
    if (book == null)
        Console.WriteLine($"  [!] No book found with ID '{id}'.");
    else
        PrintBookTable(new List<Book> { book });
}

void SearchBooks()
{
    Console.WriteLine("\n── Search Books by Title ─────────────────");
    var term  = Prompt("  Search term: ");
    var books = service.SearchBooksByTitle(term);
    if (books.Count == 0)
        Console.WriteLine("  No books matched your search.");
    else
    {
        PrintBookTable(books);
        Console.WriteLine($"\n  Found: {books.Count} book(s).");
    }
}

// ---------------------------------------------------------------------------
// Member operations
// ---------------------------------------------------------------------------

void RegisterMember()
{
    Console.WriteLine("\n── Register Member ───────────────────────");
    var id   = Prompt("  Member ID  : ");
    var name = Prompt("  Name       : ");
    PrintResult(service.RegisterMember(new Member { Id = id, Name = name }));
}

// ---------------------------------------------------------------------------
// Lending operations
// ---------------------------------------------------------------------------

void BorrowBook()
{
    Console.WriteLine("\n── Borrow Book ───────────────────────────");
    var bookId   = Prompt("  Book ID  : ");
    var memberId = Prompt("  Member ID: ");
    PrintResult(service.BorrowBook(bookId, memberId));
}

void ReturnBook()
{
    Console.WriteLine("\n── Return Book ───────────────────────────");
    var bookId = Prompt("  Book ID: ");
    PrintResult(service.ReturnBook(bookId));
}

void ListBorrowedBooks()
{
    Console.WriteLine("\n── Currently Borrowed Books ──────────────");
    var books = service.ListBorrowedBooks();
    if (books.Count == 0)
    {
        Console.WriteLine("  No books are currently borrowed.");
    }
    else
    {
        PrintBookTable(books);
        Console.WriteLine($"\n  Total borrowed: {books.Count} book(s).");
    }
}

void ListLoanHistory()
{
    Console.WriteLine("\n── Loan History ──────────────────────────");
    var history = service.GetLoanHistory();
    if (history.Count == 0)
    {
        Console.WriteLine("  No loans have been completed yet.");
        return;
    }

    Console.WriteLine($"\n  {"Book ID",-8} {"Member ID",-10} {"Loan Date"}");
    Console.WriteLine($"  {new string('-', 40)}");
    foreach (var loan in history)
    {
        Console.WriteLine($"  {loan.BookId,-8} {loan.MemberId,-10} {loan.LoanDate:yyyy-MM-dd}");
    }
    Console.WriteLine($"\n  Total: {history.Count} completed loan(s).");
}

// ---------------------------------------------------------------------------
// Helpers
// ---------------------------------------------------------------------------

static string Prompt(string label)
{
    Console.Write(label);
    return Console.ReadLine()?.Trim() ?? string.Empty;
}

static void PrintResult(ServiceResult result)
{
    var icon = result.Success ? "[✓]" : "[!]";
    Console.WriteLine($"\n  {icon} {result.Message}");
}

static void PrintBookTable(List<Book> books)
{
    Console.WriteLine($"\n  {"ID",-6} {"Borrowed",-9} {"Year",-6} {"Title",-45} {"Author"}");
    Console.WriteLine($"  {new string('-', 90)}");
    foreach (var b in books)
    {
        var borrowed = b.IsBorrowed ? "Yes" : "No";
        Console.WriteLine($"  {b.Id,-6} {borrowed,-9} {b.Year,-6} {b.Title,-45} {b.Author}");
    }
}

static bool Continue()
{
    Console.Write("\n  Continue? (y/n): ");
    var continueInput = Console.ReadLine()?.Trim().ToLowerInvariant();
    if (continueInput != "y" && continueInput != "yes")
    {
        return false;
    }
    return true;
}


