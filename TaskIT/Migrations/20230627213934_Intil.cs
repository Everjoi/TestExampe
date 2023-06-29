using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskIT.Migrations
{
    /// <inheritdoc />
    public partial class Intil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FirstTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    RecordDate = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    CropYear = table.Column<int>(type: "int", nullable: false),
                    CounterpartyId = table.Column<int>(type: "int", nullable: false),
                    CounterpartyName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ContactId = table.Column<int>(type: "int", nullable: true),
                    Product = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(7,4)", nullable: false),
                    Process = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Wetness = table.Column<decimal>(type: "decimal(4,1)", nullable: true),
                    Garbage = table.Column<decimal>(type: "decimal(4,1)", nullable: true),
                    Infection = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FirstTab__3214EC07B07D95B1", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FirstTable");
        }
    }
}
