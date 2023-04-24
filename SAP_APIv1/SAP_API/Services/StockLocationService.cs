using SAP_API.DataAccess.Repositories;
using SAP_API.DTOs.Responses.StockLocation;
using SAP_API.Mappers;
using SAP_API.Models;
using System.Collections.Generic;


namespace SAP_API.Services
{
    public class StockLocationService : IStockLocationService
    {
        private readonly IStockLocationRepository _stockLocationRepository;

        public StockLocationService(IStockLocationRepository stockLocationRepository)
        {
            _stockLocationRepository = stockLocationRepository;
        }
        public List<StockLocationResponse> GetAll()
        {
            List<StockLocation> stockLocations = (List<StockLocation>)_stockLocationRepository.GetAll();
            return StockLocationMapper.GetStockLocationResponseListFromStockLocationList(stockLocations); 
        }
    }
}
