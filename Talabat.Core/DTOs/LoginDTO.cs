﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.DTOs
{
    public class LoginDTO
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }

    }
}
