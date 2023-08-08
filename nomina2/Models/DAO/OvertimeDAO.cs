using MySql.Data.MySqlClient;
using nomina2.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nomina2.Models.DAO
{
    public class OvertimeDAO
    {
        public List<OvertimeDTO> ReadOvertime()
        {
            List<OvertimeDTO> overtimes = new List<OvertimeDTO>();
            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();
                    string selectQuery = "SELECT * FROM tb_overtime WHERE state = 1";
                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                OvertimeDTO overtime = new OvertimeDTO();
                                //overtime.Id = reader.GetInt32("id");
                                overtime.Overtime_id = reader.GetInt32("id_overtime");
                                overtime.Overtime_description = reader.GetString("description");
                                //overtime.Type_action = reader.GetString("type_action");
                                overtimes.Add(overtime);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in nomina.Models.DAO.Roles.InserUser:" + ex.Message);
            }
            return overtimes;
        }





        public List<OvertimeDTO> ReadActiveOvertimeByUserId(int userId)
        {
            List<OvertimeDTO> overtimes = new List<OvertimeDTO>();
            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();
                    string selectQuery = "SELECT * FROM tb_overtime WHERE user_id = @userId AND state = 1"; // Filtrar solo registros activos
                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                OvertimeDTO overtime = new OvertimeDTO();
                                overtime.Overtime_id = reader.GetInt32("id_overtime");
                                overtime.Id = reader.GetInt32("user_id");
                                overtime.Overtime_description = reader.GetString("description");
                                overtime.Type_action = reader.GetString("type_action");
                                overtime.Overtime_value = reader.GetDecimal("value");
                                overtimes.Add(overtime);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in nomina2.Models.DAO.OvertimeDAO.ReadActiveOvertimeByUserId: " + ex.Message);
            }
            return overtimes;
        }

        public bool SoftDeleteOvertime(int overtimeId)
        {
            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();
                    string softDeleteQuery = "UPDATE tb_overtime SET state = 0 WHERE id_overtime = @overtimeId";
                    using (MySqlCommand command = new MySqlCommand(softDeleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@overtimeId", overtimeId);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in nomina2.Models.DAO.OvertimeDAO.SoftDeleteOvertime: " + ex.Message);
                return false;
            }
        }
    
        



        public string InsertOvertime(OvertimeDTO overtime)
          


        {
            string response = "Failed";

            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();
                    
                    string insertOvertimeQuery = "INSERT INTO tb_overtime (user_id, type_action, description, value, state) VALUES (@userId, @typeAction, @description, @value, 1)";


                    using (MySqlCommand overtimeCommand = new MySqlCommand(insertOvertimeQuery, connection))

                    {

                        overtimeCommand.Parameters.AddWithValue("@userId", overtime.Id);
                        overtimeCommand.Parameters.AddWithValue("@typeAction", overtime.Type_action);
                        overtimeCommand.Parameters.AddWithValue("@description", overtime.Overtime_description);
                        overtimeCommand.Parameters.AddWithValue("@value", overtime.Overtime_value); ;

                        int rowsAffected = overtimeCommand.ExecuteNonQuery();

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

        public OvertimeDTO GetOvertimeById(int id)
        {
            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();

                    string selectQuery = "SELECT Overtime_id, Id, Type_action FROM tb_overtime WHERE Id = @id";

                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                OvertimeDTO overtime = new OvertimeDTO
                                {
                                    Overtime_id = Convert.ToInt32(reader["Overtime_id"]),
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Type_action = reader["Type_action"].ToString()
                                };

                                return overtime;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in OvertimeDAO.GetOvertimeById: " + ex.Message);
            }

            return null;
        }





    }
}