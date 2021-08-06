using System.Linq;
using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext            //this class will derive from a particular framework class called DbContext
    {                                                                     // this below is a constructor in C#. it creates a connection string using the first options. then the base options passes the data from options to the base constructor (its Parent Class) which is the huge DbContext.
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)                          
        { 
        }

        public DbSet<Product> Products { get; set; }     // specifying DbSet allows us to access the products and then use some of the methods defined in the huge DbContext Class to perform things like find by Id etc bc it allows us to query the data.
        public DbSet<ProductBrand> ProductBrands { get; set; }          // when we create our migration our main table will have foreign keys pointing to these two tables as well 
        public DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Converts decimals into doubles bc SQLite doesnt support decimals
            if(Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                // loop over all of our differnet properties
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));

                    foreach (var property in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                    }
                }
            }
        }
    }
}