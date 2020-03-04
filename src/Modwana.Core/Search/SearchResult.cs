using System;
using System.Collections.Generic;
using System.Text;

namespace Modwana.Core.Search
{
    public class SearchResult<T> where T : class
    {
        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public SearchCriteria<T> SearchCriteria { get; set; }

        public List<T> Result { get; set; }

        public int TotalResultsCount { get; set; }

        public int PageCount
        {
            get { return (int)Math.Ceiling((double)TotalResultsCount / PageSize); }
        }

        public SearchResult()
        {
            this.Result = new List<T>();
        }

        public SearchResult(SearchCriteria<T> searchCriteria) : this()
        {
            this.PageNumber = searchCriteria.PageNumber;
            this.PageSize = searchCriteria.PageSize;
            this.SearchCriteria = searchCriteria;
        }

    }
}
