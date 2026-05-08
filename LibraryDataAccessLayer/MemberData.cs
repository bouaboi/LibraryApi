using Microsoft.Data.SqlClient;
using System.Data;
using LibraryDTOs;

namespace LibraryDataAccessLayer
{
    public class MemberData
    {
        public static List<MemberDTO> GetAllMembers()
        {
            var MembersList = new List<MemberDTO>();
            try
            {
                using (SqlConnection connection = new SqlConnection(DbConnection.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetAllMembers", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MembersList.Add(new MemberDTO
                                (reader.GetInt32(reader.GetOrdinal("MemberID")),
                                reader.GetString(reader.GetOrdinal("NationalID")),
                                reader.GetString(reader.GetOrdinal("FirstName")),
                                reader.GetString(reader.GetOrdinal("LastName")),
                                reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                reader.GetDateTime(reader.GetOrdinal("DateOfJoin")),
                                reader.GetString(reader.GetOrdinal("Address")),
                                reader.GetString(reader.GetOrdinal("Phone"))
                                ));

                            }
                        }
                    }
                }
            }
            catch 
            {
                return null;
            }
            return MembersList;

        }

        public static MemberDTO GetMemberById(int memberId)
        {
            try
            {
                using (var connection = new SqlConnection(DbConnection.ConnectionString))
                using (var command = new SqlCommand("SP_GetMemberByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@MemberID", memberId);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new MemberDTO(
                                reader.GetInt32(reader.GetOrdinal("MemberID")),
                                reader.GetString(reader.GetOrdinal("NationalID")),
                                reader.GetString(reader.GetOrdinal("FirstName")),
                                reader.GetString(reader.GetOrdinal("LastName")),
                                reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                reader.GetDateTime(reader.GetOrdinal("DateOfJoin")),
                                reader.GetString(reader.GetOrdinal("Address")),
                                reader.GetString(reader.GetOrdinal("Phone"))
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

        public static int AddNewMember(MemberDTO MemberDTO)
        {

            try
            {
                using (var connection = new SqlConnection(DbConnection.ConnectionString))
                using (var command = new SqlCommand("SP_AddMember", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@NationalID", MemberDTO.NationalID);
                    command.Parameters.AddWithValue("@FirstName", MemberDTO.FirstName);
                    command.Parameters.AddWithValue("@LastName", MemberDTO.LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", MemberDTO.DateOfBirth);
                    command.Parameters.AddWithValue("@Address", MemberDTO.Address);
                    command.Parameters.AddWithValue("@Phone", MemberDTO.Phone);

                    var outputIdParam = new SqlParameter("@NewMemberID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);

                    connection.Open();
                    command.ExecuteNonQuery();

                    return (int)outputIdParam.Value;


                }
            }
            catch 
            {
                return -1;
            }
        }

        public static bool UpdateMember(MemberDTO MemberDTO)
        {

            try
            {
                using (var connection = new SqlConnection(DbConnection.ConnectionString))
                using (var command = new SqlCommand("SP_UpdateMember", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MemberID", MemberDTO.MemberID);
                    command.Parameters.AddWithValue("@NationalID", MemberDTO.NationalID);
                    command.Parameters.AddWithValue("@FirstName", MemberDTO.FirstName);
                    command.Parameters.AddWithValue("@LastName", MemberDTO.LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", MemberDTO.DateOfBirth);
                    command.Parameters.AddWithValue("@Address", MemberDTO.Address);
                    command.Parameters.AddWithValue("@Phone", MemberDTO.Phone);

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


        public static bool DeleteMember(int memberId)
        {
            try
            {
                using (var connection = new SqlConnection(DbConnection.ConnectionString))
                using (var command = new SqlCommand("SP_DeactivateMember", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@MemberID", memberId);

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

        public static bool ReactiveMember(int memberId)
        {
            try
            {
                using (var connection = new SqlConnection(DbConnection.ConnectionString))
                using (var command = new SqlCommand("SP_ReactivateMember", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@MemberID", memberId);

                    connection.Open();
                    int RowsAffected = command.ExecuteNonQuery();
                    return (RowsAffected > 0);
                }
            }
            catch 
            {
                return false;
            }
        }
        public static List<MemberDTO> SearchMember(string query)
        {
            var membersList = new List<MemberDTO>();
            try
            {
                using (var connection = new SqlConnection(DbConnection.ConnectionString))
                using (var command = new SqlCommand("SP_SearchMember", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Query", query);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            membersList.Add(new MemberDTO(
                                reader.GetInt32(reader.GetOrdinal("MemberID")),
                                reader.GetString(reader.GetOrdinal("NationalID")),
                                reader.GetString(reader.GetOrdinal("FirstName")),
                                reader.GetString(reader.GetOrdinal("LastName")),
                                reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                reader.GetDateTime(reader.GetOrdinal("DateOfJoin")),
                                reader.GetString(reader.GetOrdinal("Address")),
                                reader.GetString(reader.GetOrdinal("Phone"))
                            ));
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
            return membersList;
        }
    }
}