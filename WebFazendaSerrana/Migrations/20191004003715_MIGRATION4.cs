using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFazendaSerrana.Migrations
{
    public partial class MIGRATION4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CulturaCodigo",
                table: "TB_PRAGA",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_PRAGA_CulturaCodigo",
                table: "TB_PRAGA",
                column: "CulturaCodigo");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_PRAGA_TB_CULTURA_CulturaCodigo",
                table: "TB_PRAGA",
                column: "CulturaCodigo",
                principalTable: "TB_CULTURA",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_PRAGA_TB_CULTURA_CulturaCodigo",
                table: "TB_PRAGA");

            migrationBuilder.DropIndex(
                name: "IX_TB_PRAGA_CulturaCodigo",
                table: "TB_PRAGA");

            migrationBuilder.DropColumn(
                name: "CulturaCodigo",
                table: "TB_PRAGA");
        }
    }
}
