using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FIAP.IRRIGACAO.API.Migrations
{
    /// <inheritdoc />
    public partial class FaucetIsEnabledString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IsEnabled",
                table: "Faucet",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "NUMBER(1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsEnabled",
                table: "Faucet",
                type: "NUMBER(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");
        }
    }
}
