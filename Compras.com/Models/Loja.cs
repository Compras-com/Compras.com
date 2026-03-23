using System.ComponentModel.DataAnnotations;

namespace Compras.com.Models
{
    public class Loja
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public int FornecedorId { get; set; }
    }
}