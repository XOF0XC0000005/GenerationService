using GenerationService.Interfaces;
using GenerationService.Models;
using GenerationService.Persisntence;
using GenerationService.Repositories;
using GenerationService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GenerationService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            /* builder.Services.ConfigureHttpJsonOptions(options =>
             {
                 options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
             });*/

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

            /*app.MapPost("api/files", async(HttpContext context) =>
            {
                var request = await JsonSerializer.DeserializeAsync<ExcelDataRequest>(
                    context.Request.Body,
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }
                    ) ;

                return Results.Ok( request );
            });*/

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Generation Service API v1");
                });
            }

            app.UseRouting();
            app.UseHttpsRedirection();
            //app.UseAuthorization();
            app.MapControllers();

          /*  var sampleTodos = new Todo[] {
                new(1, "Walk the dog"),
                new(2, "Do the dishes", DateOnly.FromDateTime(DateTime.Now)),
                new(3, "Do the laundry", DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
                new(4, "Clean the bathroom"),
                new(5, "Clean the car", DateOnly.FromDateTime(DateTime.Now.AddDays(2)))
            };

            var todosApi = app.MapGroup("/todos");
            todosApi.MapGet("/", () => sampleTodos);
            todosApi.MapGet("/{id}", (int id) =>
                sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
                    ? Results.Ok(todo)
                    : Results.NotFound());*/

            app.Run();
        }
    }

   /* public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

    [JsonSerializable(typeof(Todo[]))]
    internal partial class AppJsonSerializerContext : JsonSerializerContext
    {

    }*/
}
