using System.Linq.Expressions;

namespace SchoolSchedule.Domain.Extensions;

public static class QueryExtension
{
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool condition,
        Expression<Func<T, bool>> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }
}