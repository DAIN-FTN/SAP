using System;

namespace SAP_API.Models
{
    public class Order: IEntity
    {
        public Guid Id { get; set; }
        public DateTime ShouldBeDoneAt { get; set; }
        public OrderStatus Status { get; set; }
        public Customer Customer { get; set; }
    }
}
