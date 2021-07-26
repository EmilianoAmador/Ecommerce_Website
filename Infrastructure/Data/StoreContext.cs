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
    }
}