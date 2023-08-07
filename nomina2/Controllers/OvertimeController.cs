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
    public class OvertimeController : Controller
    {
        private OvertimeDAO overtimeRepository = new OvertimeDAO();


    


        public ActionResult ListOvertime(int id)
        {
            // Obtener la lista de overtime filtrada por el ID de usuario
            List<OvertimeDTO> userOvertimes = overtimeRepository.ReadOvertimeByUserId(id);

            // Pasar la lista filtrada a la vista

            ViewBag.UserId = id;
            return View(userOvertimes);
        }


        public ActionResult CreateOvertime(int userId)
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
                string result = overtimeRepository.InsertOvertime(overtime);;

                if (result == "Success")

                {

                    int Id_actual = overtime.Id;

                    // Crear una instancia de RouteValueDictionary para mantener los parámetros de filtro

                    var routeValues = new RouteValueDictionary(new { id = Id_actual });
                    // Redireccionar a la vista "Index" en caso de éxito

                    return RedirectToAction("ListOvertime", routeValues);



                }
                else
                {
                    // Si la inserción falla, agregar un mensaje de alerta a la ViewBag
                    ViewBag.ErrorMessage = "Error al insertar el usuario en la base de datos.";
                }
            }
            catch (Exception ex)
            {
                // En caso de excepción, agregar un mensaje de alerta a la ViewBag
                ViewBag.ErrorMessage = "Ocurrió un error durante la inserción del usuario: " + ex.Message;
            }

            // Devolver la vista "CreateUser" con los datos del usuario ingresados previamente
            // y el mensaje de alerta (si corresponde).


            return View(overtime);
        }








    }
}
