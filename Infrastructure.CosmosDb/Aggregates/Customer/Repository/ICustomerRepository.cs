namespace Infrastructure.CosmosDb.Repositories
{
    using Model;
    using System.Collections.Generic;

    public interface ICustomerRepository : IRepository<Customer>
    {
        IEnumerable<Customer> GetByName();

        IEnumerable<Customer> GetOrdered();
    }
}
