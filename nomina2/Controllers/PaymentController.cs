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
                    int Id_actual = payment.Id_user;

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


        // GET: Payment/EditPayment/5
        public ActionResult EditPayment(int id, int userId, decimal amountSalary, decimal department)
        {
            ViewBag.UserId = userId; // Pasar el ID de usuario a la vista
            ViewBag.AmountSalary = amountSalary; // Pasar el AmountSalary a la vista
            ViewBag.Department_id = department; // Pasar el AmountSalary a la vista
            try
            {
                // Obtén el pago específico utilizando el método GetPaymentById del repositorio PaymentDAO
                PaymentDTO payment = paymentRepository.GetPaymentById(id);
                if (payment != null)
                {
                    // Muestra la vista de edición con los detalles del pago
                    return View(payment);
                }
                else
                {
                    Console.WriteLine("Payment not found");
                    return RedirectToAction("ListPayment");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting payment: " + ex.Message);
                return RedirectToAction("ListPayment");
            }
        }

        // POST: Payment/EditPayment/5
        [HttpPost]
        public ActionResult EditPayment(PaymentDTO payment)
        {
            try
            {
                string result = paymentRepository.UpdatePayment(payment);
                Console.WriteLine("Payment updated: " + result);

                int Id_actual = payment.Id_user;

                // Redireccionar a la vista "ListPayment" con el UserId como valor de búsqueda
                return RedirectToAction("ListPayment", new { searchKeyword = Id_actual });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating payment: " + ex.Message);
                return View(payment);
            }
        }

        public ActionResult DeletePayment(int id, int userId)
        {
            ViewBag.UserId = userId; // Pasar el ID de usuario a la vista
            try
            {
                string result = paymentRepository.DeletePayment(id);
                Console.WriteLine("Payment deleted: " + result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting payment: " + ex.Message);
            }

            // Redireccionar a la vista "ListPayment" después de eliminar el pago
            int Id_actual = userId;

            // Redireccionar a la vista "ListPayment" con el UserId como valor de búsqueda
            return RedirectToAction("ListPayment", new { searchKeyword = Id_actual });
            //return RedirectToAction("ListPayment");
        }







    }
}