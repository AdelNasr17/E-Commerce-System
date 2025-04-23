
using Domain.Contracts;
using E_Commerce_System.Factories;
using E_Commerce_System.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Contexts;
using Persistence.Data.DataSeeding;
using Persistence.Repositories;
using Services;
using Services_Abstraction;
using System.Reflection.Metadata;


namespace E_Commerce_System
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

           
            #region Add services to the container.

            builder.Services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
        
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //DbContext
            builder.Services.AddDbContext<APPDbContext>(options =>
            {
               
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
            });


            //DataSeeding 
            builder.Services.AddScoped<IDbInitializer,DbInitializer>();

            //UnitOfWork
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);

            //ServiceManager
            builder.Services.AddScoped<IServicesManager, ServicesManager>();

            //Custom Validation error Response Factory 
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;
            }      
            );


            #endregion



            var app = builder.Build();
            //DataSeeding 
            await SeedDbAsync(app);
            #region Configure the HTTP request pipeline.

            //MiddleWare
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run(); 

            async Task SeedDbAsync(WebApplication app)
            {
                using var scope =app.Services.CreateScope();
                var DbIntializer= scope.ServiceProvider.GetRequiredService<IDbInitializer>();

                await DbIntializer.InitializerAsync();


            }
            #endregion
        }
    }
}
