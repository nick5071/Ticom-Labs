using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laboratorio_projeto.Migrations
{
    /// <inheritdoc />
    public partial class migrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tabela Laboratorio",
                columns: table => new
                {
                    CPF = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Plano = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Convenio = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tabela Laboratorio", x => x.CPF);
                });

            migrationBuilder.CreateTable(
                name: "ExamesTabela2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Exames = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamesTabela2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamesTabela2_Tabela Laboratorio_CPF",
                        column: x => x.CPF,
                        principalTable: "Tabela Laboratorio",
                        principalColumn: "CPF",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamesTabela2_CPF",
                table: "ExamesTabela2",
                column: "CPF");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamesTabela2");

            migrationBuilder.DropTable(
                name: "Tabela Laboratorio");
        }
    }
}
