using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataStore.EF.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Owner = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnteredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReportDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Tickets_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "Name" },
                values: new object[] { 1, "Project 1" });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "Name" },
                values: new object[] { 2, "Project 2" });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketId", "Description", "DueDate", "EnteredDate", "Owner", "ProjectId", "ReportDate", "Title" },
                values: new object[] { 1, null, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Frank Liu", 1, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bug #1" });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketId", "Description", "DueDate", "EnteredDate", "Owner", "ProjectId", "ReportDate", "Title" },
                values: new object[] { 2, null, new DateTime(2021, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Maria Yurchenko", 1, new DateTime(2021, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bug #2" });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketId", "Description", "DueDate", "EnteredDate", "Owner", "ProjectId", "ReportDate", "Title" },
                values: new object[] { 3, null, new DateTime(2021, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Maria Yurchenko", 2, new DateTime(2021, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bug #3" });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ProjectId",
                table: "Tickets",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
