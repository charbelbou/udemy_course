namespace udemy_course1.Controllers.Resources
{
    // Vehicle Query Resource (Same as VehicleQuery)
    public class VehicleQueryResource
    {
        public int? MakeId { get; set; }
        public int? ModelId { get; set; }
        public string SortBy { get; set; }
        public bool isSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

    }
}