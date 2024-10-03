global using Microsoft.EntityFrameworkCore;
global using PhanQuyenAPI.Datas;
global using PhanQuyenAPI.Models;
global using PhanQuyenAPI.DTOs;
namespace PhanQuyenAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            //Add Cors
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowPolicy", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            //Add services to the datacontext
            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowPolicy");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
