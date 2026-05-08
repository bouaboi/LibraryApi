using LibraryDataAccessLayer;
using LibraryDTOs;

public class SettingService
{
    public SettingDto SEDTO
    {
        get
        {
            return new SettingDto(this.SettingID, this.MaxBorrowLimit,
                this.BorrowDurationDays, this.DailyFineRate);
        }
    }
    public int SettingID { get; set; }
    public int MaxBorrowLimit { get; set; }
    public int BorrowDurationDays { get; set; }
    public decimal DailyFineRate { get; set; }

    public SettingService(SettingDto SEDTO)
    {
        this.SettingID = SEDTO.SettingID;
        this.MaxBorrowLimit = SEDTO.MaxBorrowLimit;
        this.BorrowDurationDays = SEDTO.BorrowDurationDays;
        this.DailyFineRate = SEDTO.DailyFineRate;
    }

    private bool _UpdateSettings()
    {
        return SettingData.UpdateSettings(SEDTO);
        
    }
    public static SettingService GetCurrentSettings()
    {
        SettingDto SDTO = SettingData.GetSettings();
        if (SDTO != null)
            return new SettingService(SDTO);
        else
            return null;
    }
    public bool Save()
    {
        return _UpdateSettings();
    }

    public static SettingDto GetSettings()
    {
        return SettingData.GetSettings();
    }
}