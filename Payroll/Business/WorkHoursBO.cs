using Microsoft.EntityFrameworkCore;
using Payroll.Data;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Business
{
    public class WorkHoursBO : BusinessObject<WorkHours>
    {

        public WorkHoursBO(GenericDAO<WorkHours> dao) : base(dao)
        {

        }

        public override Task<WorkHours> Edit(Guid id, WorkHours data, string userIdentity)
        {
            _dao
                .GetContext()
                .WorkHoursEmployee
                .RemoveRange(_dao
                    .GetContext()
                    .WorkHoursEmployee
                    .Where(a => a.WorkHoursId == id));
            _dao
                .GetContext()
                .WorkHourItems
                .RemoveRange(_dao
                    .GetContext()
                    .WorkHourItems
                    .Where(a => a.WorkHoursId == id));

            return base.Edit(id, data, userIdentity);
        }

        public override async Task<List<WorkHours>> Search(int page = 1, string filter = "", string sort = "", string order = "ASC")
        {
            var result = await base.Search(page, filter, sort, order);

            result.ForEach(a =>
            {
                a.WorkHourItems = _dao
                .GetContext()
                .WorkHours
                .Include(b => b.WorkHourItems)
                .First(b => b.Id == a.Id)
                .WorkHourItems
                .DefaultIfEmpty()
                .ToList();
            });

            result.ForEach(a =>
            {
                a.Employees = _dao
                .GetContext()
                .WorkHoursEmployee
                .Include(b => b.Employee)
                .Where(b => b.WorkHoursId == a.Id)
                .ToList();
            });

            return result;
        }
    }
}
