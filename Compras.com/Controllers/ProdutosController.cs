using Microsoft.AspNetCore.Mvc;
using Compras.com.Data;
using Compras.com.Models;
using System.Linq;

namespace Compras.com.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ LISTA DE PRODUTOS
        public IActionResult Index()
        {
            var tipo = HttpContext.Session.GetString("UsuarioTipo");
            var email = HttpContext.Session.GetString("UsuarioEmail");

            // 👉 FORNECEDOR vê só os produtos dele
            if (tipo == "Fornecedor")
            {
                var produtos = _context.Produtos
                    .Where(p => p.EmailFornecedor == email)
                    .ToList();

                return View(produtos);
            }

            // 👉 Admin e Comprador veem tudo
            return View(_context.Produtos.ToList());
        }

        // ✅ CREATE
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Produto produto)
        {
            var email = HttpContext.Session.GetString("UsuarioEmail");

            // 👉 vincula produto ao fornecedor logado
            produto.EmailFornecedor = email;

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // ✅ BUSCAR (Comprador)
        public IActionResult Buscar(string termo)
        {
            var produtos = _context.Produtos.AsQueryable();

            if (!string.IsNullOrEmpty(termo))
            {
                produtos = produtos.Where(p => p.Nome.Contains(termo));
            }

            return View(produtos.ToList());
        }

        // ✅ MAIS BARATO
        public IActionResult MaisBarato()
        {
            var produtos = _context.Produtos
                .GroupBy(p => p.Nome)
                .Select(g => g.OrderBy(p => p.PrecoUnitario).First())
                .ToList();

            return View(produtos);
        }

        // ✅ EQUALIZAÇÃO (BASE)
        public IActionResult Equalizacao(string termo)
        {
          var produtos = _context.Produtos.AsQueryable();

          if (!string.IsNullOrEmpty(termo))
         {
         produtos = produtos.Where(p => p.Nome.Contains(termo));
         }

          var lista = produtos.ToList();

          var fornecedores = lista
           .Select(p => p.EmailFornecedor)
           .Distinct()
           .ToList();

          var produtosAgrupados = lista
           .GroupBy(p => p.Nome)
           .ToList();

          ViewBag.Fornecedores = fornecedores;

         return View(produtosAgrupados);
        }
    }
}