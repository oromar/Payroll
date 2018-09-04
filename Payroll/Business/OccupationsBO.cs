using Payroll.Data;
using Payroll.Models;

namespace Payroll.Business
{
    public class OccupationsBO: GenericBO<Occupation>
    {
        public OccupationsBO(ApplicationDbContext context): base(context) {}
    }
}
