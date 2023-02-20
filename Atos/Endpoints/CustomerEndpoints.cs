using Atos.Database.Models;
using Atos.DatabaseContext;
using Atos.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Atos.Endpoints
{
    public class CustomerEndpoints
    {
        public async static Task<IResult> GetAllCustomers(CustomerContext dbContext, CancellationToken token)
            => Results.Ok(await dbContext.Customers.Select(p => new CustomerDto(p)).ToListAsync(token));

        public async static Task<IResult> CreateCustomer(ILogger<CustomerEndpoints> logger, CustomerDto customerDto, CustomerContext dbContext, CancellationToken token)
        {
            var customer = new Customer
            {
                Id = customerDto.Id,
                Firstname = customerDto.Firstname,
                Surname = customerDto.Surname
            };

            await dbContext.Customers.AddAsync(customer, token);
            await dbContext.SaveChangesAsync(token);
            logger.LogInformation($"User with id: {customerDto.Id} has been created.");

            return Results.Created($"/customers", null);
        }

        public async static Task<IResult> DeleteCustomerById(ILogger<CustomerEndpoints> logger, int id, CustomerContext dbContext, CancellationToken token)
        {
            if (await dbContext.Customers.FindAsync(id, token) is Customer customer)
            {
                dbContext.Customers.Remove(customer);
                await dbContext.SaveChangesAsync(token);
                logger.LogInformation($"User with id: {id} has been deleted.");

                return Results.Ok();
            }

            return Results.NotFound();
        }
    }
}
