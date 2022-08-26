using APICODERHOUSE.Models;
using Microsoft.Data.SqlClient;

namespace APICODERHOUSE.Repository
{
    public class SaleHandler : DBHandler
    {
        SqlConnection sqlConnection = new SqlConnection(ConnectionString);
        public static List<Sale> GetSale()
        {
            List<Sale> sales = new List<Sale>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCmd = new SqlCommand("SELECT * FROM Venta", sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCmd.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Sale sale = new Sale();
                                sale.id = Convert.ToInt32(dataReader["Id"]);
                                sale.comment = dataReader["Comentarios"].ToString();
                            }
                        }
                    }
                    sqlConnection.Close();
                }

            }
            return sales;
        }
        public static bool CreateSale(Sale sale)
        {
            bool result = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryCreate = "INSERT INTO [SistemaGestion].[dbo].[Venta] " +
                        "(Comentarios) " +
                        "VALUES (@comentarios)";

                    SqlParameter commentParameter = new SqlParameter("@comentarios", System.Data.SqlDbType.Char)
                    {
                        Value = sale.comment
                    };

                    sqlConnection.Open();
                    bool confirmProduct = UpdateStock(sale.id);
                    if (confirmProduct == true)
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(queryCreate, sqlConnection))
                        {
                            sqlCommand.Parameters.Add(commentParameter);
                            int rowsAffected = sqlCommand.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                result = true;
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("The product that was chose for purchase does not exist.");
                    }
                    sqlConnection.Close();
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return result;
            }
        }
        public static bool UpdateStock(int id)
        {
            bool result = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryUpdate = "UPDATE Producto SET Stock = Stock-1 WHERE Id = @IdProducto";

                    SqlParameter idParameter = new SqlParameter("idProducto", System.Data.SqlDbType.BigInt)
                    {
                        Value = id
                    };
                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(idParameter);
                        int rowsAffected = sqlCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            result = true;
                        }
                    }

                    sqlConnection.Close();
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return result;
            }
        }
    }
}
