using Microsoft.EntityFrameworkCore.Migrations;

namespace MapperAPI.Migrations
{
    public partial class _111420_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "mappedBy34",
                table: "PlanViewProjects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "mappedByName",
                table: "PlanViewProjects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "mappedBy34",
                table: "PlanViewProjects");

            migrationBuilder.DropColumn(
                name: "mappedByName",
                table: "PlanViewProjects");
        }
    }
}
