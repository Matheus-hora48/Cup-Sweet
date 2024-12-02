using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CupcakeStoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarEstadoEndereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacao_AspNetUsers_UserId",
                table: "Avaliacao");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Enderecos",
                type: "character varying(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacao_AspNetUsers_UserId",
                table: "Avaliacao",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacao_AspNetUsers_UserId",
                table: "Avaliacao");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Enderecos");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacao_AspNetUsers_UserId",
                table: "Avaliacao",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
