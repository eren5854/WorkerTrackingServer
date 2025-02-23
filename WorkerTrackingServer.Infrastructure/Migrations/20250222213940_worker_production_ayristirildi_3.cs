using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkerTrackingServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class worker_production_ayristirildi_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DailylyYield",
                table: "WorkerDailyProductions",
                newName: "DailyYield");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DailyYield",
                table: "WorkerDailyProductions",
                newName: "DailylyYield");
        }
    }
}
