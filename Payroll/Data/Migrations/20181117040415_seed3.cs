using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Migrations;
using MMLib.Extensions;
using Payroll.Business;
using Payroll.Models;

namespace Payroll.Data.Migrations
{
    public partial class seed3 : Migration
    {

        private readonly ApplicationDbContext _context;

        public seed3(ApplicationDbContext context)
        {
            _context = context;
        }
        protected async override void Up(MigrationBuilder migrationBuilder)
        {
            string[] companyNames = { "Comércio e Indústria de Papéis LTDA", "Agronegócio SA", "Trindade Advogados LTDA", "UNIDAT SA", "Sá e Souza SA" };
            string[] companyAddress = { "Rua do Hospício", "Rua do Aragão", "Rua A", "Av Dantas Barreto", "Avenida Presidente Kennedy" };
            string[] neighborhoods = { "Boa Vista", "Boa Vista", "Santo Antonio", "Sao José", "Peixinhos" };
            string[] companyCities = { "Recife", "Recife", "Recife", "Recife", "Olinda" };
            string[] personalJuridicalNames = { "19203292000198", "48306438000129", "01724647000129", "04560384000158", "12023908000108" };
            string[] occupationAreas = { "Indústria de Papel", "Agropecuária", "Advocacia", "Universidade", "Comércio" };
            string[] socialReasons = { "Papel macio", "Engordando o gado", "Trindade e Amigos", "Universidade", "Compra e venda" };

            Guid[] currencyIds = _context.Currency.Select(a => a.Id).ToArray();

            for (var i = 0; i < 5; i++)
            {
                var company = new Company
                {
                    Name = companyNames[i],
                    CreatedAt = DateTime.Now,
                    CreatedBy = "oromar.melo@gmail.com",
                    Address = companyAddress[i],
                    City = companyCities[i],
                    Country = "Brasil",
                    CurrencyId = currencyIds[0],
                    FoundationDate = DateTime.Now.AddYears(-10).AddMonths(-3).AddDays(-2),
                    HasStrangers = true,
                    Nacionality = "Brasileira",
                    Neighborhood = neighborhoods[i],
                    PersonalJuridicalName = personalJuridicalNames[i],
                    OccupationArea = occupationAreas[i],
                    SocialReason = socialReasons[i],
                    State = "Pernambuco",
                    IsDeleted = false,
                    SearchFields = companyNames[i].RemoveDiacritics().Trim()
                };
                await new GenericDAO<Company>(_context).Create(company);
            }

            var companies = _context.Company.ToList();
            foreach (var company in companies)
            {

            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
