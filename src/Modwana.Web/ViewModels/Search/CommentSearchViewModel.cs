using Modwana.Core.Search;
using Modwana.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modwana.Web.ViewModels
{
    public class CommentSearchViewModel : BaseSearchViewModel<Comment>
    {
        public string BlogId { get; set; }

        public override SearchCriteria<Comment> ToSearchModel()
        {
            if (!string.IsNullOrWhiteSpace(BlogId))
                AddAndFilter(a => a.BlogId == BlogId);

            return this;
        }
    }
}
