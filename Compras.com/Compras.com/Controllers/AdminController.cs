using Microsoft.AspNetCore.Mvc;
using Compras.com.Data;
using Compras.com.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Compras.com.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // 🔒 Verifica se é Admin
        private bool EhAdmin()
        {
            return User.FindFirst("Tipo")?.Value == "Admin";
        }

        public IActionResult Index()
        {
            if (!EhAdmin())
                return RedirectToAction("Index", "Login");

            var usuarios = _context.Usuarios.ToList();
            return View(usuarios);
        }

        public IActionResult Criar()
        {
            if (!EhAdmin())
                return RedirectToAction("Index", "Login");

            return View();
        }

        [HttpPost]
        public IActionResult Criar(Usuario usuario)
        {
            if (!EhAdmin())
                return RedirectToAction("Index", "Login");

            if (ModelState.IsValid)
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        public IActionResult Detalhes(int id)
        {
            if (!EhAdmin())
                return RedirectToAction("Index", "Login");

            var user = _context.Usuarios.FirstOrDefault(x => x.Id == id);

            if (user == null)
                return RedirectToAction("Index");

            return View(user);
        }

        public IActionResult Bloquear(int id)
        {
            if (!EhAdmin())
                return RedirectToAction("Index", "Login");

            var user = _context.Usuarios.FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                user.Ativo = false;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Excluir(int id)
        {
            if (!EhAdmin())
                return RedirectToAction("Index", "Login");

            var user = _context.Usuarios.FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                _context.Usuarios.Remove(user);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}