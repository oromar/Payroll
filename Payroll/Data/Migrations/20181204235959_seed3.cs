using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using MMLib.Extensions;
using Payroll.Business;
using Payroll.Common;
using Payroll.Models;

namespace Payroll.Data.Migrations
{
    public partial class seed3 : Migration
    {

        private readonly ApplicationDbContext _context;

        public seed3()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-Payroll-B7711109-52C7-47B5-9040-04D860B59D94;Trusted_Connection=True;MultipleActiveResultSets=true");
            _context = new ApplicationDbContext(optionsBuilder.Options);
        }

        public seed3(ApplicationDbContext context)
        {
            _context = context;
        }
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string[] companyNames = { "Comércio e Indústria de Papéis LTDA", "Agronegócio SA", "Trindade Advogados LTDA", "UNIDAT SA", "Sá e Souza SA" };
            string[] companyAddress = { "Rua do Hospício", "Rua do Aragão", "Rua A", "Av Dantas Barreto", "Avenida Presidente Kennedy" };
            string[] neighborhoods = { "Boa Vista", "Boa Vista", "Santo Antonio", "Sao José", "Peixinhos" };
            string[] companyCities = { "Recife", "Recife", "Recife", "Recife", "Olinda" };
            string[] personalJuridicalNames = { "19203292000198", "48306438000129", "01724647000129", "04560384000158", "12023908000108" };
            string[] occupationAreas = { "Indústria de Papel", "Agropecuária", "Advocacia", "Universidade", "Comércio" };
            string[] socialReasons = { "Papel macio", "Engordando o gado", "Trindade e Amigos", "Universidade", "Compra e venda" };
            string[] departments = {"Gerência Operacional", "Vendas", "TI", "Compras", "Marketing", "Diretoria"};
            string[] jobRoles = {"Analista", "Técnico", "Auxiliar", "Assistente"};
            string[] functions = {"Gerente de TI", "Gerente Técnico", "Diretor Executivo", "Técnico de Enfermagem", "Técnico de Manutenção", "Auxiliar de Cozinha", "Técnico de Eletricidade"};
            string[] workplaces = {"SEDE", "FILIAL 1", "FILIAL 2", "FILIAL 3", "FILIAL 4"};
            string[] employees = {"João da Silva Xavier", "José de Oliveira e Silva", "Maria das Dores Almeida", "Ana Maria Cavalcante", "Severina José dos Santos"};

            var currencyIds = _context.Currency.Select(a => a.Id).ToArray();

