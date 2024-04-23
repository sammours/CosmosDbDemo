namespace Infrastructure.CosmosDb.Repositories
{
    using Infrastructure.CosmosDb.Model;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomerRepository : CosmosDbRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IConfiguration configuration)
            : base("category", configuration)
        {
        }

        public IEnumerable<Customer> GetByName()
        {
            var query = this.Container.GetItemLinqQueryable<Customer>(allowSynchronousQueryExecution: true)
                .Where(x => x.Name == "John");
            return query;
        }

        public IEnumerable<Customer> GetOrdered()
        {
            var query = this.Container.GetItemLinqQueryable<Customer>(allowSynchronousQueryExecution: true)
                .OrderByDescending(x => x.Name);
            return query;
        }
    }
}
