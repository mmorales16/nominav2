﻿using nomina2.Models.DAO;
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

        public ActionResult EditDeduction(int id)
        {
            try
            {
                // Intenta obtener un usuario específico utilizando el método GetUserById del repositorio UserDAO
                DeductionDTO deduction = deductionRepository.GetDeductionById(id);
                if (deduction != null)
                {
                    // Si el usuario existe, muestra la vista de edición con los detalles del usuario
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
                Console.WriteLine("Error getting deduction: " + ex.Message);
                return RedirectToAction("ListDeduction");
            }
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }

        public DeductionDAO GetDeductionRepository()
        {
            return deductionRepository;
        }

        public ActionResult DeleteDedcution(int id)
        {
            bool success = deductionRepository.SoftDeleteDeduction(id);
            if (success)
            {
                return RedirectToAction("ListDeduction", new { id = id });
            }
            else
            {
                ViewBag.ErrorMessage = "Error al eliminar el registro.";
                return View("ListOvertime", deductionRepository.ReadActiveDeductionByUserId(id));
            }
        }


    }
}
