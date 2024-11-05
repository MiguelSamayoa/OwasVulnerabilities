using Microsoft.AspNetCore.Mvc;
using Owasp.Models;
using Owasp.Services;

namespace Owasp.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderService orderService;

        public OrderController(ILogger<HomeController> logger, IOrderService orderService)
        {
            _logger = logger;
            this.orderService = orderService;
        }

        public IActionResult OrdenDetail( int Id ) {
            OrderDetailViewModel Modelo = orderService.GetOrderDetails(Id);
            return View(Modelo);
        }
    }
}
