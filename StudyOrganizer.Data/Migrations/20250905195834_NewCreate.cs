using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudyOrganizer.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "Id", "Content", "Description", "DisciplineId", "EndDate", "MasteryLevel", "MaterialUrl", "Notes", "StartDate", "Status" },
                values: new object[,]
                {
                    { 1, null, "Interpretação de textos", 1, null, null, null, null, null, 0 },
                    { 2, null, "Ortografia oficial", 1, null, null, null, null, null, 0 },
                    { 3, null, "Acentuação gráfica", 1, null, null, null, null, null, 0 },
                    { 4, null, "Proposições, conectivos e tabelas-verdade", 2, null, null, null, null, null, 0 },
                    { 5, null, "Problemas de lógica e contagem", 2, null, null, null, null, null, 0 },
                    { 6, null, "Conceitos de sistemas operacionais", 3, null, null, null, null, null, 0 },
                    { 7, null, "Segurança da informação", 3, null, null, null, null, null, 0 },
                    { 8, null, "Constituição Federal - Artigos 37 a 41", 4, null, null, null, null, null, 0 },
                    { 9, null, "Lei de Acesso à Informação (Lei 12.527/2011)", 4, null, null, null, null, null, 0 },
                    { 10, null, "Ciclo de vida de software", 5, null, null, null, null, null, 0 },
                    { 11, null, "UML - Casos de uso, diagramas de classes e sequência", 5, null, null, null, null, null, 0 },
                    { 12, null, "Modelo relacional e normalização", 6, null, null, null, null, null, 0 },
                    { 13, null, "SQL - comandos DDL, DML, DCL", 6, null, null, null, null, null, 0 },
                    { 14, null, "Modelo OSI e TCP/IP", 7, null, null, null, null, null, 0 },
                    { 15, null, "Endereçamento IPv4 e IPv6", 7, null, null, null, null, null, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
