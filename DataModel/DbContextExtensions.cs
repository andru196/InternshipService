using DataModel.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataModel
{
	public static class DbContextExtensions
	{
		public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
		{
			string connectionString =
				$"User ID = {configuration["PG_USER"]};" +
				$"Password = {configuration["PG_PASSWORD"]};" +
				$"Database = {configuration["DB_NAME"]};" +
				$"Host = {configuration["DB_HOST"]};" +
				$"Port = {configuration["DB_PORT"]};";
			services.AddDbContext<InternshipsDbContect>(options =>
				options.UseNpgsql(connectionString).UseLazyLoadingProxies(), ServiceLifetime.Transient);
			services.AddTransient<InternshipsDbContect>();
			return services;
		}
	}
}
