namespace SchoolSchedule.Domain.Filters;

public class PagingFilter
{
    public int PageSize { get; set; }

    public int ItemForSkip { get; set; }
}