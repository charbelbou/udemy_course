using System.Collections.Generic;

namespace udemy_course1.Core.Models
{
    // QueryResult
    public class QueryResult<T>
    {
        // Number of items
        public int TotalItems { get; set; }  
        // Collection of items
        public IEnumerable<T> Items { get; set; }
    }
}