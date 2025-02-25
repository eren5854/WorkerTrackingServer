using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkerTrackingServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class context_duzenlendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkerProductions_AppUserId",
                table: "WorkerProductions");

            migrationBuilder.DropIndex(
                name: "IX_WorkerProductions_ProductId",
                table: "WorkerProductions");

            migrationBuilder.DropIndex(
                name: "IX_WorkerAssignments_AppUserId",
                table: "WorkerAssignments");

            migrationBuilder.DropIndex(
                name: "IX_WorkerAssignments_MachineId",
                table: "WorkerAssignments");

            migrationBuilder.DropIndex(
                name: "IX_WorkerAssignments_WorkerProductionId",
                table: "WorkerAssignments");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerProductions_AppUserId",
                table: "WorkerProductions",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerProductions_ProductId",
                table: "WorkerProductions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerAssignments_AppUserId",
                table: "WorkerAssignments",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerAssignments_MachineId",
                table: "WorkerAssignments",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerAssignments_WorkerProductionId",
                table: "WorkerAssignments",
                column: "WorkerProductionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkerProductions_AppUserId",
                table: "WorkerProductions");

            migrationBuilder.DropIndex(
                name: "IX_WorkerProductions_ProductId",
                table: "WorkerProductions");

            migrationBuilder.DropIndex(
                name: "IX_WorkerAssignments_AppUserId",
                table: "WorkerAssignments");

            migrationBuilder.DropIndex(
                name: "IX_WorkerAssignments_MachineId",
                table: "WorkerAssignments");

            migrationBuilder.DropIndex(
                name: "IX_WorkerAssignments_WorkerProductionId",
                table: "WorkerAssignments");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerProductions_AppUserId",
                table: "WorkerProductions",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkerProductions_ProductId",
                table: "WorkerProductions",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkerAssignments_AppUserId",
                table: "WorkerAssignments",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkerAssignments_MachineId",
                table: "WorkerAssignments",
                column: "MachineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkerAssignments_WorkerProductionId",
                table: "WorkerAssignments",
                column: "WorkerProductionId",
                unique: true);
        }
    }
}
