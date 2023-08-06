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
                                overtime.Id = reader.GetInt32("id");
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
    }
}