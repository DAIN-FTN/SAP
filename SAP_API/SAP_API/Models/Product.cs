namespace SAP_API.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int BakingTimeInMins { get; set; }
        public int BakingTempInC { get; set; }
        public int Size { get; set; }
    }
}
