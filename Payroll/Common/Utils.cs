using Microsoft.AspNetCore.Mvc.Rendering;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Common
{
    public static class Utils
    {
        public static List<SelectListItem> GetOptions<T>(IQueryable<T> items) where T : Basic
        {
            var firstOption = new SelectListItem
            {
                Value = Constants.VALUE_FIRST_SELECT,
                Text = Resource.SelectItem
            };

            var result = new List<SelectListItem>
            {
                firstOption
            };
            result.AddRange(items.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            })
            .AsEnumerable());

            return result;
        }
    }
}
