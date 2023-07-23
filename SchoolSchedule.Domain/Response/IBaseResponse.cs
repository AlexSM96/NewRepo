using SchoolSchedule.Domain.Enum;

namespace SchoolSchedule.Domain.Response;

public interface IBaseResponse<T>
{
    public string Description { get; }

    public StatusCode StatusCode { get; }

    public T Data { get; }
}