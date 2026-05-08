

using LibraryDTOs;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LibraryDataAccessLayer
{
    public class SettingData
    {

        public static bool UpdateSettings(SettingDto settingDTO)
        {
            try
            {
                using (var connection = new SqlConnection(DbConnection.ConnectionString))
                using (var command = new SqlCommand("SP_UpdateSettings", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MaxBorrowLimit", settingDTO.MaxBorrowLimit);
                    command.Parameters.AddWithValue("@BorrowDurationDays", settingDTO.BorrowDurationDays);
                    command.Parameters.AddWithValue("@DailyFineRate", settingDTO.DailyFineRate);
                   


                    connection.Open();
                    command.ExecuteNonQuery();

                    return true;
                }
            }
            catch
            {
                return false;
            }

        }

        public static SettingDto GetSettings()
        {
            try
            {
                using (var connection = new SqlConnection(DbConnection.ConnectionString))
                using (var command = new SqlCommand("SP_GetSettings", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new SettingDto(
                                reader.GetInt32(reader.GetOrdinal("SettingID")),
                                reader.GetInt32(reader.GetOrdinal("MaxBorrowLimit")),
                                reader.GetInt32(reader.GetOrdinal("BorrowDurationDays")),
                                reader.GetDecimal(reader.GetOrdinal("DailyFineRate"))
                            );
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
