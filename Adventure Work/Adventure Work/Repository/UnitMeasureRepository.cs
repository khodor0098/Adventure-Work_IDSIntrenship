using Adventure_Work.Interfaces;
using Adventure_Work.Models;
using System.Data.SqlClient;

namespace Adventure_Work.Repository
{
    public class UnitMeasureRepository : IUnitMeasureRepository
    {
        private string connectionString = "Data Source=65.109.233.2,50555;Initial Catalog=AdventureWorks2012;Persist Security Info=True;User ID=apiuser;Password=P@ssw0rd123";

        public bool GetByUnitMeasureCode(string code)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Production.UnitMeasure WHERE UnitMeasureCode = @code", connection);
                command.Parameters.AddWithValue("@code", code);

                int count = (int)command.ExecuteScalar();

                return count > 0;
            }
        }
    }
}
