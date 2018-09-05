using Payroll.Business;
using Payroll.Common;
using Payroll.Models;

namespace Payroll.Controllers
{
    public class LicenseTypesController : GenericController<LicenseType>
    {
        public LicenseTypesController(BusinessObject<LicenseType> licenseTypeBO, Message message)
            :base(licenseTypeBO, message) {}
    }
}
