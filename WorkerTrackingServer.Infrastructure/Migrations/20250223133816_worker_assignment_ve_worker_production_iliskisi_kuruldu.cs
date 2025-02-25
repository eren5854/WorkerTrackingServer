using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkerTrackingServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class worker_assignment_ve_worker_production_iliskisi_kuruldu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkerAssignments_Products_ProductId",
                table: "WorkerAssignments");

            migrationBuilder.DropColumn(
                name: "ActualQuantity",
                table: "WorkerAssignments");

            migrationBuilder.DropColumn(
                name: "TargetQuantity",
                table: "WorkerAssignments");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "WorkerAssignments",
                newName: "WorkerProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkerAssignments_ProductId",
                table: "WorkerAssignments",
                newName: "IX_WorkerAssignments_WorkerProductionId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerAssignments_WorkerProductions_WorkerProductionId",
                table: "WorkerAssignments",
                column: "WorkerProductionId",
                principalTable: "WorkerProductions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkerAssignments_WorkerProductions_WorkerProductionId",
                table: "WorkerAssignments");

            migrationBuilder.RenameColumn(
                name: "WorkerProductionId",
                table: "WorkerAssignments",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkerAssignments_WorkerProductionId",
                table: "WorkerAssignments",
                newName: "IX_WorkerAssignments_ProductId");

            migrationBuilder.AddColumn<int>(
                name: "ActualQuantity",
                table: "WorkerAssignments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TargetQuantity",
                table: "WorkerAssignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerAssignments_Products_ProductId",
                table: "WorkerAssignments",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
