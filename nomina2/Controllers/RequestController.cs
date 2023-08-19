using nomina2.Models.DAO;
using nomina2.Models.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace nomina2.Controllers
{
    public class RequestController : Controller
    {
        private RequestDAO requestRepository = new RequestDAO();    


        public ActionResult ListRequest(int id)
        {
            // Obtener la lista de overtime filtrada por el ID de usuario
            List<RequestDTO> userRequest = requestRepository.ReadActiveRequestByUserId(id);

            // Pasar la lista filtrada a la vista

            ViewBag.UserId = id;
            return View(userRequest);
        }

        // GET: LicenseRequest
        public ActionResult CraeteRequest(int id)
        {

            // Obtener la lista de deduction filtrada por el ID de usuario
            List<RequestDTO> userRequest = requestRepository.ReadActiveRequestByUserId(id);

            // Pasar la lista filtrada a la vista

            ViewBag.UserId = id;
            return View(userRequest);
        }

        public ActionResult CreateRequest(int userId)
        {
            ViewBag.UserId = userId; // Pasar el ID de usuario a la vista

            return View();
        }

        // POST: License/Create
        [HttpPost]
        public ActionResult CreateRequest(RequestDTO request)
        {
            try
            {
                string result = requestRepository.CreateRequest(request);

                if (result == "Success")
                {
                    int Id_actual = request.Id;

                    // Crear una instancia de RouteValueDictionary para mantener los parámetros de filtro
                    var routeValues = new RouteValueDictionary(new { id = Id_actual });

                    // Redireccionar a la vista "ListDeduction" en caso de éxito
                    return RedirectToAction("CreateLicense", routeValues);
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

            return View(request);
        }


        /* public ActionResult CreateOvertime(int userId)
         {
             ViewBag.UserId = userId; // Pasar el ID de usuario a la vista

             return View();
         }



         // POST: User/Create
         [HttpPost]
         public ActionResult CreateOvertime(OvertimeDTO overtime)
         {
             try
             {
                 string result = overtimeRepository.InsertOvertime(overtime);

                 if (result == "Success")
                 {
                     int Id_actual = overtime.Id;

                     // Crear una instancia de RouteValueDictionary para mantener los parámetros de filtro
                     var routeValues = new RouteValueDictionary(new { id = Id_actual });

                     // Redireccionar a la vista "ListOvertime" en caso de éxito
                     return RedirectToAction("ListOvertime", routeValues);
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

             // Devolver la vista "CreateOvertime" con los datos del overtime ingresados previamente y el mensaje de alerta (si corresponde).
             return View(overtime);
         }

         public ActionResult DeleteOvertime(int id)
         {
             bool success = overtimeRepository.SoftDeleteOvertime(id);
             if (success)
             {
                 return RedirectToAction("ListOvertime", new { id = id });
             }
             else
             {
                 ViewBag.ErrorMessage = "Error al eliminar el registro.";
                 return View("ListOvertime", overtimeRepository.ReadActiveOvertimeByUserId(id));
             }
         }*/





    }
}
