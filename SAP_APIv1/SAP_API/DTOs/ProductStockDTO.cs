﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.DTOs
{
    public class ProductStockDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int AvailableQuantity { get; set; }
    }
}
