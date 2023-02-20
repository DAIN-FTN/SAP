using SAP_API.DTOs.Responses;
using System.Collections.Generic;

namespace SAP_API.DTOs.Requests
{
    public class AvailableProgramsResponse
    {
        public List<AvailableBakingProgramResponse> BakingPrograms { get; set; }
        public bool AllProductsCanBeSuccessfullyArranged { get; set; }
        public bool IsThereEnoughStockedProducts { get; set; }
    }
}
