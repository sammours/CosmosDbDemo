namespace CosmosDbDemo.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Infrastructure.CosmosDb.Model;
    using Infrastructure.CosmosDb.Repositories;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository repository;

        public CustomersController(ICustomerRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            var result = this.repository.GetByName();
            return this.Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Customer>>> Get(string id)
        {
            var result = await this.repository.GetByIdAsync(id).ConfigureAwait(false);
            return this.Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Post([FromBody] Customer model)
        {
            var result = await this.repository.AddOrUpdateAsync(model).ConfigureAwait(false);
            return this.Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(string id)
        {
            var result = await this.repository.DeleteAsync(id).ConfigureAwait(false);
            return this.Ok(result);
        }
    }
}
