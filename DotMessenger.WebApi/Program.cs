using DotMessenger.Adapter.EntityFrameworkContexts;
using DotMessenger.Adapter.EntityFrameworkRepositories;
using DotMessenger.Core.Interactors;
using DotMessenger.Core.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace DotMessenger.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            var connectionMessenger = configuration.GetConnectionString("AppConnection");
            
            builder.Services.AddScoped<AccountsInteractor>();
            builder.Services.AddScoped<IAccountsRepository, AccountsRepository>();
            builder.Services.AddScoped<AppRolesInteractor>();
            builder.Services.AddScoped<IAppRolesRepository, AppRolesRepository>();

            builder.Services.AddScoped<ChatsInteractor>();
            builder.Services.AddScoped<IChatsRepository, ChatsRepository>();
            builder.Services.AddScoped<IChatProfilesRepository, ChatProfilesRepository>();

            builder.Services.AddScoped<MessagesInteractor>();
            builder.Services.AddScoped<IMessagesRepository, MessagesRepository>();

            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionMessenger));

            builder.Services.AddSingleton<AuthenticationOptions>(provider => GetAuthenticationOptions(configuration));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            /* Setup authentication start */

            var authOptions = GetAuthenticationOptions(configuration);
            Console.WriteLine(authOptions);

            builder.Services.AddAuthorization();
            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authOptions.Issuer,

                        ValidateAudience = true,
                        ValidAudience = authOptions.Audience,

                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = true,

                        IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                    };
                });

            /* Setup authentication end */

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "DotMessenger", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
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
            app.UseRouting();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseAuthorization();
            
            app.MapControllers();

            app.Run();
        }

        public static AuthenticationOptions GetAuthenticationOptions(IConfiguration config)
        {
            return config.GetSection("AuthenticationOptions").Get<AuthenticationOptions>()!;
        }
    }
}