﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nomina2.Models.DTO;
using nomina2.Models;

namespace nomina2.Models.DAO
{
    public class UserDAO
    {
        public List<UserDTO> ReadUsers(string searchKeyword)
        {
            List<UserDTO> users = new List<UserDTO>();

            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();
                    string selectQuery = "SELECT * FROM tb_users WHERE name LIKE @searchKeyword OR email LIKE @searchKeyword";

                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@searchKeyword", "%" + searchKeyword + "%");

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UserDTO user = new UserDTO();
                                user.Id = reader.GetInt32("id");
                                user.Name = reader.GetString("name");
                                user.Last_Name = reader.GetString("last_name");
                                user.Email = reader.GetString("email");
                                user.Telephone_number = reader.GetString("telephone_number");
                                user.Department_id = reader.GetInt32("department_id");
                                user.Password = reader.GetString("password");
                                user.Type_payment = reader.GetString("type_payment");
                                user.Amount_salary = reader.GetDecimal("amount_salary");
                                user.Role_id = reader.GetInt32("role_id");
                                user.State = reader.GetString("state");
                                user.Update_date = reader.GetDateTime("update_date");
                                user.Update_user = reader.GetString("update_user");
                                user.Create_date = reader.GetDateTime("create_date");
                                user.Create_user = reader.GetString("create_user");
                                users.Add(user);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in nomina.Models.DAO.UserDAO.ReadUsers:" + ex.Message);
            }
            return users;
        }




