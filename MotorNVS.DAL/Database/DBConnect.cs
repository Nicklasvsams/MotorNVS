using Microsoft.Data.SqlClient;

namespace MotorNVS.DAL.Database
{
    public class DBConnect
    {
        private static string connString = "Server=localhost;Database=MotorDBNVS;Trusted_Connection=True;";

        public SqlConnection conn { get; } = new SqlConnection(connString);
    }
}
