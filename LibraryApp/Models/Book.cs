namespace LibraryApp.Models;

public class Book
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int Year { get; set; }
    public bool IsBorrowed { get; set; }
}

