using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using MMLib.Extensions;

namespace Payroll.Models
{
    public abstract class Basic
    {
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
        public string SearchFields { get; set; }
        public List<RelatedItem> GetRelatedItems()
        {
            var result = new List<RelatedItem>();

            foreach (var property in GetType().GetProperties().Where(a => a.GetGetMethod().IsVirtual))
            {
                result.Add(new RelatedItem
                {
                    Name = property.Name,
                    Type = property.PropertyType,
                    Value = property.GetValue(this)
                });
            }
            return result;
        }
        public abstract void CreateSearchText();
        public abstract List<string> GetSearchFields();
        public abstract Expression SortBy(string sort);

        //  public void CreateSearchText()
        // {
        //     var values = new List<string>();
        //     var properties = GetType().GetProperties();
        //     var types = new Type[] { typeof(string), typeof(double), typeof(int), typeof(char) };
        //     foreach (var item in properties.Where(a => types.Contains(a.PropertyType)))
        //     {
        //         if (item.GetValue(this) != null)
        //         {
        //             if (item.PropertyType.BaseType == typeof(Basic))
        //             {
        //                 var entity = (Basic)item.GetValue(this);
        //                 values.Add(entity.Name);
        //             }
        //             else if (item.PropertyType.BaseType == typeof(Addressable))
        //             {
        //                 var entity = (Addressable)item.GetValue(this);
        //                 var addressableProperties = entity.GetType().GetProperties().Where(a => types.Contains(a.PropertyType));
        //                 foreach (var property in addressableProperties)
        //                 {
        //                     var value = entity.GetPropertyValue(property.Name);
        //                     if (value != null)
        //                     {
        //                         values.Add(value.ToString());
        //                     }
        //                 }
        //             }
        //             else if (types.Contains(item.PropertyType))
        //             {
        //                 values.Add(item.GetValue(this).ToString());
        //             }
        //         }
        //     }
        //     SearchFields = string.Join(" ", values.ToArray());
        // }
    }

    public class RelatedItem
    {
        public Type Type { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
