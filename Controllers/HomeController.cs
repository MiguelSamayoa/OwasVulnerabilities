using Microsoft.AspNetCore.Mvc;
using Owasp.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using Owasp.Services;

namespace Owasp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderService orderService;

        public HomeController(ILogger<HomeController> logger, IOrderService orderService)
        {
            _logger = logger;
            this.orderService = orderService;
        }

        public IActionResult Index()
        {
            List<User> users = null;
            var usersJson = HttpContext.Session.GetString("Users");
            if (!string.IsNullOrEmpty(usersJson))
            {
                users = JsonConvert.DeserializeObject<List<User>>(usersJson);
            }

            UserViewModel Modelo = new UserViewModel
            {
                Users = users ?? new List<User>()
            };

            List<Order> Ordenes = orderService.GetOrdersByUser(users ?? new List<User>());

            Modelo.Order = Ordenes;
            return View(Modelo);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
