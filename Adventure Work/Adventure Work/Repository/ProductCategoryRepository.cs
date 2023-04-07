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
    }
}
