using Historial_C.Data;
using Historial_C.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Historial_C.Controllers;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Historial_C
{
    public static class StartUp
    {
        public static WebApplication InicializarApps(string[] args)
        {
            //Creamos una nueva instancia de nuestro servidor web 
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);//Lo configuramos, con sus respectivos sericios

            var app = builder.Build();//Sobre esta app, configuraremos luego los middleware
            Configure(app);//Configuramos los middleware

            return app;//retornamos la app, ya inicializada 
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddDbContext<HistorialContext>(options => options.UseInMemoryDatabase("MiContexto")); //Cambio el contexto de db para que use SQL Server tal lo pide en el entregablo 4
            builder.Services.AddDbContext<HistorialContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MedicuritaDBCS")));

            #region Identity
            builder.Services.AddIdentity<Persona, Rol>().AddEntityFrameworkStores<HistorialContext>();

            builder.Services.Configure<IdentityOptions>(opciones =>
            { 
                opciones.Password.RequireNonAlphanumeric = true;
                opciones.Password.RequireLowercase = true;
                opciones.Password.RequireUppercase = true;
                opciones.Password.RequireDigit = true;
                opciones.Password.RequiredLength = 6;

            }
             );

            #endregion
            builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
                opciones =>
            {
                opciones.LoginPath = "/Account/Iniciarsesion";
                opciones.AccessDeniedPath = "/Account/AccesoDenegado";
                opciones.Cookie.Name = "IdentidadHistoriasApp";
            });


        }

        private static void Configure(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                var contexto = serviceScope.ServiceProvider.GetRequiredService<HistorialContext>();

                contexto.Database.Migrate();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
