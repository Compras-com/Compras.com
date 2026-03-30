using Microsoft.AspNetCore.Mvc;
using Compras.com.Data;
using Compras.com.Models;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

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
        public async Task<IActionResult> Entrar(string email, string senha)
        {
            // Admin fixo
            if (email == "admin@compras.com" && senha == "admin123")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, email),
                    new Claim("Tipo", "Admin")
                };

                var identity = new ClaimsIdentity(claims, "Cookies");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("Cookies", principal);

                return RedirectToAction("Index", "Admin");
            }

            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Email == email && u.Senha == senha);

            if (usuario == null)
            {
                ViewBag.Erro = "Usuário ou senha inválidos";
                return View("Index");
            }

            var claimsUser = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Email),
                new Claim("Tipo", usuario.Tipo)
            };

            var identityUser = new ClaimsIdentity(claimsUser, "Cookies");
            var principalUser = new ClaimsPrincipal(identityUser);

            await HttpContext.SignInAsync("Cookies", principalUser);

            return RedirectToAction("Index", "Produtos");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index");
        }
    }
}