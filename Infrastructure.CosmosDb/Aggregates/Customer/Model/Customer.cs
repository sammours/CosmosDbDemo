namespace Infrastructure.CosmosDb.Model
{
    using Domain.Model;

    public class Customer : Entity
    {
        public string Name { get; set; }

        public string Category { get; set; }
    }
}
