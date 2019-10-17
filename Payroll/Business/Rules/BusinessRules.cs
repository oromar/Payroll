using Payroll.Data;
using Payroll.Models;

namespace Payroll.Business
{
    public abstract class BusinessRule<T> where T : Basic
    {
        protected readonly GenericDAO<T> dao;
        public BusinessRule(GenericDAO<T> dao)
        {
            this.dao = dao;
        }

        public abstract void Apply(T entity);
    }
}
