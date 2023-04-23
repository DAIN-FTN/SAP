
using SAP_API.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace SAP_API.DTOs.Requests
{
    public class UpdateStockedProductRequest
    {
        [Required(ErrorMessage = "LocationId must be provided.")]
        public Guid? LocationId { get; set; }
        [Required(ErrorMessage = "ProductId must be provided.")]
        public Guid? ProductId { get; set; }
        [Required(ErrorMessage = "Quantity must be provided.")]
        public int? Quantity { get; set; }
        [Required(ErrorMessage = "ReservedQuantity must be provided.")]
        public int? ReservedQuantity { get; set; }

        public CustomValidationResult IsValid()
        {
            if(Quantity < 0 || ReservedQuantity < 0)
                return new CustomValidationResult(false, "Quantity and ReservedQuantity must be positive");
            if (Quantity < ReservedQuantity)
                return new CustomValidationResult(false, "Quantity cannot be less than ReservedQuantity");
            return new CustomValidationResult(true, "");
        }
    }
}
