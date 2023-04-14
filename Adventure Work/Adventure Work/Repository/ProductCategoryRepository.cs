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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO Production.ProductCategory (Name, rowguid, ModifiedDate) VALUES (@name, @rowguid, @modifiedDate); SELECT CAST(SCOPE_IDENTITY() AS INT)", connection);
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(
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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(
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
        public ICollection<Product> GetProductsByCategory(int productCategoryId)
        {
            List<Product> products = new List<Product>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(
                        @"SELECT *
                   FROM Production.Product
                   WHERE ProductSubcategoryID IN(
                   SELECT ProductSubcategoryID
                   FROM Production.ProductSubcategory
                   WHERE ProductCategoryID = @ProductCategoryID)",
                        connection);
                    command.Parameters.AddWithValue("@ProductCategoryID", productCategoryId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product();
                            product.ProductID = (int)reader["ProductID"];
                            product.Name = (string)reader["Name"];
                            product.ProductNumber = (string)reader["ProductNumber"];
                            product.MakeFlag = (bool)reader["MakeFlag"];
                            product.FinishedGoodsFlag = (bool)reader["FinishedGoodsFlag"];
                            product.Color = reader["Color"] == DBNull.Value ? null : (string)reader["Color"];
                            product.SafetyStockLevel = (short)reader["SafetyStockLevel"];
                            product.ReorderPoint = (short)reader["ReorderPoint"];
                            product.StandardCost = (decimal)reader["StandardCost"];
                            product.ListPrice = (decimal)reader["ListPrice"];
                            product.Size = reader["Size"] == DBNull.Value ? null : (string)reader["Size"];
                            product.SizeUnitMeasureCode = reader["SizeUnitMeasureCode"] == DBNull.Value ? null : (string)reader["SizeUnitMeasureCode"];
                            product.WeightUnitMeasureCode = reader["WeightUnitMeasureCode"] == DBNull.Value ? null : (string)reader["WeightUnitMeasureCode"];
                            product.Weight = reader["Weight"] == DBNull.Value ? 0 : (decimal)reader["Weight"];
                            product.DaysToManufacture = (int)reader["DaysToManufacture"];
                            product.ProductLine = reader["ProductLine"] == DBNull.Value ? null : (string)reader["ProductLine"];
                            product.Class = reader["Class"] == DBNull.Value ? null : (string)reader["Class"];
                            product.Style = reader["Style"] == DBNull.Value ? null : (string)reader["Style"];
                            product.ProductSubcategoryID = reader["ProductSubcategoryID"] == DBNull.Value ? 0 : (int)reader["ProductSubcategoryID"];
                            product.ProductModelID = reader["ProductModelID"] == DBNull.Value ? 0 : (int)reader["ProductModelID"]; ;
                            product.SellStartDate = (DateTime)reader["SellStartDate"];
                            product.SellEndDate = reader["SellEndDate"] == DBNull.Value ? null : (DateTime?)reader["SellEndDate"];
                            product.DiscontinuedDate = reader["DiscontinuedDate"] == DBNull.Value ? null : (DateTime?)reader["DiscontinuedDate"];
                            product.rowguid = (Guid)reader["rowguid"];
                            product.ModifiedDate = (DateTime)reader["ModifiedDate"];

                            products.Add(product);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }
            return products;
        }

    }
}
