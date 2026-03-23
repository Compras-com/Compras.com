using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Compras.com.Data;
using System.Linq;

namespace Compras.com.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View(DadosFake.Usuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(UsuarioFake usuario)
        {
            usuario.Id = DadosFake.Usuarios.Count + 1;
            DadosFake.Usuarios.Add(usuario);

            return RedirectToAction("Index");
        }

        // 👁️ VISUALIZAR
        public IActionResult Detalhes(int id)
        {
            var user = DadosFake.Usuarios.FirstOrDefault(x => x.Id == id);
            return View(user);
        }

        // 🔒 BLOQUEAR
        public IActionResult Bloquear(int id)
        {
            var user = DadosFake.Usuarios.FirstOrDefault(x => x.Id == id);
            if (user != null)
                user.Ativo = false;

            return RedirectToAction("Index");
        }

        // 🗑️ EXCLUIR
        public IActionResult Excluir(int id)
        {
            var user = DadosFake.Usuarios.FirstOrDefault(x => x.Id == id);
            if (user != null)
                DadosFake.Usuarios.Remove(user);

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