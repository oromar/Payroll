using Payroll.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Payroll.Models
{
    public class Employee : Addressable
    {
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        [Display(ResourceType =typeof(Resource), Name ="Company")]
        public Guid CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        [Display(ResourceType = typeof(Resource), Name = "Department")]
        public Guid DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        [Display(ResourceType = typeof(Resource), Name = "JobRole")]
        public Guid JobRoleId { get; set; }
        [ForeignKey("JobRoleId")]
        public virtual JobRole JobRole { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        [Display(ResourceType = typeof(Resource), Name = "Workplace")]
        public Guid WorkplaceId { get; set; }
        [ForeignKey("WorkplaceId")]
        public virtual Workplace Workplace { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        [Display(ResourceType = typeof(Resource), Name = "Function")]
        public Guid FunctionId { get; set; }
        [ForeignKey("FunctionId")]
        public virtual Function Function { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Manager")]
        public Guid? ManagerId { get; set; }
        [ForeignKey("ManagerId")]
        public virtual Employee Manager { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        [Display(ResourceType = typeof(Resource), Name = "Occupation")]
        public Guid OccupationId { get; set; }
        [ForeignKey("OccupationId")]
        public Occupation Occupation { get; set; }

        public virtual IEnumerable<EmployeeHistory> Occurrences { get; set; }
        public virtual IEnumerable<Employee> Subordinates { get; set; }
        public virtual IEnumerable<ProjectEmployee> Projects { get; set; }
        public virtual IEnumerable<VacationEmployee> Vacations { get; set; }
        public virtual IEnumerable<WorkHoursEmployee> WorkHours { get; set; }
        public virtual IEnumerable<Certification> Certifications {get; set;}

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        [Display(ResourceType = typeof(Resource), Name = "IDName")]
        public string IDName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        [Display(ResourceType = typeof(Resource), Name = "Nationality")]
        public string Nationality { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        [Display(ResourceType = typeof(Resource), Name = "EmployeeNumber")]
        public string EmployeeNumber { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        [Display(ResourceType = typeof(Resource), Name = "DateBirth")]
        public DateTime? DateBirth { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        [Display(ResourceType = typeof(Resource), Name = "AdmissionalDate")]
        public DateTime? AdmissionalDate { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        [Display(ResourceType = typeof(Resource), Name = "PersonalDocument")]
        public string PersonalDocument { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        [Display(ResourceType = typeof(Resource), Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "Salary")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public double Salary { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Gender")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public Gender Gender { get; set; }

        public override Expression SortBy(string sort)
        {
            Expression<Func<Employee, object>> result = null;

            switch (sort)
            {
                case Constants.SORT_COMPANY_NAME:
                    result = a => a.Company.Name;
                    break;

                case Constants.SORT_SALARY:
                    result = a => a.Salary;
                    break;

                case Constants.SORT_CREATED_BY:
                    result = a => a.CreatedBy;
                    break;

                case Constants.SORT_EMPLOYEE_NUMBER:
                    result = a => a.EmployeeNumber;
                    break;

                default:
                    result = a => a.Name;
                    break;
            }

            return result as Expression;
        }
    }
}
