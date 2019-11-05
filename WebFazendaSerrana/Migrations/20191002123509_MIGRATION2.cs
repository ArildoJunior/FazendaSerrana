using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFazendaSerrana.Migrations
{
    public partial class MIGRATION2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_APLICAR_AGROTOXICO",
                table: "TB_APLICAR_AGROTOXICO");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_APLICAR_AGROTOXICO",
                table: "TB_APLICAR_AGROTOXICO",
                column: "NumeroAreaPlantio");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_APLICAR_AGROTOXICO",
                table: "TB_APLICAR_AGROTOXICO");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_APLICAR_AGROTOXICO",
                table: "TB_APLICAR_AGROTOXICO",
                columns: new[] { "NumeroAreaPlantio", "CodigoAgrotoxico" });
        }
    }
}
