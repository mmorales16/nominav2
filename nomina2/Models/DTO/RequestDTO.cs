using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nomina2.Models.DTO
{
    public class RequestDTO
    {
        public string Type_request { get; set; }
        //public decimal Overtime_value { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public int Available_days { get; set; }
        public string Request_note { get; set; }
        public int Request_id { get; set; }
        public string CreateLicense { get; set; }
        public string Update_request { get; set; }
        public int Id { get; set; }
    }
}