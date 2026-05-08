

namespace LibraryModols
{
    public class Borrow
    { 

       public int BorrowID { get; set; }
       public int MemberID { get; set; }
       public int BookID { get; set; }
       public DateTime BorrowDate { get; set; }
       public DateTime DueDate { get; set; }
       public DateTime ReturnDate { get; set; }
    }
}
