using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Compras.com.Data;
using Compras.com.Models;
using System.Linq;

namespace Compras.com.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // 🛡️ Segurança extra para formulários
        public IActionResult Entrar(string email, string senha)
        {
            // 🔐 LOGIN DO ADMIN (ESTÁTICO)
            if (email == "admin@compras.com" && senha == "admin123") 
            {
                HttpContext.Session.SetString("tipo", "Admin");
                HttpContext.Session.SetString("email", email);
                return RedirectToAction("Index", "Admin");
            }

            // 🔽 BUSCA NO BANCO DE DADOS
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Email == email && u.Senha == senha);

            if (usuario == null)
            {
                TempData["Erro"] = "❌ Usuário ou senha inválidos";
                return RedirectToAction("Index");
            }

            // ✅ GRAVA NA SESSÃO
            HttpContext.Session.SetString("tipo", usuario.Tipo);
            HttpContext.Session.SetString("email", usuario.Email);

            // 🚀 REDIRECIONAMENTO
            return RedirectToAction("Index", "Produtos");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}