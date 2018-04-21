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

            string employeeLogin = "vlasov";
            string employeePassword = "qwerty";

            string sectionName = "Оборудование";
            string subSectionName = "Деревообрабатывающее";
            string catalogName = "Пилорамы ленточные горизонтальные";
            string productName = "Горизонтальная ленточная пилорама МВ-2000М";

            string currencyName = "RUB";
            string unitName = "шт";

            string processName = "Новый заказ";
            string dealName = "Тестовый заказ";

            string companyName = "Globaledge";
            string contactName = "Иван";

            string fromName = "Заказ по телефону";

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

                User employee = db.Users.FirstOrDefault(u => u.Login == employeeLogin);
                if (employee == null)
                {
                    db.Users.Add(new User
                    {
                        FirstName = "Сергей",
                        LastName = "Власов",
                        Patronymic = "Владимирович",
                        Login = employeeLogin,
                        Password = employeePassword,
                        Role = employeeRole

                    });
                    db.SaveChanges();
                }

                Section section = db.Sections.FirstOrDefault(u => u.Name == sectionName);
                if (section == null)
                {
                    db.Sections.Add(new Section
                    {
                        Name = sectionName
                    });
                    db.SaveChanges();
                }
                SubSection subSection = db.SubSections.FirstOrDefault(u => u.Name == subSectionName);
                if(subSection == null)
                {
                    Section sectionLegacy = db.Sections.FirstOrDefault(u => u.Name == sectionName);
                    db.SubSections.Add(new SubSection
                    {
                        Name = subSectionName,
                        SectionId = sectionLegacy.Id,
                    });
                    db.SaveChanges();
                }

                Catalog catalog = db.Catalogs.FirstOrDefault(u => u.Name == catalogName);
                if (catalog == null)
                {
                    SubSection subSectionLegacy = db.SubSections.FirstOrDefault(u => u.Name == subSectionName);
                    db.Catalogs.Add(new Catalog
                    {
                        Name = catalogName,
                        SubSectionId = subSectionLegacy.Id,
                    });
                    db.SaveChanges();
                }

                Currency currency = db.Currencies.FirstOrDefault(u => u.Name == currencyName);
                if(currency == null)
                {
                    db.Currencies.Add(new Currency
                    {
                        Name = currencyName
                    });
                    db.SaveChanges();
                }
                Unit unit = db.Units.FirstOrDefault(u => u.Name == unitName);
                if (unit == null)
                {
                    db.Units.Add(new Unit
                    {
                        Name = unitName
                    });
                    db.SaveChanges();
                }

                Product product = db.Products.FirstOrDefault(u => u.Name == productName);
                if(product == null)
                {
                    Catalog catalogLegacy = db.Catalogs.FirstOrDefault(u => u.Name == catalogName);
                    Currency currencyLegacy = db.Currencies.FirstOrDefault(u => u.Name == currencyName);
                    Unit unitLegacy = db.Units.FirstOrDefault(u => u.Name == unitName);
                    db.Products.Add(new Product
                    {
                        Name = productName,
                        CatalogId = catalogLegacy.Id,
                        Description = "Пилорама является усовершенствованным продолжением пилорамы MB-2000, в конструкцию пилорамы были внесены улучшения, которые позволили увеличить производительность на 10%.",
                        Price = "280000",
                        CurrencyId = currencyLegacy.Id,
                        UnitId = unitLegacy.Id,                        
                    });
                    db.SaveChanges();
                }

                Process process = db.Processes.FirstOrDefault(u => u.Title == processName);
                if(process == null)
                {
                    db.Processes.Add(new Process
                    {
                        Title = processName,
                        Order = 1
                    });
                    db.SaveChanges();
                }

                Company company = db.Companies.FirstOrDefault(u => u.Name == companyName);
                if(company == null)
                {
                    db.Companies.Add(new Company
                    {
                        Name = companyName,
                        Address = "г. Москва, ул.Ленинградская, д. 1",
                        Phone = "+7 (777) 777-77-77"                      
                    });

                    db.SaveChanges();
                }
                Contact contact = db.Contacts.FirstOrDefault(u => u.FirstName == contactName);
                if (contact == null)
                {
                    Company companyLegacy = db.Companies.FirstOrDefault(u => u.Name == companyName);
                    db.Contacts.Add(new Contact
                    {
                        FirstName = contactName,
                        LastName = "Иванов",
                        Patronymic = "Иванович",
                        Address = "г. Москва, ул.Ленинградская, д. 1", 
                        Phone = "+7 (777) 777-77-77",
                        CompanyId = companyLegacy.Id
                    });
                    db.SaveChanges();
                }

                From from = db.Froms.FirstOrDefault(u => u.Name == fromName);
                if (from == null)
                {
                    db.Froms.Add(new From
                    {
                        Name = fromName
                    });
                    db.SaveChanges();
                }


                Deal deal = db.Deals.FirstOrDefault(u => u.Title == dealName);
                if (deal == null)
                {
                    Process processLegacy = db.Processes.FirstOrDefault(u => u.Title == processName);
                    Product productLegacy = db.Products.FirstOrDefault(u => u.Name == productName);
                    From fromLegacy = db.Froms.FirstOrDefault(u => u.Name == fromName);
                    Contact contactLegacy = db.Contacts.FirstOrDefault(u => u.FirstName == contactName);
                    User employeeLegacy = db.Users.FirstOrDefault(u => u.Login == employeeLogin);

                    db.Deals.Add(new Deal
                    {
                        Title = dealName,
                        ProcessId = processLegacy.Id,
                        Description = "Тестовый заказ при инициализации базы данных.",
                        ContactId = contactLegacy.Id,
                        ProductId = productLegacy.Id,
                        FromId = fromLegacy.Id,     
                        UserId = employeeLegacy.Id
                    });
                    db.SaveChanges();
                }
            }
        }
    }
}
