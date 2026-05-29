namespace LibraryApp.Models;

public class Loan
{
    public string BookId { get; set; } = string.Empty;
    public string MemberId { get; set; } = string.Empty;
    public DateTime LoanDate { get; set; }
}

