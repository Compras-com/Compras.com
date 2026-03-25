using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Compras.com.Data;
using Compras.com.Models;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Compras.com.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var tipo = HttpContext.Session.GetString("tipo");
            var email = HttpContext.Session.GetString("email");

            if (tipo == "Fornecedor")
            {
                return View(_context.Produtos.Where(p => p.EmailFornecedor == email).ToList());
            }
            return View(new List<Produto>());
        }

        [HttpPost]
        public IActionResult Create(Produto produto)
        {
            var emailLogado = HttpContext.Session.GetString("email");
            if (string.IsNullOrEmpty(emailLogado)) return RedirectToAction("Index", "Login");

            produto.EmailFornecedor = emailLogado;
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // --- NOVAS FUNÇÕES: EDITAR E EXCLUIR ---

        public IActionResult Edit(int id)
        {
            var emailLogado = HttpContext.Session.GetString("email");
            var produto = _context.Produtos.FirstOrDefault(p => p.Id == id && p.EmailFornecedor == emailLogado);
            
            if (produto == null) return RedirectToAction("Index");
            return View(produto);
        }

        [HttpPost]
        public IActionResult Atualizar(Produto produto)
        {
            var emailLogado = HttpContext.Session.GetString("email");
            var produtoBanco = _context.Produtos.FirstOrDefault(p => p.Id == produto.Id && p.EmailFornecedor == emailLogado);

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
            var emailLogado = HttpContext.Session.GetString("email");
            var produto = _context.Produtos.FirstOrDefault(p => p.Id == id && p.EmailFornecedor == emailLogado);

            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // --- BUSCA E EQUALIZAÇÃO ---

        public IActionResult Buscar(string termo)
        {
            var lista = _context.Produtos
                .Where(p => string.IsNullOrEmpty(termo) || p.Nome.Contains(termo))
                .ToList();
            return View("Index", lista);
        }

        public IActionResult Equalizacao(string termo)
        {
            var listaGeral = _context.Produtos.Where(p => !string.IsNullOrEmpty(p.EmailFornecedor)).ToList();
            if (!string.IsNullOrEmpty(termo)) listaGeral = listaGeral.Where(p => p.Nome.Contains(termo)).ToList();

            ViewBag.Fornecedores = listaGeral.Select(p => p.EmailFornecedor).Distinct().ToList();
            return View(listaGeral.GroupBy(p => p.Nome).ToList());
        }

        public IActionResult SelecionarProduto(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);
            if (produto == null) return RedirectToAction("Equalizacao");
            var emailComprador = HttpContext.Session.GetString("email");
            string assunto = Uri.EscapeDataString($"Pedido: {produto.Nome}");
            string corpo = Uri.EscapeDataString($"Produto: {produto.Nome}\nFornecedor: {produto.EmailFornecedor}\nPreço: {produto.Preco:C}");
            return Redirect($"mailto:{emailComprador}?subject={assunto}&body={corpo}");
        }
    }
}