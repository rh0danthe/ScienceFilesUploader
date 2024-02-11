using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScienceFileUploader.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.UniqueConstraint("AK_Files_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstExperimentTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastExperimentTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MaxExperimentDuration = table.Column<int>(type: "INTEGER", nullable: false),
                    MinExperimentDuration = table.Column<int>(type: "INTEGER", nullable: false),
                    AvgExperimentDuration = table.Column<double>(type: "REAL", nullable: false),
                    AvgByParameters = table.Column<double>(type: "REAL", nullable: false),
                    MedianByParameters = table.Column<double>(type: "REAL", nullable: false),
                    MaxParameterValue = table.Column<int>(type: "INTEGER", nullable: false),
                    MinParameterValue = table.Column<int>(type: "INTEGER", nullable: false),
                    AmountOfExperiments = table.Column<int>(type: "INTEGER", nullable: false),
                    FileName = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_Files_FileName",
                        column: x => x.FileName,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Values",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TimeInMs = table.Column<int>(type: "INTEGER", nullable: false),
                    Parameter = table.Column<double>(type: "REAL", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Values", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Values_Files_FileName",
                        column: x => x.FileName,
                        principalTable: "Files",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Results_FileName",
                table: "Results",
                column: "FileName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Values_FileName",
                table: "Values",
                column: "FileName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Values");

            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}
