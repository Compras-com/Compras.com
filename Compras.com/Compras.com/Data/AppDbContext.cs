using Compras.com.Models;
using Microsoft.EntityFrameworkCore;

namespace Compras.com.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Estas são as tabelas que serão criadas no seu banco pago
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
    }
}