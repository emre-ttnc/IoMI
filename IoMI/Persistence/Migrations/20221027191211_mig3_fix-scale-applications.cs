using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IoMI.Persistence.Migrations
{
    public partial class mig3_fixscaleapplications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scales_ScaleInspectionApplications_ScaleInspectionApplicati~",
                table: "Scales");

            migrationBuilder.DropIndex(
                name: "IX_Scales_ScaleInspectionApplicationId",
                table: "Scales");

            migrationBuilder.DropColumn(
                name: "ScaleInspectionApplicationId",
                table: "Scales");

            migrationBuilder.CreateTable(
                name: "ScaleScaleInspectionApplication",
                columns: table => new
                {
                    InspectionApplicationsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ScalesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScaleScaleInspectionApplication", x => new { x.InspectionApplicationsId, x.ScalesId });
                    table.ForeignKey(
                        name: "FK_ScaleScaleInspectionApplication_ScaleInspectionApplications~",
                        column: x => x.InspectionApplicationsId,
                        principalTable: "ScaleInspectionApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScaleScaleInspectionApplication_Scales_ScalesId",
                        column: x => x.ScalesId,
                        principalTable: "Scales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScaleScaleInspectionApplication_ScalesId",
                table: "ScaleScaleInspectionApplication",
                column: "ScalesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScaleScaleInspectionApplication");

            migrationBuilder.AddColumn<Guid>(
                name: "ScaleInspectionApplicationId",
                table: "Scales",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scales_ScaleInspectionApplicationId",
                table: "Scales",
                column: "ScaleInspectionApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scales_ScaleInspectionApplications_ScaleInspectionApplicati~",
                table: "Scales",
                column: "ScaleInspectionApplicationId",
                principalTable: "ScaleInspectionApplications",
                principalColumn: "Id");
        }
    }
}
