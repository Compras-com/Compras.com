using Microsoft.AspNetCore.Mvc;
using Compras.com.Models;
using Compras.com.Data;
using System.Linq;

namespace Compras.com.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context; // 🔥 ADICIONAR

        public HomeController(AppDbContext context) // 🔥 ADICIONAR
        {
            _context = context;
        }

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
            var produtos = _context.Produtos.ToList(); // ✅ AGORA FUNCIONA
            return View(produtos);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}