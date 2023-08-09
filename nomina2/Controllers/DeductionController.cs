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
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
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



        // POST: User/Create
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

        private string GetDebuggerDisplay()
        {
            return ToString();
        }

        public DeductionDAO GetDeductionRepository()
        {
            return deductionRepository;
        }

        // GET: Deduction/Delete/
        public ActionResult DeleteDeduction(int id, DeductionDAO deductionRepository)
        {
            // Obtiene un usuario específico utilizando el método GetDeductionById del repositorio DeductionDAO
            DeductionDTO deduction = deductionRepository.GetDeductionById(id);
            bool v = deduction;
            if (v)
            {
                // El usuario no existe, mostrar mensaje de error o redirigir a otra vista
                return RedirectToAction("ListDeduction");
            }

            return View(deduction);
        }
        // POST: User/Delete/
        [HttpPost]
        [ActionName("DeleteDeduction")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // Intenta eliminar el usuario utilizando el método DeleteUser del repositorio UserDAO
                string result = deductionRepository.DeleteDeduction(id);
                Console.WriteLine("Deduction deleted: " + result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting deduction: " + ex.Message);
            }
            // Redirige a la vista Index después de eliminar el usuario
            return RedirectToAction("ListDeduction");
        }

    }
}
