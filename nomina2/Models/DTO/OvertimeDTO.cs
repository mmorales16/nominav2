using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nomina2.Models.DTO
{
    public class OvertimeDTO
    {
        public string Type_action { get; set; }
        public decimal Overtime_value { get; set; }
        public string Overtime_description { get; set; }
        public int Overtime_id { get; set; }
        public string Create_overtime { get; set; }
        public string Update_overtime { get; set; }
        public int Id { get; set; }
    }
}