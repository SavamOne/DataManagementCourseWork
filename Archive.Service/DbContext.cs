using Npgsql;
using System.Data;

namespace Archive.Service
{
    public class DbContext
    {
        private readonly string connectionString;
        private IDbConnection connection;

        public string Login { get; set; }

        public DbContext(string host, ushort port, string login, string password)
        {
            connectionString = $"host={host};Port={port};Database=archive;Username={login};Password={password}";
            Login = login;
            CreateOrGet();
        }

        public IDbConnection Connection => CreateOrGet();

        private IDbConnection CreateOrGet()
        {
            if (connection == null || connection.State != ConnectionState.Open)
            {
                connection?.Dispose();
                connection = new NpgsqlConnection(connectionString);
                connection.Open();
            }

            return connection;
        }
    }
}
