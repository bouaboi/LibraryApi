using LibraryDataAccessLayer;
using LibraryDTOs;

namespace LibraryBusinessLayer
{
    public class BookService
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        // Dynamically packages the current property values into a Data Transfer Object (DTO)
        // This is useful for passing data down to the Data Access Layer
        public BookDTO BDTO
        {
            get
            {
                return (new BookDTO(this.BookID, this.Title, this.ISBN, this.Genre, this.Author, this.TotalCopies, this.AvailableCopies));
            }
        }

        public int BookID { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }

        // Constructor initializes the service object using data mapped from a DTO
        public BookService(BookDTO BDTO, enMode cMode = enMode.AddNew)
        {
            this.BookID = BDTO.BookID;
            this.Title = BDTO.Title;
            this.ISBN = BDTO.ISBN;
            this.Genre = BDTO.Genre;
            this.Author = BDTO.Author;
            this.TotalCopies = BDTO.TotalCopies;
            this.AvailableCopies = BDTO.AvailableCopies;

            Mode = cMode;
        }

        private bool _AddNewBook()
        {
            this.BookID = BookData.AddNewBook(BDTO);

            // If the returned ID is not -1, the database insertion was successful
            return (this.BookID != -1);
        }

        private bool _UpdateBook()
        {
            return BookData.UpdateBook(BDTO);
        }

        public bool Save()
        {
            // Routes the save action (Insert vs Update) based on the object's current state
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewBook())
                    {
                        // Once added successfully, switch mode to Update so future saves don't create duplicates
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:

                    return _UpdateBook();
            }

            return false;
        }

        public static BookService Find(int bookId)
        {
            // Fetch the raw book data from the Data Access Layer
            BookDTO BDTO = BookData.GetBookById(bookId);

            // If a record is found, return a new BookService instance populated with the data and set to Update mode
            if (BDTO != null)
            {
                return new BookService(BDTO, enMode.Update);
            }
            else
            {
                // Return null if the book doesn't exist in the database
                return null;
            }

        }
    
        public static List<BookDTO> GetAllBooks()
        {
            return BookData.GetAllBooks();
        }

        public static bool DeleteBook(int bookId)
        {
            return BookData.DeleteBook(bookId);
        }

        public static List<BookDTO> SearchBook(string query)
        {
            return BookData.SearchBook(query);
        }
    }
}
