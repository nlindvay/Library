using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Integration.Tests.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentState = table.Column<int>(type: "integer", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Isbn = table.Column<string>(type: "text", nullable: true),
                    ReservationId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.CorrelationId);
                });

            migrationBuilder.CreateTable(
                name: "ThankYou",
                columns: table => new
                {
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentState = table.Column<int>(type: "integer", nullable: false),
                    BookId = table.Column<Guid>(type: "uuid", nullable: false),
                    MemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReservationId = table.Column<Guid>(type: "uuid", nullable: true),
                    ThankYouStatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThankYou", x => x.CorrelationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_Isbn",
                table: "Book",
                column: "Isbn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThankYou_BookId_MemberId",
                table: "ThankYou",
                columns: new[] { "BookId", "MemberId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "ThankYou");
        }
    }
}
