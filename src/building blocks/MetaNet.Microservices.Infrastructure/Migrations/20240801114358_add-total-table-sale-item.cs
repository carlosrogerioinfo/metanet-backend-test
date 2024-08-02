using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetaNet.Microservices.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addtotaltablesaleitem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "SaleItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "SaleItems");
        }
    }
}
