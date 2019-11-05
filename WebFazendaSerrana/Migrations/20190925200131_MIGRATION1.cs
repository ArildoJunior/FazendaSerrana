using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFazendaSerrana.Migrations
{
    public partial class MIGRATION1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nvarchar(15)",
                table: "TB_AREAPLANTIO",
                newName: "Status");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "TB_AREAPLANTIO",
                type: "nvarchar(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "TB_AREAPLANTIO",
                newName: "nvarchar(15)");

            migrationBuilder.AlterColumn<string>(
                name: "nvarchar(15)",
                table: "TB_AREAPLANTIO",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)");
        }
    }
}
