using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Compras.com.Data;
using System.Linq;

namespace Compras.com.Controllers
{
    public class ContaController : Controller
    {
        public IActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AlterarSenha(string novaSenha)
        {
            var email = HttpContext.Session.GetString("email");

            // 🔥 SE FOR ADMIN
            if (email == AdminConfig.Email)
            {
                AdminConfig.Senha = novaSenha;
                return RedirectToAction("Index", "Admin");
            }

            // 🔽 USUÁRIOS NORMAIS
            var user = DadosFake.Usuarios.FirstOrDefault(u => u.Email == email);

            if (user != null)
                user.Senha = novaSenha;

            return RedirectToAction("Index", "Login");
        }
    }
}