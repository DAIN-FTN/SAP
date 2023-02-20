using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.Services
{
    public class ArrangingResult
    {
        public List<BakingProgram> BakingPrograms { get; set; }
        public bool AllProductsCanBeSuccessfullyArranged { get; set; }
        public bool IsThereEnoughStockedProducts { get; set; }
    }
}
