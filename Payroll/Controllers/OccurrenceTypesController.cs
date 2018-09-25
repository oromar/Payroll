using Payroll.Business;
using Payroll.Common;
using Payroll.Models;

namespace Payroll.Controllers
{
    public class OccurrenceTypesController : GenericController<OccurrenceType>
    {
        public OccurrenceTypesController(BusinessObject<OccurrenceType> businessObject, Message message) 
            : base(businessObject, message) { }
    }
}
