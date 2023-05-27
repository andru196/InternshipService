
using AutoMapper;
using DataModel;
using InternshipService;
using InternshipService.Controllers;
using InternshipService.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthorization();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidIssuer = AuthOptions.ISSUER,
		ValidateAudience = true,
		ValidAudience = AuthOptions.AUDIENCE,
		ValidateLifetime = true,
		IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
		ValidateIssuerSigningKey = true,
	};
});
builder.Services.AddControllers(o =>
{
	o.Filters.Add(typeof(InternshipService.Filters.ExceptionFilter));
}).AddJsonOptions(x => x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bearer", Version = "v1" });
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		BearerFormat = "JWT",
		Type = SecuritySchemeType.Http,
		Scheme = "Bearer",
		In = ParameterLocation.Header,
		Description = "Basic Authorization header using the Bearer scheme.",
		//OpenIdConnectUrl = new Uri("https://localhost:44387/Auth")
	});
	c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						  new OpenApiSecurityScheme
							{
								Reference = new OpenApiReference
								{
									Type = ReferenceType.SecurityScheme,
									Id = "Bearer"
								},
								Scheme = "oauth2",
			  Name = "Bearer",
			  In = ParameterLocation.Header,
							},
							new string[] {}
					}
				});
});
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddLogging(config =>
{
	config.AddDebug();
	config.AddConsole();
});
builder.Services.AddTransient<ILogger>(x => x.GetService<ILoggerFactory>().CreateLogger<Program>());

builder.Services.AddSingleton(new MapperConfiguration(mc =>
{
	mc.AddProfile(new MappingProfile());
}).CreateMapper());

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger(c =>
	{
	});
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("v1/swagger.json", $"InternshipService {GetAssemblyVersion()}");
		c.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Example);
	});
}

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthorization();
app.UseMiddleware<IdentityMiddleware>();
app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
});


app.Run();

string GetAssemblyVersion()
{
	return typeof(ControllerBasePlusAuth).Assembly.GetName().Version.ToString();
}