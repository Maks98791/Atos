using Atos.Database.Models;

namespace Atos.DatabaseContext
{
    public static class Seeder
    {
        public async static void SeedInMemoryDatabase(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<CustomerContext>()!;

            await dbContext.AddRangeAsync(
                new Customer { Id = 1, Firstname = "Daniel", Surname = "Doe" },
                new Customer { Id = 2, Firstname = "Michael", Surname = "Novak" },
                new Customer { Id = 3, Firstname = "Josh", Surname = "Smith" },
                new Customer { Id = 4, Firstname = "Andy", Surname = "Lorrey" },
                new Customer { Id = 5, Firstname = "Annete", Surname = "Storm" },
                new Customer { Id = 6, Firstname = "Will", Surname = "Larkin" });

            await dbContext.SaveChangesAsync();
        }
    }
}
