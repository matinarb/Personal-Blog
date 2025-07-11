namespace PersonalBlog.CoreLayer.Utilities;

public class BasePagination
{
    public int EntityCount { get; set; }
    public int PageCount { get; set; }
    public int StartPage { get; set; }
    public int EndPage { get; set; }
    public int CurrentPage { get; set; }
    public int Take { get; set; }
    public void GeneratePaging(IQueryable<Object> data, int take, int currentPage)
    {
        var entityCount = data.Count();
        var pageCount = (int)Math.Ceiling(entityCount / (double)take);
        PageCount = pageCount;
        EntityCount = entityCount;
        CurrentPage = currentPage;
        Take = take;
        EndPage = (currentPage + 5 > pageCount) ? pageCount : currentPage + 5;
        StartPage = (currentPage - 4 <= 0) ? 1 : currentPage - 4;
    }

}