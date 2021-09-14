using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using udemy.Models;
using udemy_course1.Core.Models;

namespace udemy_course1.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject queryObject ,Dictionary<string,Expression<Func<T,object>>> columnMaps){
            // If sortBy is null, or columnMaps doesnt contain the sortby, then just return the query
            if(String.IsNullOrEmpty(queryObject.SortBy) || !columnMaps.ContainsKey(queryObject.SortBy)){
                return query;
            }
            // If SortAscending, then order normally
            if(queryObject.isSortAscending){
                return query.OrderBy(columnMaps[queryObject.SortBy]);
            }
            // Otherwise order by descending order
            else{
                return query.OrderByDescending(columnMaps[queryObject.SortBy]);
            }
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IQueryObject queryObject){
            // If page is set to 0 or less, set it to 1
            if( queryObject.Page <= 0){
                queryObject.Page = 1;
            }
            // If page size is set to 0 or less, set it to 10
            if(queryObject.PageSize<=0){
                queryObject.PageSize = 10;
            }
            return query.Skip((queryObject.Page-1)*queryObject.PageSize).Take(queryObject.PageSize);
        }
    }
}