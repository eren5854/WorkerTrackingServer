using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkerTrackingServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dailyproductions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkerDailyProductions_WorkerProductions_WorkerProductionId",
                table: "WorkerDailyProductions");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerDailyProductions_WorkerProductions_WorkerProductionId",
                table: "WorkerDailyProductions",
                column: "WorkerProductionId",
                principalTable: "WorkerProductions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkerDailyProductions_WorkerProductions_WorkerProductionId",
                table: "WorkerDailyProductions");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerDailyProductions_WorkerProductions_WorkerProductionId",
                table: "WorkerDailyProductions",
                column: "WorkerProductionId",
                principalTable: "WorkerProductions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
