﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.Models
{
    public interface IEntity
    {
        public Guid Id { get; set; }
    }
}
