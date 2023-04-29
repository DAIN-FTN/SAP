using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.Validation
{
    public class CustomValidationResult
    {

        public CustomValidationResult(bool v1, string v2)
        {
            this.Success = v1;
            this.ErrorMessage = v2;
        }

        public string? ErrorMessage { get; set; }
        public bool Success { get; set; }
    }
}
