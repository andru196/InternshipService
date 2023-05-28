using DataModel.Models;
using InternshipService.DTO;
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

		public static IQueryable<TSource> WhereNotNull<TSource>(this IQueryable<TSource> source, object? field, Expression<Func<TSource, bool>> predicate)
		{
			if (field != null)
				return Queryable.Where(source, predicate);
			return source;
		}

		public static IQueryable<TSource> WhereTrue<TSource>(this IQueryable<TSource> source, bool flag, Expression<Func<TSource, bool>> predicate)
		{
			if (flag)
				return Queryable.Where(source, predicate);
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


		public static TSource? FirstOrDefaultFromDto<TSource, TSourceDto>(this IEnumerable<TSource> source, TSourceDto dto) where TSourceDto : EntityDto
				where TSource : Entity
		=> source.FirstOrDefault(x => x.Guid == dto.Guid);


		public static async Task<TSource?> FirstOrDefaultFromDtoAsync<TSource, TSourceDto>(this IQueryable<TSource> source, TSourceDto dto) where TSourceDto : EntityDto
				where TSource : Entity
		=> await source.FirstOrDefaultAsync(x => x.Guid == dto.Guid);

	}
}
