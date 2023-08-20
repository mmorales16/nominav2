using nomina2.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nomina2.Controllers
{
    public class PaymentResumenController : Controller
    {

        private PaymentResumenDAO paymentResumen = new PaymentResumenDAO();



        // GET: User
        public ActionResult ListPaymentResumen()
        {
            // Devuelve la vista Index con la lista de usuarios
            return View(paymentResumen.ReadPaymentResumen());
        }



    }
}