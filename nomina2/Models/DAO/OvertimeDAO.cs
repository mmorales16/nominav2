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
                    string selectQuery = "SELECT * FROM tb_overtime";
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





        public List<OvertimeDTO> ReadOvertimeByUserId(int userId)
        {
            List<OvertimeDTO> overtimes = new List<OvertimeDTO>();
            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();
                    string selectQuery = "SELECT * FROM tb_overtime WHERE user_id = @userId";
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
                Console.WriteLine("Error in nomina2.Models.DAO.OvertimeDAO.ReadOvertimeByUserId: " + ex.Message);
            }
            return overtimes;
        }





        public string InsertOvertime(OvertimeDTO overtime)
          


        {
            string response = "Failed";

            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();

                    string insertOvertimeQuery = "INSERT INTO tb_overtime (user_id, type_action, description, value) VALUES (@userId, @typeAction, @description, @value)";


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





    }
}