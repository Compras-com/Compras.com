using Microsoft.AspNetCore.Mvc;
using Compras.com.Models;
using Compras.com.Data;
using System.Linq;

namespace Compras.com.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        public AdminController(AppDbContext context) => _context = context;

        // Esta é a sua tela de listagem (onde você bloqueia/exclui)
        public IActionResult Index() => View(_context.Usuarios.ToList());

        [HttpGet]
        public IActionResult Criar(string tipo) { 
            ViewBag.Tipo = tipo; 
            return View(); 
        }

        [HttpPost]
        public IActionResult Salvar(Usuario u) {
            _context.Usuarios.Add(u);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult AlternarStatus(int id) {
            var u = _context.Usuarios.Find(id);
            if (u != null) { u.Ativo = !u.Ativo; _context.SaveChanges(); }
            return RedirectToAction("Index");
        }

        public IActionResult Excluir(int id) {
            var u = _context.Usuarios.Find(id);
            if (u != null) { _context.Usuarios.Remove(u); _context.SaveChanges(); }
            return RedirectToAction("Index");
        }
    }
}