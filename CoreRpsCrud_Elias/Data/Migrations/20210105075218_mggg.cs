using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreRpsCrud_Elias.Data.Migrations
{
    public partial class mggg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admissions",
                columns: table => new
                {
                    AdmissionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TraineeID = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    InstractorName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TransanctionId = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    CourseFee = table.Column<int>(nullable: false),
                    AdmissionDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admissions", x => x.AdmissionId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admissions");
        }
    }
}
