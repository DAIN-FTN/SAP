using SAP_API.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace SAP_API.DTOs.Requests
{
    public class CreateStockedProductRequest
    {
        public Guid? ProductId { get; set; }
        [Required(ErrorMessage = "LocationId must be provided.")]
        
        public Guid? LocationId { get; set; }
        [Required(ErrorMessage = "Quantity must be provided.")]
        public int? Quantity { get; set; }

        public CustomValidationResult IsValid()
        {
            if(Quantity < 0)
                return new CustomValidationResult(false, "Quantity must be positive");

            return new CustomValidationResult(true, null);
        }
    }
}
