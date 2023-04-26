using SAP_API.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.DTOs.Requests
{
    public class CreateProductRequest
    {
        [Required(ErrorMessage = "Name must be provided.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "BakingTimeInMins must be provided.")]
        public int? BakingTimeInMins { get; set; }
        [Required(ErrorMessage = "BakingTempInC must be provided.")]
        public int? BakingTempInC { get; set; }
        [Required(ErrorMessage = "Size must be provided.")]
        public int? Size { get; set; }
        public List<CreateStockedProductRequest> Stock { get; set; }

        public CustomValidationResult IsValid()
        {
            if (Size < 0)
                return new CustomValidationResult(false, "Size must be positive");

            if(BakingTempInC < 0)
                return new CustomValidationResult(false, "BakingTempInC must be positive");

            if (BakingTimeInMins < 0)
                return new CustomValidationResult(false, "BakingTimeInMins must be positive");


            foreach (CreateStockedProductRequest stock in Stock)
            {
                CustomValidationResult validationResult = stock.IsValid();
                if(!validationResult.Success)
                    return new CustomValidationResult(false, validationResult.ErrorMessage);
            }

            return new CustomValidationResult(true, null);

        }
    }
}
