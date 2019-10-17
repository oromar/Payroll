using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Payroll.Data;
using Payroll.Models;

namespace Payroll.Business
{
    public class ProjectBO : BusinessObject<Project>
    {

        public ProjectBO(GenericDAO<Project> dao, CreateProjectBusinessRule createRule,
        EditProjectBusinessRule editRule, DeleteProjectBusinessRule deleteRule) : base(dao, createRule, editRule, deleteRule)
        {
        }

        public override async Task<List<Project>> Search(int page = 1, string filter = "", string sort = "", string order = "ASC")
        {
            var result = await base.Search(page, filter, sort, order);

            result.ForEach(a =>
            {
                a.Employees = _dao
                .GetContext()
                .ProjectEmployee
                .Include(b => b.Employee)
                .Where(b => b.ProjectId == a.Id)
                .ToList();
            });


            return result;

        }

    }
}
