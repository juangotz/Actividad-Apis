using Api.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Api.Repository
{
    public class SaleHandler : DBHandler
    {
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
