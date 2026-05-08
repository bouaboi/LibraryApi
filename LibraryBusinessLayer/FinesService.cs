using LibraryDataAccessLayer;
using LibraryDTOs;


namespace LibraryBusinessLayer
{
    public class FinesService
    {
        public static List<FineDto> GetAllFines()
        {
            return FinesData.GetAllFines();
        }

        public static List<FineDto> GetFineByMemberID(int FinesMemberId)
        {
            return FinesData.GetFinesByMemberId(FinesMemberId);
        }

        public static bool SettleFine(int fineId, decimal paidAmount)
        {
            return FinesData.SettleFine(fineId, paidAmount);
        }

    }
}
