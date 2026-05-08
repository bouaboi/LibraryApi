namespace LibraryDTOs
{
    public class FineDto
    {
        public FineDto(int fineId, int borrowId, string memberName, string bookTitle,
            decimal amount, decimal paidAmount, DateTime createdDate, bool isSettled)
        {
            this.FineID = fineId;
            this.BorrowID = borrowId;
            this.MemberName = memberName;
            this.BookTitle = bookTitle;
            this.Amount = amount;
            this.PaidAmount = paidAmount;
            this.CreatedDate = createdDate;
            this.IsSettled = isSettled;
        }
        public int FineID { get; set; }
        public int BorrowID { get; set; }
        public string MemberName { get; set; }
        public string BookTitle { get; set; }
        public decimal Amount { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsSettled { get; set; }
    }

    public class SettleFineDto
    {
        public decimal PaidAmount { get; set; }
    }
}