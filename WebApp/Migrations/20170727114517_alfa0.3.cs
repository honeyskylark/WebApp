using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApp.Migrations
{
    public partial class alfa03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Catalogs_Sections_EquipmentId",
                table: "Catalogs");

            migrationBuilder.RenameColumn(
                name: "EquipmentId",
                table: "Catalogs",
                newName: "SubSectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Catalogs_EquipmentId",
                table: "Catalogs",
                newName: "IX_Catalogs_SubSectionId");

            migrationBuilder.CreateTable(
                name: "SubSection",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    SectionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubSection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubSection_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubSection_SectionId",
                table: "SubSection",
                column: "SectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Catalogs_SubSection_SubSectionId",
                table: "Catalogs",
                column: "SubSectionId",
                principalTable: "SubSection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Catalogs_SubSection_SubSectionId",
                table: "Catalogs");

            migrationBuilder.DropTable(
                name: "SubSection");

            migrationBuilder.RenameColumn(
                name: "SubSectionId",
                table: "Catalogs",
                newName: "EquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Catalogs_SubSectionId",
                table: "Catalogs",
                newName: "IX_Catalogs_EquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Catalogs_Sections_EquipmentId",
                table: "Catalogs",
                column: "EquipmentId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
