using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudyOrganizer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddContentToSubtopic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Subtopics",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subtopics",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subtopics",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Subtopics",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Subtopics",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Subtopics",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Subtopics",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Subtopics",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Subtopics",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Subtopics",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Subtopics",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Subtopics",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Subtopics",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Subtopics",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Subtopics",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Disciplines",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Disciplines",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Disciplines",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Disciplines",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Disciplines",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Disciplines",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Disciplines",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Subtopics",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Subtopics");

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
        }
    }
}
