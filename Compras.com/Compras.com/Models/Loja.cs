using System.ComponentModel.DataAnnotations;

namespace Compras.com.Models
{
    public class Loja
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty; // 🔥 evita null

        public int FornecedorId { get; set; }

        // 🔥 RELACIONAMENTO (opcional, mas recomendado)
        public Usuario? Fornecedor { get; set; }
    }
}