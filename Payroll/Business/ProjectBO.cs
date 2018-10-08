using Payroll.Data;
using Payroll.Models;

namespace Payroll.Business
{
    public class ProjectBO: BusinessObject<Project>
    {
        public ProjectBO(GenericDAO<Project> dao): base(dao)
        {
        }
    }
}
