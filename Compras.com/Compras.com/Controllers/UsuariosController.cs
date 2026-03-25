using Microsoft.AspNetCore.Mvc;
using Compras.com.Models;
using Compras.com.Data;
using System.Linq;

namespace Compras.com.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly AppDbContext _context; // 🔥 BANCO REAL

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ LISTAR
        public IActionResult Index()
        {
            var usuarios = _context.Usuarios.ToList();
            return View(usuarios);
        }

        // ✅ FORM
        public IActionResult Create()
        {
            return View();
        }

        // ✅ SALVAR
        [HttpPost]
        public IActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Usuarios.Add(usuario); // 🔥 BANCO
                _context.SaveChanges();         // 🔥 SALVA

                return RedirectToAction("Index");
            }

            return View(usuario);
        }
    }
}