using API.Interfaces;
using API.Models;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<WonderLynxContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IReferenceItemService, ReferenceItemService>();
            builder.Services.AddScoped<ITagService, TagService>();


            // Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(); // configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            // Use CORS policy
            app.UseCors("AllowAll");

            app.MapControllers();

            app.Run();
        }
    }
}