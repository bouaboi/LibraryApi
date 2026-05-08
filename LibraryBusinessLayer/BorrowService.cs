using LibraryDataAccessLayer;
using LibraryDTOs;


namespace LibraryBusinessLayer
{
    public class BorrowService
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public BorrowDTO BODTO
        {
            get
            {
                return new BorrowDTO(this.BorrowID, this.MemberID, this.MemberName, this.BookID, this.BookTitle, this.BorrowDate,
                    this.DueDate, this.ReturnDate);
            }
        }

        public int BorrowID { get; set; }
        public int MemberID { get; set; }
        public string MemberName { get; set; }
        public int BookID { get; set; }
        public string BookTitle { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public BorrowService(BorrowDTO BODTO, enMode cMode = enMode.AddNew)
        {
            this.BorrowID = BODTO.BorrowID;
            this.MemberID = BODTO.MemberID;
            this.MemberName = BODTO.MemberName;
            this.BookID = BODTO.BookID;
            this.BookTitle = BODTO.BookTitle;
            this.BorrowDate = BODTO.BorrowDate;
            this.DueDate = BODTO.DueDate;
            this.ReturnDate = BODTO.ReturnDate;

            Mode = cMode;
        }

        public static BorrowService Find(int borrowId)
        {
            BorrowDTO BODTO = BorrowData.GetBorrowById(borrowId);


            if (BODTO != null)
            {
                return new BorrowService(BODTO, enMode.Update);
            }
            else
            {
                return null;
            }

        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewBorrow())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
            }
            return false;
        }

        private bool _AddNewBorrow()
        {
            this.BorrowID = BorrowData.AddNewBorrow(BODTO);

            return (this.BorrowID != -1); // If BorrowID is -1, -2, or -3 the SP rejected the borrow

        }

        public static List<BorrowDTO> GetAllBorrows()
        {
            return BorrowData.GetAllBorrows();
        }


        public static List<BorrowDTO> GetBorrowByMemberId(int borrowMemberId)
        {
            return BorrowData.GetBorrowByMemberId(borrowMemberId);
        }

        public static List<BorrowDTO> GetActiveBorrows()
        {
            return BorrowData.GetActiveBorrows();
        }

        public static bool ReturnBook(int borrowId)
        {
            return BorrowData.ReturnBook(borrowId);
        }
    }
}
