using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using vega.Models;

namespace vega.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, SortQuery sortQuery, Dictionary<string, Expression<Func<T, object>>> sortLogic)
        {

            if (string.IsNullOrEmpty(sortQuery.SortBy) || !sortLogic.ContainsKey(sortQuery.SortBy)) return query;

            if (sortQuery.IsSortByAsc)
                return query.OrderBy(sortLogic[sortQuery.SortBy]);
            else
                return query.OrderByDescending(sortLogic[sortQuery.SortBy]);
        }
    }
}
