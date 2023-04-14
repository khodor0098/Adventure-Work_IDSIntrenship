using Adventure_Work.Interfaces;
using Adventure_Work.Models;
using System.Data.SqlClient;

namespace Adventure_Work.Repository
{
    public class ProductRepository : IProductRepository
    {
        private string connectionString = "Data Source=65.109.233.2,50555;Initial Catalog=AdventureWorks2012;Persist Security Info=True;User ID=apiuser;Password=P@ssw0rd123";
        //insert new product
        public int Add(Product product)
        {
            int productId=0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(
                        @"INSERT INTO [Production].[Product]
                ([Name],[ProductNumber],[MakeFlag],[FinishedGoodsFlag],
                 [Color],[SafetyStockLevel],[ReorderPoint],[StandardCost],
                 [ListPrice],[Size],[SizeUnitMeasureCode],[WeightUnitMeasureCode],
                 [Weight],[DaysToManufacture],[ProductLine],[Class],[Style],
                 [ProductSubcategoryID],[ProductModelID],[SellStartDate],
                 [SellEndDate],[DiscontinuedDate],[rowguid],[ModifiedDate])
              VALUES
                (@Name,@ProductNumber,@MakeFlag,@FinishedGoodsFlag,
                 @Color,@SafetyStockLevel,@ReorderPoint,@StandardCost,
                 @ListPrice,@Size,@SizeUnitMeasureCode,@WeightUnitMeasureCode,
                 @Weight,@DaysToManufacture,@ProductLine,@Class,@Style,
                 @ProductSubcategoryID,@ProductModelID,@SellStartDate,
                 @SellEndDate,@DiscontinuedDate,@rowguid,@ModifiedDate);
             SELECT CAST(SCOPE_IDENTITY() AS INT)", connection);
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@ProductNumber", product.ProductNumber);
                    command.Parameters.AddWithValue("@MakeFlag", product.MakeFlag);
                    command.Parameters.AddWithValue("@FinishedGoodsFlag", product.FinishedGoodsFlag);
                    command.Parameters.AddWithValue("@Color", product.Color);
                    command.Parameters.AddWithValue("@SafetyStockLevel", product.SafetyStockLevel);
                    command.Parameters.AddWithValue("@ReorderPoint", product.ReorderPoint);
                    command.Parameters.AddWithValue("@StandardCost", product.StandardCost);
                    command.Parameters.AddWithValue("@ListPrice", product.ListPrice);
                    command.Parameters.AddWithValue("@Size", product.Size);
                    command.Parameters.AddWithValue("@SizeUnitMeasureCode", product.SizeUnitMeasureCode);
                    command.Parameters.AddWithValue("@WeightUnitMeasureCode", product.WeightUnitMeasureCode);
                    command.Parameters.AddWithValue("@Weight", product.Weight);
                    command.Parameters.AddWithValue("@DaysToManufacture", product.DaysToManufacture);
                    command.Parameters.AddWithValue("@ProductLine", product.ProductLine);
                    command.Parameters.AddWithValue("@Class", product.Class);
                    command.Parameters.AddWithValue("@Style", product.Style);
                    command.Parameters.AddWithValue("@ProductSubcategoryID", product.ProductSubcategoryID);
                    command.Parameters.AddWithValue("@ProductModelID", product.ProductModelID);
                    command.Parameters.AddWithValue("@SellStartDate", product.SellStartDate);
                    command.Parameters.AddWithValue("@SellEndDate", product.SellEndDate ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@DiscontinuedDate", product.DiscontinuedDate ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@rowguid", product.rowguid);
                    command.Parameters.AddWithValue("@ModifiedDate", product.ModifiedDate);
                    productId = (int)command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while Adding the product: " + ex.Message);
            }

            return productId;
        }

        //Checl if the product Id exists
        public bool ProductExists(int productId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Production.Product WHERE ProductID = @productId", connection);
                command.Parameters.AddWithValue("@productId", productId);

                int count = (int)command.ExecuteScalar();
                return (count > 0);
            }
        }
        //Update Product 
        public int UpdateProduct(Product product)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("UPDATE Production.Product SET Name = @name, ProductNumber = @productNumber, MakeFlag = @makeFlag, FinishedGoodsFlag = @finishedGoodsFlag, Color = @color, SafetyStockLevel = @safetyStockLevel, ReorderPoint = @reorderPoint, StandardCost = @standardCost, ListPrice = @listPrice, Size = @size, SizeUnitMeasureCode = @sizeUnitMeasureCode, WeightUnitMeasureCode = @weightUnitMeasureCode, Weight = @weight, DaysToManufacture = @daysToManufacture, ProductLine = @productLine, Class = @class, Style = @style, ProductSubcategoryID = @productSubcategoryID, ProductModelID = @productModelID, SellStartDate = @sellStartDate, SellEndDate = @sellEndDate, DiscontinuedDate = @discontinuedDate, rowguid = @rowguid, ModifiedDate = @modifiedDate WHERE ProductID = @productId", connection))
                    {
                        command.Parameters.AddWithValue("@productId", product.ProductID);
                        command.Parameters.AddWithValue("@name", product.Name);
                        command.Parameters.AddWithValue("@productNumber", product.ProductNumber);
                        command.Parameters.AddWithValue("@makeFlag", product.MakeFlag);
                        command.Parameters.AddWithValue("@finishedGoodsFlag", product.FinishedGoodsFlag);
                        command.Parameters.AddWithValue("@color", product.Color);
                        command.Parameters.AddWithValue("@safetyStockLevel", product.SafetyStockLevel);
                        command.Parameters.AddWithValue("@reorderPoint", product.ReorderPoint);
                        command.Parameters.AddWithValue("@standardCost", product.StandardCost);
                        command.Parameters.AddWithValue("@listPrice", product.ListPrice);
                        command.Parameters.AddWithValue("@size", product.Size);
                        command.Parameters.AddWithValue("@sizeUnitMeasureCode", product.SizeUnitMeasureCode);
                        command.Parameters.AddWithValue("@weightUnitMeasureCode", product.WeightUnitMeasureCode);
                        command.Parameters.AddWithValue("@weight", product.Weight);
                        command.Parameters.AddWithValue("@daysToManufacture", product.DaysToManufacture);
                        command.Parameters.AddWithValue("@productLine", product.ProductLine);
                        command.Parameters.AddWithValue("@class", product.Class);
                        command.Parameters.AddWithValue("@style", product.Style);
                        command.Parameters.AddWithValue("@productSubcategoryID", product.ProductSubcategoryID);
                        command.Parameters.AddWithValue("@productModelID", product.ProductModelID);
                        command.Parameters.AddWithValue("@sellStartDate", product.SellStartDate);
                        command.Parameters.AddWithValue("@sellEndDate", (object)product.SellEndDate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@discontinuedDate", (object)product.DiscontinuedDate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@rowguid", product.rowguid);
                        command.Parameters.AddWithValue("@modifiedDate", DateTime.Now);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while updating the product: " + ex.Message);
                return -1;
            }
        }
        //Delete Product
        public int DeleteProduct(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DELETE FROM Production.Product WHERE ProductID = @id", connection);
                    command.Parameters.AddWithValue("@id", id);

                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An exception occurred while deleting the product: " + ex.Message);
                return 0;
            }
        }
        //Get All Products
        public ICollection<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "select * from Production.Product";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

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
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while getting the products: " + ex.Message);
                return null;
            }

            return products;
        }

    }
}
