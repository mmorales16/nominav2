using nomina2.Models.DAO;
using nomina2.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nomina2.Controllers
{
    public class DeductionController : Controller
    {
        private DeductionDAO deductionRepository = new DeductionDAO();

        // public ActionResult ListOvertime()
        // {
        //     return View(overtimeRepository.ReadOvertime());
        // }


        public ActionResult ListDeduction(int id)
        {
            // Obtener la lista de overtime filtrada por el ID de usuario
            List<DeductionDTO> userDeduction = deductionRepository.ReadDeductionByUserId(id);

            // Pasar la lista filtrada a la vista

            ViewBag.UserId = id;
            return View(userDeduction);
        }
    }
}
