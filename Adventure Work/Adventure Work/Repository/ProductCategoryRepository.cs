using Adventure_Work.Interfaces;
using Adventure_Work.Models;
using System.Data.SqlClient;

namespace Adventure_Work.Repository
{
    public class ProductCategoryRepository: IProductCategoryRepository
    {
        private string connectionString = "Data Source=65.109.233.2,50555;Initial Catalog=AdventureWorks2012;Persist Security Info=True;User ID=apiuser;Password=P@ssw0rd123";
        public int Add(ProductCategory category)
        {
            int categoryId = 0;

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("INSERT INTO Production.ProductCategory (Name, rowguid, ModifiedDate) VALUES (@name, @rowguid, @modifiedDate); SELECT CAST(SCOPE_IDENTITY() AS INT)", connection);
                    command.Parameters.AddWithValue("@name", category.Name);
                    command.Parameters.AddWithValue("@rowguid", category.rowguid);
                    command.Parameters.AddWithValue("@modifiedDate", category.ModifiedDate);

                    categoryId = (int)command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting product category: " + ex.Message);
            }

            return categoryId;
        }
        public int Update(ProductCategory productCategory)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        "UPDATE Production.ProductCategory SET Name = @Name, rowguid = @RowGuid, ModifiedDate = @ModifiedDate WHERE ProductCategoryID = @ProductCategoryID", connection);
                    command.Parameters.AddWithValue("@Name", productCategory.Name);
                    command.Parameters.AddWithValue("@RowGuid", productCategory.rowguid);
                    command.Parameters.AddWithValue("@ModifiedDate", productCategory.ModifiedDate);
                    command.Parameters.AddWithValue("@ProductCategoryID", productCategory.ProductCategoryID);
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error updating product category: {ex.Message}");
                return -1;
            }
        }
        public bool CategoryExists(int productCategoryId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                    "SELECT COUNT(*) FROM Production.ProductCategory WHERE ProductCategoryId = @ProductCategoryId", connection);
                command.Parameters.AddWithValue("@ProductCategoryId", productCategoryId);
                return (int)command.ExecuteScalar() > 0;
            }
        }
        public int Delete(int productCategoryId)
        {
            int rowsAffected = 0;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        "DELETE FROM Production.ProductCategory WHERE ProductCategoryID = @ProductCategoryID", connection);
                    command.Parameters.AddWithValue("@ProductCategoryID", productCategoryId);
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An exception occurred while deleting the productCategory: " + ex.Message);
                return 0;
            }
            return rowsAffected;
        }

    }
}
