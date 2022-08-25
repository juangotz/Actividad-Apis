using Api.Model;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Api.Repository
{
    public class ProductHandler : DBHandler
    {
        public static List<Product> GetProductos()
        {
            List<Product> result = new List<Product>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Producto", sqlConnection))
                {
                    sqlConnection.Open();
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Product producto = new Product();

                                producto.id = Convert.ToInt32(dataReader["Id"]);
                                producto.description = dataReader["Nombre"].ToString();
                                producto.price = Convert.ToInt32(dataReader["Precio/Venta"]);
                                producto.priceSell = Convert.ToInt32(dataReader["Costo"]);
                                producto.stock = Convert.ToInt32(dataReader["Stock"]);
                                producto.idUser = Convert.ToInt32(dataReader["IdUsuario"]);

                                result.Add(producto);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return result;
        }
        public static bool DeleteProduct(int id)
        {
            bool result = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryDelete = "DELETE FROM [SistemaGestion].[dbo].[Producto] WHERE Id = @idProducto";
                    SqlParameter idParameter = new SqlParameter("@IdProducto", System.Data.SqlDbType.BigInt)
                    {
                        Value = id
                    };
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
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
        public static bool UpdateDescription(Product product)
        {
            bool result = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryUpdate = "UPDATE [SistemaGestion].[dbo].[Producto]" +
                        "SET Descripciones = @nombre WHERE Id = @idProducto";

                    SqlParameter idParameter = new SqlParameter("@idProducto", System.Data.SqlDbType.BigInt)
                    {
                        Value = product.id
                    };
                    SqlParameter nameParameter = new SqlParameter("@nombre", System.Data.SqlDbType.Char)
                    {
                        Value = product.description
                    };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(nameParameter);
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
        public static bool CreateProduct(Product product)
        {
            bool result = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryCreate = "INSERT INTO [SistemaGestion].[dbo].[Producto] " +
                        "('Descripciones', 'Precio/Venta', 'Stock', 'Costo', 'IdUsuario') " +
                        "VALUES (@descripciones, @precioVenta, @stock, @costo, @idUsuario)";

                    SqlParameter descriptionParameter = new SqlParameter("@descripciones", System.Data.SqlDbType.Char)
                    {
                        Value = product.description
                    };
                    SqlParameter priceParameter = new SqlParameter("@precioVenta", System.Data.SqlDbType.BigInt)
                    {
                        Value = product.price
                    };
                    SqlParameter stockParameter = new SqlParameter("@stock", System.Data.SqlDbType.BigInt)
                    {
                        Value = product.stock
                    };
                    SqlParameter costParameter = new SqlParameter("@costo", System.Data.SqlDbType.BigInt)
                    {
                        Value = product.priceSell
                    };
                    SqlParameter idUserParameter = new SqlParameter("@idUsuario", System.Data.SqlDbType.BigInt)
                    {
                        Value = product.idUser
                    };

                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(queryCreate, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(descriptionParameter);
                        sqlCommand.Parameters.Add(priceParameter);
                        sqlCommand.Parameters.Add(stockParameter);
                        sqlCommand.Parameters.Add(costParameter);
                        sqlCommand.Parameters.Add(idUserParameter);
                        int rowsAffected = sqlCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            result = true;
                        }
                    }
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
