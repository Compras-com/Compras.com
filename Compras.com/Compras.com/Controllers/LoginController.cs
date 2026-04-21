using Microsoft.AspNetCore.Mvc;
using Compras.com.Models;
using Compras.com.Data;
using System.Linq;

namespace Compras.com.Controllers
{
    public class LoginController : Controller
    {
        private readonly BancoContext _context;

        public LoginController(BancoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(string login, string senha)
        {
            // Busca o usuário pelo Login e Senha
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Login == login && u.Senha == senha);

            if (usuario != null)
            {
                // Verifica se o usuário está bloqueado
                if (!usuario.Ativo)
                {
                    TempData["Erro"] = "Sua conta está bloqueada pelo Administrador.";
                    return RedirectToAction("Index");
                }

                // REDIRECIONAMENTO COMPLETO
                if (usuario.Tipo == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                
                if (usuario.Tipo == "Fornecedor")
                {
                    return RedirectToAction("Index", "Fornecedor");
                }

                if (usuario.Tipo == "Comprador")
                {
                    // Envia para o Index do CompradorController 
                    // (que por sua vez abre a sua página na pasta Home)
                    return RedirectToAction("Index", "Comprador");
                }
            }

            // Caso os dados estejam errados
            TempData["Erro"] = "Usuário ou senha inválidos!";
            return RedirectToAction("Index");
        }

        public IActionResult Sair()
        {
            return RedirectToAction("Index");
        }
    }
}