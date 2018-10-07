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
}
