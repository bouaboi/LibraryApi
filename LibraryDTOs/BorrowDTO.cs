

namespace LibraryDTOs
{
    public class BorrowDTO
    {
        public BorrowDTO(int borrowId, int memberId, string memberName, int bookId, string bookTitle,
            DateTime borrowDate, DateTime dueDate, DateTime? returnDate)
        {
            this.BorrowID = borrowId;
            this.MemberID = memberId;
            this.MemberName = memberName;
            this.BookID = bookId;
            this.BookTitle = bookTitle;
            this.BorrowDate = borrowDate;
            this.DueDate = dueDate;
            this.ReturnDate = returnDate;
        }

        public int BorrowID { get; set; }
        public int MemberID { get; set; }
        public string MemberName { get; set; }
        public int BookID { get; set; }
        public string BookTitle { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }

    public class CreateBorrowDTO
    {
        public int MemberID { get; set; }
        public int BookID { get; set; }
    }


}

