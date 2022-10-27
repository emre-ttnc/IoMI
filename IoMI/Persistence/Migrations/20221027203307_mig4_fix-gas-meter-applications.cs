using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IoMI.Persistence.Migrations
{
    public partial class mig4_fixgasmeterapplications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GasMeters_GasMeterInspectionApplications_GasMeterInspection~",
                table: "GasMeters");

            migrationBuilder.DropIndex(
                name: "IX_GasMeters_GasMeterInspectionApplicationId",
                table: "GasMeters");

            migrationBuilder.DropColumn(
                name: "GasMeterInspectionApplicationId",
                table: "GasMeters");

            migrationBuilder.CreateTable(
                name: "GasMeterGasMeterInspectionApplication",
                columns: table => new
                {
                    ApplicationsId = table.Column<Guid>(type: "uuid", nullable: false),
                    GasMetersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GasMeterGasMeterInspectionApplication", x => new { x.ApplicationsId, x.GasMetersId });
                    table.ForeignKey(
                        name: "FK_GasMeterGasMeterInspectionApplication_GasMeterInspectionApp~",
                        column: x => x.ApplicationsId,
                        principalTable: "GasMeterInspectionApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GasMeterGasMeterInspectionApplication_GasMeters_GasMetersId",
                        column: x => x.GasMetersId,
                        principalTable: "GasMeters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GasMeterGasMeterInspectionApplication_GasMetersId",
                table: "GasMeterGasMeterInspectionApplication",
                column: "GasMetersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GasMeterGasMeterInspectionApplication");

            migrationBuilder.AddColumn<Guid>(
                name: "GasMeterInspectionApplicationId",
                table: "GasMeters",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GasMeters_GasMeterInspectionApplicationId",
                table: "GasMeters",
                column: "GasMeterInspectionApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_GasMeters_GasMeterInspectionApplications_GasMeterInspection~",
                table: "GasMeters",
                column: "GasMeterInspectionApplicationId",
                principalTable: "GasMeterInspectionApplications",
                principalColumn: "Id");
        }
    }
}
