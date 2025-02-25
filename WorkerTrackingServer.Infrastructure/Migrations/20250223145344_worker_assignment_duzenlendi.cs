using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkerTrackingServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class worker_assignment_duzenlendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkerAssignments_Users_AppUserId",
                table: "WorkerAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerProductions_Products_ProductId",
                table: "WorkerProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerProductions_Users_AppUserId",
                table: "WorkerProductions");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "WorkerProductionId",
                table: "WorkerAssignments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MachineId",
                table: "WorkerAssignments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerAssignments_Users_AppUserId",
                table: "WorkerAssignments",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerProductions_Products_ProductId",
                table: "WorkerProductions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerProductions_Users_AppUserId",
                table: "WorkerProductions",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkerAssignments_Users_AppUserId",
                table: "WorkerAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerProductions_Products_ProductId",
                table: "WorkerProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerProductions_Users_AppUserId",
                table: "WorkerProductions");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "WorkerProductionId",
                table: "WorkerAssignments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "MachineId",
                table: "WorkerAssignments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerAssignments_Users_AppUserId",
                table: "WorkerAssignments",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerProductions_Products_ProductId",
                table: "WorkerProductions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerProductions_Users_AppUserId",
                table: "WorkerProductions",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
