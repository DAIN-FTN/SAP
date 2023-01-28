namespace SAP_API.Models
{
    public class Oven
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public int MaxTempInC { get; set; }
        public int Capacity { get; set; }

    }
}
