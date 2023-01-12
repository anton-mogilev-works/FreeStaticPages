using Microsoft.Extensions.FileProviders;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.EntityFrameworkCore;
using FreeStaticPages.Models;
using Microsoft.Extensions.Configuration;
using FreeStaticPages.Services;
using Microsoft.Extensions.WebEncoders;
using System.Text.Encodings.Web;
using System.Text.Unicode;


var builder = WebApplication.CreateBuilder(args);


string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connection));


builder.Services.Configure<WebEncoderOptions>(
    options =>
    {
        options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
    }
);


// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

if (!Directory.Exists("images"))
{
    Directory.CreateDirectory("images");
}

app.UseStaticFiles();
app.UseStaticFiles(
    new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(builder.Environment.ContentRootPath, "wwwrootadmin")
        ),
        RequestPath = "/wwwrootadmin"
    }
);
app.UseStaticFiles(
    new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(builder.Environment.ContentRootPath, "images")
        ),
        RequestPath = "/images"
    }
);

app.UseRouting();

// app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
Helper.url = Environment.GetEnvironmentVariable("ASPNETCORE_URLS").Split(";")[0];

// Add init main page
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

    if (dbContext.StaticPages.Any() == false)
    {
        dbContext.StaticPages.Add(
            new StaticPage()
            {
                Name = "Главная страница",
                Content =
                    "Ваша главная страница <p>Перейдите по адресу <a href=\"/Admin\">/Admin</a> для управления сайтом",
                Link = new Link() { Path = "index" }
            }
        );
        dbContext.SaveChanges();
    }
}


app.Run();
