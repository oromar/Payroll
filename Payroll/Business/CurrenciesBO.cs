using Payroll.Data;
using Payroll.Models;

namespace Payroll.Business
{
    public class CurrenciesBO : GenericBO<Currency>
    {
        public CurrenciesBO(ApplicationDbContext context) : base(context) { }
    }
}
