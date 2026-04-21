using Microsoft.AspNetCore.Mvc;
using Compras.com.Models;
using Compras.com.Data;
using System.Linq;

namespace Compras.com.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly BancoContext _context;
        public FornecedorController(BancoContext context) => _context = context;

        public IActionResult Index() {
            // No futuro, pegar o ID do fornecedor logado
            return View(_context.Produtos.ToList()); 
        }

        [HttpPost]
        public IActionResult CadastrarProduto(Produto p) {
            _context.Produtos.Add(p);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}