using Microsoft.AspNetCore.Mvc;
using Compras.com.Data;
using Compras.com.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Compras.com.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var tipo = User.FindFirst("Tipo")?.Value;
            var email = User.Identity?.Name;

            if (tipo == "Fornecedor")
            {
                return View(_context.Produtos.Where(p => p.EmailFornecedor == email).ToList());
            }

            return View(new List<Produto>());
        }

        [HttpPost]
        public IActionResult Create(Produto produto)
        {
            var email = User.Identity?.Name;

            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Index", "Login");

            produto.EmailFornecedor = email;

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var email = User.Identity?.Name;

            var produto = _context.Produtos
                .FirstOrDefault(p => p.Id == id && p.EmailFornecedor == email);

            if (produto == null) return RedirectToAction("Index");

            return View(produto);
        }

        [HttpPost]
        public IActionResult Atualizar(Produto produto)
        {
            var email = User.Identity?.Name;

            var produtoBanco = _context.Produtos
                .FirstOrDefault(p => p.Id == produto.Id && p.EmailFornecedor == email);

            if (produtoBanco != null)
            {
                produtoBanco.Nome = produto.Nome;
                produtoBanco.Preco = produto.Preco;
                produtoBanco.Imposto = produto.Imposto;
                produtoBanco.Desconto = produto.Desconto;
                produtoBanco.ComImposto = produto.ComImposto;

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var email = User.Identity?.Name;

            var produto = _context.Produtos
                .FirstOrDefault(p => p.Id == id && p.EmailFornecedor == email);

            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}