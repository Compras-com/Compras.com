using Compras.com.Data;
using Compras.com.Models;
using System.Linq;

namespace Compras.com.Services
{
    public class UsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 LOGIN
        public Usuario? Login(string email, string senha)
        {
            return _context.Usuarios
                .FirstOrDefault(u => u.Email == email 
                                  && u.Senha == senha 
                                  && u.Ativo);
        }

        // 🔹 LISTAR TODOS
        public List<Usuario> GetAll()
        {
            return _context.Usuarios.ToList();
        }

        // 🔹 BUSCAR POR ID
        public Usuario? GetById(int id)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Id == id);
        }

        // 🔹 ADICIONAR
        public void Add(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        // 🔹 ATUALIZAR
        public void Update(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }

        // 🔹 EXCLUIR
        public void Delete(int id)
        {
            var usuario = _context.Usuarios.Find(id);

            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
            }
        }
    }
}