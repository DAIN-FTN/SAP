

using SAP_API.Validation;
using System.ComponentModel.DataAnnotations;

namespace SAP_API.DTOs.Requests
{
    public class UpdateProductRequest
    {
        [Required(ErrorMessage = "Name must be provided.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "BakingTimeInMins must be provided.")]
        public int? BakingTimeInMins { get; set; }
        [Required(ErrorMessage = "BakingTempInC must be provided.")]
        public int? BakingTempInC { get; set; }
        [Required(ErrorMessage = "Size must be provided.")]
        public int? Size { get; set; }

        public CustomValidationResult IsValid()
        {
            if (Size < 0)
                return new CustomValidationResult(false, "Size must be positive");

            if (BakingTempInC < 0)
                return new CustomValidationResult(false, "BakingTempInC must be positive");

            if (BakingTimeInMins < 0)
                return new CustomValidationResult(false, "BakingTimeInMins must be positive");

            return new CustomValidationResult(true, null);

        }
    }
}
