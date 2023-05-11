
using DotMessenger.Core.Interactors;
using DotMessenger.Core.Repositories;
using DotMessenger.WebApi.Data;
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
            var connectionMessenger = configuration.GetConnectionString("ConnectionMessenger");

            builder.Services.AddScoped<AccountInteractor>();
            builder.Services.AddScoped<IAccountRepository, AccountEntityFrameworkRepository>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<MessengerDbContext>(options => options.UseSqlite(connectionMessenger));

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