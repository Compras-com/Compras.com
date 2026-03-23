using Microsoft.AspNetCore.Mvc;
using Compras.com.Models;
using System.Collections.Generic;

namespace Compras.com.Controllers
{
    public class UsuariosController : Controller
    {
        private static List<Usuario> usuarios = new List<Usuario>();

        public IActionResult Index()
        {
            return View(usuarios);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Usuario usuario)
        {
            usuario.Id = usuarios.Count + 1;
            usuarios.Add(usuario);

            return RedirectToAction("Index");
        }
    }
}