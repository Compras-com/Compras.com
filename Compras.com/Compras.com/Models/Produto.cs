using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Compras.com.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório")]
        public string Nome { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; }

        // ✅ Adicionando os campos que o compilador está cobrando:
        [Column(TypeName = "decimal(18,2)")]
        public decimal Imposto { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Desconto { get; set; }

        public bool ComImposto { get; set; }

        public string? EmailFornecedor { get; set; } 
        
        public int? UsuarioId { get; set; } 
    }
}