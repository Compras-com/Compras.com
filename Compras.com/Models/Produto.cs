using System.ComponentModel.DataAnnotations;

namespace Compras.com.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public decimal PrecoUnitario { get; set; }

        public bool ComImposto { get; set; }

        public string EmailFornecedor { get; set; }
    }
}