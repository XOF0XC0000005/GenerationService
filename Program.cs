using GenerationService.Interfaces;
using GenerationService.Persisntence;
using GenerationService.Repositories;
using GenerationService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace GenerationService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddAuthorization();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.TypeInfoResolver = AppJsonSerializerContext.Default;
            });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Generation Service API", Version = "v1" });
            });

            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<IFileRepository, FileRepository>();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Generation Service API v1");
                });

                app.UseHttpsRedirection();
            }

            app.UseRouting();
            
            //app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
