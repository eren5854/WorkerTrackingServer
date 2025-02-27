using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkerTrackingServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class other_productions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkerMonthlyProductions_WorkerProductions_WorkerProductionId",
                table: "WorkerMonthlyProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerWeeklyProductions_WorkerProductions_WorkerProductionId",
                table: "WorkerWeeklyProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerYearlyProductions_WorkerProductions_WorkerProductionId",
                table: "WorkerYearlyProductions");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerMonthlyProductions_WorkerProductions_WorkerProductionId",
                table: "WorkerMonthlyProductions",
                column: "WorkerProductionId",
                principalTable: "WorkerProductions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerWeeklyProductions_WorkerProductions_WorkerProductionId",
                table: "WorkerWeeklyProductions",
                column: "WorkerProductionId",
                principalTable: "WorkerProductions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerYearlyProductions_WorkerProductions_WorkerProductionId",
                table: "WorkerYearlyProductions",
                column: "WorkerProductionId",
                principalTable: "WorkerProductions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkerMonthlyProductions_WorkerProductions_WorkerProductionId",
                table: "WorkerMonthlyProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerWeeklyProductions_WorkerProductions_WorkerProductionId",
                table: "WorkerWeeklyProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerYearlyProductions_WorkerProductions_WorkerProductionId",
                table: "WorkerYearlyProductions");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerMonthlyProductions_WorkerProductions_WorkerProductionId",
                table: "WorkerMonthlyProductions",
                column: "WorkerProductionId",
                principalTable: "WorkerProductions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerWeeklyProductions_WorkerProductions_WorkerProductionId",
                table: "WorkerWeeklyProductions",
                column: "WorkerProductionId",
                principalTable: "WorkerProductions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerYearlyProductions_WorkerProductions_WorkerProductionId",
                table: "WorkerYearlyProductions",
                column: "WorkerProductionId",
                principalTable: "WorkerProductions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
