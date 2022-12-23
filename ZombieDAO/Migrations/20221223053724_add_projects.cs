using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZombieDAO.Migrations
{
    /// <inheritdoc />
    public partial class addprojects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    website = table.Column<string>(type: "text", nullable: false),
                    emailaddress = table.Column<string>(name: "email_address", type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "project_members",
                columns: table => new
                {
                    projectid = table.Column<Guid>(name: "project_id", type: "uuid", nullable: false),
                    userwallet = table.Column<string>(name: "user_wallet", type: "text", nullable: false),
                    level = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_members", x => new { x.projectid, x.userwallet });
                    table.ForeignKey(
                        name: "FK_project_members_projects_project_id",
                        column: x => x.projectid,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_project_members_users_user_wallet",
                        column: x => x.userwallet,
                        principalTable: "users",
                        principalColumn: "wallet",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_project_members_user_wallet",
                table: "project_members",
                column: "user_wallet");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "project_members");

            migrationBuilder.DropTable(
                name: "projects");
        }
    }
}
