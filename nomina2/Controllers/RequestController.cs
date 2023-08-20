﻿using nomina2.Models.DAO;
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
                string result = requestRepository.CreateRequest(request);

                if (result == "Success")
                {
                    int Id_actual = request.Id;

                    // Crear una instancia de RouteValueDictionary para mantener los parámetros de filtro
                    var routeValues = new RouteValueDictionary(new { id = Id_actual });

                    // Redireccionar a la vista "ListDeduction" en caso de éxito
                    return RedirectToAction("CreateRequest", routeValues);
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
                ViewBag.ErrorMessage = "Ocurrió un error durante la inserción del CreateRequest: " + ex.Message;
            }

            // Devolver la vista "CreateDeduction" con los datos de deduction ingresados previamente y el mensaje de alerta (si corresponde).

            return View(request);
        }


      


    }
}
