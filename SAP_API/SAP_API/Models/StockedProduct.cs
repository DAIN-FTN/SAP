namespace SAP_API.Models
{
    public class StockedProduct
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public StockLocation StockLocation {get; set; }
        public int Quantity { get; set; }
        public int ReservedQuantity { get; set; } 


    }
}
