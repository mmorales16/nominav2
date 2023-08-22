using nomina2.Models.DAO;
using nomina2.Models.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace nomina2.Controllers
{
    public class RequestController : Controller
    {
        private RequestDAO requestRepository = new RequestDAO();


        public ActionResult RequestList()
        {
            List<RequestDTO> allRequests = requestRepository.ReadAllRequests(); // Modificar el nombre del método según corresponda

            return View(allRequests);
        }


        public ActionResult RequestListAdmin()
        {
            List<RequestDTO> allRequests = requestRepository.ReadAllRequests(); // Modificar el nombre del método según corresponda

            return View(allRequests);
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
                // Quemar los valores predeterminados
                request.Active = 1;
                request.Pending = true;
                request.Pending_request = "MMORALES";
                string result = requestRepository.CreateRequest(request);

                if (result == "Success")
                {
                    //request.Active = 2; // Por defecto, establecer Active como 1
                    //request.Pending = true; // Por defecto, establecer Pending como true
                    int Id_actual = request.Id;

                    // Redirect to the "RequestList" action with the userId parameter
                    return RedirectToAction("CreateRequest", new { userId = Id_actual });
                }
                else
                {
                    // If the insertion fails, add an error message to the ViewBag
                    ViewBag.ErrorMessage = "Error al insertar el deduction en la base de datos.";
                }
            }
            catch (Exception ex)
            {
                // In case of an exception, add an error message to the ViewBag
                ViewBag.ErrorMessage = "Ocurrió un error durante la inserción del CreateRequest: " + ex.Message;
            }

            // Return the "CreateRequest" view with the entered request data and the error message (if applicable).
            return View(request);
        }


        public ActionResult PendingRequest(int userId)
        {
            ViewBag.UserId = userId; // Pasar el ID de usuario a la vista

            return View();
        }

        [HttpPost]
        public ActionResult PendingRequest(RequestDTO request)
        {
            try
            {
                string result = requestRepository.PendingRequest(request);

                if (result == "Success")
                {
                    int Id_actual = request.Id;
                    // Redireccionar a la vista de lista de solicitudes después de la actualización exitosa
                    return RedirectToAction("RequestListAdmin", new { userId = Id_actual });
                }
                else
                {
                    ViewBag.ErrorMessage = "Error al actualizar la solicitud en la base de datos.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error durante la actualización de la solicitud: " + ex.Message;
            }

            // Devolver la vista con los datos de la solicitud actualizada y el mensaje de alerta (si corresponde)
            return View(request);
        }



    }
}
