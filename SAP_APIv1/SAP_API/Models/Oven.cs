using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SAP_API.Models
{
    public class Oven: IEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public int MaxTempInC { get; set; }
        public int Capacity { get; set; }
    }
}
