
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistance.Data.Contexts;
using Persistance.Data.DataSeeding;

namespace E_Commerce_System
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

           
            #region Add services to the container.

            builder.Services.AddControllers();
        
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //DbContext
            builder.Services.AddDbContext<APPDbContext>(options =>
            {
               
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
            });


            //DataSeeding 
            builder.Services.AddScoped<IDbInitializer,DbInitializer>();

            //Resolve For Some Dependance ==> Create Scope

            #endregion



            var app = builder.Build();
            //DataSeeding 
            await SeedDbAsync(app);
            #region Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

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
