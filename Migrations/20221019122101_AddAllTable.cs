using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TO_DO_LIST.Migrations
{
    public partial class AddAllTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    TimeZone = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DailyList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyList_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deadline = table.Column<DateTime>(type: "datetime", nullable: false),
                    Done = table.Column<bool>(type: "bit", nullable: false),
                    AddedToDone = table.Column<DateTime>(type: "datetime", nullable: true),
                    DailyListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_DailyList_DailyListId",
                        column: x => x.DailyListId,
                        principalTable: "DailyList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "TimeZone", "Username" },
                values: new object[] { 1, "test@gmail.com", new byte[] { 119, 84, 220, 67, 86, 159, 130, 146, 228, 205, 88, 157, 108, 185, 139, 138, 51, 105, 89, 253, 203, 226, 208, 151, 91, 75, 55, 69, 103, 242, 99, 133, 121, 44, 26, 109, 46, 181, 51, 67, 249, 148, 161, 122, 81, 122, 210, 174, 9, 7, 196, 156, 222, 124, 254, 55, 67, 183, 244, 185, 132, 49, 192, 94 }, new byte[] { 3, 92, 251, 81, 68, 64, 147, 216, 77, 140, 26, 4, 224, 148, 211, 138, 107, 165, 20, 142, 122, 187, 43, 6, 217, 90, 107, 98, 238, 96, 174, 22, 117, 91, 231, 214, 221, 104, 222, 237, 59, 75, 53, 117, 200, 229, 248, 75, 133, 250, 190, 238, 112, 148, 184, 141, 18, 3, 207, 72, 255, 198, 75, 128, 99, 65, 64, 209, 42, 222, 112, 24, 93, 119, 177, 75, 64, 182, 19, 137, 244, 174, 126, 236, 186, 173, 36, 246, 170, 203, 16, 246, 195, 157, 52, 167, 232, 3, 136, 113, 254, 255, 72, 237, 171, 221, 134, 254, 93, 77, 72, 70, 188, 236, 149, 175, 206, 100, 54, 120, 18, 247, 224, 230, 100, 190, 179, 123 }, 4, "edo" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "TimeZone", "Username" },
                values: new object[] { 2, "test@gmail.com", new byte[] { 161, 30, 35, 71, 11, 99, 83, 138, 83, 66, 101, 206, 77, 158, 135, 21, 43, 48, 142, 230, 71, 216, 210, 237, 193, 104, 166, 43, 103, 3, 225, 68, 89, 43, 66, 18, 72, 145, 174, 138, 139, 181, 93, 108, 158, 72, 187, 220, 169, 83, 241, 49, 58, 251, 251, 192, 61, 38, 22, 221, 87, 185, 23, 208 }, new byte[] { 222, 198, 231, 175, 16, 222, 162, 74, 238, 216, 172, 126, 179, 141, 125, 148, 100, 18, 21, 158, 78, 147, 87, 91, 164, 182, 213, 17, 48, 231, 128, 30, 10, 8, 201, 254, 13, 200, 8, 106, 148, 204, 40, 201, 223, 144, 81, 168, 114, 197, 214, 213, 244, 170, 31, 144, 170, 0, 240, 140, 94, 123, 20, 89, 85, 80, 2, 42, 144, 163, 182, 32, 97, 1, 116, 19, 217, 62, 187, 10, 132, 73, 83, 55, 192, 137, 246, 193, 67, 113, 230, 125, 19, 5, 180, 64, 101, 12, 36, 102, 179, 106, 80, 93, 51, 161, 181, 200, 222, 174, 22, 77, 186, 231, 226, 76, 162, 233, 185, 47, 254, 103, 159, 202, 220, 70, 178, 204 }, 3, "fikro" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "TimeZone", "Username" },
                values: new object[] { 3, "test@gmail.com", new byte[] { 143, 220, 186, 199, 30, 88, 204, 119, 172, 141, 107, 29, 193, 204, 7, 62, 81, 136, 129, 187, 168, 78, 234, 70, 171, 209, 25, 61, 67, 169, 224, 156, 206, 226, 86, 232, 67, 89, 206, 245, 101, 223, 147, 3, 101, 40, 43, 128, 198, 211, 71, 254, 8, 99, 135, 176, 156, 187, 46, 215, 172, 255, 22, 159 }, new byte[] { 120, 196, 203, 186, 60, 182, 20, 61, 124, 34, 113, 111, 53, 226, 155, 8, 110, 66, 118, 202, 246, 250, 41, 15, 103, 233, 81, 212, 162, 133, 224, 1, 144, 45, 71, 195, 68, 0, 113, 191, 192, 231, 98, 175, 127, 193, 34, 29, 116, 238, 134, 11, 62, 91, 66, 71, 114, 136, 212, 38, 15, 86, 217, 148, 233, 186, 18, 61, 93, 75, 150, 30, 11, 101, 207, 104, 139, 11, 106, 8, 94, 87, 45, 88, 106, 33, 0, 213, 120, 73, 110, 151, 126, 92, 124, 133, 19, 194, 161, 220, 92, 21, 171, 142, 57, 210, 61, 43, 25, 238, 154, 235, 26, 88, 48, 208, 17, 114, 6, 156, 62, 32, 242, 201, 65, 115, 102, 250 }, 2, "hus" });

            migrationBuilder.CreateIndex(
                name: "IX_DailyList_UserId",
                table: "DailyList",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_DailyListId",
                table: "Task",
                column: "DailyListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "DailyList");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
