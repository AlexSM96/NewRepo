namespace SchoolSchedule.Domain.Response;

public class DataTableResult<T>
{
    public T Data { get; set; }

    public int ItemsCount { get; set; }
}