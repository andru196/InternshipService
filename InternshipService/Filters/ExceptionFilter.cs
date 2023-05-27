using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace InternshipService.Filters
{
	public class ExceptionFilter : IActionFilter, IOrderedFilter
	{
		public int Order => 1;

		public void OnActionExecuted(ActionExecutedContext context)
		{
			switch (context.Exception)
			{
				case HttpException ex:
					{
						context.Result = new JsonResult(new BadRequestMessageResult { Message = context.Exception.PrettyMessage() })
						{
							StatusCode = ex.StatusCode
						};

						context.ExceptionHandled = true;
						break;
					}
				case DbUpdateException ex:
					{
						context.Result = new JsonResult(new BadRequestMessageResult { Message = context.Exception.PrettyMessage(), Exception = new HttpError(ex) })
						{ StatusCode = 400 };
						context.ExceptionHandled = true;
						break;
					}
			default: break;
			}
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			
		}
	}
}
