using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkerTrackingServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class worker_production_ayristirildi_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkerDailyProductions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkerProductionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DailyActual = table.Column<int>(type: "int", nullable: true),
                    DailyTarget = table.Column<int>(type: "int", nullable: true),
                    DailylyYield = table.Column<int>(type: "int", nullable: true),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerDailyProductions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkerDailyProductions_WorkerProductions_WorkerProductionId",
                        column: x => x.WorkerProductionId,
                        principalTable: "WorkerProductions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkerDailyProductions_WorkerProductionId",
                table: "WorkerDailyProductions",
                column: "WorkerProductionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkerDailyProductions");
        }
    }
}
