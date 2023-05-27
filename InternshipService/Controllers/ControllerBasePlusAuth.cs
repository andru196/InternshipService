using AutoMapper;
using DataModel.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipService.Controllers
{
	[Authorize]
	public class ControllerBasePlusAuth : ControllerBasePlus
	{
		public ControllerBasePlusAuth(ILogger logger, InternshipsDbContect context, IMapper mapper) : base(logger, context, mapper) { }
	}

	public class ControllerBasePlus: ControllerBase
	{
		protected readonly InternshipsDbContect _dbContext;
		protected readonly ILogger _logger;
		protected IMapper _mapper;
		public ControllerBasePlus(ILogger logger, InternshipsDbContect context, IMapper mapper) => (_logger, _dbContext, _mapper) = (logger, context, mapper);
	}
}
