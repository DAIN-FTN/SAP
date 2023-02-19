using SAP_API.DTOs.Responses;
using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.Services
{
    public interface IStartPreparingService
    {
        public void SetProgramToPrepare(BakingProgram bakingProgram);

        public void UseReservedProductsFromOrdersForPreparing();
        public StartPreparingResponse CreateStartPreparingResponse();


    }
}
