using Microsoft.AspNetCore.Mvc.Rendering;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Payroll.Common
{
    public static class Utils
    {
        public static Action<Employee> GetEmployeeOption => 
            a => a.Name = a.Name + " | " + @Resource.EmployeeNumber + ": " + a.EmployeeNumber;

        public static List<SelectListItem> GetOptions<T>(IEnumerable<T> items) where T : Basic
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
            }));

            return result;
        }
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
        public static List<SelectListItem> GetGenders()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = Gender.MASC.GetDescription(),
                    Value = ((int)Gender.MASC).ToString()
                },
                new SelectListItem
                {
                    Text = Gender.FEM.GetDescription(),
                    Value = ((int)Gender.FEM).ToString()
                }
            };
        }
        public static List<SelectListItem> GetDaysOfWeek()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = DayOfWeek.SUNDAY.GetDescription(),
                    Value = ((int)DayOfWeek.SUNDAY).ToString()
                },
                new SelectListItem
                {
                    Text = DayOfWeek.MONDAY.GetDescription(),
                    Value = ((int)DayOfWeek.MONDAY).ToString()
                },
                new SelectListItem
                {
                    Text = DayOfWeek.TUESDAY.GetDescription(),
                    Value = ((int)DayOfWeek.TUESDAY).ToString()
                },
                new SelectListItem
                {
                    Text = DayOfWeek.WEDNESDAY.GetDescription(),
                    Value = ((int)DayOfWeek.WEDNESDAY).ToString()
                },
                new SelectListItem
                {
                    Text = DayOfWeek.THURSDAY.GetDescription(),
                    Value = ((int)DayOfWeek.THURSDAY).ToString()
                },
                new SelectListItem
                {
                    Text = DayOfWeek.FRIDAY.GetDescription(),
                    Value = ((int)DayOfWeek.FRIDAY).ToString()
                },
                new SelectListItem
                {
                    Text = DayOfWeek.SATURDAY.GetDescription(),
                    Value = ((int)DayOfWeek.SATURDAY).ToString()
                },
            };
        }

        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    var attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return Resource.ResourceManager.GetString(attr.Description);
                    }
                }
            }
            return null;
        }

        public static DateTime GetFirstDayOfMonth(this DateTime value) {
            return new DateTime(value.Year, value.Month, 1);
        }

        public static DateTime GetLastDayOfMonth(this DateTime value) {
            return value.AddMonths(1).GetFirstDayOfMonth().AddDays(-1);
        }
    }
}
