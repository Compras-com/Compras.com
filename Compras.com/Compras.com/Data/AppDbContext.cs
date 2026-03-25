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

        // 🔥 CONFIGURAÇÕES EXTRA (EVITA ERROS)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 Usuario
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Nome)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Email)
                .IsRequired();

            // 🔹 Produto
            modelBuilder.Entity<Produto>()
                .Property(p => p.Nome)
                .IsRequired();

            modelBuilder.Entity<Produto>()
                .Property(p => p.Preco)
                .HasColumnType("decimal(18,2)");
        }
    }
}