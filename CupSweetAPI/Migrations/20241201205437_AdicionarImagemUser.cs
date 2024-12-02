using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CupcakeStoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarImagemUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagemUrl",
                table: "AspNetUsers",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemUrl",
                table: "AspNetUsers");
        }
    }
}
