using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IoMI.Persistence.Migrations
{
    public partial class mig2_applicationadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ScaleInspectionApplicationId",
                table: "Scales",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GasMeterInspectionApplicationId",
                table: "GasMeters",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GasMeterInspectionApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsAccepted = table.Column<bool>(type: "boolean", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    ApplicantId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GasMeterInspectionApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GasMeterInspectionApplications_AspNetUsers_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ScaleInspectionApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsAccepted = table.Column<bool>(type: "boolean", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    ApplicantId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScaleInspectionApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScaleInspectionApplications_AspNetUsers_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Scales_ScaleInspectionApplicationId",
                table: "Scales",
                column: "ScaleInspectionApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_GasMeters_GasMeterInspectionApplicationId",
                table: "GasMeters",
                column: "GasMeterInspectionApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_GasMeterInspectionApplications_ApplicantId",
                table: "GasMeterInspectionApplications",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_ScaleInspectionApplications_ApplicantId",
                table: "ScaleInspectionApplications",
                column: "ApplicantId");

            migrationBuilder.AddForeignKey(
                name: "FK_GasMeters_GasMeterInspectionApplications_GasMeterInspection~",
                table: "GasMeters",
                column: "GasMeterInspectionApplicationId",
                principalTable: "GasMeterInspectionApplications",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Scales_ScaleInspectionApplications_ScaleInspectionApplicati~",
                table: "Scales",
                column: "ScaleInspectionApplicationId",
                principalTable: "ScaleInspectionApplications",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GasMeters_GasMeterInspectionApplications_GasMeterInspection~",
                table: "GasMeters");

            migrationBuilder.DropForeignKey(
                name: "FK_Scales_ScaleInspectionApplications_ScaleInspectionApplicati~",
                table: "Scales");

            migrationBuilder.DropTable(
                name: "GasMeterInspectionApplications");

            migrationBuilder.DropTable(
                name: "ScaleInspectionApplications");

            migrationBuilder.DropIndex(
                name: "IX_Scales_ScaleInspectionApplicationId",
                table: "Scales");

            migrationBuilder.DropIndex(
                name: "IX_GasMeters_GasMeterInspectionApplicationId",
                table: "GasMeters");

            migrationBuilder.DropColumn(
                name: "ScaleInspectionApplicationId",
                table: "Scales");

            migrationBuilder.DropColumn(
                name: "GasMeterInspectionApplicationId",
                table: "GasMeters");
        }
    }
}
