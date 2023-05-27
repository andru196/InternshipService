using AutoMapper;
using DataModel.Context;
using DataModel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace InternshipService.Controllers
{
	[Authorize]
	public class ControllerBasePlusAuth : ControllerBasePlus
	{
		public ControllerBasePlusAuth(ILogger logger, InternshipsDbContect context, IMapper mapper) : base(logger, context, mapper) { }
	}

	public class ControllerBasePlus : ControllerBase
	{
		protected readonly InternshipsDbContect _dbContext;
		protected readonly ILogger _logger;
		protected IMapper _mapper;
		protected UserServiceIdentity Identity
		{
			get
			{
				if (User.Identity is ClaimsIdentity identity)
				{
					var userRole = Enum.Parse<UserType>(identity.Claims.First(x => x.Type == ClaimTypes.Role).Value, true);
					return new UserServiceIdentity(
						UserId: identity.Claims
							.First(x=>x.Type== ClaimTypes.NameIdentifier).Value,
						Role: userRole,
						DirectionId: userRole == UserType.Mentor ? 
							identity.Claims.First(x => x.Type == "DirectionId").Value : null,
						OrganizationId: userRole == UserType.OrganizationAdmin ?
							identity.Claims.First(x => x.Type == "OrganizationId").Value : null
						);
				}
				throw new HttpException(System.Net.HttpStatusCode.Unauthorized);
			}
		}
		public ControllerBasePlus(ILogger logger, InternshipsDbContect context, IMapper mapper) 
		{
			(_logger, _dbContext, _mapper)  = (logger, context, mapper);
		}
	}

	public record UserServiceIdentity(string UserId,
		UserType Role, 
		string? DirectionId = null, 
		string? OrganizationId = null);
}
