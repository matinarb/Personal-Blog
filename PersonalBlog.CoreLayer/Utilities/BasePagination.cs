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
    public void GeneratePaging(IQueryable<Object> data, int take, int PageId)
    {
        var entityCount = data.Count();
        var pageCount = (int)Math.Ceiling(entityCount / (double)take);
        PageCount = pageCount;
        EntityCount = entityCount;
        CurrentPage = PageId;
        CurrentPage = (PageId > PageCount) ? PageCount : PageId;
        CurrentPage = (PageId < 1) ? 1 : PageId;
        Take = take;
        Skip = (CurrentPage - 1) * Take;
        EndPage = (CurrentPage + 5 > pageCount) ? pageCount : CurrentPage + 5;
        StartPage = (CurrentPage - 4 <= 0) ? 1 : CurrentPage - 4;
    }

}