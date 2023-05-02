using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace SAP_API.Models
{
    public class User: IEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public bool Active { get; set; }
        public List<BakingProgram> BakingProgramsMade { get; set; }
    }
}
