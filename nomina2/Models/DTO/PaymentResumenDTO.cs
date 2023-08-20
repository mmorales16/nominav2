using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nomina2.Models.DTO
{
    public class PaymentResumenDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public decimal GrossSalary { get; set; }
        public int WorkedDays { get; set; }
        public decimal TotalMontoDeduction { get; set; }
        public decimal TotalMontoOvertime { get; set; }
        public decimal TotalDeductionPorcentaje { get; set; }
        public decimal TotalOvertimePorcentaje { get; set; }
        public decimal TotalToPay { get; set; }
    }
}