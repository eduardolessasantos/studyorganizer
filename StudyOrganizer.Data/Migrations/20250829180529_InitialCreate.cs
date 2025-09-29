using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudyOrganizer.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Disciplines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disciplines_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subtopics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DisciplineId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasteryLevel = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subtopics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subtopics_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudySessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubtopicId = table.Column<int>(type: "int", nullable: false),
                    SessionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudySessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudySessions_Subtopics_SubtopicId",
                        column: x => x.SubtopicId,
                        principalTable: "Subtopics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Conhecimentos Gerais" },
                    { 2, "Conhecimentos Específicos" }
                });

            migrationBuilder.InsertData(
                table: "Disciplines",
                columns: new[] { "Id", "ModuleId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Língua Portuguesa" },
                    { 2, 1, "Raciocínio Lógico" },
                    { 3, 1, "Informática" },
                    { 4, 1, "Legislação" },
                    { 5, 2, "Engenharia de Software" },
                    { 6, 2, "Banco de Dados" },
                    { 7, 2, "Redes de Computadores" }
                });

            migrationBuilder.InsertData(
                table: "Subtopics",
                columns: new[] { "Id", "Description", "DisciplineId", "EndDate", "MasteryLevel", "MaterialUrl", "Notes", "StartDate", "Status" },
                values: new object[,]
                {
                    { 1, "Interpretação de textos", 1, null, null, null, null, null, 0 },
                    { 2, "Ortografia oficial", 1, null, null, null, null, null, 0 },
                    { 3, "Acentuação gráfica", 1, null, null, null, null, null, 0 },
                    { 4, "Proposições, conectivos e tabelas-verdade", 2, null, null, null, null, null, 0 },
                    { 5, "Problemas de lógica e contagem", 2, null, null, null, null, null, 0 },
                    { 6, "Conceitos de sistemas operacionais", 3, null, null, null, null, null, 0 },
                    { 7, "Segurança da informação", 3, null, null, null, null, null, 0 },
                    { 8, "Constituição Federal - Artigos 37 a 41", 4, null, null, null, null, null, 0 },
                    { 9, "Lei de Acesso à Informação (Lei 12.527/2011)", 4, null, null, null, null, null, 0 },
                    { 10, "Ciclo de vida de software", 5, null, null, null, null, null, 0 },
                    { 11, "UML - Casos de uso, diagramas de classes e sequência", 5, null, null, null, null, null, 0 },
                    { 12, "Modelo relacional e normalização", 6, null, null, null, null, null, 0 },
                    { 13, "SQL - comandos DDL, DML, DCL", 6, null, null, null, null, null, 0 },
                    { 14, "Modelo OSI e TCP/IP", 7, null, null, null, null, null, 0 },
                    { 15, "Endereçamento IPv4 e IPv6", 7, null, null, null, null, null, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_ModuleId",
                table: "Disciplines",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_StudySessions_SubtopicId",
                table: "StudySessions",
                column: "SubtopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Subtopics_DisciplineId",
                table: "Subtopics",
                column: "DisciplineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudySessions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Subtopics");

            migrationBuilder.DropTable(
                name: "Disciplines");

            migrationBuilder.DropTable(
                name: "Modules");
        }
    }
}
