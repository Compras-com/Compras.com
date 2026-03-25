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

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(string email, string senha)
        {
            // 🔐 ADMIN DINÂMICO
            if (email == AdminConfig.Email && senha == AdminConfig.Senha)
            {
                HttpContext.Session.SetString("tipo", "Admin");
                HttpContext.Session.SetString("email", email);
                return RedirectToAction("Index", "Admin");
            }

            // 🔽 USUÁRIOS DO BANCO (REAL)
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Email == email && u.Senha == senha);

            if (usuario == null)
            {
                ViewBag.Erro = "❌ Usuário ou senha inválidos";
                return View("Index");
            }

            // ✅ GRAVA NA SESSÃO
            HttpContext.Session.SetString("tipo", usuario.Tipo);
            HttpContext.Session.SetString("email", usuario.Email);

            // 🚀 REDIRECIONAMENTO CORRIGIDO:
            // Agora enviamos ambos para a Index de Produtos, onde a lógica que criamos
            // vai mostrar a tela certa para cada um (Cadastro ou Busca).
            return RedirectToAction("Index", "Produtos");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}