using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkerTrackingServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class siniflar_duzenlendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkerAssignments_Workers_WorkerId",
                table: "WorkerAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerLogins_Workers_WorkerId",
                table: "WorkerLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerProductions_Workers_WorkerId",
                table: "WorkerProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Departments_DepartmentId",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Workers_DepartmentId",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_WorkerAssignments_WorkerId",
                table: "WorkerAssignments");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "WorkerAssignments");

            migrationBuilder.RenameColumn(
                name: "WorkerId",
                table: "WorkerProductions",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkerProductions_WorkerId",
                table: "WorkerProductions",
                newName: "IX_WorkerProductions_AppUserId");

            migrationBuilder.RenameColumn(
                name: "WorkerId",
                table: "WorkerLogins",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkerLogins_WorkerId",
                table: "WorkerLogins",
                newName: "IX_WorkerLogins_AppUserId");

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "WorkerAssignments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "varchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Users",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkerCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmailSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    AppPassword = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    SmtpDomainName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    SmtpPort = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSettings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkerAssignments_AppUserId",
                table: "WorkerAssignments",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerAssignments_Users_AppUserId",
                table: "WorkerAssignments",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerLogins_Users_AppUserId",
                table: "WorkerLogins",
                column: "AppUserId",
                principalTable: "Users",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerAssignments_Users_AppUserId",
                table: "WorkerAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerLogins_Users_AppUserId",
                table: "WorkerLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerProductions_Users_AppUserId",
                table: "WorkerProductions");

            migrationBuilder.DropTable(
                name: "EmailSettings");

            migrationBuilder.DropIndex(
                name: "IX_WorkerAssignments_AppUserId",
                table: "WorkerAssignments");

            migrationBuilder.DropIndex(
                name: "IX_Users_DepartmentId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "WorkerAssignments");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WorkerCode",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "WorkerProductions",
                newName: "WorkerId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkerProductions_AppUserId",
                table: "WorkerProductions",
                newName: "IX_WorkerProductions_WorkerId");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "WorkerLogins",
                newName: "WorkerId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkerLogins_AppUserId",
                table: "WorkerLogins",
                newName: "IX_WorkerLogins_WorkerId");

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "Workers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkerId",
                table: "WorkerAssignments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldMaxLength: 250);

            migrationBuilder.CreateIndex(
                name: "IX_Workers_DepartmentId",
                table: "Workers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerAssignments_WorkerId",
                table: "WorkerAssignments",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerAssignments_Workers_WorkerId",
                table: "WorkerAssignments",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerLogins_Workers_WorkerId",
                table: "WorkerLogins",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerProductions_Workers_WorkerId",
                table: "WorkerProductions",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Departments_DepartmentId",
                table: "Workers",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }
    }
}
