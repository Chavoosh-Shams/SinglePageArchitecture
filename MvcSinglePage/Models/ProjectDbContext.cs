using Microsoft.EntityFrameworkCore;
using MvcSinglePage.Models.DomainModels.PersonAggregates;
using MvcSinglePage.Models.DomainModels.ProductAggregates;

namespace MvcSinglePage.Models
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {

        }
        public DbSet<Person> Person { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}
