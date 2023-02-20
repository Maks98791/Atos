using Atos.Database.Models;
using System.Collections;

namespace Atos.Tests.TestDataProvider.TestCases
{
    public class DeleteCustomerByIdTestCases : IEnumerable<object[]>
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
                2,
                200,
            },
            new object[]
            {
                new Customer[]
                {
                    new Customer { Id = 1, Firstname = "John", Surname = "Doe" },
                    new Customer { Id = 2, Firstname = "Michael", Surname = "Fold" },
                    new Customer { Id = 3, Firstname = "Piter", Surname = "Novak" },
                },
                4,
                404,
            }
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _data.GetEnumerator();
    }
}
