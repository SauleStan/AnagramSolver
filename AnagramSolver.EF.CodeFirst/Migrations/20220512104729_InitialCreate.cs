using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnagramSolver.EF.CodeFirst.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Word",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Word", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CachedWord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InputWord = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AnagramId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CachedWord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CachedWord_Word_AnagramId",
                        column: x => x.AnagramId,
                        principalTable: "Word",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SearchInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserIp = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ExecTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    SearchedWord = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AnagramId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SearchInfo_Word_AnagramId",
                        column: x => x.AnagramId,
                        principalTable: "Word",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CachedWord_AnagramId",
                table: "CachedWord",
                column: "AnagramId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchInfo_AnagramId",
                table: "SearchInfo",
                column: "AnagramId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CachedWord");

            migrationBuilder.DropTable(
                name: "SearchInfo");

            migrationBuilder.DropTable(
                name: "Word");
        }
    }
}
