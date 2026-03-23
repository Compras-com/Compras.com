using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Compras.com.Data;
using System.Linq;

namespace Compras.com.Controllers
{
    public class LoginController : Controller
    {
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

            // 🔽 USUÁRIOS CADASTRADOS
            var usuario = DadosFake.Usuarios
                .FirstOrDefault(u => u.Email == email && u.Senha == senha && u.Ativo);

            if (usuario == null)
            {
                ViewBag.Erro = "Usuário ou senha inválidos";
                return View("Index");
            }

            HttpContext.Session.SetString("tipo", usuario.Tipo);
            HttpContext.Session.SetString("email", usuario.Email);

            if (usuario.Tipo == "Fornecedor")
                return RedirectToAction("Fornecedor", "Home");

            return RedirectToAction("Comprador", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}