using Atos.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Atos.DatabaseContext
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
    }
}
