using System.Collections.Generic;

namespace Compras.com.Data
{
    public static class DadosFake
    {
        public static List<UsuarioFake> Usuarios = new List<UsuarioFake>();
        public static List<ProdutoFake> Produtos = new List<ProdutoFake>();
    }

    public class UsuarioFake
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; } // Admin / Fornecedor / Comprador
        public string Email { get; set; }
        public string Senha { get; set; } // 🔥 NOVO
        public string Documento { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string Contato { get; set; }
        public bool Ativo { get; set; } = true;
    }

    public class ProdutoFake
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public decimal Imposto { get; set; }
        public decimal Desconto { get; set; }
        public int FornecedorId { get; set; }
    }
}