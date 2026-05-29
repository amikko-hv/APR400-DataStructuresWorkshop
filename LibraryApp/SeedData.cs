using LibraryApp.Models;

namespace LibraryApp;

/// <summary>
/// Hard-coded seed data loaded at application startup.
/// In a real application this would probably come from a database.
/// </summary>
public static class SeedData
{
    public static LibraryData GetSeedData()
    {
        var books = new List<Book>
        {
            // ── Classic Literature (B001–B025) ────────────────────────────────
            new Book { Id = "B001", Title = "1984",                                      Author = "George Orwell",           Year = 1949, IsBorrowed = true  },
            new Book { Id = "B002", Title = "To Kill a Mockingbird",                     Author = "Harper Lee",              Year = 1960, IsBorrowed = true  },
            new Book { Id = "B003", Title = "The Great Gatsby",                          Author = "F. Scott Fitzgerald",     Year = 1925 },
            new Book { Id = "B004", Title = "Pride and Prejudice",                       Author = "Jane Austen",             Year = 1813 },
            new Book { Id = "B005", Title = "Sense and Sensibility",                     Author = "Jane Austen",             Year = 1811 },
            new Book { Id = "B006", Title = "Brave New World",                           Author = "Aldous Huxley",           Year = 1932 },
            new Book { Id = "B007", Title = "Of Mice and Men",                           Author = "John Steinbeck",          Year = 1937 },
            new Book { Id = "B008", Title = "The Grapes of Wrath",                       Author = "John Steinbeck",          Year = 1939 },
            new Book { Id = "B009", Title = "Moby Dick",                                 Author = "Herman Melville",         Year = 1851 },
            new Book { Id = "B010", Title = "Jane Eyre",                                 Author = "Charlotte Bronte",        Year = 1847 },
            new Book { Id = "B011", Title = "Wuthering Heights",                         Author = "Emily Bronte",            Year = 1847 },
            new Book { Id = "B012", Title = "Great Expectations",                        Author = "Charles Dickens",         Year = 1861 },
            new Book { Id = "B013", Title = "A Tale of Two Cities",                      Author = "Charles Dickens",         Year = 1859 },
            new Book { Id = "B014", Title = "Oliver Twist",                              Author = "Charles Dickens",         Year = 1838 },
            new Book { Id = "B015", Title = "Crime and Punishment",                      Author = "Fyodor Dostoevsky",       Year = 1866 },
            new Book { Id = "B016", Title = "War and Peace",                             Author = "Leo Tolstoy",             Year = 1869 },
            new Book { Id = "B017", Title = "Anna Karenina",                             Author = "Leo Tolstoy",             Year = 1878 },
            new Book { Id = "B018", Title = "Don Quixote",                               Author = "Miguel de Cervantes",     Year = 1605 },
            new Book { Id = "B019", Title = "Les Miserables",                            Author = "Victor Hugo",             Year = 1862 },
            new Book { Id = "B020", Title = "The Hunchback of Notre-Dame",               Author = "Victor Hugo",             Year = 1831 },
            new Book { Id = "B021", Title = "Madame Bovary",                             Author = "Gustave Flaubert",        Year = 1857 },
            new Book { Id = "B022", Title = "The Count of Monte Cristo",                 Author = "Alexandre Dumas",         Year = 1844 },
            new Book { Id = "B023", Title = "The Three Musketeers",                      Author = "Alexandre Dumas",         Year = 1844 },
            new Book { Id = "B024", Title = "Rebecca",                                   Author = "Daphne du Maurier",       Year = 1938 },
            new Book { Id = "B025", Title = "The Catcher in the Rye",                    Author = "J.D. Salinger",           Year = 1951 },

            // ── Science Fiction (B026–B045) ───────────────────────────────────
            new Book { Id = "B026", Title = "Dune",                                      Author = "Frank Herbert",           Year = 1965 },
            new Book { Id = "B027", Title = "Foundation",                                Author = "Isaac Asimov",            Year = 1951 },
            new Book { Id = "B028", Title = "The Hitchhiker's Guide to the Galaxy",      Author = "Douglas Adams",           Year = 1979 },
            new Book { Id = "B029", Title = "Neuromancer",                               Author = "William Gibson",          Year = 1984 },
            new Book { Id = "B030", Title = "Ender's Game",                              Author = "Orson Scott Card",        Year = 1985 },
            new Book { Id = "B031", Title = "The Martian",                               Author = "Andy Weir",               Year = 2011 },
            new Book { Id = "B032", Title = "Fahrenheit 451",                            Author = "Ray Bradbury",            Year = 1953 },
            new Book { Id = "B033", Title = "A Wrinkle in Time",                         Author = "Madeleine L'Engle",       Year = 1962 },
            new Book { Id = "B034", Title = "The War of the Worlds",                     Author = "H.G. Wells",              Year = 1898 },
            new Book { Id = "B035", Title = "Do Androids Dream of Electric Sheep?",      Author = "Philip K. Dick",          Year = 1968 },
            new Book { Id = "B036", Title = "The Time Machine",                          Author = "H.G. Wells",              Year = 1895 },
            new Book { Id = "B037", Title = "I, Robot",                                  Author = "Isaac Asimov",            Year = 1950 },
            new Book { Id = "B038", Title = "Childhood's End",                           Author = "Arthur C. Clarke",        Year = 1953 },
            new Book { Id = "B039", Title = "2001: A Space Odyssey",                     Author = "Arthur C. Clarke",        Year = 1968 },
            new Book { Id = "B040", Title = "The Left Hand of Darkness",                 Author = "Ursula K. Le Guin",       Year = 1969 },
            new Book { Id = "B041", Title = "Ringworld",                                 Author = "Larry Niven",             Year = 1970 },
            new Book { Id = "B042", Title = "Contact",                                   Author = "Carl Sagan",              Year = 1985 },
            new Book { Id = "B043", Title = "Snow Crash",                                Author = "Neal Stephenson",         Year = 1992 },
            new Book { Id = "B044", Title = "The Stars My Destination",                  Author = "Alfred Bester",           Year = 1956 },
            new Book { Id = "B045", Title = "Hyperion",                                  Author = "Dan Simmons",             Year = 1989 },

            // ── Fantasy (B046–B065) ───────────────────────────────────────────
            new Book { Id = "B046", Title = "The Lord of the Rings",                     Author = "J.R.R. Tolkien",          Year = 1954 },
            new Book { Id = "B047", Title = "The Hobbit",                                Author = "J.R.R. Tolkien",          Year = 1937 },
            new Book { Id = "B048", Title = "Harry Potter and the Philosopher's Stone",  Author = "J.K. Rowling",            Year = 1997 },
            new Book { Id = "B049", Title = "Harry Potter and the Chamber of Secrets",   Author = "J.K. Rowling",            Year = 1998 },
            new Book { Id = "B050", Title = "A Game of Thrones",                         Author = "George R.R. Martin",      Year = 1996 },
            new Book { Id = "B051", Title = "The Name of the Wind",                      Author = "Patrick Rothfuss",        Year = 2007 },
            new Book { Id = "B052", Title = "The Way of Kings",                          Author = "Brandon Sanderson",       Year = 2010 },
            new Book { Id = "B053", Title = "American Gods",                             Author = "Neil Gaiman",             Year = 2001 },
            new Book { Id = "B054", Title = "Good Omens",                                Author = "Terry Pratchett",         Year = 1990 },
            new Book { Id = "B055", Title = "The Night Circus",                          Author = "Erin Morgenstern",        Year = 2011 },
            new Book { Id = "B056", Title = "Mistborn: The Final Empire",                Author = "Brandon Sanderson",       Year = 2006 },
            new Book { Id = "B057", Title = "The Colour of Magic",                       Author = "Terry Pratchett",         Year = 1983 },
            new Book { Id = "B058", Title = "Eragon",                                    Author = "Christopher Paolini",     Year = 2003 },
            new Book { Id = "B059", Title = "A Wizard of Earthsea",                      Author = "Ursula K. Le Guin",       Year = 1968 },
            new Book { Id = "B060", Title = "The Chronicles of Narnia",                  Author = "C.S. Lewis",              Year = 1950 },
            new Book { Id = "B061", Title = "The Golden Compass",                        Author = "Philip Pullman",          Year = 1995 },
            new Book { Id = "B062", Title = "Stardust",                                  Author = "Neil Gaiman",             Year = 1999 },
            new Book { Id = "B063", Title = "The Princess Bride",                        Author = "William Goldman",         Year = 1973 },
            new Book { Id = "B064", Title = "Elantris",                                  Author = "Brandon Sanderson",       Year = 2005 },
            new Book { Id = "B065", Title = "The Sword of Kaigen",                       Author = "M.L. Wang",               Year = 2019 },

            // ── Adventure (B066–B080) ─────────────────────────────────────────
            new Book { Id = "B066", Title = "Treasure Island",                           Author = "Robert Louis Stevenson",  Year = 1883 },
            new Book { Id = "B067", Title = "Robinson Crusoe",                           Author = "Daniel Defoe",            Year = 1719 },
            new Book { Id = "B068", Title = "Twenty Thousand Leagues Under the Sea",     Author = "Jules Verne",             Year = 1870 },
            new Book { Id = "B069", Title = "Around the World in Eighty Days",           Author = "Jules Verne",             Year = 1872 },
            new Book { Id = "B070", Title = "Journey to the Centre of the Earth",        Author = "Jules Verne",             Year = 1864 },
            new Book { Id = "B071", Title = "The Mysterious Island",                     Author = "Jules Verne",             Year = 1875 },
            new Book { Id = "B072", Title = "The Swiss Family Robinson",                 Author = "Johann David Wyss",       Year = 1812 },
            new Book { Id = "B073", Title = "Gulliver's Travels",                        Author = "Jonathan Swift",          Year = 1726 },
            new Book { Id = "B074", Title = "The Adventures of Tom Sawyer",              Author = "Mark Twain",              Year = 1876 },
            new Book { Id = "B075", Title = "The Call of the Wild",                      Author = "Jack London",             Year = 1903 },
            new Book { Id = "B076", Title = "White Fang",                                Author = "Jack London",             Year = 1906 },
            new Book { Id = "B077", Title = "The Jungle Book",                           Author = "Rudyard Kipling",         Year = 1894 },
            new Book { Id = "B078", Title = "The Man in the Iron Mask",                  Author = "Alexandre Dumas",         Year = 1850 },
            new Book { Id = "B079", Title = "King Solomon's Mines",                      Author = "H. Rider Haggard",        Year = 1885 },
            new Book { Id = "B080", Title = "The Island of Doctor Moreau",               Author = "H.G. Wells",              Year = 1896 },

            // ── Children's Books (B081–B095) ──────────────────────────────────
            new Book { Id = "B081", Title = "Charlotte's Web",                           Author = "E.B. White",              Year = 1952 },
            new Book { Id = "B082", Title = "The Giver",                                 Author = "Lois Lowry",              Year = 1993 },
            new Book { Id = "B083", Title = "The Hunger Games",                          Author = "Suzanne Collins",         Year = 2008 },
            new Book { Id = "B084", Title = "Little Women",                              Author = "Louisa May Alcott",       Year = 1868 },
            new Book { Id = "B085", Title = "Alice's Adventures in Wonderland",          Author = "Lewis Carroll",           Year = 1865 },
            new Book { Id = "B086", Title = "Through the Looking-Glass",                 Author = "Lewis Carroll",           Year = 1871 },
            new Book { Id = "B087", Title = "Peter Pan",                                 Author = "J.M. Barrie",             Year = 1911 },
            new Book { Id = "B088", Title = "The Wonderful Wizard of Oz",                Author = "L. Frank Baum",           Year = 1900 },
            new Book { Id = "B089", Title = "Anne of Green Gables",                      Author = "L.M. Montgomery",         Year = 1908 },
            new Book { Id = "B090", Title = "The Secret Garden",                         Author = "Frances Hodgson Burnett", Year = 1911 },
            new Book { Id = "B091", Title = "A Little Princess",                         Author = "Frances Hodgson Burnett", Year = 1905 },
            new Book { Id = "B092", Title = "Winnie-the-Pooh",                           Author = "A.A. Milne",              Year = 1926 },
            new Book { Id = "B093", Title = "The Wind in the Willows",                   Author = "Kenneth Grahame",         Year = 1908 },
            new Book { Id = "B094", Title = "Matilda",                                   Author = "Roald Dahl",              Year = 1988 },
            new Book { Id = "B095", Title = "Holes",                                     Author = "Louis Sachar",            Year = 1998 },

            // ── Computer Programming (B096–B110) ──────────────────────────────
            new Book { Id = "B096", Title = "Clean Code",                                Author = "Robert C. Martin",        Year = 2008 },
            new Book { Id = "B097", Title = "The Pragmatic Programmer",                  Author = "David Thomas",            Year = 1999 },
            new Book { Id = "B098", Title = "Design Patterns",                           Author = "Gang of Four",            Year = 1994 },
            new Book { Id = "B099", Title = "The Mythical Man-Month",                    Author = "Fred Brooks",             Year = 1975 },
            new Book { Id = "B100", Title = "Code Complete",                             Author = "Steve McConnell",         Year = 2004 },
            new Book { Id = "B101", Title = "Refactoring",                               Author = "Martin Fowler",           Year = 1999 },
            new Book { Id = "B102", Title = "Introduction to Algorithms",                Author = "Cormen, Leiserson, Rivest, Stein", Year = 2009 },
            new Book { Id = "B103", Title = "Structure and Interpretation of Computer Programs", Author = "Abelson and Sussman", Year = 1996 },
            new Book { Id = "B104", Title = "The Art of Computer Programming",           Author = "Donald E. Knuth",         Year = 1968 },
            new Book { Id = "B105", Title = "Programming Pearls",                        Author = "Jon Bentley",             Year = 1986 },
            new Book { Id = "B106", Title = "Working Effectively with Legacy Code",      Author = "Michael Feathers",        Year = 2004 },
            new Book { Id = "B107", Title = "Domain-Driven Design",                      Author = "Eric Evans",              Year = 2003 },
            new Book { Id = "B108", Title = "The Clean Coder",                           Author = "Robert C. Martin",        Year = 2011 },
            new Book { Id = "B109", Title = "Algorithms",                                Author = "Robert Sedgewick",        Year = 2011 },
            new Book { Id = "B110", Title = "A Philosophy of Software Design",           Author = "John Ousterhout",         Year = 2018 },
        };

        var members = new List<Member>
        {
            new Member { Id = "M001", Name = "Alice Johnson"  },
            new Member { Id = "M002", Name = "Bob Smith"      },
            new Member { Id = "M003", Name = "Carol Williams" },
            new Member { Id = "M004", Name = "David Brown"    },
            new Member { Id = "M005", Name = "Emma Davis"     },
            new Member { Id = "M006", Name = "Frank Miller"   },
            new Member { Id = "M007", Name = "Grace Wilson"   },
        };

        // B001 and B002 are already marked IsBorrowed = true above to match these loans.
        var loans = new List<Loan>
        {
            new Loan { BookId = "B001", MemberId = "M001", LoanDate = new DateTime(2026, 5,  1) },
            new Loan { BookId = "B002", MemberId = "M003", LoanDate = new DateTime(2026, 5, 10) },
        };

        return new LibraryData { Books = books, Members = members, Loans = loans };
    }
}
