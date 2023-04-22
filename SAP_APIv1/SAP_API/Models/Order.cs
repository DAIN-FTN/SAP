using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAP_API.Models
{
    public class Order: IEntity
    {
        public Guid Id { get; set; }
        public DateTime ShouldBeDoneAt { get; set; }
        public OrderStatus Status { get; set; }
        public string CustomerFullName { get; set; }

        [EmailAddress]
        public string CustomerEmail { get; set; }
        [Phone]
        public string CustomerTelephone { get; set; }
        public List<ReservedOrderProduct> Products { get; set; } 
    }
}
