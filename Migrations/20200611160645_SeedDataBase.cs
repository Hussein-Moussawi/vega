using Microsoft.EntityFrameworkCore.Migrations;

namespace vega.Migrations
{
    public partial class SeedDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Make1')");
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Make2')");
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Make3')");

            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Make1-ModelA',(SELECT Id From Makes where Name = 'Make1')) ");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Make1-ModelB',(SELECT Id From Makes where Name = 'Make1')) ");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Make1-ModelC',(SELECT Id From Makes where Name = 'Make1')) ");

            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Make1-ModelA',(SELECT Id From Makes where Name = 'Make2')) ");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Make1-ModelB',(SELECT Id From Makes where Name = 'Make2')) ");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Make1-ModelC',(SELECT Id From Makes where Name = 'Make2')) ");

            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Make1-ModelA',(SELECT Id From Makes where Name = 'Make3')) ");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Make1-ModelB',(SELECT Id From Makes where Name = 'Make3')) ");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Make1-ModelC',(SELECT Id From Makes where Name = 'Make3')) ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE From Makes Where Name IN ('Make1', 'Make2', 'Make3')");
        }
    }
}
