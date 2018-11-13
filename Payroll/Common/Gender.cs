using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Payroll.Common
{
    public enum Gender
    {
        [Description(nameof(Resource.Masc))]
        MASC = 0,
        [Description(nameof(Resource.Fem))]
        FEM  = 1
    }

    public enum DayOfWeek
    {
        [Description(nameof(Resource.Sunday))]
        SUNDAY = 1,
        [Description(nameof(Resource.Monday))]
        MONDAY = 2,
        [Description(nameof(Resource.Tuesday))]
        TUESDAY = 3,
        [Description(nameof(Resource.Wednesday))]
        WEDNESDAY = 4,
        [Description(nameof(Resource.Thursday))]
        THURSDAY = 5,
        [Description(nameof(Resource.Friday))]
        FRIDAY = 6,
        [Description(nameof(Resource.Saturday))]
        SATURDAY = 7
    }
}
