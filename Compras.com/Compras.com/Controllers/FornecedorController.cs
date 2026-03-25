using Microsoft.AspNetCore.Mvc;
using Compras.com.Data;
using Compras.com.Models;
using System.Globalization;

namespace Compras.com.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly AppDbContext _context; // 🔥 INJEÇÃO

        public FornecedorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Salvar(string Nome, string Preco, string Imposto, string Desconto)
        {
            var produto = new Produto
            {
                Nome = Nome,

                // 🔥 CONVERSÃO SEGURA
                Preco = decimal.Parse(Preco.Replace(",", "."), CultureInfo.InvariantCulture),
                Imposto = decimal.Parse(Imposto.Replace(",", "."), CultureInfo.InvariantCulture),
                Desconto = decimal.Parse(Desconto.Replace(",", "."), CultureInfo.InvariantCulture)
            };

            _context.Produtos.Add(produto);
            _context.SaveChanges(); // 🔥 SALVA NO BANCO

            return RedirectToAction("Fornecedor", "Home");
        }
    }
}