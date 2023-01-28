using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.Models
{
    public class StockLocation: IEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public int Capacity { get; set; }
    }
}
