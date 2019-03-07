using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IfiNavet.Persistence.Migrations
{
    public partial class InitialCraetion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IfiEvents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Food = table.Column<string>(nullable: true),
                    PlacesLeft = table.Column<int>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Open = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IfiEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "UserLogins",
                columns: new[] { "Id", "ModifiedDate", "Password", "Username" },
                values: new object[] { "8ca276ca-cd53-4233-9805-dadbf08535ac", null, "ifibot123", "sodnrefi" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IfiEvents");

            migrationBuilder.DropTable(
                name: "UserLogins");
        }
    }
}
