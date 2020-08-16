using Modwana.Core.Extensions;
using Modwana.Core.Resources;
using Modwana.Core.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Modwana.Web.ViewModels
{
    public class BaseSearchViewModel<TModel> : SearchCriteria<TModel> where TModel : class
    {
        [Display(Name = nameof(CommonText.Keyword), ResourceType = typeof(CommonText))]
        public virtual string Keyword { get; set; }

        public virtual SearchCriteria<TModel> ToSearchModel()
        {
            
            return this;
        }

        public virtual Dictionary<string, string> ToRouteValueDictionary(bool ignorePageNumber = false)
        {
            var type = this.GetType();

            var properties = type.GetProperties();

            Dictionary<string, string> result = new Dictionary<string, string>();

            foreach (var item in properties)
            {
                if (item.Name == nameof(FilterExpression) ||
                    item.Name == nameof(SortExpression) ||
                    item.Name == nameof(StartIndex))
                    continue;

                if (!ignorePageNumber)
                {
                    if (item.Name == nameof(PageNumber)) continue;
                }

                var value = item.GetValue(this);

                if (value != null)
                {
                    if (value is DateTime date)
                    {
                        result.Add(item.Name, date.ToSystemDate());
                    }
                    else
                    {
                        result.Add(item.Name, value.ToString());
                    }
                }
            }

            return result;
        }

    }
}
