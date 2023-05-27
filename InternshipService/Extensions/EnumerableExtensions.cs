using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace InternshipService.Extensions
{
	internal static class EnumerableExtensions
	{
		public static IEnumerable<TSource> WhereNotNull<TSource>(this IEnumerable<TSource> source, object? field,Func<TSource, bool> predicate)
		{
			if (field != null)
				return source.Where(predicate);
			return source;
		}

		//public static IQueryable<TSource> TakePage<TSource>(this IQueryable<TSource> source, int page, int pageSize)
		//{
		//	if (page < 1)
		//		page = 1;
		//	if (pageSize > 100)
		//		pageSize = 80;
		//	return source.Skip(page * pageSize - pageSize).Take(pageSize);
		//}

		public static IEnumerable<TSource> TakePage<TSource>(this IEnumerable<TSource> source, int page, int pageSize)
		{
			if (page < 1)
				page = 1;
			if (pageSize > 100)
				pageSize = 80;
			return source.Skip(page * pageSize - pageSize).Take(pageSize);
		}

		public static IQueryable<TEntity> Include<TEntity, TProperty>
			(this IQueryable<TEntity> source,
			object? field,
			Expression<Func<TEntity, TProperty>> navigationPropertyPath)
		where TEntity : class
		{
			if (field != null)
				return source.Include(navigationPropertyPath);
			return source;
		}
	}
}
