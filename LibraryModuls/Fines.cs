

namespace LibraryModols
{
    public class Fines
    {
        public int FineID { get; set; }
        public int BorrowID { get; set; }
        public decimal Amount { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsSettled { get; set; }


    }
}
