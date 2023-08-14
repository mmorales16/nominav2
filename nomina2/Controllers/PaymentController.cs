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
        private UserDAO userRepostiory = new UserDAO();




        // GET: Payment
        public ActionResult ListPayment(string searchKeyword)
        {
            ViewBag.SearchKeyword = searchKeyword; // Guarda el valor en ViewBag

            // Obtén la lista de pagos y la lista de usuarios
            List<PaymentDTO> payments = paymentRepository.ReadPayments(searchKeyword);
            List<UserDTO> users = userRepostiory.ReadUsers3(searchKeyword); // Cambia "userRepostiory" al nombre correcto de tu repositorio de usuarios

            // Crea un modelo compuesto que incluya tanto la lista de pagos como la lista de usuarios
            var model = new Tuple<List<PaymentDTO>, List<UserDTO>>(payments, users);

            // Devuelve la vista con el modelo compuesto
            return View(model);
        }


        public ActionResult CreatePayment(int userId, decimal amountSalary, decimal department)
        {
            ViewBag.UserId = userId; // Pasar el ID de usuario a la vista
            ViewBag.AmountSalary = amountSalary; // Pasar el AmountSalary a la vista
            ViewBag.Department_id = department; // Pasar el AmountSalary a la vista

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