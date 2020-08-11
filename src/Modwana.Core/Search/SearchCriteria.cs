using Modwana.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Modwana.Core.Search
{
    public class SearchCriteria<T> where T : class
    {
        public Expression<Func<T, bool>> FilterExpression { get; set; }

        public Func<IQueryable<T>, IOrderedQueryable<T>> SortExpression { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public int StartIndex { get { return PageSize * (PageNumber - 1); } }

        public SearchCriteria()
        {
            this.PageSize = 10;
            this.PageNumber = 1;
        }

        public SearchCriteria(int? pageNumber)
        {
            this.PageSize = 10;
            this.PageNumber = pageNumber ?? 1;
        }

        public SearchCriteria(int pageSize, int pageNumber)
        {
            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
        }

        public SearchCriteria(int pageSize, int pageNumber, Expression<Func<T, bool>> filterExpression) : this(pageSize, pageNumber)
        {
            this.FilterExpression = filterExpression;

        }

        public SearchCriteria(Expression<Func<T, bool>> filterExpression) : this()
        {
            this.FilterExpression = filterExpression;

        }

        public SearchCriteria(int pageSize, int pageNumber, Func<IQueryable<T>, IOrderedQueryable<T>> sortExpression, Expression<Func<T, bool>> filterExpression) : this(pageSize, pageNumber)
        {
            this.FilterExpression = filterExpression;
            this.SortExpression = sortExpression;
        }

        public void AddAndFilter(Expression<Func<T, bool>> filter)
        {
            if (FilterExpression == null)
                FilterExpression = filter;
            else
                FilterExpression = FilterExpression.And(filter);

        }

        public void AddOrFilter(Expression<Func<T, bool>> filter)
        {
            if (FilterExpression == null)
                FilterExpression = filter;
            else
                FilterExpression = FilterExpression.Or(filter);

        }

    }
}
