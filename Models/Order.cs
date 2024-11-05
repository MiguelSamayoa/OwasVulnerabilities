namespace Owasp.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int? UserID { get; set; }  // Permite null si el usuario es eliminado
        public OrderStatus Status { get; set; }   // Código para el estado del pedido
        public string? Username { get; set; }
    }

    public class OrderDetailViewModel
    {
        public Order Order { get; set; }
        public List<OrderProductDetail> OrderDetails { get; set; } = new List<OrderProductDetail>();
    }

    public class OrderProductDetail
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total => Quantity * UnitPrice; // Calcula el total de cada producto en la orden
    }


    public enum OrderStatus {
        Pendiente = 1,
        Enviado = 2,
        Completado = 3,
        Cancelado = 4
    }
}
