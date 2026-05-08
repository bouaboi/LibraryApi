using LibraryDTOs;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LibraryDataAccessLayer
{
    public class BookData
    {
        public static int AddNewBook(BookDTO bookDTO)
        {
            try
            {
                using (var connection = new SqlConnection(DbConnection.ConnectionString))
                using (var command = new SqlCommand("SP_AddBook", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Title", bookDTO.Title);
                    command.Parameters.AddWithValue("@ISBN", bookDTO.ISBN);
                    command.Parameters.AddWithValue("@Genre", bookDTO.Genre);
                    command.Parameters.AddWithValue("@Author", bookDTO.Author);
                    command.Parameters.AddWithValue("@TotalCopies", bookDTO.TotalCopies);

                    var outputIdParam = new SqlParameter("@NewBookID", SqlDbType.Int)
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

        public static BookDTO GetBookById(int bookId)
        {
            try
            {
                using (var connection = new SqlConnection(DbConnection.ConnectionString))
                using (var command = new SqlCommand("SP_GetBookByID", connection))
                {
                    
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookID", bookId);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new BookDTO(
                                reader.GetInt32(reader.GetOrdinal("BookID")),
                                reader.GetString(reader.GetOrdinal("Title")),
                                reader.GetString(reader.GetOrdinal("ISBN")),
                                reader.GetString(reader.GetOrdinal("Genre")),
                                reader.GetString(reader.GetOrdinal("Author")),
                                reader.GetInt32(reader.GetOrdinal("TotalCopies")),
                                reader.GetInt32(reader.GetOrdinal("AvailableCopies"))
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

        public static bool UpdateBook(BookDTO BookDTO)
        {
            try
            {
                using (var connection =new SqlConnection(DbConnection.ConnectionString))
                using (var command = new SqlCommand("SP_UpdateBook", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@BookID", BookDTO.BookID);
                    command.Parameters.AddWithValue("@Title", BookDTO.Title);
                    command.Parameters.AddWithValue("@ISBN", BookDTO.ISBN);
                    command.Parameters.AddWithValue("@Genre", BookDTO.Genre);
                    command.Parameters.AddWithValue("@Author", BookDTO.Author);
                    command.Parameters.AddWithValue("@TotalCopies", BookDTO.TotalCopies);


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

        public static List<BookDTO> GetAllBooks()
        {
            var BookList = new List<BookDTO>();
            try
            {
                using (var connection =new SqlConnection(DbConnection.ConnectionString))
                using (var command = new SqlCommand("SP_GetAllBooks", connection))
                {
                    command.CommandType= CommandType.StoredProcedure;
                   
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BookList.Add(new BookDTO
                                (
                                reader.GetInt32(reader.GetOrdinal("BookID")),
                                reader.GetString(reader.GetOrdinal("Title")),
                                reader.GetString(reader.GetOrdinal("ISBN")),
                                reader.GetString(reader.GetOrdinal("Genre")),
                                reader.GetString(reader.GetOrdinal("Author")),
                                reader.GetInt32(reader.GetOrdinal("TotalCopies")),
                                reader.GetInt32(reader.GetOrdinal("AvailableCopies"))
                                ));
                        }
                    }
                }

            }
            catch
            {
                return null;
            }
            return BookList;

        }

        public static bool DeleteBook(int bookId)
        {
            try
            {
                using (var connection = new SqlConnection(DbConnection.ConnectionString))
                using (var command = new SqlCommand("SP_DeleteBook", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookID", bookId);

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

        public static List<BookDTO> SearchBook(string query)
        {
            var BookList = new List<BookDTO>();
            try
            {
                using (var connection = new SqlConnection(DbConnection.ConnectionString))
                using (var command = new SqlCommand("SP_SearchBook", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Query", query);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BookList.Add(new BookDTO
                                (
                                reader.GetInt32(reader.GetOrdinal("BookID")),
                                reader.GetString(reader.GetOrdinal("Title")),
                                reader.GetString(reader.GetOrdinal("ISBN")),
                                reader.GetString(reader.GetOrdinal("Genre")),
                                reader.GetString(reader.GetOrdinal("Author")),
                                reader.GetInt32(reader.GetOrdinal("TotalCopies")),
                                reader.GetInt32(reader.GetOrdinal("AvailableCopies"))
                                ));
                        }
                    }
                }

            }
            catch
            {
                return null;
            }
            return BookList;

        }
    }
}
