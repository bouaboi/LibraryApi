namespace LibraryDTOs
{
    public class SettingDto
    {
        public SettingDto(int settingId, int maxBorrowLimit, int borrowDurationDays, decimal dailyFineRate)
        {
            this.SettingID = settingId;
            this.MaxBorrowLimit = maxBorrowLimit;
            this.BorrowDurationDays = borrowDurationDays;
            this.DailyFineRate = dailyFineRate;
        }
        public int SettingID { get; set; }
        public int MaxBorrowLimit { get; set; }
        public int BorrowDurationDays { get; set; }
        public decimal DailyFineRate { get; set; }
    }

    public class UpdateSettingDto
    {
        public int MaxBorrowLimit { get; set; }
        public int BorrowDurationDays { get; set; }
        public decimal DailyFineRate { get; set; }
    }
}