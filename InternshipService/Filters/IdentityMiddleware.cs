namespace InternshipService.Filters
{
	public class IdentityMiddleware
	{
		private readonly RequestDelegate next;

		public IdentityMiddleware(RequestDelegate next)
		{
			this.next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			if (context.User.Identity.IsAuthenticated)
			{
				
			}
			await next.Invoke(context);
		}
	}
}
