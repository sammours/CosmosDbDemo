namespace Infrastructure.CosmosDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain.Model;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Extensions.Configuration;

    public class CosmosDbRepository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        private readonly CosmosClient client;
        private readonly PartitionKey partitionKey;
        private readonly Database database;
        private readonly Container container;

        public CosmosDbRepository(string partitionKeyPath, IConfiguration configuration)
        {
            var database = "Demo";
            this.client = new CosmosClient(
                    configuration.GetValue<string>("CosmosDBDemo:EndPoint"),
                    configuration.GetValue<string>("CosmosDBDemo:PrimaryKey"));
            this.partitionKey = new PartitionKey(partitionKeyPath);
            this.database = this.client.CreateDatabaseIfNotExistsAsync(database).Result;
            this.container = this.database.CreateContainerIfNotExistsAsync(typeof(TEntity).Name, $"/{partitionKeyPath}").Result;
        }

        public Container Container { get => this.container; }

        public IEnumerable<TEntity> GetAll()
        {
            var query = this.container.GetItemLinqQueryable<TEntity>(allowSynchronousQueryExecution: true);
            return query;
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            var response = await this.container.ReadItemAsync<TEntity>(id, this.partitionKey).ConfigureAwait(false);
            return response.Resource;
        }

        public async Task<TEntity> AddOrUpdateAsync(TEntity entity)
        {
            var result = await this.container.UpsertItemAsync(entity).ConfigureAwait(false);
            return result;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await this.GetByIdAsync(id).ConfigureAwait(false);
            if (entity != null)
            {
                await this.container.DeleteItemAsync<TEntity>(id, this.partitionKey).ConfigureAwait(false);
                return true;
            }

            return false;
        }
    }
}
