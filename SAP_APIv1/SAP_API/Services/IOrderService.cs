using SAP_API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.Services
{
    public interface IOrderService
    {
        public bool CreateOrder(FindAvailableBakingProgramsRequest createDto);
    }
}
