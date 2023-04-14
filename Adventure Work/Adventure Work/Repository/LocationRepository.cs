using Adventure_Work.Interfaces;
using System.Data.SqlClient;

namespace Adventure_Work.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private string connectionString = "Data Source=65.109.233.2,50555;Initial Catalog=AdventureWorks2012;Persist Security Info=True;User ID=apiuser;Password=P@ssw0rd123";

        public bool LocationExists(short id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Production.Location WHERE LocationID = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                int result = (int)command.ExecuteScalar();
                return (result > 0);
            }
        }
    }
}
