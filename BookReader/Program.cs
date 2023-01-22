using BookReader.Api.BookReaderHttpClient.Interfaces;
using BookReader.Api.BookReaderHttpClient.BaseImplementation.Implementations;
using BookReader.AutoMapperProfiles;

namespace BookReader
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient<IBookReaderHttpClient, BookReaderHttpClient>
                (cfg => cfg.BaseAddress = new Uri("https://localhost:7222"));
            /*
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IPageRepository, PageRepository>();
            builder.Services.AddDbContext<RepositoryDbContext>(opts => 
                opts.UseSqlite(builder.Configuration.GetConnectionString("Default")));
            */
            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<RepositoryViewModelProfile>());
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}