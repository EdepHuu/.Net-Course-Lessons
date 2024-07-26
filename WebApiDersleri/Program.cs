using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApiDersleri.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text;
using Microsoft.OpenApi.Models;

namespace WebApiDersleri
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			// SQL Server baðlantý dizgisi
			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
			builder.Services.AddDbContext<ProductsContext>(x => x.UseSqlServer(connectionString));
			builder.Services.AddIdentity<AppUsers, AppRoles>().AddEntityFrameworkStores<ProductsContext>();
			builder.Services.Configure<IdentityOptions>(options =>
			{

				options.Password.RequiredLength = 6;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireDigit = false;

				options.User.RequireUniqueEmail = true;
				options.Lockout.MaxFailedAccessAttempts = 5;
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);

			}); 
			builder.Services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = false;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidAudience = "",
					ValidAudiences = new string[] { "a", "b" },
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
						builder.Configuration.GetSection("AppSettings:Secret").Value ?? "")),
					ValidateLifetime = true
				};
			});
			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(option =>
			 {
				 option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
				 option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				 {
					 In = ParameterLocation.Header,
					 Description = "Please enter a valid token",
					 Name = "Authorization",
					 Type = SecuritySchemeType.Http,
					 BearerFormat = "JWT",
					 Scheme = "Bearer"
				 });
				 option.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type=ReferenceType.SecurityScheme,
								Id="Bearer"
							}
						},
						new string[]{}
					}
				});
			 });
			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();
			app.MapControllers();
			app.Run();
		}
	}
}
