using System;
using MySql.Data.MySqlClient;

namespace WebApplication1.Services
{
    public class MySqlDatabase : IDisposable
    {
        public MySqlConnection Connection;

        public MySqlDatabase()
        {
            Connection = new MySqlConnection("Server=localhost;Database=Employee;User Id=root;Password=root");
            this.Connection.Open();
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}




