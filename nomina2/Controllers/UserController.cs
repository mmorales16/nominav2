
using nomina2.Models.DAO;
using nomina2.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nomina2.Controllers
{
    public class UserController : Controller
    {
        private UserDAO userRepository = new UserDAO();
        private readonly OvertimeDAO overtimeRepository = new OvertimeDAO();
        private readonly DeductionDAO deductionRepository = new DeductionDAO();

        // GET: User
        public ActionResult ListUser(string searchKeyword)
        {
            // Devuelve la vista Index con la lista de usuarios
            return View(userRepository.ReadUsers(searchKeyword));
        }



        public ActionResult RolesList()
        {
            // Devuelve la vista Index con la lista de usuarios
            return View(userRepository.ReadRoles());
        }

        public ActionResult ListDepartament()
        {
            // Devuelve la vista Index con la lista de usuarios
            return View(userRepository.ReadDepartament());
        }

        public ActionResult CreateUser()
        {
            // Obtener los roles disponibles
            var roles = userRepository.ReadRoles().ToList();
            var departaments = userRepository.ReadDepartament().ToList();
            ViewBag.Roles = new SelectList(roles, "id", "Description");
            ViewBag.Departaments = new SelectList(departaments, "id_departament", "Description");

            return View();
        }


        [HttpPost]
        public ActionResult CreateUser(UserDTO user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (user.Password == user.ConfirmPassword)
                    {
                        string result = userRepository.InsertUser(user);

                        if (result == "Success")
                        {
                            // Redireccionar a la vista "Index" en caso de éxito
                            return RedirectToAction("Login");
                        }
                        else
                        {
                            // Si la inserción falla, agregar un mensaje de alerta a la ViewBag
                            ViewBag.ErrorMessage = "Error al insertar el usuario en la base de datos.";
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("ConfirmPassword", "Passwords do not match");
                    }
                }

                // Si la validación falla, recuperar las listas de roles y departamentos
                var roles = userRepository.ReadRoles().ToList();
                var departaments = userRepository.ReadDepartament().ToList();
                ViewBag.Roles = new SelectList(roles, "id", "Description");
                ViewBag.Departaments = new SelectList(departaments, "id_departament", "Description");

                return View(user);
            }
            catch (Exception ex)
            {
                // En caso de excepción, agregar un mensaje de alerta a la ViewBag
                ViewBag.ErrorMessage = "Ocurrió un error durante la inserción del usuario: " + ex.Message;
                return View(user);
            }
        }



        [HttpGet]
        public ActionResult Login()
        {
            // Muestra la vista de inicio de sesión
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserDTO user)
        {
            
                UserDAO userDAO = new UserDAO();
                bool isValidCredentials = userDAO.ValidateUser(user.Email, user.Password);

                if (isValidCredentials)
                {
                // Credenciales válidas, permite el acceso (por ejemplo, redirige a la página de inicio)
                    return RedirectToAction("Index", "Home");
            }
                else
                {
                    // Credenciales inválidas, muestra mensaje de error
                    ViewBag.ErrorMessage = "Incorrect credentials, check the email and password";
                }
            

            // Si el modelo no es válido o las credenciales son inválidas, vuelve a mostrar la vista de inicio de sesión con los errores
            return View(user);
        }

        public ActionResult EditUser(int id)
        {
            try
            {
                // Intenta obtener un usuario específico utilizando el método GetUserById del repositorio UserDAO
                UserDTO user = userRepository.GetUserById(id);
                if (user != null)
                {
                    // Si el usuario existe, muestra la vista de edición con los detalles del usuario
                    return View(user);
                }
                else
                {
                    Console.WriteLine("User not found");
                    return RedirectToAction("ListUser");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting user: " + ex.Message);
                return RedirectToAction("ListUser");
            }
        }

        public ActionResult EditUser2(int id)
        {
            try
            {
                // Intenta obtener un usuario específico utilizando el método GetUserById del repositorio UserDAO
                OvertimeDTO overtime = userRepository.GetUser2ById(id);
                if (overtime != null)
                {
                    // Si el usuario existe, muestra la vista de edición con los detalles del usuario
                    return View(overtime);
                }
                else
                {
                    Console.WriteLine("User not found");
                    return RedirectToAction("ListUser");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting user: " + ex.Message);
                return RedirectToAction("ListUser");
            }
        }


        // POST: User/Edit/
        [HttpPost]
        public ActionResult EditUser(UserDTO user)
        {
            try
            {
                // Intenta actualizar los detalles del usuario utilizando el método UpdateUser del repositorio UserDAO
                string result = userRepository.UpdateUser(user);
                Console.WriteLine("User updated: " + result);
                return RedirectToAction("ListUser");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating user: " + ex.Message);
                return View(user);
            }
        }
        // GET: User/Delete/
        public ActionResult DeleteUser(int id)
        {
            // Obtiene un usuario específico utilizando el método GetUserById del repositorio UserDAO
            UserDTO user = userRepository.GetUserById(id);
            if (user == null)
            {
                // El usuario no existe, mostrar mensaje de error o redirigir a otra vista
                return RedirectToAction("ListUser");
            }

            return View(user);
        }
        // POST: User/Delete/
        [HttpPost]
        [ActionName("DeleteUser")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // Intenta eliminar el usuario utilizando el método DeleteUser del repositorio UserDAO
                string result = userRepository.DeleteUser(id);
                Console.WriteLine("User deleted: " + result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting user: " + ex.Message);
            }
            // Redirige a la vista Index después de eliminar el usuario
            return RedirectToAction("ListUser");
        }
    }

}