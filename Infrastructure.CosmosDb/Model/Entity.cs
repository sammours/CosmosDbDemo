namespace Domain.Model
{
    using System;
    using System.Diagnostics;
    using Newtonsoft.Json;

    [DebuggerDisplay("Type={GetType().Name}, Id={Id}")]
    public class Entity
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
