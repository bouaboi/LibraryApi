using LibraryDTOs;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LibraryDataAccessLayer
{
    public class BorrowData
    {
        
        public static int AddNewBorrow(BorrowDTO BorrowDTO)
        {
            try
            {
                using (var connection = new SqlConnection(DbConnection.ConnectionString))
                using (var command = new SqlCommand("SP_AddBorrow", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MemberID", BorrowDTO.MemberID);
                    command.Parameters.AddWithValue("@BookID", BorrowDTO.BookID);

                    var outputParam = new SqlParameter("@NewBorrowId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParam);
                    connection.Open();
                    command.ExecuteNonQuery();

                    return (int)outputParam.Value;
                }
            }
            catch
            {
                return -1;
            }
        }

        public static List<BorrowDTO> GetAllBorrows()
        {

            var BorrowList = new List<BorrowDTO>();
            try
            {
                using (var connection = new SqlConnection(DbConnection.ConnectionString))
                using (var command = new SqlCommand("SP_GetAllBorrows", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BorrowList.Add(new BorrowDTO
                                (
                                reader.GetInt32(reader.GetOrdinal("BorrowID")),
                                reader.GetInt32(reader.GetOrdinal("MemberID")),
                                reader.GetString(reader.GetOrdinal("MemberName")),
                                reader.GetInt32(reader.GetOrdinal("BookID")),
                                reader.GetString(reader.GetOrdinal("BookTitle")),
                                reader.GetDateTime(reader.GetOrdinal("BorrowDate")),
                                reader.GetDateTime(reader.GetOrdinal("DueDate")),

                                // ReturnDate is null until the book is returned
                                reader.IsDBNull(reader.GetOrdinal("ReturnDate")) ?
                                (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ReturnDate"))));

                        }
                    }
                }
            }
            catch
            {
                return null;
            }

            return BorrowList;

        }

        public static BorrowDTO GetBorrowById(int borrowId)
        {
            try
            {
                using (var connection = new SqlConnection(DbConnection.ConnectionString))
                using (var command = new SqlCommand("SP_GetBorrowByID", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BorrowID", borrowId);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new BorrowDTO(
                                reader.GetInt32(reader.GetOrdinal("BorrowID")),
                                reader.GetInt32(reader.GetOrdinal("MemberID")),
                                reader.GetString(reader.GetOrdinal("MemberName")),
                                reader.GetInt32(reader.GetOrdinal("BookID")),
                                reader.GetString(reader.GetOrdinal("BookTitle")),
                                reader.GetDateTime(reader.GetOrdinal("BorrowDate")),
                                reader.GetDateTime(reader.GetOrdinal("DueDate")),
                                reader.IsDBNull(reader.GetOrdinal("ReturnDate")) ?
                                (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ReturnDate")));

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



        public static List<BorrowDTO> GetBorrowByMemberId(int borrowMemberId)
        {
            var BorrowList = new List<BorrowDTO>();

            try
            {
                using (var connection = new SqlConnection(DbConnection.ConnectionString))
                using (var command = new SqlCommand("SP_GetBorrowsByMemberID", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@MemberID", borrowMemberId);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BorrowList.Add(new BorrowDTO(
                                reader.GetInt32(reader.GetOrdinal("BorrowID")),
                                reader.GetInt32(reader.GetOrdinal("MemberID")),
                                reader.GetString(reader.GetOrdinal("MemberName")),
                                reader.GetInt32(reader.GetOrdinal("BookID")),
                                reader.GetString(reader.GetOrdinal("BookTitle")),
                                reader.GetDateTime(reader.GetOrdinal("BorrowDate")),
                                reader.GetDateTime(reader.GetOrdinal("DueDate")),
                                reader.IsDBNull(reader.GetOrdinal("ReturnDate")) ?
                                (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ReturnDate"))));

                        }


                    }
                }
            }
            catch
            {
                return null;
            }
            return BorrowList;
        }


        public static List<BorrowDTO> GetActiveBorrows()
        {

            var BorrowList = new List<BorrowDTO>();
            try
            {
                using (var connection = new SqlConnection(DbConnection.ConnectionString))
                using (var command = new SqlCommand("SP_GetActiveBorrows", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BorrowList.Add(new BorrowDTO
                                (
                                reader.GetInt32(reader.GetOrdinal("BorrowID")),
                                reader.GetInt32(reader.GetOrdinal("MemberID")),
                                reader.GetString(reader.GetOrdinal("MemberName")),
                                reader.GetInt32(reader.GetOrdinal("BookID")),
                                reader.GetString(reader.GetOrdinal("BookTitle")),
                                reader.GetDateTime(reader.GetOrdinal("BorrowDate")),
                                reader.GetDateTime(reader.GetOrdinal("DueDate")),
                                reader.IsDBNull(reader.GetOrdinal("ReturnDate")) ?
                                (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ReturnDate"))));

                        }
                    }
                }
            }
            catch
            {
                return null;
            }

            return BorrowList;

        }

        public static bool ReturnBook(int borrowId)
        {
            try
            {
                using (var connection = new SqlConnection(DbConnection.ConnectionString))
                using (var command = new SqlCommand("SP_ReturnBook", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BorrowID", borrowId);
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
