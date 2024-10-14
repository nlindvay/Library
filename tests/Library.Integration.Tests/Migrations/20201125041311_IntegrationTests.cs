using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Integration.Tests.Migrations
{
    public partial class IntegrationTests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ThankYou",
                columns: table => new
                {
                    CorrelationId = table.Column<Guid>(nullable: false),
                    CurrentState = table.Column<int>(nullable: false),
                    BookInstanceId = table.Column<Guid>(nullable: false),
                    MemberId = table.Column<Guid>(nullable: false),
                    ReservationId = table.Column<Guid>(nullable: true),
                    ThankYouStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThankYou", x => x.CorrelationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThankYou_BookInstanceId_MemberId",
                table: "ThankYou",
                columns: new[] { "BookInstanceId", "MemberId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThankYou");
        }
    }
}
