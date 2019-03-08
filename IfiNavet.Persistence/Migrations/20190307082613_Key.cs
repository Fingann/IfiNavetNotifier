using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IfiNavet.Persistence.Migrations
{
    public partial class Key : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IfiEvents",
                table: "IfiEvents");

            migrationBuilder.DeleteData(
                table: "UserLogins",
                keyColumn: "Id",
                keyValue: "8ca276ca-cd53-4233-9805-dadbf08535ac");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "IfiEvents");

            migrationBuilder.AlterColumn<string>(
                name: "Link",
                table: "IfiEvents",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IfiEvents",
                table: "IfiEvents",
                column: "Link");

            migrationBuilder.InsertData(
                table: "UserLogins",
                columns: new[] { "Id", "ModifiedDate", "Password", "Username" },
                values: new object[] { "c4c833a3-fa25-419b-8b0e-c7c952d70fc3", null, "ifibot123", "sodnrefi" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IfiEvents",
                table: "IfiEvents");

            migrationBuilder.DeleteData(
                table: "UserLogins",
                keyColumn: "Id",
                keyValue: "c4c833a3-fa25-419b-8b0e-c7c952d70fc3");

            migrationBuilder.AlterColumn<string>(
                name: "Link",
                table: "IfiEvents",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "IfiEvents",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IfiEvents",
                table: "IfiEvents",
                column: "Id");

            migrationBuilder.InsertData(
                table: "UserLogins",
                columns: new[] { "Id", "ModifiedDate", "Password", "Username" },
                values: new object[] { "8ca276ca-cd53-4233-9805-dadbf08535ac", null, "ifibot123", "sodnrefi" });
        }
    }
}
