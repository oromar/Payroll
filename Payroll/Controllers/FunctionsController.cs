using Payroll.Business;
using Payroll.Common;
using Payroll.Models;

namespace Payroll.Controllers
{
    public class FunctionsController : GenericController<Function>
    {
        public FunctionsController(BusinessObject<Function> businessObject, Message message)
            :base(businessObject, message) { }
    }
}