using System;


namespace SAP_API.Models
{
    public class Role : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
