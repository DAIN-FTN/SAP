using SAP_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SAP_API.Services.ArrangingProductsToProgramsService;

namespace SAP_API.Models
{
    public class BakingProgram: IEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public DateTime CreatedAt { get; set; }
        public BakingProgramStatus Status { get; set; }
        public int BakingTimeInMins { get; set; }
        public int BakingTempInC { get; set; }
        public DateTime BakingProgrammedAt { get; set; }
        public DateTime BakingStartedAt { get; set; }
        public Oven Oven { get; set; }
        public User PreparedBy { get; set; }
        public int RemainingOvenCapacity { get; set; }

        public List<BakingProgramProduct> Products { get; set; }

        public void AddProductToProgram(BakingProgramProduct product)
        {
            Products.Add(product);
            int productSize = product.Product.Size;
            int productQuantity = product.QuantityТoBake;
            RemainingOvenCapacity -= productSize * productQuantity;
        }

        internal void SetTimeAndTemp(TimeAndTempGroup group)
        {
            BakingTempInC = group.Temp;
            BakingTimeInMins = group.Time;
        }
    }
}
