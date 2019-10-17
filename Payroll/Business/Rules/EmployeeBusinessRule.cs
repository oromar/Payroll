using System.Linq;
using Payroll.Data;
using Payroll.Models;

namespace Payroll.Business
{
    public class CreateEmployeeBusinessRule : BusinessRule<Employee>
    {
        public CreateEmployeeBusinessRule(GenericDAO<Employee> dao) : base(dao)
        {
        }
        public override void Apply(Employee entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Name))
                throw new BusinessException($"{Resource.RequiredField}: {nameof(Employee.Name)}");
            if (dao.GetContext().Employee.Any(a => a.PersonalDocument == entity.PersonalDocument))
                throw new BusinessException($"CPF já existente: {entity.PersonalDocument}");
        }
    }

    public class EditEmployeeBusinessRule : BusinessRule<Employee>
    {
        public EditEmployeeBusinessRule(GenericDAO<Employee> dao) : base(dao)
        {
        }

        public override void Apply(Employee entity)
        {
            if (dao.GetContext().Employee.Where(a => a.Id != entity.Id).Any(a => a.PersonalDocument == entity.PersonalDocument))
                throw new BusinessException($"CPF já existente: {entity.PersonalDocument}");

            dao
            .GetContext()
            .Certification
            .RemoveRange(dao
                .GetContext()
                .Certification
                .Where(a => a.EmployeeId == entity.Id));

        }
    }

    public class DeleteEmployeeBusinessRule : BusinessRule<Employee>
    {
        public DeleteEmployeeBusinessRule(GenericDAO<Employee> dao) : base(dao)
        {
        }

        public override void Apply(Employee entity)
        {
            
        }
    }
}