using Atos.Database.Models;
using Atos.DatabaseContext;
using Atos.Dtos;
using Atos.Endpoints;
using Atos.Tests.TestDataProvider;
using Atos.Tests.TestDataProvider.TestCases;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text.Json;

namespace Atos.Tests.Endpoints
{
    // Tests and logic can be easily extended by checking for additional cases like eg:
    // case when customer with provided id already exists in db
    // case when we do not provide an Id while creating new customer
    // etc.
    public class CustomerEndpointsTests : IDisposable
    {
        private readonly CustomerContext _dbContext;
        private readonly Mock<ILogger<CustomerEndpoints>> _mockLogger;
        private readonly CancellationToken _cancellationToken = CancellationToken.None;
        private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web);

        public CustomerEndpointsTests()
        {
            _mockLogger = new Mock<ILogger<CustomerEndpoints>>();
            _dbContext = InMemorySetup.GetCustomerContext();
        }

        public void Dispose() => _dbContext.Dispose();

        [Theory]
        [ClassData(typeof(GetAllCustomersTestCases))]
        public async Task GetAllCustomers_ReturnsAllCustomersInDatabase(Customer[] customers,
            CustomerDto[] expectedCustomers, int expectedStatusCodeValue)
        {
            // Assign
            var mockHttpContext = CreateMockHttpContext();
            await UploadCustomersInDatabase(customers);

            // Act
            var result = await CustomerEndpoints.GetAllCustomers(_dbContext, _cancellationToken);
            await result.ExecuteAsync(mockHttpContext);
            mockHttpContext.Response.Body.Position = 0;
            var responseCustomers = await JsonSerializer.DeserializeAsync<CustomerDto[]>(
                mockHttpContext.Response.Body, _jsonOptions);

            // Assert
            responseCustomers.Should().BeEquivalentTo(expectedCustomers);
            mockHttpContext.Response.StatusCode.Should().Be(expectedStatusCodeValue);
        }

        [Theory]
        [ClassData(typeof(CreateUserTestCases))]
        public async Task CreateCustomer_GetsNewCustomer_CorectlyCreatesNewCustomerInDatabase(
            Customer[] customers, CustomerDto newCustomer, int expectedStatusCodeValue)
        {
            // Assign
            var mockHttpContext = CreateMockHttpContext();
            await UploadCustomersInDatabase(customers);

            // Act
            var result = await CustomerEndpoints.CreateCustomer(_mockLogger.Object,
                newCustomer, _dbContext, _cancellationToken);
            await result.ExecuteAsync(mockHttpContext);

            // Assert
            mockHttpContext.Response.StatusCode.Should().Be(expectedStatusCodeValue);
            _dbContext.Customers.Any(p => p.Firstname == newCustomer.Firstname
                && p.Surname == newCustomer.Surname && p.Id == newCustomer.Id);
        }

        [Theory]
        [ClassData(typeof(DeleteCustomerByIdTestCases))]
        public async Task DeleteCustomerById_GetsIdOfCustomerToDelete_DeletesUserIfFound(
            Customer[] customers, int requestedId, int expectedStatusCodeValue)
        {
            // Assign
            var mockHttpContext = CreateMockHttpContext();
            await UploadCustomersInDatabase(customers);

            // Act
            var result = await CustomerEndpoints.DeleteCustomerById(_mockLogger.Object,
                requestedId, _dbContext, _cancellationToken);
            await result.ExecuteAsync(mockHttpContext);

            // Assert
            mockHttpContext.Response.StatusCode.Should().Be(expectedStatusCodeValue);
            _dbContext.Customers.Any(p => p.Id == requestedId).Should().BeFalse();
        }

        private async Task UploadCustomersInDatabase(Customer[] customers)
        {
            await _dbContext.AddRangeAsync(customers);
            await _dbContext.SaveChangesAsync();
        }

        private static HttpContext CreateMockHttpContext() => new DefaultHttpContext
        {
            RequestServices = new ServiceCollection().AddLogging().BuildServiceProvider(),
            Response = { Body = new MemoryStream() },
        };
    }
}