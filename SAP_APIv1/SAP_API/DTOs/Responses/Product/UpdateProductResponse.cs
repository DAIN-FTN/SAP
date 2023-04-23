using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.DTOs.Responses
{
    public class UpdateProductResponse
    {
        public string Name { get; set; }
        public int BakingTimeInMins { get; set; }
        public int BakingTempInC { get; set; }
        public int Size { get; set; }
    }
}
