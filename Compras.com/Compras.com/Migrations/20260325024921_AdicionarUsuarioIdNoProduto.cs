using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Compras.com.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarUsuarioIdNoProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Produtos",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Produtos");
        }
    }
}
