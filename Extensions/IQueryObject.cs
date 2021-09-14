namespace udemy_course1.Extensions
{
    // Interface for QueryObject
    public interface IQueryObject
    {
        // Sort by (Options: make, model,contactname, id)
        string SortBy { get; set; }
        //Ascending or Descending
        bool isSortAscending { get; set; }
        // Amount of pages
        int Page { get; set; }
        // Amount of objects in page
        byte PageSize { get; set; }
    }
}