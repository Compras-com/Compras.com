using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Compras.com.Data;
using Compras.com.Models;
using System.Linq;

namespace Compras.com.Controllers
{
    public class ContaController : Controller
    {
        private readonly AppDbContext _context; // 🔥 INJEÇÃO

        public ContaController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AlterarSenha(string novaSenha)
        {
            var email = HttpContext.Session.GetString("email");

            // 🔥 ADMIN
            if (email == AdminConfig.Email)
            {
                AdminConfig.Senha = novaSenha;
                return RedirectToAction("Index", "Admin");
            }

            // 🔽 USUÁRIO REAL DO BANCO
            var user = _context.Usuarios.FirstOrDefault(u => u.Email == email);

            if (user != null)
            {
                user.Senha = novaSenha;
                _context.SaveChanges(); // 🔥 SALVA NO BANCO
            }

            return RedirectToAction("Index", "Login");
        }
    }
}
