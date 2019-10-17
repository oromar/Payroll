using System.Linq;
using Payroll.Data;
using Payroll.Models;

namespace Payroll.Business
{
    public class CreateWorkHoursBusinessRule : BusinessRule<WorkHours>
    {
        public CreateWorkHoursBusinessRule(GenericDAO<WorkHours> dao) : base(dao)
        {
        }

        public override void Apply(WorkHours entity)
        {
        }
    }

    public class EditWorkHoursBusinessRule : BusinessRule<WorkHours>
    {
        public EditWorkHoursBusinessRule(GenericDAO<WorkHours> dao) : base(dao)
        {
        }

        public override void Apply(WorkHours entity)
        {
            dao
                .GetContext()
                .WorkHoursEmployee
                .RemoveRange(dao
                    .GetContext()
                    .WorkHoursEmployee
                    .Where(a => a.WorkHoursId == entity.Id));
            dao
                .GetContext()
                .WorkHourItems
                .RemoveRange(dao
                    .GetContext()
                    .WorkHourItems
                    .Where(a => a.WorkHoursId == entity.Id));
        }
    }
    public class DeleteWorkHoursBusinessRule : BusinessRule<WorkHours>
    {
        public DeleteWorkHoursBusinessRule(GenericDAO<WorkHours> dao) : base(dao)
        {
        }

        public override void Apply(WorkHours entity)
        {
            throw new System.NotImplementedException();
        }
    }
}