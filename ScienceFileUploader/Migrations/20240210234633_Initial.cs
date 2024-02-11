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
                name: "Clients",
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
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.UniqueConstraint("AK_Clients_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
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
                    FileId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_FileId",
                        column: x => x.FileId,
                        principalTable: "Clients",
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
                        name: "FK_Values_Clients_FileName",
                        column: x => x.FileName,
                        principalTable: "Clients",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FileId",
                table: "Orders",
                column: "FileId",
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
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Values");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
