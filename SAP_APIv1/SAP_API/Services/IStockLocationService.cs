using SAP_API.DTOs.Responses.StockLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.Services
{
    public interface IStockLocationService
    {
        public List<StockLocationResponse> GetAll();
    }
}
