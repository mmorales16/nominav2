using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace nomina2.Models.DTO
{
    public class PaymentDTO
    {
        public int Id_payment { get; set; }
        public int Id_user { get; set; }
        public decimal Salary { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public int Worked_days { get; set; }
        public int Regular_Hours { get; set; }
        public int Overtime_hours { get; set; }
        public decimal Gross_Salary { get; set; }
        public string Detail { get; set; }
        public string Observation { get; set; }
        public DateTime Update_date { get; set; }
        public string Update_user { get; set; }
        public DateTime Create_date { get; set; }
        public string Create_user { get; set; }

        public string UserName { get; set; } // Propiedad para almacenar el nombre del usuario
        public string LastName { get; set; } // Propiedad para almacenar el nombre del usuario
        public decimal AmountSalary { get; set; } // Propiedad para almacenar el nombre del usuario 
    }
}