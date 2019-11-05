using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFazendaSerrana.Migrations
{
    public partial class MIGRATION3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_APLICAR_AGROTOXICO",
                table: "TB_APLICAR_AGROTOXICO");

            migrationBuilder.AddColumn<int>(
                name: "IdAplicarAgrotoxico",
                table: "TB_APLICAR_AGROTOXICO",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_APLICAR_AGROTOXICO",
                table: "TB_APLICAR_AGROTOXICO",
                column: "IdAplicarAgrotoxico");

            migrationBuilder.CreateIndex(
                name: "IX_TB_APLICAR_AGROTOXICO_NumeroAreaPlantio",
                table: "TB_APLICAR_AGROTOXICO",
                column: "NumeroAreaPlantio");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_APLICAR_AGROTOXICO",
                table: "TB_APLICAR_AGROTOXICO");

            migrationBuilder.DropIndex(
                name: "IX_TB_APLICAR_AGROTOXICO_NumeroAreaPlantio",
                table: "TB_APLICAR_AGROTOXICO");

            migrationBuilder.DropColumn(
                name: "IdAplicarAgrotoxico",
                table: "TB_APLICAR_AGROTOXICO");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_APLICAR_AGROTOXICO",
                table: "TB_APLICAR_AGROTOXICO",
                column: "NumeroAreaPlantio");
        }
    }
}
