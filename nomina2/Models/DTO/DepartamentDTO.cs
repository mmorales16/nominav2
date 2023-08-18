using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace nomina2.Models.DTO
{
    public class DepartamentDTO
    {
        public int Id_departament { get; set; }

        [StringLength(15)]
        public string Description { get; set; }
    }
}