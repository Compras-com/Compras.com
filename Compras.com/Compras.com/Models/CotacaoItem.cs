using System.ComponentModel.DataAnnotations;

namespace Compras.com.Models
{
    public class CotacaoItem
    {
        [Required]
        public string NomeProduto { get; set; } = string.Empty;

        public decimal MelhorPreco { get; set; }

        [Required]
        public string MelhorFornecedor { get; set; } = string.Empty;

        // 🔥 OPCIONAL (AJUDA NO SISTEMA)
        public int ProdutoId { get; set; }
    }
}