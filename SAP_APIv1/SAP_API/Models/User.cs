using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace SAP_API.Models
{
    public class User: IEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public List<BakingProgram> BakingProgramsMade { get; set; }
    }
}
