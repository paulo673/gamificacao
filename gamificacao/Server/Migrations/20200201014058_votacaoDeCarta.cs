using Microsoft.EntityFrameworkCore.Migrations;

namespace gamificacao.Server.Migrations
{
    public partial class votacaoDeCarta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VotacoesDeCartas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JogadorId = table.Column<int>(nullable: false),
                    CartaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VotacoesDeCartas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VotacoesDeCartas_Cartas_CartaId",
                        column: x => x.CartaId,
                        principalTable: "Cartas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VotacoesDeCartas_Jogadores_JogadorId",
                        column: x => x.JogadorId,
                        principalTable: "Jogadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VotacoesDeCartas_CartaId",
                table: "VotacoesDeCartas",
                column: "CartaId");

            migrationBuilder.CreateIndex(
                name: "IX_VotacoesDeCartas_JogadorId",
                table: "VotacoesDeCartas",
                column: "JogadorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VotacoesDeCartas");
        }
    }
}
