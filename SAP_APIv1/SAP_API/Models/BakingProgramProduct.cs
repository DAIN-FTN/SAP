using System;


namespace SAP_API.Models
{
    public class BakingProgramProduct: IEntity
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid BakingProgramId { get; set; }
        public BakingProgram BakingProgram { get; set; }
        public int QuantityТoBake { get; set; }
    }
}
