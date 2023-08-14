using nomina2.Models.DAO;
using nomina2.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace nomina9.Controllers
{
    public class PaymentController : Controller
    {
        private PaymentDAO paymentRepository = new PaymentDAO();




        // GET: Payment
        public ActionResult ListPayment(string searchKeyword)


        {
            ViewBag.SearchKeyword = searchKeyword; // Guarda el valor en ViewBag

            // Devuelve la vista Index con la lista de Payment
            return View(paymentRepository.ReadPayments(searchKeyword));
        }

        public ActionResult CreatePayment(int userId, decimal amountSalary)
        {
            ViewBag.UserId = userId; // Pasar el ID de usuario a la vista
            ViewBag.AmountSalary = amountSalary; // Pasar el AmountSalary a la vista

            return View();
        }



        // POST: User/Create
        [HttpPost]
        public ActionResult CreatePayment(PaymentDTO payment)
        {
            try
            {
                string result = paymentRepository.InsertPayment(payment);

                if (result == "Success")
                {
                    int Id_actual = payment.Id;

                    // Redireccionar a la vista "ListPayment" con el UserId como valor de búsqueda
                    return RedirectToAction("ListPayment", new { searchKeyword = Id_actual });
                }
                else
                {
                    // Si la inserción falla, agregar un mensaje de alerta a la ViewBag
                    ViewBag.ErrorMessage = "Error al insertar el overtime en la base de datos.";
                }
            }
            catch (Exception ex)
            {
                // En caso de excepción, agregar un mensaje de alerta a la ViewBag
                ViewBag.ErrorMessage = "Ocurrió un error durante la inserción del overtime: " + ex.Message;
            }

            // Devolver la vista "Createpayment" con los datos del overtime ingresados previamente y el mensaje de alerta (si corresponde).
            return View(payment);
        }


    }
}