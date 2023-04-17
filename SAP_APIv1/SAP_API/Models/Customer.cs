using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.Models
{
    public class Customer: IEntity
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Telephone { get; set; }
    }
}
