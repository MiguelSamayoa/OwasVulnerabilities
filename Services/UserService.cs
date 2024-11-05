using Dapper;
using MySql.Data.MySqlClient;
using Owasp.Models;

namespace Owasp.Services
{
    public interface IUserService
    {
        public List<User> Authenticate(string username, string password);
        public bool Register(string username, string password);
    }

    public class UserService : IUserService
    {
        string ConnectionString;
        public UserService( IConfiguration config )
        {
            ConnectionString = config.GetConnectionString("DefaultConnection");
        }

        public List<User> Authenticate(string email, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string query = $"Select * from Users where Email = '{email}' AND password = '{password}' ";
                var result = connection.Query<User>(query).ToList();

                if(result.Count() == 0) return null;

                return result;
            }
        }

        public bool Register(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
