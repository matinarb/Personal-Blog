namespace PersonalBlog.CoreLayer.Utilities;

public class BasePagination
{
    public int EntityCount { get; set; }
    public int PageCount { get; set; }
    public int StartPage { get; set; }
    public int EndPage { get; set; }
    public int CurrentPage { get; set; }
    public int Take { get; set; }
    public int Skip { get; set; }
    public void GeneratePaging(IQueryable<Object> data, int take, int currentPage)
    {
        var entityCount = data.Count();
        var pageCount = (int)Math.Ceiling(entityCount / (double)take);
        PageCount = pageCount;
        EntityCount = entityCount;
        CurrentPage = currentPage;
        CurrentPage = (currentPage > PageCount) ? PageCount : currentPage;
        CurrentPage = (currentPage < 1) ? 1 : currentPage;
        Take = take;
        Skip = (CurrentPage - 1) * take;
        EndPage = (currentPage + 5 > pageCount) ? pageCount : currentPage + 5;
        StartPage = (currentPage - 4 <= 0) ? 1 : currentPage - 4;
    }

}