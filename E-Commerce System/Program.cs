
using Domain.Contracts;
using E_Commerce_System.Extensions;
using E_Commerce_System.Factories;
using E_Commerce_System.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Contexts;
using Persistence.Data.DataSeeding;
using Persistence.Repositories;
using Presentation;
using Services;
using Services_Abstraction;




namespace E_Commerce_System
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);


            #region Add services to the container.

            builder.Services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
            builder.Services.AddSwaggerServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);

            builder.Services.AddApplicationServices();

            //Custom Validation error Response Factory 
            builder.Services.AddWebApplicationServices();


            #endregion



            var app = builder.Build();
            //DataSeeding 
            await app.SeedDatabaseAsync();
            #region Configure the HTTP request pipeline.

            //MiddleWare
            app.UseCustomExceptionMiddleWare();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWares();
            }
            //
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

      
            #endregion
        }
    }
}
