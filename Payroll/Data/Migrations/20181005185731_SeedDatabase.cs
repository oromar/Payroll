using Microsoft.EntityFrameworkCore.Migrations;

namespace Payroll.Data.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Currency (Id, CreatedAt, CreatedBy, IsDeleted, Name, Exchange, Symbol, SearchFields) VALUES (" +
                "NEWID(), GETDATE(), 'oldm@cin.ufpe.br', 0, 'Real', 1, 'BRL', 'Real 1 BRL oldm@cin.ufpe.br')");

            migrationBuilder.Sql("INSERT INTO Currency (Id, CreatedAt, CreatedBy, IsDeleted, Name, Exchange, Symbol, SearchFields) VALUES (" +
                "NEWID(), GETDATE(), 'oldm@cin.ufpe.br', 0, 'Dólar Americano', 4, 'USD', 'Dólar Americano 4 USD oldm@cin.ufpe.br')");

            migrationBuilder.Sql("INSERT INTO Currency (Id, CreatedAt, CreatedBy, IsDeleted, Name, Exchange, Symbol, SearchFields) VALUES (" +
                "NEWID(), GETDATE(), 'oldm@cin.ufpe.br', 0, 'Dólar Canadense', 2, 'USD', 'Dólar Canadense 2 CND oldm@cin.ufpe.br')");

            migrationBuilder.Sql("INSERT INTO Currency (Id, CreatedAt, CreatedBy, IsDeleted, Name, Exchange, Symbol, SearchFields) VALUES (" +
                "NEWID(), GETDATE(), 'oldm@cin.ufpe.br', 0, 'Euro', 5, 'EUR', 'Euro 5 EUR oldm@cin.ufpe.br')");

            migrationBuilder.Sql("INSERT INTO Occupation(Id, CreatedAt, CreatedBy, IsDeleted, Name, IsRegulated, CouncilName, SearchFields) VALUES (" +
                "NEWID(), GETDATE(), 'oldm@cin.ufpe.br', 0, 'Analista de Sistemas', 0, '', 'Analista de Sistemas oldm@cin.ufpe.br')");

            migrationBuilder.Sql("INSERT INTO Occupation(Id, CreatedAt, CreatedBy, IsDeleted, Name, IsRegulated, CouncilName, SearchFields) VALUES (" +
                "NEWID(), GETDATE(), 'oldm@cin.ufpe.br', 0, 'Analista de Banco de dados', 0, '', 'Analista de Banco de dados oldm@cin.ufpe.br')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
