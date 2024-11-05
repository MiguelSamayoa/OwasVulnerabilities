using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Owasp.Models;
using Owasp.Services;
using Newtonsoft.Json;

namespace Owasp.Controllers
{
    public class AuthController : Controller
    {
        public IUserService UserService { get; set; }

        public AuthController( IUserService userService )
        {
            this.UserService = userService;
        }
        // GET: AuthController
        public ActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(string Email, string Password) {

            List<User> isAutenticate = UserService.Authenticate(Email, Password);

            if (isAutenticate == null) return View();

            HttpContext.Session.SetString("Users", JsonConvert.SerializeObject(isAutenticate));
            return RedirectToAction("Index", "Home");
        }

        //// GET: AuthController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: AuthController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: AuthController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: AuthController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: AuthController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: AuthController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: AuthController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
