using Compras.com.Data;
using Compras.com.Models;
using Microsoft.EntityFrameworkCore;

namespace Compras.com.Services
{
    public class ProdutoService
    {
        private readonly AppDbContext _context;

        public ProdutoService(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 LISTAR TODOS
        public List<Produto> GetAll()
        {
            return _context.Produtos.ToList();
        }

        // 🔹 BUSCAR POR ID
        public Produto? GetById(int id)
        {
            return _context.Produtos.FirstOrDefault(p => p.Id == id);
        }

        // 🔹 ADICIONAR
        public void Add(Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();
        }

        // 🔹 ATUALIZAR
        public void Update(Produto produto)
        {
            _context.Produtos.Update(produto);
            _context.SaveChanges();
        }

        // 🔹 DELETAR
        public void Delete(int id)
        {
            var produto = _context.Produtos.Find(id);

            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                _context.SaveChanges();
            }
        }
    }
}