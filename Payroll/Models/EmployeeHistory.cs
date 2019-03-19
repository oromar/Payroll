using MMLib.Extensions;
using Payroll.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Payroll.Models
{
    public class EmployeeHistory: Basic
    {
        [Display(ResourceType = typeof(Resource), Name = "EmployeeName")]
        public Guid EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "OccurrenceType")]
        public Guid OccurrenceTypeId { get; set; }
        [ForeignKey("OccurrenceTypeId")]       
        public virtual OccurrenceType OccurrenceType { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        [Display(ResourceType = typeof(Resource), Name = "Occurrence")]
        public string Occurrence { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        [Display(ResourceType = typeof(Resource), Name = "StartDate")]
        public DateTime? Start { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        [Display(ResourceType = typeof(Resource), Name = "EndDate")]
        public DateTime? End { get; set; }

        public override void CreateSearchText()
        {
            SearchFields = string.Join(" ", Employee.Name, 
                                            OccurrenceType.Name, 
                                            Occurrence,
                                            CreatedBy)
                                 .RemoveDiacritics();
        }

        public override List<string> GetSearchFields()
        {
            return new List<string>
            {
                Resource.EmployeeName,
                Resource.OccurrenceType,
                Resource.Occurrence,
                Resource.CreatedBy
            };
        }

        public override Expression SortBy(string sort)
        {
            Expression<Func<EmployeeHistory, object>> result = null;

            switch (sort)
            {
                case Constants.SORT_OCCURRENCE_TYPE_NAME:
                    result = a => a.OccurrenceType.Name;
                    break;

                case Constants.SORT_OCCURRENCE:
                    result = a => a.Occurrence;
                    break;

                case Constants.SORT_END_DATE:
                    result = a => a.End;
                    break;

                case Constants.SORT_START_DATE:
                    result = a => a.Start;
                    break;

                case Constants.SORT_EMPLOYEE_NAME:
                    result = a => a.Employee.Name;
                    break;

                case Constants.SORT_CREATED_BY:
                    result = a => a.CreatedBy;
                    break;

                default:
                    result = a => a.Name;
                    break;
            }

            return result as Expression;
        }



    }
}
