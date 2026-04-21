using System.ComponentModel.DataAnnotations;

namespace Compras.com.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        
        [Required] 
        public string Nome { get; set; } = string.Empty;
        
        public decimal Preco { get; set; }
        
        public decimal Ipi { get; set; }
        
        public decimal Desconto { get; set; }
        
        public decimal Frete { get; set; }
        
        // Campo adicionado para corrigir o erro CS1061 no Index.cshtml
        public string EmailFornecedor { get; set; } = string.Empty;
        
        public int FornecedorId { get; set; }
        
        public Usuario? Fornecedor { get; set; }
    }
}