﻿namespace SAP_API.Models
{
    public class BakingProgramProduct
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }

    }
}
