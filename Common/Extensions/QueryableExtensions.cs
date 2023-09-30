using Common.Forms;
using System.Linq.Expressions;

namespace Common.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TSource> WhereIf<TSource>(
            this IQueryable<TSource> source,
            bool condition,
            Expression<Func<TSource, bool>> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }

        public static IQueryable<TSource> ApplyPagination<TSource>(
            this IQueryable<TSource> source,
            BaseQuery basePage)
        {
            return source.Skip(basePage.Skip).Take(basePage.Take);
        }
    }
}