using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Payroll.Data;
using Payroll.Models;

namespace Payroll.Business
{
    public class VacationBO : BusinessObject<Vacation>
    {

        public VacationBO(GenericDAO<Vacation> dao) : base(dao)
        {

        }

        public override Task<Vacation> Edit(Guid id, Vacation data, string userIdentity)
        {
            _dao
                .GetContext()
                .VacationEmployee
                .RemoveRange(_dao
                    .GetContext()
                    .VacationEmployee
                    .Where(a => a.VacationId == id));

            return base.Edit(id, data, userIdentity);
        }

        public override async Task<List<Vacation>> Search(int page = 1, string filter = "", string sort = "", string order = "ASC")
        {
            var result = await base.Search(page, filter, sort, order);

            result.ForEach(a =>
            {
                a.Employees = _dao
                .GetContext()
                .VacationEmployee
                .Include(b => b.Employee)
                .Where(b => b.VacationId == a.Id)
                .ToList();
            });

            return result;
        }
    }
}
