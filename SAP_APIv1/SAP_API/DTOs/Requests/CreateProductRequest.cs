using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.DTOs.Requests
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public int BakingTimeInMins { get; set; }
        public int BakingTempInC { get; set; }
        public int Size { get; set; }
        public List<CreateStockedProductRequest> Stock { get; set; }
    }
}
