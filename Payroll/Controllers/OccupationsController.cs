using Payroll.Business;
using Payroll.Common;
using Payroll.Models;

namespace Payroll.Controllers
{
    public class OccupationsController : GenericController<Occupation>
    {
        public OccupationsController(BusinessObject<Occupation> occupationBO, Message message): 
            base(occupationBO, message) {}
    }
}
