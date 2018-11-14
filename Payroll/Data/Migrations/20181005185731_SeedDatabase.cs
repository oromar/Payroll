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

            migrationBuilder.Sql("INSERT INTO Occupation(Id, CreatedAt, CreatedBy, IsDeleted, Name, IsRegulated, CouncilName, SearchFields) VALUES (" +
                "NEWID(), GETDATE(), 'oldm@cin.ufpe.br', 0, 'Engenheiro Civil', 1, 'CREA', 'Engenheiro Civil CREA oldm@cin.ufpe.br')");

            migrationBuilder.Sql("INSERT INTO Occupation(Id, CreatedAt, CreatedBy, IsDeleted, Name, IsRegulated, CouncilName, SearchFields) VALUES (" +
                "NEWID(), GETDATE(), 'oldm@cin.ufpe.br', 0, 'Engenheiro Eletrônico', 1, 'CREA', 'Engenheiro Eletronico CREA oldm@cin.ufpe.br')");

            migrationBuilder.Sql("INSERT INTO Occupation(Id, CreatedAt, CreatedBy, IsDeleted, Name, IsRegulated, CouncilName, SearchFields) VALUES (" +
                "NEWID(), GETDATE(), 'oldm@cin.ufpe.br', 0, 'Médico', 1, 'CREMEPE', 'Médico CREMEPE oldm@cin.ufpe.br')");

            migrationBuilder.Sql("INSERT INTO Occupation(Id, CreatedAt, CreatedBy, IsDeleted, Name, IsRegulated, CouncilName, SearchFields) VALUES (" +
                "NEWID(), GETDATE(), 'oldm@cin.ufpe.br', 0, 'Enfermeiro', 1, 'COREN', 'Engenheiro Civi CREA oldm@cin.ufpe.br')");

            migrationBuilder.Sql("INSERT INTO LicenseType(Id, CreatedAt, CreatedBy, IsDeleted, Name, Description, QtyDaysDefault, SearchFields) VALUES (" +
                "NEWID(), GETDATE(), 'oldm@cin.ufpe.br', 0, 'Gala', 'Licença de Casamento', 5, 'Gala Licença de Casamento 5 oldm@cin.ufpe.br')");

            migrationBuilder.Sql("INSERT INTO LicenseType(Id, CreatedAt, CreatedBy, IsDeleted, Name, Description, QtyDaysDefault, SearchFields) VALUES (" +
                "NEWID(), GETDATE(), 'oldm@cin.ufpe.br', 0, 'Nojo', 'Quando um parente de primeiro grau vai a óbito', 8, 'Nojo Quando um parente de primeiro grau vai a óbito 8 oldm@cin.ufpe.br')");

            migrationBuilder.Sql("INSERT INTO LicenseType(Id, CreatedAt, CreatedBy, IsDeleted, Name, Description, QtyDaysDefault, SearchFields) VALUES (" +
                "NEWID(), GETDATE(), 'oldm@cin.ufpe.br', 0, 'Paternidade', 'Pelo nascimento de um filho (Funcionário do sexo masculino)', 5, 'Paternidade Pelo nascimento de um filho (Funcionário do sexo masculino) 5 oldm@cin.ufpe.br')");

            migrationBuilder.Sql("INSERT INTO LicenseType(Id, CreatedAt, CreatedBy, IsDeleted, Name, Description, QtyDaysDefault, SearchFields) VALUES (" +
                "NEWID(), GETDATE(), 'oldm@cin.ufpe.br', 0, 'Maternidade', 'Pelo nascimento de um filho (Funcionário do sexo feminino)', 120, 'Maternidade Pelo nascimento de um filho (Funcionário do sexto feminino) 120 oldm@cin.ufpe.br')");

            migrationBuilder.Sql("INSERT INTO OccurrenceType(Id, CreatedAt, CreatedBy, IsDeleted, Name, Description, IsAbsence, SearchFields) VALUES (" +
                "NEWID(), GETDATE(), 'oldm@cin.ufpe.br', 0, 'Licença', 'Licença de qualquer tipo', 1, 'Licenca Licenca de qualquer tipo oldm@cin.ufpe.br')");

            migrationBuilder.Sql("INSERT INTO OccurrenceType(Id, CreatedAt, CreatedBy, IsDeleted, Name, Description, IsAbsence, SearchFields) VALUES (" +
                "NEWID(), GETDATE(), 'oldm@cin.ufpe.br', 0, 'Acidente de Trabalho', 'Quando funcionário sofre acidente no trabalho ou no percurso casa-trabalho/trabalho-casa', 0, 'Acidente de Trabalho Quando funcionário sofre acidente no trabalho ou no percurso casa-trabalho/trabalho-casa oldm@cin.ufpe.br')");

            migrationBuilder.Sql("INSERT INTO OccurrenceType(Id, CreatedAt, CreatedBy, IsDeleted, Name, Description, IsAbsence, SearchFields) VALUES (" +
                "NEWID(), GETDATE(), 'oldm@cin.ufpe.br', 0, 'Greve', 'Funcionários realizam greve', 1, 'Greve Funcionários realizam greve oldm@cin.ufpe.br')");

            migrationBuilder.Sql("INSERT INTO OccurrenceType(Id, CreatedAt, CreatedBy, IsDeleted, Name, Description, IsAbsence, SearchFields) VALUES (" +
                "NEWID(), GETDATE(), 'oldm@cin.ufpe.br', 0, 'Elogio', 'Elogio ao funcionário por mérito', 0, 'Elogio Elogio ao funcionário por mérito oldm@cin.ufpe.br')");

            migrationBuilder.Sql("INSERT INTO OccurrenceType(Id, CreatedAt, CreatedBy, IsDeleted, Name, Description, IsAbsence, SearchFields) VALUES (" +
                "NEWID(), GETDATE(), 'oldm@cin.ufpe.br', 0, 'Advertência', 'Advertência escrita ao funcionário', 0, 'Advertência Advertência escrita ao funcionário oldm@cin.ufpe.br')");

            migrationBuilder.Sql("INSERT INTO OccurrenceType(Id, CreatedAt, CreatedBy, IsDeleted, Name, Description, IsAbsence, SearchFields) VALUES (" +
                "NEWID(), GETDATE(), 'oldm@cin.ufpe.br', 0, 'Suspensão', 'Suspensão aplicada ao funcionário', 1, 'Suspensão aplicada ao funcionário oldm@cin.ufpe.br')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
