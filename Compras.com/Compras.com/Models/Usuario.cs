using System.ComponentModel.DataAnnotations;

namespace Compras.com.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Senha { get; set; } = string.Empty;

        [Required]
        public string Tipo { get; set; } = string.Empty; // Admin / Fornecedor / Comprador

        public bool Ativo { get; set; } = true;

        // 🔥 CAMPOS USADOS NAS VIEWS
        public string Documento { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Contato { get; set; } = string.Empty;
    }
}