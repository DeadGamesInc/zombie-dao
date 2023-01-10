using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZombieDAO.Migrations
{
    /// <inheritdoc />
    public partial class addgnosis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "gnosis_safes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    projectid = table.Column<Guid>(name: "project_id", type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    chainid = table.Column<int>(name: "chain_id", type: "integer", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gnosis_safes", x => x.id);
                    table.ForeignKey(
                        name: "FK_gnosis_safes_projects_project_id",
                        column: x => x.projectid,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gnosis_safe_tokens",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    safeid = table.Column<Guid>(name: "safe_id", type: "uuid", nullable: false),
                    symbol = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gnosis_safe_tokens", x => x.id);
                    table.ForeignKey(
                        name: "FK_gnosis_safe_tokens_gnosis_safes_safe_id",
                        column: x => x.safeid,
                        principalTable: "gnosis_safes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gnosis_safe_transactions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    safe = table.Column<Guid>(type: "uuid", nullable: false),
                    user = table.Column<string>(type: "text", nullable: false),
                    to = table.Column<string>(type: "text", nullable: false),
                    data = table.Column<string>(type: "text", nullable: false),
                    operation = table.Column<int>(type: "integer", nullable: false),
                    safetxgas = table.Column<string>(name: "safe_tx_gas", type: "text", nullable: false),
                    basegas = table.Column<string>(name: "base_gas", type: "text", nullable: false),
                    gasprice = table.Column<string>(name: "gas_price", type: "text", nullable: false),
                    gastoken = table.Column<string>(name: "gas_token", type: "text", nullable: false),
                    refundreceiver = table.Column<string>(name: "refund_receiver", type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false),
                    nonce = table.Column<int>(type: "integer", nullable: false),
                    executed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gnosis_safe_transactions", x => x.id);
                    table.ForeignKey(
                        name: "FK_gnosis_safe_transactions_gnosis_safes_safe",
                        column: x => x.safe,
                        principalTable: "gnosis_safes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gnosis_safe_transactions_users_user",
                        column: x => x.user,
                        principalTable: "users",
                        principalColumn: "wallet",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "safe_confirmations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    signature = table.Column<string>(type: "text", nullable: false),
                    transaction = table.Column<Guid>(type: "uuid", nullable: false),
                    user = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_safe_confirmations", x => x.id);
                    table.ForeignKey(
                        name: "FK_safe_confirmations_gnosis_safe_transactions_transaction",
                        column: x => x.transaction,
                        principalTable: "gnosis_safe_transactions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_safe_confirmations_users_user",
                        column: x => x.user,
                        principalTable: "users",
                        principalColumn: "wallet",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_gnosis_safe_tokens_safe_id",
                table: "gnosis_safe_tokens",
                column: "safe_id");

            migrationBuilder.CreateIndex(
                name: "IX_gnosis_safe_transactions_safe",
                table: "gnosis_safe_transactions",
                column: "safe");

            migrationBuilder.CreateIndex(
                name: "IX_gnosis_safe_transactions_user",
                table: "gnosis_safe_transactions",
                column: "user");

            migrationBuilder.CreateIndex(
                name: "IX_gnosis_safes_project_id",
                table: "gnosis_safes",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_safe_confirmations_transaction",
                table: "safe_confirmations",
                column: "transaction");

            migrationBuilder.CreateIndex(
                name: "IX_safe_confirmations_user",
                table: "safe_confirmations",
                column: "user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gnosis_safe_tokens");

            migrationBuilder.DropTable(
                name: "safe_confirmations");

            migrationBuilder.DropTable(
                name: "gnosis_safe_transactions");

            migrationBuilder.DropTable(
                name: "gnosis_safes");
        }
    }
}
