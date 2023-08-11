using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nomina2.Models.DTO
{
    public class DeductionDTO
    {
        public string Type_action { get; set; }
        public decimal Deduction_value { get; set; }
        public string Deduction_description { get; set; }
        public int Deduction_id { get; set; }
        public string Create_deduction { get; set; }
        public string Update_deduction { get; set; }
        public int Id { get; set; }

        //public static implicit operator bool(DeductionDTO v) => throw new NotImplementedException();
    }
}