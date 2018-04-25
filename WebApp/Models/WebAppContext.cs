using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Models
{
    public class WebAppContext : DbContext
    {
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SubSection> SubSections { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<Process>Processes { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<From> Froms { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        public WebAppContext(DbContextOptions<WebAppContext> options) : base(options)
        {

        }


    }
}