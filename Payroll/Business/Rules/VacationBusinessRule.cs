using System.Linq;
using Payroll.Data;
using Payroll.Models;

namespace Payroll.Business
{
    public class CreateVacationBusinessRule : BusinessRule<Vacation>
    {
        public CreateVacationBusinessRule(GenericDAO<Vacation> dao) : base(dao)
        {
        }

        public override void Apply(Vacation entity)
        {
        }
    }

    public class EditVacationBusinessRule : BusinessRule<Vacation>
    {
        public EditVacationBusinessRule(GenericDAO<Vacation> dao) : base(dao)
        {
        }

        public override void Apply(Vacation entity)
        {
            dao
                .GetContext()
                .VacationEmployee
                .RemoveRange(dao
                    .GetContext()
                    .VacationEmployee
                    .Where(a => a.VacationId == entity.Id));
        }
    }

    public class DeleteVacationBusinessRule : BusinessRule<Vacation>
    {
        public DeleteVacationBusinessRule(GenericDAO<Vacation> dao) : base(dao)
        {
        }

        public override void Apply(Vacation entity)
        {
        }
    }
}