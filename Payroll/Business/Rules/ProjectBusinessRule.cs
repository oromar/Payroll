using System.Linq;
using Payroll.Data;
using Payroll.Models;

namespace Payroll.Business
{
    public class CreateProjectBusinessRule : BusinessRule<Project>
    {
        public CreateProjectBusinessRule(GenericDAO<Project> dao) : base(dao)
        {
        }

        public override void Apply(Project entity)
        {
            
        }
    }

    public class EditProjectBusinessRule : BusinessRule<Project>
    {
        public EditProjectBusinessRule(GenericDAO<Project> dao) : base(dao)
        {
        }

        public override void Apply(Project entity)
        {
            dao
                .GetContext()
                .ProjectEmployee
                .RemoveRange(dao
                    .GetContext()
                    .ProjectEmployee
                    .Where(a => a.ProjectId == entity.Id));

        }
    }

    public class DeleteProjectBusinessRule : BusinessRule<Project>
    {
        public DeleteProjectBusinessRule(GenericDAO<Project> dao) : base(dao)
        {
        }

        public override void Apply(Project entity)
        {
        }
    }
}