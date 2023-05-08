using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaCadastroEleitoral.Migrations
{
    public partial class AutomatizarCampoDataObservacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Observacoes");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataObservacao",
                table: "Observacoes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataObservacao",
                table: "Observacoes");

            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "Observacoes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
