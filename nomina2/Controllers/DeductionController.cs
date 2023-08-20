using nomina2.Models.DAO;
using nomina2.Models.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace nomina2.Controllers
{
 
    public class DeductionController : Controller
    {
        private DeductionDAO deductionRepository = new DeductionDAO();


        public ActionResult ListDeduction(int id)
        {
            // Obtener la lista de deduction filtrada por el ID de usuario
            List<DeductionDTO> userDeduction = deductionRepository.ReadActiveDeductionByUserId(id);

            // Pasar la lista filtrada a la vista

            ViewBag.UserId = id;
            return View(userDeduction);
        }

        public ActionResult CreateDeduction(int userId)
        {
            ViewBag.UserId = userId; // Pasar el ID de usuario a la vista

            return View();
        }



        // POST: Deduction/Create
        [HttpPost]
        public ActionResult CreateDeduction(DeductionDTO deduction)
        {
            try
            {
                string result = deductionRepository.InsertDeduction(deduction);

                if (result == "Success")
                {
                    int Id_actual = deduction.Id;

                    // Crear una instancia de RouteValueDictionary para mantener los parámetros de filtro
                    var routeValues = new RouteValueDictionary(new { id = Id_actual });

                    // Redireccionar a la vista "ListDeduction" en caso de éxito
                    return RedirectToAction("ListDeduction", routeValues);
                }
                else
                {
                    // Si la inserción falla, agregar un mensaje de alerta a la ViewBag
                    ViewBag.ErrorMessage = "Error al insertar el deduction en la base de datos.";
                }
            }
            catch (Exception ex)
            {
                // En caso de excepción, agregar un mensaje de alerta a la ViewBag
                ViewBag.ErrorMessage = "Ocurrió un error durante la inserción del overtime: " + ex.Message;
            }

            // Devolver la vista "CreateDeduction" con los datos de deduction ingresados previamente y el mensaje de alerta (si corresponde).

            return View(deduction);
        }



        // GET: Payment/EditPayment/5
        public ActionResult UpdateDeduction(int id, int userId)
        {
            ViewBag.UserId = userId; // Pasar el ID de usuario a la vista
            try
            {
                // Obtén el pago específico utilizando el método GetPaymentById del repositorio PaymentDAO
                DeductionDTO deduction = deductionRepository.GetDeductionById(id);
                if (deduction != null)
                {
                    // Muestra la vista de edición con los detalles del pago

                    return View(deduction);
                }
                else
                {
                    Console.WriteLine("Deduction not found");
                    return RedirectToAction("ListDeduction");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating deduction: " + ex.Message);
                return View("ListDeduction");
            }
        }

        // POST: Payment/EditPayment/5
        [HttpPost]
        public ActionResult UpdateDeduction(DeductionDTO deduction)
        {
            try
            {
                string result = deductionRepository.UpdateDeduction(deduction);
                Console.WriteLine("Deduction updated: " + result);

                int Id_actual = deduction.Id;

                // Redireccionar a la vista "ListPayment" con el UserId como valor de búsqueda
                return RedirectToAction("ListDeduction", new { searchKeyword = Id_actual });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating deduction: " + ex.Message);
                return View(deduction);
            }
        }




        public DeductionDAO GetDeductionRepository()
        {
            return deductionRepository;
        }

        public ActionResult DeleteDeduction(int id, int userId)
        {
            bool success = deductionRepository.SoftDeleteDeduction(id);
            if (success)
            { 

                int Id_actual = userId;
                var routeValues = new RouteValueDictionary(new { id = Id_actual });
                return RedirectToAction("ListDeduction", routeValues);
            }
            else
            {
                ViewBag.ErrorMessage = "Error al eliminar el registro.";
                return View("ListDeduction", deductionRepository.ReadActiveDeductionByUserId(id));
            }
        }


    }
}
