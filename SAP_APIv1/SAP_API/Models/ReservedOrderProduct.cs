using System;
namespace SAP_API.Models
{
    public class ReservedOrderProduct: IEntity
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }

        /// <summary>
        /// Quantity that is reserved from the location when order is created.
        /// </summary>
        public int ReservedQuantity { get; set; }

        /// <summary>
        /// Quantity that was taken from the location - this is set when StartPreparing 
        /// is done.
        /// That is because the same product can be prepared in different programs.
        /// Eg. for order O1 10 products P1 should be baked. 5 of these products are reserved
        /// on location L1 and 5 are reserved on location L2.
        /// Two baking programs are made to bake products from order, BP1 (3P1) and BP2(7P1).
        /// BP1 is prepared first, and 3 products are set to be picked up from L1.
        /// Prepared quantity for P1 on L1 is set to 3.
        /// After and while BP1 is being prepared, for BP2 there should be set to pick up
        /// remaining 2 products from L1 and 5 products from L2.
        /// </summary>
        public int PreparedQuantity { get; set; }
        public StockLocation LocationWhereProductIsReserved { get; set; }

        public void GetReadyForPreparing(int quantityToBePrepared)
        {
            PreparedQuantity += quantityToBePrepared;
        }

        public int GetReservedQuantityLeftForPreparing()
        {
            return ReservedQuantity - PreparedQuantity;
        }
    }
}
