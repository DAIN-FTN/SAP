﻿using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.DTOs.Responses.Order
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public DateTime ShouldBeDoneAt { get; set; }
        public OrderStatus Status { get; set; }
        public Customer Customer { get; set; }
    }
}
