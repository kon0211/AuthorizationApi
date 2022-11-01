using AuthorizationApi.Domain;
using AuthorizationApi.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace AuthorizationApi
{
    /*
        Ќаписать api проект с авторизацией. 
        ѕользователь должен иметь возможность зарегистрироватьс€, сделать логин и при этом получить jwt токен авторизации. 
        ј также получать и измен€ть информацию о себе (достаточно полей name, surname, phone, email)
         од проекта нужно прислать в виде ссылки на свой публичный репозиторий в git.
    */
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();
            ConfigureRequestPipeline(app);

            app.Run();
        }

        /// <summary>
        /// Configure services)
        /// </summary>
        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization();
            services.AddEndpointsApiExplorer();
            #region swagger
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            #endregion //swagger
            services.AddControllers();
            services.AddScoped<PasswordHasher<User>>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.Audience = JwtAuthOptions.ISSUER;
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = JwtAuthOptions.ISSUER,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = JwtAuthOptions.GetSymmetricSecurityKey(),

                        ValidateLifetime = true,
                    };
                });


            services.AddDbContextPool<UserDbContext>((serviceProvder, opt) =>
            {
                var connectionString = configuration.GetConnectionString("Default");
                opt.UseSqlServer(connectionString);
            });

            services.AddScoped<IUserRepository, MsSqlUserRepository>();
        }

        /// <summary>
        /// Configure the HTTP request pipeline.
        /// </summary>
        private static void ConfigureRequestPipeline(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();


            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(config =>
            {
                config.MapControllers();
            });
        }

    }
}