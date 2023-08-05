using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace nomina2.Models
{
    public class Roles
    {
        public int Id { get; set; }

        [StringLength(15)]
        public string Description { get; set; }
    }
}