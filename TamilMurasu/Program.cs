using Microsoft.Extensions.DependencyInjection.Extensions;
using TamilMurasu.Interface;
using TamilMurasu.Services;
using TamilMurasu.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using System.ComponentModel.Design;
using System.ComponentModel;
using TamilMurasu.Interface.Admin;
using TamilMurasu.Services.Admin;



internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
     
        builder.Services.TryAddSingleton<ILoginService, LoginService>();
       
        builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.TryAddSingleton<ICategoryService, CategoryService>();
        builder.Services.TryAddSingleton<INewsService, NewsService>();
        builder.Services.TryAddSingleton<ILatestNewsService, LatestNewsService>();
        builder.Services.TryAddSingleton<IAnmeegamService, AnmeegamService>();
        builder.Services.TryAddSingleton<IAdangapaService, AdangapaService>();
        builder.Services.TryAddSingleton<INewImageService, NewImageService>();
        builder.Services.TryAddSingleton<INewAlbumService, NewAlbumService>();
        builder.Services.TryAddSingleton<IRelexService, RelexService>();
        builder.Services.TryAddSingleton<IVideoService, VideoService>();


        builder.Services.AddSession();
     

        builder.Services.AddControllers();
        builder.Services.Configure<FormOptions>(x =>
        {
            x.ValueCountLimit = int.MaxValue;
        });
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
        app.UseSession();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Account}/{action=Login}/{fid?}");



        app.Run();
    }

   
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
            webBuilder.UseWebRoot("wwwroot");
            webBuilder.UseStartup<IStartup>();

        });

    [Obsolete]
    public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
    {
        // Other configurations...

        app.UseStaticFiles(); // Enable static file serving, e.g., for wwwroot folder

        // More configurations...
    }
}
