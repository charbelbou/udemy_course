using udemy_course1.Extensions;

namespace udemy_course1.Core.Models
{
    // Implementation of IQueryObject
    public class VehicleQuery : IQueryObject
    {
        // Vehicle MakeId
        public int? MakeId { get; set; }
        // Vehicle ModelId
        public int? ModelId { get; set; }
        public string SortBy { get; set; }
        public bool isSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}