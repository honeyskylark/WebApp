using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApp.Contexts;
using WebApp.Models;

namespace WebApp.Services.Background.OnInit
{
    public class ResourcesService : IHostedService
    {
        private readonly IServiceProvider _provider;
        private ResourceContext _instance;
        public IConfiguration _configuration { get; }

        public ResourcesService(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _provider = serviceProvider;
            _instance = ResourceContext.GetInstance();
            _configuration = configuration;
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
                        var globalLanguage = _configuration.GetSection("Settings")["GlobalLanguage"];

                        Language language = _context.Languages.FirstOrDefault(u => u.Name == globalLanguage);

                        if (language == null)
                        {
                            _context.Languages.Add(new Language
                            {
                                Name = globalLanguage
                            });
                            _context.SaveChanges();
                            language = _context.Languages.FirstOrDefault(u => u.Name == globalLanguage);
                        }

                        var resources = _context.Resources.Where(u => u.LanguageId == language.Id);
                        Dictionary<string, string> result = new Dictionary<string, string> { };

                        foreach (var resource in resources)
                        {
                            result.Add(resource.Key, resource.Value);
                        }

                        _instance.Resources = result;
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
