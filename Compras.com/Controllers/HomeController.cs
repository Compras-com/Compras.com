using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Compras.com.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Admin()
        {
            return RedirectToAction("Index", "Admin");
        }

        public IActionResult Fornecedor()
        {
            return View();
        }

        public IActionResult Comprador()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}