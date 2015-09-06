using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Motonet.Models
{
    public class Settings
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string AdminPassword { get; set; }
    }
}