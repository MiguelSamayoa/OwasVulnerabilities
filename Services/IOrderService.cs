using Owasp.Models;
using MySql.Data.MySqlClient;
using Dapper;

namespace Owasp.Services
{
    public interface IOrderService
    {
        public List<Order> GetOrdersByUser(List<User> usuarios);
        public OrderDetailViewModel GetOrderDetails(int orderId);
    }

    public class OrderService : IOrderService
    {
        string ConnectionString;
        public OrderService(IConfiguration config)
        {
            ConnectionString = config.GetConnectionString("DefaultConnection");
        }

        public List<Order> GetOrdersByUser(List<User> usuarios)
        {
            if (usuarios == null || usuarios.Count == 0) return new List<Order>();

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                List<int> Ids = usuarios.Select(user => user.UserID).ToList();
                string idsString = string.Join(",", Ids);

                string query = @$"
                            SELECT 
                                o.*, 
                                u.Username
                            FROM Orders o
                            LEFT JOIN Users u ON u.UserID = o.UserID
                            WHERE o.UserID IN ({idsString})";

                var result = connection.Query<Order>(query).ToList();

                return result;
            }
        }

        public OrderDetailViewModel GetOrderDetails(int orderId)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                // Consultar información de la orden
                var orderQuery = @"
            SELECT o.OrderID, o.OrderDate, o.UserID, u.Username, o.Status
            FROM Orders o
            LEFT JOIN Users u ON o.UserID = u.UserID
            WHERE o.OrderID = @OrderID";

                var order = connection.QuerySingleOrDefault<Order>(orderQuery, new { OrderID = orderId });

                if (order == null)
                    return null; // Retorna null si no se encuentra la orden

                // Consultar detalles de la orden
                var orderDetailsQuery = @"
            SELECT p.ProductName, od.Quantity, od.UnitPrice
            FROM OrderDetails od
            INNER JOIN Products p ON od.ProductID = p.ProductID
            WHERE od.OrderID = @OrderID";

                var orderDetails = connection.Query<OrderProductDetail>(orderDetailsQuery, new { OrderID = orderId }).ToList();

                // Crear y retornar el modelo de vista
                return new OrderDetailViewModel
                {
                    Order = order,
                    OrderDetails = orderDetails
                };
            }
        }

    }
}
