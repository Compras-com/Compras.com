using System.ComponentModel.DataAnnotations;

namespace Compras.com.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        [Required] public string Nome { get; set; }
        [Required] public decimal Preco { get; set; }
        public decimal Ipi { get; set; }
        public decimal Desconto { get; set; }
        public decimal Frete { get; set; }
        
        public int FornecedorId { get; set; }
        public Usuario? Fornecedor { get; set; }
    }
}