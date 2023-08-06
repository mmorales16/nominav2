using nomina2.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nomina2.Controllers
{
    public class OvertimeController : Controller
    {
        private OvertimeDAO overtimeRepository = new OvertimeDAO();
        public ActionResult ListOvertime()
        {
            return View(overtimeRepository.ReadOvertime());
        }
    }
}
