using LibraryDTOs;
using Microsoft.Data.SqlClient;
using System.Data;


namespace LibraryDataAccessLayer
{
    public class FinesData
    {

        public static List<FineDto> GetAllFines()
        {

            var FinesList = new List<FineDto>();
            try
            {
                using (var connection = new SqlConnection(DbConnection.ConnectionString))
                using (var command = new SqlCommand("SP_GetAllFines", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FinesList.Add(new FineDto
                                (
                                reader.GetInt32(reader.GetOrdinal("FineID")),
                                reader.GetInt32(reader.GetOrdinal("BorrowID")),
                                reader.GetString(reader.GetOrdinal("MemberName")),
                                reader.GetString(reader.GetOrdinal("BookTitle")),
                                reader.GetDecimal(reader.GetOrdinal("Amount")),
                                reader.GetDecimal(reader.GetOrdinal("PaidAmount")),
                                reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                reader.GetBoolean(reader.GetOrdinal("IsSettled")
                                )));
                        }
                    }
                }
            }
            catch
            {
                return null;
            }

            return FinesList;

        }


        public static List<FineDto> GetFinesByMemberId(int FinesMemberId)
        {
            var FinesList = new List<FineDto>();

            try
            {
                using (var connection = new SqlConnection(DbConnection.ConnectionString))
                using (var command = new SqlCommand("SP_GetFinesByMemberID", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@MemberID", FinesMemberId);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                                FinesList.Add(new FineDto
                                (
                                reader.GetInt32(reader.GetOrdinal("FineID")),
                                reader.GetInt32(reader.GetOrdinal("BorrowID")),
                                reader.GetString(reader.GetOrdinal("MemberName")),
                                reader.GetString(reader.GetOrdinal("BookTitle")),
                                reader.GetDecimal(reader.GetOrdinal("Amount")),
                                reader.GetDecimal(reader.GetOrdinal("PaidAmount")),
                                reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                reader.GetBoolean(reader.GetOrdinal("IsSettled")
                                )));

                        }


                    }
                }
            }
            catch
            {
                return null;
            }
            return FinesList;
        }

        public static bool SettleFine(int fineId, decimal paidAmount)
        {
            try
            {
                using (var connection = new SqlConnection(DbConnection.ConnectionString))
                using (var command = new SqlCommand("SP_SettleFine", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FineID", fineId);
                    command.Parameters.AddWithValue("@PaidAmount", paidAmount);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return (rowsAffected > 0);
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
