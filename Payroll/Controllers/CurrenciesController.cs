using Payroll.Business;
using Payroll.Common;
using Payroll.Models;

namespace Payroll.Controllers
{
    public class CurrenciesController : GenericController<Currency>
    {
        public CurrenciesController(BusinessObject<Currency> currencyBO, Message message): 
            base(currencyBO, message) {}
    }
}
