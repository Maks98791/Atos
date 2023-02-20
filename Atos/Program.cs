using Atos.DatabaseContext;
using Atos.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Enable logging
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CustomerContext>(opt => opt.UseInMemoryDatabase("CustomerInMemoryDb"));

var app = builder.Build();

Seeder.SeedInMemoryDatabase(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Customer endpoints
app.MapGet("/customers", CustomerEndpoints.GetAllCustomers);
app.MapPost("/customers", CustomerEndpoints.CreateCustomer);
app.MapDelete("/customers", CustomerEndpoints.DeleteCustomerById);

app.Run();