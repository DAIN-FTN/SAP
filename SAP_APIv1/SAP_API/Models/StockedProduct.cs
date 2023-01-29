using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.Models
{
    public class StockedProduct: IEntity
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public StockLocation Location { get; set; }
        public int Quantity { get; set; }
        public int ReservedQuantity { get; set; }
    }
}
