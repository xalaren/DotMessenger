using DotMessenger.Core.Interactors;
using DotMessenger.Core.Repositories;
using DotMessenger.WebApi.Data.EntityFrameworkContexts;
using DotMessenger.WebApi.Data.EntityFrameworkRepositories;
using Microsoft.EntityFrameworkCore;

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

            builder.Services.AddScoped<ChatsInteractor>();
            builder.Services.AddScoped<IChatsRepository, ChatsRepository>();
            builder.Services.AddScoped<IChatProfilesRepository, ChatProfilesRepository>();

            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionMessenger));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();
            
            app.MapControllers();

            app.Run();
        }
    }
}