using Microsoft.EntityFrameworkCore;
using Compras.com.Models;

namespace Compras.com.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cotacao> Cotacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Nome).IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Email).IsRequired();

            modelBuilder.Entity<Produto>()
                .Property(p => p.Nome).IsRequired();

            // 🔴 PostgreSQL correto
            modelBuilder.Entity<Produto>()
                .Property(p => p.Preco)
                .HasColumnType("numeric(18,2)");
        }
    }
}