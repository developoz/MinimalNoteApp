
using Microsoft.EntityFrameworkCore;
using MinimalNote.Datas;
using MinimalNote.Models;

namespace MinimalNote
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddCors(options =>
        { 
                options.AddDefaultPolicy(policy =>
                {
                policy.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
                

                });

         });

        builder.Services.AddDbContext<NoteDbContext>(options =>

                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            

            //Create 
            app.MapPost("/notes", async (Note not,NoteDbContext db) =>
            {
                not.CreatedAt = DateTime.Now;
                db.Notes.Add(not);
                await db.SaveChangesAsync();
                return Results.Ok("not eklendi");
               
            });

            //Read All
            app.MapGet("/notes", async (NoteDbContext db) =>
            {
               var notes = await db.Notes.ToListAsync();
                return Results.Ok(notes);
            });

            //Read By Id
            app.MapGet("/notes/{id:int}", async (int id, NoteDbContext db) =>
            {
                var note = await db.Notes.FindAsync(id);
                if (note == null) return Results.NotFound();
                return Results.Ok(note);
            });

            //Update

            app.MapPut("/notes/{id:int}",async (int id, Note updatedNote , NoteDbContext db) =>
            {
                var note = await db.Notes.FindAsync(id);
                if (note == null) return Results.NotFound();
                note.Title = updatedNote.Title;
                note.Content = updatedNote.Content;
                await db.SaveChangesAsync();
                return Results.Ok("not güncellendi");

            });

            //Delete 
            app.MapDelete("/notes/{id:int}", async (int id,NoteDbContext db) =>
            {
                var note = await db.Notes.FindAsync(id);
                if (note == null) return Results.NotFound();
                db.Notes.Remove(note);   
                await db.SaveChangesAsync();
                return Results.Ok("not silindi");
            });



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            app.UseCors();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            app.MapGet("/weatherforecast", (HttpContext httpContext) =>
            {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = summaries[Random.Shared.Next(summaries.Length)]
                    })
                    .ToArray();
                return forecast;
            })
            .WithName("GetWeatherForecast");

            app.Run();
        }
    }
}
