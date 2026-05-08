

namespace LibraryDTOs
{
    public class BookDTO
    {
        public BookDTO(int bookId, string title, string isbn, string genre,
            string author, int totalcopies, int availablecopies)
        {
            this.BookID = bookId;
            this.Title = title;
            this.ISBN = isbn;
            this.Genre = genre;
            this.Author = author;
            this.TotalCopies = totalcopies;
            this.AvailableCopies = availablecopies;
        }

        public int BookID { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
    }

    public class CreateBookDTO
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public int TotalCopies { get; set; }
    }

    public class UpdateBookDTO
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public int TotalCopies { get; set; }
    }


}
