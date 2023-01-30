using System;


namespace SAP_API.Models
{
    public class BakingProgramProduct: IEntity
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
        public int QuantityТoBake { get; set; }

    }
}
