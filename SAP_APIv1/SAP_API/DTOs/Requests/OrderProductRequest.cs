using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.DTOs
{
    public class OrderProductRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

    }
}
