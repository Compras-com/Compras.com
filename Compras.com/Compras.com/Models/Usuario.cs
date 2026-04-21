using System.ComponentModel.DataAnnotations;

namespace Compras.com.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required] public string Nome { get; set; }
        [Required] public string Login { get; set; }
        [Required] public string Senha { get; set; }
        public string? Email { get; set; }
        public string Tipo { get; set; } // Admin, Fornecedor, Comprador
        public bool Ativo { get; set; } = true;
        public string? CpfCnpj { get; set; }
    }
}