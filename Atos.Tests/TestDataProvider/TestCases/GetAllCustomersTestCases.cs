using Atos.Database.Models;
using Atos.Dtos;
using System.Collections;

namespace Atos.Tests.TestDataProvider.TestCases
{
    public class GetAllCustomersTestCases : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new()
        {
            new object[]
            {
                new Customer[]
                {
                    new Customer { Id = 1, Firstname = "John", Surname = "Doe" },
                    new Customer { Id = 2, Firstname = "Michael", Surname = "Fold" },
                    new Customer { Id = 3, Firstname = "Piter", Surname = "Novak" },
                },
                new CustomerDto[]
                {
                    new CustomerDto { Id = 1, Firstname = "John", Surname = "Doe" },
                    new CustomerDto { Id = 2, Firstname = "Michael", Surname = "Fold" },
                    new CustomerDto { Id = 3, Firstname = "Piter", Surname = "Novak" },
                },
                200,
            }
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _data.GetEnumerator();
    }
}
