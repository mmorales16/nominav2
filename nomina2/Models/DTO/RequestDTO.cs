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
        public string Request_note { get; set; }
        public int Request_id { get; set; }
        public string Create_request { get; set; }
        public string Update_request { get; set; }
        public int Id { get; set; }
    }
}