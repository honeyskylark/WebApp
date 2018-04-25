using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApp.Contexts;
using WebApp.Extensions;
using WebApp.Models;
namespace WebApp.Services.Background.OnInit
{
    public class ProductsInitService : IHostedService
    {
        private readonly IServiceProvider _provider;
        public IConfiguration _configuration { get; }
        private ProductContext _instance;
        public ProductsInitService(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _provider = serviceProvider;
            _configuration = configuration;
            _instance = ProductContext.GetInstance();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
               
                if ((!cancellationToken.IsCancellationRequested))
                {
                    using (IServiceScope scope = _provider.CreateScope())
                    {
                        var _context = scope.ServiceProvider.GetRequiredService<WebAppContext>();

                        var products = _context.Products.Include(p => p.Catalog)
                                                        .Include(p => p.Currency)
                                                        .Include(p => p.Unit).ToList();

                        List<Product> result = new List<Product> { };
                        foreach (var product in products)
                        {
                            result.Add(product);
                        }

                        _instance.Products = result;
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
