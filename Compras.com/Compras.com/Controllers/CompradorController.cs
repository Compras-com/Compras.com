using Microsoft.AspNetCore.Mvc;
using Compras.com.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Compras.com.Controllers
{
    public class CompradorController : Controller
    {
        private readonly BancoContext _context;
        public CompradorController(BancoContext context) => _context = context;

        public IActionResult Index() 
        {
            var lista = _context.Produtos.Include(p => p.Fornecedor).ToList();
            // IMPORTANTE: Aqui ele vai buscar em Views/Home/Comprador.cshtml
            return View("~/Views/Home/Comprador.cshtml", lista);
        }
    }
}