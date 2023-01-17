using BookReader.Api.AutoMapperProfiles;
using BookReader.Api.Repository.Interfaces;
using BookReader.Api.Repository.RepositoryDb;
using BookReader.Api.Repository.RepositoryDb.Implementations;
using Microsoft.EntityFrameworkCore;

namespace BookReader.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IPageRepository, PageRepository>();
            builder.Services.AddDbContext<RepositoryDbContext>(opts =>
                opts.UseSqlite(builder.Configuration.GetConnectionString("Default")));
            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<RepositoryDtoProfile>());


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}