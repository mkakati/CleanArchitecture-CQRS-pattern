using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchitecture.Persistence.Migrations
{
    public partial class TokenBased : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "storeDeviceTokens",
                columns: table => new
                {
                    DiviceTokenId = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedById = table.Column<string>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    UpdateById = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UserById = table.Column<string>(nullable: true),
                    TokenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_storeDeviceTokens", x => x.DiviceTokenId);
                    table.ForeignKey(
                        name: "FK_storeDeviceTokens_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_storeDeviceTokens_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_storeDeviceTokens_AspNetUsers_UpdateById",
                        column: x => x.UpdateById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_storeDeviceTokens_AspNetUsers_UserById",
                        column: x => x.UserById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_storeDeviceTokens_CreatedById",
                table: "storeDeviceTokens",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_storeDeviceTokens_DeletedById",
                table: "storeDeviceTokens",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_storeDeviceTokens_UpdateById",
                table: "storeDeviceTokens",
                column: "UpdateById");

            migrationBuilder.CreateIndex(
                name: "IX_storeDeviceTokens_UserById",
                table: "storeDeviceTokens",
                column: "UserById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "storeDeviceTokens");
        }
    }
}
