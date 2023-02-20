using Atos.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Atos.Tests.TestDataProvider
{
    public static class InMemorySetup
    {
        public static CustomerContext GetCustomerContext() => SetupDatabaseContext().Result;

        private static async Task<CustomerContext> SetupDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<CustomerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CustomerContext(options);

            await databaseContext.Database.EnsureCreatedAsync();

            return databaseContext;
        }
    }
}
