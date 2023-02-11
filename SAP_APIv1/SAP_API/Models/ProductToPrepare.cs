using System;

namespace SAP_API.Models
{
    public class ProductToPrepare : IEntity
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public BakingProgram BakingProgram { get; set; }
        public Order Order { get; set; }
        public StockLocation LocationToPrepareFrom { get; set; }
        public int QuantityToPrepare { get; set; }

    }
}
