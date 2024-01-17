using MeetupAPI.Core.Models.Jwt;
using MeetupAPI.Core.Services;
using MeetupAPI.Core.Services.Abstractions;
using MeetupAPI.DAL;
using MeetupAPI.DAL.UnitOfWork;
using MeetupAPI.DAL.UnitOfWork.Abstractions;
using MeetupAPI.PresentationLayer.Middlewares.ExceptionMiddleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace MeetupAPI.PresentationLayer.Extensions
{
    public static class ServicesInjectionExtension
    {
        public static void InjectDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<MeetupDatabaseContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>().AddRoles<IdentityRole<Guid>>().AddEntityFrameworkStores<MeetupDatabaseContext>();
        }

        public static void InjectBusinessLogicLayer(this IServiceCollection services)
        {
            services.AddScoped<IMeetupService, MeetupService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IOrganizerService, OrganizerService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static void InjectAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection("JWTOptions"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWTOptions:SecretKey").Value!)),
                    ValidIssuer = configuration.GetSection("JWTOptions:Issuer").Value!,
                    ValidAudience = configuration.GetSection("JWTOptions:Audience").Value!,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static void InjectSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "MeetupAPI", 
                    Version = "v1",
                    Description = "CRUD Web API for meetups manipulation"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = "Please, enter token(e.g. 'Bearer [token]')",
                    Name = "Auth",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}
