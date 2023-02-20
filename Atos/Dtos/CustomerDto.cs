using Atos.Database.Models;
using System.Text.Json.Serialization;

namespace Atos.Dtos
{
    public class CustomerDto
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("firstname")]
        public string? Firstname { get; set; }

        [JsonPropertyName("surname")]
        public string? Surname { get; set; }

        public CustomerDto() { }
        public CustomerDto(Customer customer)
            => (Id, Firstname, Surname) = (customer.Id, customer.Firstname, customer.Surname);
    }
}
