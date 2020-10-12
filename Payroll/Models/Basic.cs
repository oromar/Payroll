using MMLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;

namespace Payroll.Models
{
    public abstract class Basic
    {
        private static readonly Type[] typesToSearch = { typeof(string), typeof(int), typeof(double), typeof(decimal) };

        public Guid Id { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public string Name { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "CreatedAt")]
        public DateTime? CreatedAt { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "CreatedBy")]
        public string CreatedBy { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "UpdatedBy")]
        public string UpdatedBy { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "Deleted")]
        public bool IsDeleted { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "DeletedAt")]
        public DateTime? DeletedAt { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "DeletedBy")]
        public string DeletedBy { get; set; }
        [NotMapped]
        public List<RelatedItem> RelatedItems
        {
            get
            {
                return GetType().GetProperties()
                .Where(a => a.GetGetMethod().IsVirtual)
                .Select(a => new RelatedItem
                {
                    Name = a.Name,
                    Type = a.PropertyType,
                    Value = a.GetValue(this)
                })
                .ToList();
            }
        }
        private string _searchText;
        public string SearchText
        {
            get
            {
                return string.Join(" ", GetType()
                    .GetProperties()
                    .Where(a => typesToSearch.Contains(a.PropertyType))
                    .Where(a => a.Name != nameof(SearchText))
                    .Select(a => a.GetValue(this)?.ToString())
                    .Where(a => !string.IsNullOrWhiteSpace(a)));
            }
            set
            {
                this._searchText = value;
            }
        }
        [NotMapped]
        public List<string> SearchFields
        {
            get
            {
                return GetType().GetProperties()
                .Where(a => (a.Name == nameof(Name)|| (a.Name == nameof(CreatedBy)) || a.DeclaringType != typeof(Basic)))
                .Where(a => typesToSearch.Contains(a.PropertyType))
                .Select(a => Resource.ResourceManager.GetString(a.Name))
                .ToList();
            }
        }
        public Expression<Func<T, object>> SortBy<T>(string sort) where T : Basic
        {
            Expression<Func<T, object>> result = null;

            if (GetType().GetProperty(sort) != null)
            {
                result = a => a.GetPropertyValue(sort);
            }
            else
            {
                result = a => a.Name;
            }
            return result;
        }
    }

    public class RelatedItem
    {
        public Type Type { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
