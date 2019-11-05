using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFazendaSerrana.Migrations
{
    public partial class MIGRATRION : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_AGROTOXICO",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    QtdDisponivel = table.Column<int>(nullable: false),
                    UnidadeMedida = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_AGROTOXICO", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "TB_CULTURA",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    TempoMaximo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MesIdeal = table.Column<string>(nullable: true),
                    MesFinal = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CULTURA", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "TB_FUNCIONARIO",
                columns: table => new
                {
                    Matricula = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Cargo = table.Column<string>(nullable: true),
                    DataAdmissao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_FUNCIONARIO", x => x.Matricula);
                });

            migrationBuilder.CreateTable(
                name: "TB_PRAGA",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NomeCientifico = table.Column<string>(nullable: true),
                    NomePopular = table.Column<string>(nullable: true),
                    EstacaoAno = table.Column<string>(nullable: true),
                    DataUltimaPraga = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PRAGA", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "TB_AREAPLANTIO",
                columns: table => new
                {
                    Numero = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Tamanho = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: false),
                    IdMatricula = table.Column<int>(nullable: false),
                    DataPlantio = table.Column<DateTime>(nullable: false),
                    CodigoCultura = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_AREAPLANTIO", x => x.Numero);
                    table.ForeignKey(
                        name: "FK_TB_AREAPLANTIO_TB_CULTURA_CodigoCultura",
                        column: x => x.CodigoCultura,
                        principalTable: "TB_CULTURA",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_AREAPLANTIO_TB_FUNCIONARIO_IdMatricula",
                        column: x => x.IdMatricula,
                        principalTable: "TB_FUNCIONARIO",
                        principalColumn: "Matricula",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_AUXILIAR_AGROTOXICO",
                columns: table => new
                {
                    CodigoAgrotoxico = table.Column<int>(nullable: false),
                    CodigoPraga = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_AUXILIAR_AGROTOXICO", x => new { x.CodigoAgrotoxico, x.CodigoPraga });
                    table.ForeignKey(
                        name: "FK_TB_AUXILIAR_AGROTOXICO_TB_AGROTOXICO_CodigoAgrotoxico",
                        column: x => x.CodigoAgrotoxico,
                        principalTable: "TB_AGROTOXICO",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_AUXILIAR_AGROTOXICO_TB_PRAGA_CodigoPraga",
                        column: x => x.CodigoPraga,
                        principalTable: "TB_PRAGA",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_TIPOCULTURA",
                columns: table => new
                {
                    CodigoCultura = table.Column<int>(nullable: false),
                    CodigoPraga = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TIPOCULTURA", x => new { x.CodigoCultura, x.CodigoPraga });
                    table.ForeignKey(
                        name: "FK_TB_TIPOCULTURA_TB_CULTURA_CodigoCultura",
                        column: x => x.CodigoCultura,
                        principalTable: "TB_CULTURA",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_TIPOCULTURA_TB_PRAGA_CodigoPraga",
                        column: x => x.CodigoPraga,
                        principalTable: "TB_PRAGA",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_APLICAR_AGROTOXICO",
                columns: table => new
                {
                    NumeroAreaPlantio = table.Column<int>(nullable: false),
                    CodigoAgrotoxico = table.Column<int>(nullable: false),
                    QtdAplicado = table.Column<int>(nullable: false),
                    Tipo = table.Column<string>(nullable: true),
                    CodigoPraga = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_APLICAR_AGROTOXICO", x => new { x.NumeroAreaPlantio, x.CodigoAgrotoxico });
                    table.ForeignKey(
                        name: "FK_TB_APLICAR_AGROTOXICO_TB_AGROTOXICO_CodigoAgrotoxico",
                        column: x => x.CodigoAgrotoxico,
                        principalTable: "TB_AGROTOXICO",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_APLICAR_AGROTOXICO_TB_PRAGA_CodigoPraga",
                        column: x => x.CodigoPraga,
                        principalTable: "TB_PRAGA",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_APLICAR_AGROTOXICO_TB_AREAPLANTIO_NumeroAreaPlantio",
                        column: x => x.NumeroAreaPlantio,
                        principalTable: "TB_AREAPLANTIO",
                        principalColumn: "Numero",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_APLICAR_AGROTOXICO_CodigoAgrotoxico",
                table: "TB_APLICAR_AGROTOXICO",
                column: "CodigoAgrotoxico");

            migrationBuilder.CreateIndex(
                name: "IX_TB_APLICAR_AGROTOXICO_CodigoPraga",
                table: "TB_APLICAR_AGROTOXICO",
                column: "CodigoPraga");

            migrationBuilder.CreateIndex(
                name: "IX_TB_AREAPLANTIO_CodigoCultura",
                table: "TB_AREAPLANTIO",
                column: "CodigoCultura");

            migrationBuilder.CreateIndex(
                name: "IX_TB_AREAPLANTIO_IdMatricula",
                table: "TB_AREAPLANTIO",
                column: "IdMatricula");

            migrationBuilder.CreateIndex(
                name: "IX_TB_AUXILIAR_AGROTOXICO_CodigoPraga",
                table: "TB_AUXILIAR_AGROTOXICO",
                column: "CodigoPraga");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TIPOCULTURA_CodigoPraga",
                table: "TB_TIPOCULTURA",
                column: "CodigoPraga");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_APLICAR_AGROTOXICO");

            migrationBuilder.DropTable(
                name: "TB_AUXILIAR_AGROTOXICO");

            migrationBuilder.DropTable(
                name: "TB_TIPOCULTURA");

            migrationBuilder.DropTable(
                name: "TB_AREAPLANTIO");

            migrationBuilder.DropTable(
                name: "TB_AGROTOXICO");

            migrationBuilder.DropTable(
                name: "TB_PRAGA");

            migrationBuilder.DropTable(
                name: "TB_CULTURA");

            migrationBuilder.DropTable(
                name: "TB_FUNCIONARIO");
        }
    }
}
