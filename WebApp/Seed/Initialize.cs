using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WebApp.Models;

namespace WebApp.Seed
{
    public class Initialize
    {
        public static void DatabaseInitialize(IServiceProvider serviceProvider)
        {
            string adminRoleName = "Administrator";
            string employeeRoleName = "Employee";
            string customerRoleName = "Customer";

            string adminLogin = "skylark";
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
                if (employeeRole == null)
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
