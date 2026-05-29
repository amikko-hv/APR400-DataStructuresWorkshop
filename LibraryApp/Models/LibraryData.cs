namespace LibraryApp.Models;

public class LibraryData
{
    public List<Book> Books { get; set; } = new();
    public List<Member> Members { get; set; } = new();
    public List<Loan> Loans { get; set; } = new();
}

