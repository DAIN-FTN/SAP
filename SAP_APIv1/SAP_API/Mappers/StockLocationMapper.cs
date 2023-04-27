using SAP_API.DTOs.Responses.StockLocation;
using SAP_API.Models;
using System;
using System.Collections.Generic;

namespace SAP_API.Mappers
{
    public class StockLocationMapper
    {
        public static StockLocationResponse GetStockLocationResponseFromStockLocation(StockLocation stockLocation)
        {
            return new StockLocationResponse
            {
                Id = stockLocation.Id,
                Capacity = stockLocation.Capacity,
                Code = stockLocation.Code
            };
        }

        internal static List<StockLocationResponse> GetStockLocationResponseListFromStockLocationList(List<StockLocation> stockLocations)
        {
            List<StockLocationResponse> response = new List<StockLocationResponse>();
            foreach(StockLocation stockLocation in stockLocations)
            {
                response.Add(GetStockLocationResponseFromStockLocation(stockLocation));
            }
            return response;
        }
    }
}