        public List<UserDTO> ReadUsersSingle(string searchKeyword)
        {
            List<UserDTO> users = new List<UserDTO>();

            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();
                    string selectQuery = "SELECT * FROM tb_users WHERE name LIKE @searchKeyword OR email LIKE @searchKeyword";

                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@searchKeyword", "%" + searchKeyword + "%");

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UserDTO user = new UserDTO();
                                user.Id = reader.GetInt32("id");
                                user.Name = reader.GetString("name");
                                user.Last_Name = reader.GetString("last_name");
                                user.Email = reader.GetString("email");
                                user.Telephone_number = reader.GetString("telephone_number");
                                user.Department_id = reader.GetInt32("department_id");
                                user.Password = reader.GetString("password");
                                user.Type_payment = reader.GetString("type_payment");
                                user.Amount_salary = reader.GetDecimal("amount_salary");
                                user.Role_id = reader.GetInt32("role_id");
                                user.State = reader.GetString("state");
                                user.Update_date = reader.GetDateTime("update_date");
                                user.Update_user = reader.GetString("update_user");
                                user.Create_date = reader.GetDateTime("create_date");
                                user.Create_user = reader.GetString("create_user");
                                users.Add(user);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in nomina.Models.DAO.UserDAO.ReadUsers:" + ex.Message);
            }
            return users;
        }




        public List<UserDTO> ReadUsers3(string searchKeyword)
        {
            List<UserDTO> users = new List<UserDTO>();
            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();
                    string selectUsersQuery = "SELECT id, name, last_name, amount_salary, department_id FROM tb_users WHERE id LIKE @searchKeyword";

                    using (MySqlCommand commandUsers = new MySqlCommand(selectUsersQuery, connection))
                    {
                        commandUsers.Parameters.AddWithValue("@searchKeyword", "%" + searchKeyword + "%");

                        using (MySqlDataReader readerUsers = commandUsers.ExecuteReader())
                        {
                            while (readerUsers.Read())
                            {
                                UserDTO user = new UserDTO();
                                user.Id = readerUsers.GetInt32("id");
                                user.Name = readerUsers.GetString("name");
                                user.Last_Name = readerUsers.GetString("last_name");
                                user.Amount_salary = readerUsers.GetDecimal("amount_salary");
                                user.Department_id = readerUsers.GetInt32("department_id");
                                users.Add(user); // Agregar el usuario a la lista
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in nomina.Models.DAO.UserDAO.ReadUsers:" + ex.Message);
            }
            return users;
        }


        public List<Roles> ReadRoles()
        {
            List<Roles> roles = new List<Roles>();

            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();
                    string selectQuery = "SELECT * FROM tb_roles";

                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Roles role = new Roles();
                                role.Id = reader.GetInt32("id");
                                role.Description = reader.GetString("description");

                                roles.Add(role);
                            }
                        }
                    }
                }
            }


            catch (Exception ex)
            {
                Console.WriteLine("Error in nomina.Models.DAO.Roles.InserUser:" + ex.Message);
            }
            return roles;
        }

        public List<DepartamentDTO> ReadDepartament()
        {
            List<DepartamentDTO> departaments = new List<DepartamentDTO>();

            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();

                    string selectQuery = "SELECT * FROM tb_departaments";

                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DepartamentDTO departament = new DepartamentDTO
                                {
                                    Id_departament = Convert.ToInt32(reader["id_departament"]),
                                    Description = Convert.ToString(reader["description"])
                                };
                                departaments.Add(departament);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in UserDAO.ReadDepartament: " + ex.Message);
            }

            return departaments;
        }


        public string InsertUser(UserDTO user)
        {
            string response = "Failed";
            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();

                    string insertUserQuery = "INSERT INTO tb_users (name, last_name, email, telephone_number, department_id, password, type_payment, amount_salary, role_id, state, update_date, update_user, create_date, create_user ) VALUES (@name, @last_name, @email, @telephone_number, @department_id, @password, @type_payment, @amount_salary, @role_id, @state, @update_date, @update_user, @create_date, @create_user)";

                    using (MySqlCommand userCommand = new MySqlCommand(insertUserQuery, connection))
                    {
                        userCommand.Parameters.AddWithValue("@name", user.Name);
                        userCommand.Parameters.AddWithValue("@last_name", user.Last_Name);
                        userCommand.Parameters.AddWithValue("@email", user.Email);
                        userCommand.Parameters.AddWithValue("@telephone_number", user.Telephone_number);
                        userCommand.Parameters.AddWithValue("@department_id", user.Department_id);
                        userCommand.Parameters.AddWithValue("@password", user.Password);
                        userCommand.Parameters.AddWithValue("@type_payment", string.IsNullOrEmpty(user.Type_payment) ? "" : user.Type_payment);
                        userCommand.Parameters.AddWithValue("@amount_salary", user.Amount_salary);



                        // Verificar si role_id es 0, y asignar 0 si es así
                        if (user.Role_id == 0)
                        {
                            userCommand.Parameters.AddWithValue("@role_id", 0);
                        }
                        else
                        {
                            userCommand.Parameters.AddWithValue("@role_id", user.Role_id);
                        }


                        // Asignar el valor fijo "active" al parámetro @state
                        userCommand.Parameters.AddWithValue("@state", "active");
                        userCommand.Parameters.AddWithValue("@update_date", DateTime.Now);
                        userCommand.Parameters.AddWithValue("@update_user", string.IsNullOrEmpty(user.Update_user) ? "MMORALES" : user.Update_user);
                        // Asignar automáticamente la fecha y hora actual al parámetro @create_date
                        userCommand.Parameters.AddWithValue("@create_date", DateTime.Now);
                        userCommand.Parameters.AddWithValue("@create_user", string.IsNullOrEmpty(user.Create_user) ? "ADMIN" : user.Create_user);

                        int rowsAffected = userCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            response = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in UserDAO.InsertUser: " + ex.Message);
            }

            return response;
        }


        public bool ValidateUser(string email, string password)
        {
            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();
                    string selectQuery = "SELECT COUNT(*) FROM tb_users WHERE email = @Email AND password = @Password";

                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);

                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0; // Si count es mayor a 0, las credenciales son válidas.
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in nomina.Models.DAO.UserDAO.ValidateUser: " + ex.Message);
                return false; // En caso de error, consideramos las credenciales inválidas.
            }
        }



        public UserDTO GetUserDetailsByEmail(string email)
        {
            UserDTO userDetails = null;

            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();

                    string selectQuery = "SELECT id, name, last_name, role_id FROM tb_users WHERE email = @Email";

                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userDetails = new UserDTO
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    Name = reader["name"].ToString(),
                                    Last_Name = reader["last_name"].ToString(),
                                    Role_id = Convert.ToInt32(reader["role_id"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in UserDAO.GetUserDetailsByEmail: " + ex.Message);
            }

            return userDetails;
        }






        public UserDTO GetUserById(int id)
        {
            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();

                    string selectQuery = "SELECT Id, Name, Last_Name, Department_Id, State, Type_Payment, Amount_Salary  FROM tb_users WHERE Id = @id";

                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                UserDTO user = new UserDTO
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = reader["Name"].ToString(),
                                    Last_Name = reader["Last_Name"].ToString(),
                                    Department_id = Convert.ToInt32(reader["Department_Id"]),
                                    State = reader["State"].ToString(),
                                    Type_payment = reader["Type_Payment"].ToString(),
                                    Amount_salary = reader.GetDecimal("Amount_Salary"),

                                };

                                return user;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in UserDAO.GetUserById: " + ex.Message);
            }

            return null;
        }




        public UserDTO GetUserSecurityById(int id)
        {
            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();

                    string selectQuery = "SELECT Id, Name,Last_Name, Email, State FROM tb_users WHERE Id = @id";

                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                UserDTO user = new UserDTO
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = reader["Name"].ToString(),
                                    Last_Name = reader["Last_Name"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    State = reader["State"].ToString(),
                                    Role_id = Convert.ToInt32(reader["Role_Id"]),
                                };

                                return user;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in UserDAO.GetUserById: " + ex.Message);
            }

            return null;
        }

        public UserDTO GetUserSingleById(int id)
        {
            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();

                    string selectQuery = "SELECT Id, Name, Last_Name,telephone_number, Email,department_id, State, Role_Id, type_payment, amount_salary FROM tb_users WHERE Id = @id";

                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                UserDTO user = new UserDTO
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = reader["Name"].ToString(),
                                    Last_Name = reader["Last_Name"].ToString(),
                                    Telephone_number = reader["telephone_number"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Department_id = Convert.ToInt32(reader["department_id"]),
                                    Role_id = Convert.ToInt32(reader["Role_Id"]),
                                    Type_payment = reader["type_payment"].ToString(),
                                    Amount_salary = reader.GetDecimal("amount_salary"),

                                };

                                return user;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in UserDAO.GetUserById: " + ex.Message);
            }

            return null;
        }




        public OvertimeDTO GetUser2ById(int id)
        {
            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();

                    string selectQuery = "SELECT Id, Overtime_description FROM tb_overtime WHERE Id = @id";

                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                OvertimeDTO overtime = new OvertimeDTO
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Overtime_description = reader["Overtime_description"].ToString(),

                                };

                                return overtime;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in UserDAO.GetUserById: " + ex.Message);
            }

            return null;
        }

        public string UpdateUser(UserDTO user)
        {
            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();

                    string updateQuery = "UPDATE tb_users SET name = @name, last_name = @last_name, department_id = @department_id, state = @state, type_payment = @type_payment, amount_salary = @amount_salary, update_date = @update_date, update_user = @update_user WHERE Id = @id";

                    using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@name", user.Name);
                        command.Parameters.AddWithValue("@last_name", user.Last_Name);
                        command.Parameters.AddWithValue("@department_id", user.Department_id);
                        command.Parameters.AddWithValue("@state", user.State);
                        command.Parameters.AddWithValue("@type_payment", user.Type_payment);
                        command.Parameters.AddWithValue("@amount_salary", user.Amount_salary);

                        command.Parameters.AddWithValue("@update_date", DateTime.Now);
                        command.Parameters.AddWithValue("@update_user", string.IsNullOrEmpty(user.Update_user) ? "MMORALES" : user.Update_user);

                        command.Parameters.AddWithValue("@id", user.Id);


                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in UserDAO.UpdateUser: " + ex.Message);
            }

            return "Failed";
        }






        public string UpdateUserSecurity(UserDTO user)
        {
            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();

                    string updateQuery = "UPDATE tb_users SET name = @name, last_name = @last_name, email = @email, state = @state WHERE Id = @id";

                    using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@name", user.Name);
                        command.Parameters.AddWithValue("@last_name", user.Last_Name);
                        command.Parameters.AddWithValue("@email", user.Email);
                        command.Parameters.AddWithValue("@state", user.State);
                        command.Parameters.AddWithValue("@role_id", user.Role_id);
                        command.Parameters.AddWithValue("@update_date", DateTime.Now);
                        command.Parameters.AddWithValue("@update_user", string.IsNullOrEmpty(user.Update_user) ? "MMORALES" : user.Update_user);
                        command.Parameters.AddWithValue("@id", user.Id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in UserDAO.UpdateUser: " + ex.Message);
            }

            return "Failed";
        }




        public string UpdateUserSingle(UserDTO user)
        {
            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();

                    string updateQuery = "UPDATE tb_users SET name = @name, last_name = @last_name, email = @email, telephone_number = @telephone_number WHERE Id = @id";

                    using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@name", user.Name);
                        command.Parameters.AddWithValue("@last_name", user.Last_Name);
                        command.Parameters.AddWithValue("@telephone_number", user.Telephone_number);
                        command.Parameters.AddWithValue("@email", user.Email);
                        command.Parameters.AddWithValue("@update_date", DateTime.Now);
                        command.Parameters.AddWithValue("@update_user", string.IsNullOrEmpty(user.Update_user) ? "MMORALES" : user.Update_user);
                        command.Parameters.AddWithValue("@id", user.Id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in UserDAO.UpdateUser: " + ex.Message);
            }

            return "Failed";
        }



        public string DeleteUser(int id)
        {
            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();

                    string deleteQuery = "DELETE FROM tb_users WHERE Id = @id";

                    using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in UserDAO.DeleteUser: " + ex.Message);
            }
            return "Failed";
        }

    }
}