using DataModel.Models;
using InternshipService.Controllers;

namespace InternshipService.Extensions
{
	internal static class EnitiesFiltersExtension
	{
		public static IQueryable<User> FilterByUser(this IQueryable<User> source, UserServiceIdentity user)
			=> user.Role switch
			{
				UserType.Admin => source,
				_ => source.Where(x=>x.Guid == new Guid(user.UserId)),
			};

		public static IQueryable<Event> FilterByUser(this IQueryable<Event> source, UserServiceIdentity user)
			=> user.Role switch
			{
				UserType.Admin => source,
				_ => source,
			};

		public static IQueryable<Intern> FilterByUser(this IQueryable<Intern> source, UserServiceIdentity user)
			=> user.Role switch
			{
				UserType.Admin => source,
				_ => source
			};

		public static IQueryable<InternRequest> FilterByUser(this IQueryable<InternRequest> source, UserServiceIdentity user)
			=> user.Role switch
			{
				UserType.Admin => source,
				UserType.Mentor => source.Where(x=>x.DirectionId == new Guid(user.DirectionId)),
				UserType.OrganizationAdmin => source.Where(x=>x.OrganizationId == new Guid(user.OrganizationId)),
				_ => source.Where(x=>x.CreatedByGuid == new Guid(user.UserId)),
			};

		public static IQueryable<InternResponse> FilterByUser(this IQueryable<InternResponse> source, UserServiceIdentity user)
			=> user.Role switch
			{
				UserType.Admin => source,
				UserType.Mentor => source.Where(x => x.InternRequest.DirectionId == new Guid(user.DirectionId)),
				UserType.OrganizationAdmin => source.Where(x => x.InternRequest.OrganizationId == new Guid(user.OrganizationId)),
				_ => source.Where(x => x.InternId == new Guid(user.UserId)),
			};
	}
}
