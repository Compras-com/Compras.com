using Microsoft.AspNetCore.Mvc;
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

        // --- PORTA SECRETA: COLE ESTE BLOCO AQUI ---
        [HttpGet("/liberar-meu-acesso")]
        public IActionResult LiberarAcesso()
        {
            try {
                var novoAdmin = new Usuario { Nome = "Admin", Email = "admin@admin.com", Senha = "123" };
                _context.Usuarios.Add(novoAdmin);
                _context.SaveChanges();
                return Content("SUCESSO! O usuário Admin (admin@admin.com / 123) foi criado no banco pago.");
            } catch (System.Exception ex) {
                return Content("Erro: " + ex.Message);
            }
        }
        // -------------------------------------------

        public IActionResult Index()
        {
            return View();
        }

        // ... resto do seu código de LoginPost abaixo ...
    }
}