using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Infra.Migrations
{
    /// <inheritdoc />
    public partial class _100720252009 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    TaskTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskStatus_TaskType_TaskTypeId",
                        column: x => x.TaskTypeId,
                        principalTable: "TaskType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManagedTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskTypeId = table.Column<int>(type: "int", nullable: false),
                    TaskStatusId = table.Column<int>(type: "int", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    AssignedUserId = table.Column<int>(type: "int", nullable: false),
                    TaskDataJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagedTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManagedTasks_TaskStatus_TaskStatusId",
                        column: x => x.TaskStatusId,
                        principalTable: "TaskStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ManagedTasks_TaskType_TaskTypeId",
                        column: x => x.TaskTypeId,
                        principalTable: "TaskType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ManagedTasks_Users_AssignedUserId",
                        column: x => x.AssignedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskStatusHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    ManagedTaskId = table.Column<int>(type: "int", nullable: true),
                    OldStatusId = table.Column<int>(type: "int", nullable: false),
                    NewStatusId = table.Column<int>(type: "int", nullable: false),
                    NextAssignedUserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskStatusHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskStatusHistories_ManagedTasks_ManagedTaskId",
                        column: x => x.ManagedTaskId,
                        principalTable: "ManagedTasks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskStatusHistories_TaskStatus_NewStatusId",
                        column: x => x.NewStatusId,
                        principalTable: "TaskStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskStatusHistories_TaskStatus_OldStatusId",
                        column: x => x.OldStatusId,
                        principalTable: "TaskStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskStatusHistories_Users_NextAssignedUserId",
                        column: x => x.NextAssignedUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ManagedTasks_AssignedUserId",
                table: "ManagedTasks",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ManagedTasks_TaskStatusId",
                table: "ManagedTasks",
                column: "TaskStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ManagedTasks_TaskTypeId",
                table: "ManagedTasks",
                column: "TaskTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskStatus_TaskTypeId",
                table: "TaskStatus",
                column: "TaskTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskStatusHistories_ManagedTaskId",
                table: "TaskStatusHistories",
                column: "ManagedTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskStatusHistories_NewStatusId",
                table: "TaskStatusHistories",
                column: "NewStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskStatusHistories_NextAssignedUserId",
                table: "TaskStatusHistories",
                column: "NextAssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskStatusHistories_OldStatusId",
                table: "TaskStatusHistories",
                column: "OldStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskStatusHistories");

            migrationBuilder.DropTable(
                name: "ManagedTasks");

            migrationBuilder.DropTable(
                name: "TaskStatus");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "TaskType");
        }
    }
}
