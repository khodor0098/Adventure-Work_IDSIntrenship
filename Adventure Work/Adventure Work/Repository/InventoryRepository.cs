using Adventure_Work.Interfaces;
using Adventure_Work.Models;
using System.Data.SqlClient;

namespace Adventure_Work.Repository
{
    public class InventoryRepository : IInventoryRepository 
    {
        private string connectionString = "Data Source=65.109.233.2,50555;Initial Catalog=AdventureWorks2012;Persist Security Info=True;User ID=apiuser;Password=P@ssw0rd123";

        public int Add(Inventory inventory)
        {
            int inventoryId = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO Production.ProductInventory (ProductID, LocationID, Shelf, Bin, Quantity, rowguid, ModifiedDate) OUTPUT INSERTED.ProductID VALUES(@productId, @locationId, @shelf, @bin, @quantity, @rowguid, @modifiedDate)", connection);
                    command.Parameters.AddWithValue("@productId", inventory.ProductID);
                    command.Parameters.AddWithValue("@locationId", inventory.LocationID);
                    command.Parameters.AddWithValue("@shelf", inventory.Shelf);
                    command.Parameters.AddWithValue("@bin", inventory.Bin);
                    command.Parameters.AddWithValue("@quantity", inventory.Quantity);
                    command.Parameters.AddWithValue("@rowguid", inventory.rowguid);
                    command.Parameters.AddWithValue("@modifiedDate", inventory.ModifiedDate);

                    inventoryId = (int)command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting inventory record: " + ex.Message);
            }

            return inventoryId;
        }
        public int Update(Inventory inventory)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE Production.ProductInventory SET Quantity = @quantity, ModifiedDate = @modifiedDate ,Shelf = @shelf ,Bin = @bin WHERE ProductID = @productId AND LocationID = @locationId ", connection);
                    command.Parameters.AddWithValue("@quantity", inventory.Quantity);
                    command.Parameters.AddWithValue("@modifiedDate", inventory.ModifiedDate);
                    command.Parameters.AddWithValue("@productId", inventory.ProductID);
                    command.Parameters.AddWithValue("@locationId", inventory.LocationID);
                    command.Parameters.AddWithValue("@shelf", inventory.Shelf);
                    command.Parameters.AddWithValue("@bin", inventory.Bin);

                    rowsAffected = command.ExecuteNonQuery();

                
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating inventory record: " + ex.Message);
            }

            return rowsAffected;
        }
        public bool InventoryExist(short locationId,int productId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Production.ProductInventory WHERE LocationID = @Lid and ProductID=@Pid", connection);
                command.Parameters.AddWithValue("@Lid", locationId);
                command.Parameters.AddWithValue("@Pid", productId);

                int count = (int)command.ExecuteScalar();
                return (count > 0);
            }
        }
        public ICollection<Product> GetProductsInShelf(string shelf)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT p.* 
                         FROM Production.Product p 
                         JOIN Production.ProductInventory i ON p.ProductID = i.ProductID 
                         WHERE i.Shelf = @shelf";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@shelf", shelf);

                List<Product> products = new List<Product>();

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

                return products;
            }
        }

        public ICollection<ProductQuantity> GetProductQuantities()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
            SELECT p.Name, SUM(i.Quantity) AS TotalQuantity
            FROM Production.Product p 
            JOIN Production.ProductInventory i ON p.ProductID = i.ProductID 
            GROUP BY p.Name";

                SqlCommand command = new SqlCommand(query, connection);

                List<ProductQuantity> productsQuantities = new List<ProductQuantity>();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProductQuantity productQuantity = new ProductQuantity();
                        productQuantity.Name = (string)reader["Name"];
                        productQuantity.Quantity = (int)reader["TotalQuantity"];

                        productsQuantities.Add(productQuantity);
                    }
                }

                return productsQuantities;
            }
        }
    }
}
