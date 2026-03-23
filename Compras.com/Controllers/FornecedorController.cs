using Microsoft.AspNetCore.Mvc;
using Compras.com.Data;
using System.Globalization;

namespace Compras.com.Controllers
{
    public class FornecedorController : Controller
    {
        [HttpPost]
        public IActionResult Salvar(string Nome, string Preco, string Imposto, string Desconto)
        {
            var produto = new ProdutoFake
            {
                Id = DadosFake.Produtos.Count + 1,
                Nome = Nome,
                Preco = decimal.Parse(Preco, CultureInfo.InvariantCulture),
                Imposto = decimal.Parse(Imposto.Replace(",", "."), CultureInfo.InvariantCulture),
                Desconto = decimal.Parse(Desconto.Replace(",", "."), CultureInfo.InvariantCulture)
            };

            DadosFake.Produtos.Add(produto);

            return RedirectToAction("Fornecedor", "Home");
        }
    }
}