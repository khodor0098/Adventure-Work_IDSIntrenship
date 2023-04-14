using Adventure_Work.Interfaces;
using Adventure_Work.Models;
using System.Data.SqlClient;

namespace Adventure_Work.Repository
{
    public class ProductPhotoRepository : IProductPhotoRepository
    {
        private string connectionString = "Data Source=65.109.233.2,50555;Initial Catalog=AdventureWorks2012;Persist Security Info=True;User ID=apiuser;Password=P@ssw0rd123";

        public byte[] GetThumbnailByProductId(int productId)
        {
            byte[] bytes = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(@"
                    SELECT TOP 1 ThumbNailPhoto
                    FROM Production.ProductPhoto
                    WHERE ProductPhotoID = @ProductId", connection);
                    command.Parameters.AddWithValue("@ProductId", productId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bytes = (byte[])reader.GetValue(0);
                        }

                        return bytes;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while Getting the product thumbnail for product:", ex.Message);
                return bytes;
            }
        }
        public byte[] GetLargePhotoByProductId(int productId)
        {
            byte[] bytes = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(@"
                    SELECT TOP 1 LargePhoto
                    FROM Production.ProductPhoto
                    WHERE ProductPhotoID = @ProductId", connection);
                    command.Parameters.AddWithValue("@ProductId", productId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bytes = (byte[])reader.GetValue(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while Getting the product LargePhoto for product:", ex.Message);
                return bytes;
            }

            return bytes;
        }
        public ICollection<ProductPhoto> GetAllProductPhotos()
        {
            List<ProductPhoto> productPhotos = new List<ProductPhoto>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(@"SELECT * FROM Production.ProductPhoto", connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductPhoto productPhoto = new ProductPhoto();
                            productPhoto.ProductPhotoID = (int)reader["ProductPhotoID"];
                            productPhoto.ThumbNailPhoto = (byte[])reader["ThumbNailPhoto"];
                            productPhoto.ThumbnailPhotoFileName = (string)reader["ThumbnailPhotoFileName"];
                            productPhoto.LargePhoto = (byte[])reader["LargePhoto"];
                            productPhoto.LargePhotoFileName = (string)reader["LargePhotoFileName"];
                            productPhoto.ModifiedDate = (DateTime)reader["ModifiedDate"];

                            productPhotos.Add(productPhoto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while getting all product photos:", ex.Message);
            }

            return productPhotos;
        }


    }
}

