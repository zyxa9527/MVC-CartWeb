﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication9.Models
{
    public class User
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }

    }
}