            for (var i = 0; i < 3; i++)
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
                    Complement = null,
                    Number = null,
                    PersonalJuridicalName = personalJuridicalNames[i],
                    OccupationArea = occupationAreas[i],
                    SocialReason = socialReasons[i],
                    State = "Pernambuco",
                    IsDeleted = false,                                      
                };
                var x = new GenericDAO<Company>(_context).Create(company).Result;

                for(var j =0; j < departments.Length; j++)
                {
                    var department = new Department
                    {
                        Name=departments[j],
                        CompanyId = company.Id,
                        Description = "Descrição de " + departments[j],
                        IsManagerDepartment =  departments[j].Contains("Gerência"),
                        IsDirectionDepartment = departments[j].Contains("Diretoria"),
                        IsOperationalDepartment = !departments[j].Contains("Gerência") && departments[j].Contains("Diretoria"),
                        CreatedAt = DateTime.Now,
                        CreatedBy = "oromar.melo@gmail.com"
                    };

                     var y = new GenericDAO<Department>(_context).Create(department).Result;
                }

                for(var k =0; k < jobRoles.Length; k++) 
                {
                    var jobRole = new JobRole
                    {
                        Name = jobRoles[k],
                        CreatedAt = DateTime.Now,
                        CreatedBy = "oromar.melo@gmail.com",
                        CompanyId = company.Id,
                        Description = "Descrição do " + jobRoles[k]
                    };

                    var z = new GenericDAO<JobRole>(_context).Create(jobRole).Result;
                }

                for(var l =0; l < functions.Length; l++) 
                {
                    var function = new Function
                    {
                        Name = functions[l],
                        CompanyId = company.Id,
                        CreatedAt = DateTime.Now,
                        CreatedBy = "oromar.melo@gmail.com",
                        Description = "Descrição de " + functions[l],
                        ManagerCommission = functions[l].Contains("Gerente") ? 1000 : 0,
                        IsManagerFunction = functions[l].Contains("Gerente"),
                        HasDangerous = functions[l].Contains("Eletricidade"),
                        HasUnhealthy = functions[l].Contains("Enfermagem")
                    };
                    var w = new GenericDAO<Function>(_context).Create(function).Result;
                }

                for(var m =0; m < workplaces.Length; m++) 
                {
                    var workplace = new Workplace
                    {
                        CompanyId = company.Id,
                        Name= workplaces[m],
                        CreatedAt = DateTime.Now,
                        CreatedBy = "oromar.melo@gmail.com",
                        Complement = null,
                        Number = null,
                        Address = companyAddress[m],
                        Neighborhood = neighborhoods[m],
                        City = companyCities[m],
                        State = "Pernambuco",
                        Country = "Brasil"
                    };
                    var k =  new GenericDAO<Workplace>(_context).Create(workplace).Result;
                }

                var jobRolesFromDB = _context.JobRole.Where(a => a.CompanyId == company.Id).Take(1).ToList();
                var functionsFromDB = _context.Function.Where(a => a.CompanyId == company.Id).Take(2).ToList();
                var workPlacesFromDB = _context.Workplace.Where(a => a.CompanyId == company.Id).Take(2).ToList();
                var departmentsFromDB = _context.Department.Where(a => a.CompanyId == company.Id).ToList();
                var occupationsFromDB = _context.Occupation.Take(2).ToList();

                foreach(var department in departmentsFromDB) 
                {
                    foreach(var workplace in workPlacesFromDB) 
                    {
                        foreach(var jobRole in jobRolesFromDB)  
                        {
                            foreach(var function in functionsFromDB)
                            {
                                foreach(var occupation in occupationsFromDB)
                                {
                                    var random = new Random().Next(0, employees.Length);

                                        var employee = new Employee
                                        {
                                            Name = employees[random],
                                            EmployeeNumber = new string(Guid.NewGuid().ToString().Where(Char.IsDigit).Take(8).ToArray()),
                                            IDName = employees[random],
                                            CompanyId = company.Id,
                                            DepartmentId = department.Id,
                                            JobRoleId = jobRole.Id,
                                            FunctionId = function.Id,
                                            WorkplaceId = workplace.Id,
                                            AdmissionalDate = DateTime.Now.AddYears(-random).AddMonths(-random).AddDays(-random),
                                            OccupationId = occupation.Id,
                                            Salary = new Random().Next(100, 10000),
                                            Nationality = "Brasileira",
                                            Gender = random <= 1 ? Gender.MASC : Gender.FEM,
                                            PersonalDocument = new string(Guid.NewGuid().ToString().Where(Char.IsDigit).Take(11).ToArray()),
                                            PhoneNumber = new string(Guid.NewGuid().ToString().Where(Char.IsDigit).Take(10).ToArray()),
                                            Address = "Rua a " + random  + " " + employees[random],
                                            Neighborhood = "Bairro " + random,
                                            Complement = null,
                                            Number = null,
                                            City = "Recife",
                                            State = "Pernambuco",
                                            Country = "Brasil",
                                            CreatedAt = DateTime.Now,
                                            CreatedBy = "oromar.melo@gmail.com",
                                            DateBirth = DateTime.Now.AddYears(-(20+random)).AddMonths(-3)
                                        };
                                        var xyz = new GenericDAO<Employee>(_context).Create(employee).Result;
                                }
                            }
                        }
                    }
                }
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
