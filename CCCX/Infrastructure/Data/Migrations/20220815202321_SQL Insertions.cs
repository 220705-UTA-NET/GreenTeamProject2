using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{ 
    public partial class SQLInsertions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Product",
                columns:new[] {"Name"},
                values:new object[,]{
                { "Prod"},
            { "Prod"}
            }
                ) ;
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
