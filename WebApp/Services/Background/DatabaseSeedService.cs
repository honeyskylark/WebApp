using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApp.Extensions;
using WebApp.Models;
namespace WebApp.Services.Background
{
    public class DatabaseSeedService : IHostedService
    {
        private readonly IServiceProvider _provider;
        public IConfiguration _configuration { get; }
        public DatabaseSeedService(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _provider = serviceProvider;
            _configuration = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                var required = _configuration.GetSection("Settings")["DatabaseSeedRequired"].ToBoolean();
                if ((!cancellationToken.IsCancellationRequested) && required)
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
                    string productName = "Ленточная пилорама МВ-2000М";

                    string currencyName = "RUB";
                    string unitName = "шт";

                    string processName = "Новый заказ";
                    string dealName = "Тестовый заказ";

                    string companyName = "Globaledge";
                    string contactName = "Иван";

                    string fromName = "Заказ через каталог сайта";
                    using (IServiceScope scope = _provider.CreateScope())
                    {
                        var _context = scope.ServiceProvider.GetRequiredService<WebAppContext>();
                        Role adminRole = _context.Roles.FirstOrDefault(x => x.Name == adminRoleName);
                        Role userRole = _context.Roles.FirstOrDefault(x => x.Name == customerRoleName);
                        Role employeeRole = _context.Roles.FirstOrDefault(x => x.Name == employeeRoleName);

                        if (adminRole == null)
                        {
                            adminRole = new Role { Name = adminRoleName };
                            _context.Roles.Add(adminRole);
                        }
                        if (userRole == null)
                        {
                            userRole = new Role { Name = customerRoleName };
                            _context.Roles.Add(userRole);
                        }
                        if (employeeRole == null)
                        {
                            employeeRole = new Role { Name = employeeRoleName };
                            _context.Roles.Add(employeeRole);
                        }
                        _context.SaveChanges();


                        User admin = _context.Users.FirstOrDefault(u => u.Login == adminLogin);
                        if (admin == null)
                        {
                            _context.Users.Add(new User
                            {
                                FirstName = "Рустам",
                                LastName = "Асылгареев",
                                Patronymic = "Фанилевич",
                                Login = adminLogin,
                                Password = adminPassword,
                                Role = adminRole

                            });
                            _context.SaveChanges();
                        }

                        User employee = _context.Users.FirstOrDefault(u => u.Login == employeeLogin);
                        if (employee == null)
                        {
                            _context.Users.Add(new User
                            {
                                FirstName = "Сергей",
                                LastName = "Власов",
                                Patronymic = "Владимирович",
                                Login = employeeLogin,
                                Password = employeePassword,
                                Role = employeeRole

                            });
                            _context.SaveChanges();
                        }

                        Section section = _context.Sections.FirstOrDefault(u => u.Name == sectionName);
                        if (section == null)
                        {
                            _context.Sections.Add(new Section
                            {
                                Name = sectionName
                            });
                            _context.SaveChanges();
                        }
                        SubSection subSection = _context.SubSections.FirstOrDefault(u => u.Name == subSectionName);
                        if (subSection == null)
                        {
                            Section sectionLegacy = _context.Sections.FirstOrDefault(u => u.Name == sectionName);
                            _context.SubSections.Add(new SubSection
                            {
                                Name = subSectionName,
                                SectionId = sectionLegacy.Id,
                            });
                            _context.SaveChanges();
                        }

                        Catalog catalog = _context.Catalogs.FirstOrDefault(u => u.Name == catalogName);
                        if (catalog == null)
                        {
                            SubSection subSectionLegacy = _context.SubSections.FirstOrDefault(u => u.Name == subSectionName);
                            _context.Catalogs.Add(new Catalog
                            {
                                Name = catalogName,
                                SubSectionId = subSectionLegacy.Id,
                            });
                            _context.SaveChanges();
                        }

                        Currency currency = _context.Currencies.FirstOrDefault(u => u.Name == currencyName);
                        if (currency == null)
                        {
                            _context.Currencies.Add(new Currency
                            {
                                Name = currencyName
                            });
                            _context.SaveChanges();
                        }
                        Unit unit = _context.Units.FirstOrDefault(u => u.Name == unitName);
                        if (unit == null)
                        {
                            _context.Units.Add(new Unit
                            {
                                Name = unitName
                            });
                            _context.SaveChanges();
                        }

                        Product product = _context.Products.FirstOrDefault(u => u.Name == productName);
                        if (product == null)
                        {
                            Catalog catalogLegacy = _context.Catalogs.FirstOrDefault(u => u.Name == catalogName);
                            Currency currencyLegacy = _context.Currencies.FirstOrDefault(u => u.Name == currencyName);
                            Unit unitLegacy = _context.Units.FirstOrDefault(u => u.Name == unitName);
                            _context.Products.Add(new Product
                            {
                                Name = productName,
                                CatalogId = catalogLegacy.Id,
                                Description = "Пилорама является усовершенствованным продолжением пилорамы MB-2000, в конструкцию пилорамы были внесены улучшения, которые позволили увеличить производительность на 10%.",
                                Price = "280000",
                                CurrencyId = currencyLegacy.Id,
                                UnitId = unitLegacy.Id,
                            });
                            _context.SaveChanges();
                        }

                        Process process = _context.Processes.FirstOrDefault(u => u.Title == processName);
                        if (process == null)
                        {
                            _context.Processes.Add(new Process
                            {
                                Title = processName,
                                Order = 1
                            });
                            _context.SaveChanges();
                        }

                        Company company = _context.Companies.FirstOrDefault(u => u.Name == companyName);
                        if (company == null)
                        {
                            _context.Companies.Add(new Company
                            {
                                Name = companyName,
                                Address = "г. Москва, ул.Ленинградская, д. 1",
                                Phone = "+7 (777) 777-77-77"
                            });

                            _context.SaveChanges();
                        }
                        Contact contact = _context.Contacts.FirstOrDefault(u => u.FirstName == contactName);
                        if (contact == null)
                        {
                            Company companyLegacy = _context.Companies.FirstOrDefault(u => u.Name == companyName);
                            _context.Contacts.Add(new Contact
                            {
                                FirstName = contactName,
                                LastName = "Иванов",
                                Patronymic = "Иванович",
                                Address = "г. Москва, ул.Ленинградская, д. 1",
                                Phone = "+7 (777) 777-77-77",
                                CompanyId = companyLegacy.Id
                            });
                            _context.SaveChanges();
                        }

                        From from = _context.Froms.FirstOrDefault(u => u.Name == fromName);
                        if (from == null)
                        {
                            _context.Froms.Add(new From
                            {
                                Name = fromName
                            });
                            _context.SaveChanges();
                        }


                        Deal deal = _context.Deals.FirstOrDefault(u => u.Title == dealName);
                        if (deal == null)
                        {
                            Process processLegacy = _context.Processes.FirstOrDefault(u => u.Title == processName);
                            Product productLegacy = _context.Products.FirstOrDefault(u => u.Name == productName);
                            From fromLegacy = _context.Froms.FirstOrDefault(u => u.Name == fromName);
                            Contact contactLegacy = _context.Contacts.FirstOrDefault(u => u.FirstName == contactName);
                            User employeeLegacy = _context.Users.FirstOrDefault(u => u.Login == employeeLogin);

                            _context.Deals.Add(new Deal
                            {
                                Title = dealName,
                                ProcessId = processLegacy.Id,
                                Description = "Тестовый заказ при инициализации базы данных.",
                                ContactId = contactLegacy.Id,
                                ProductId = productLegacy.Id,
                                FromId = fromLegacy.Id,
                                UserId = employeeLegacy.Id
                            });
                            _context.SaveChanges();
                        }
                    }
                }
            });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
