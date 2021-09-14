using System.Collections.Generic;

namespace udemy_course1.Controllers.Resources
{
    // Query Result Resource, same as Query Result
    public class QueryResultResource<T>
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}