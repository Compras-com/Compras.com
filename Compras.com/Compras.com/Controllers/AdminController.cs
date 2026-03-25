using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Compras.com.Data;
using Compras.com.Models;
using System.Linq;

namespace Compras.com.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context; // 🔥 INJEÇÃO

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var usuarios = _context.Usuarios.ToList();
            return View(usuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Usuarios.Add(usuario); // 🔥 BANCO REAL
                _context.SaveChanges();         // 🔥 SALVA

                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        // 👁️ VISUALIZAR
        public IActionResult Detalhes(int id)
        {
            var user = _context.Usuarios.FirstOrDefault(x => x.Id == id);
            return View(user);
        }

        // 🔒 BLOQUEAR
        public IActionResult Bloquear(int id)
        {
            var user = _context.Usuarios.FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                user.Ativo = false; // ⚠️ precisa existir no model
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // 🗑️ EXCLUIR
        public IActionResult Excluir(int id)
        {
            var user = _context.Usuarios.FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                _context.Usuarios.Remove(user);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // 🔥 ENTRAR COMO FORNECEDOR
        public IActionResult EntrarComoFornecedor()
        {
            HttpContext.Session.SetString("tipo", "Fornecedor");
            return RedirectToAction("Fornecedor", "Home");
        }

        // 🔥 ENTRAR COMO COMPRADOR
        public IActionResult EntrarComoComprador()
        {
            HttpContext.Session.SetString("tipo", "Comprador");
            return RedirectToAction("Comprador", "Home");
        }
    }
}