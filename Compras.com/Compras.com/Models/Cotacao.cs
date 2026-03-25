using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Compras.com.Models
{
    public class Cotacao
    {
        public int Id { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        [Required]
        public int CompradorId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecoSelecionado { get; set; }

        // 🔥 RELACIONAMENTOS (FUTURO)
        public Produto? Produto { get; set; }
        public Usuario? Comprador { get; set; }
    }
}