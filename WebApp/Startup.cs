using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WebApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {

            //string connection = "Server=(localdb)\\mssqllocaldb;Database=WebAppDB;Trusted_Connection=True;MultipleActiveResultSets=true";
            string connection = "Data Source=SQL6003.SmarterASP.NET;Initial Catalog=DB_A289C6_ecoremaster;User Id=DB_A289C6_ecoremaster_admin;Password=Naturesprophet27;";
            services.AddDbContext<WebAppContext>(options => options.UseSqlServer(connection));
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseDeveloperExceptionPage();



            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies",
                LoginPath = new Microsoft.AspNetCore.Http.PathString("/Login/Index"),
                AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Login/AccessDenied"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });



            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
            DatabaseInitialize(app.ApplicationServices);
        }



        public void DatabaseInitialize(IServiceProvider serviceProvider)
        {
            string adminRoleName = "Administrator";
            string employeeRoleName = "Employee";
            string customerRoleName = "Customer";

            string adminLogin = "monroesummer";
            string adminPassword = "qwerty";

            using (WebAppContext db = serviceProvider.GetRequiredService<WebAppContext>())
            {
                Role adminRole = db.Roles.FirstOrDefault(x => x.Name == adminRoleName);
                Role userRole = db.Roles.FirstOrDefault(x => x.Name == customerRoleName);
                Role employeeRole = db.Roles.FirstOrDefault(x => x.Name == employeeRoleName);
                // добавляем роли, если их нет
                if (adminRole == null)
                {
                    adminRole = new Role { Name = adminRoleName };
                    db.Roles.Add(adminRole);
                }
                if (userRole == null)
                {
                    userRole = new Role { Name = customerRoleName };
                    db.Roles.Add(userRole);
                }
                if(employeeRole == null)
                {
                    employeeRole = new Role { Name = employeeRoleName };
                    db.Roles.Add(employeeRole);
                }
                db.SaveChanges();

                
                User admin = db.Users.FirstOrDefault(u => u.Login == adminLogin);
                if (admin == null)
                {
                    db.Users.Add(new User
                    {
                        FirstName = "Рустам",
                        LastName = "Асылгареев",
                       	Patronymic = "Фанилевич",
                        Login = adminLogin,
                        Password = adminPassword,
                        Role = adminRole

                    });
                    db.SaveChanges();
                }
            }
        }
    }
}
