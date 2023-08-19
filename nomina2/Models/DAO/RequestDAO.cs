using MySql.Data.MySqlClient;
using nomina2.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace nomina2.Models.DAO
{
    public class RequestDAO
    {
        public List<RequestDTO> ReadRequest()
        {
            List<RequestDTO> requests = new List<RequestDTO>();
            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();
                    string selectQuery = "SELECT * FROM tb_requests";
                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RequestDTO request = new RequestDTO();
                                //overtime.Id = reader.GetInt32("id");
                                request.Request_id = reader.GetInt32("id_request");
                                request.Request_note = reader.GetString("note");
                                //overtime.Type_action = reader.GetString("type_action");
                                requests.Add(request);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in nomina.Models.DAO.Roles.InserUser:" + ex.Message);
            }
            return requests;
        }





        public List<RequestDTO> ReadActiveRequestByUserId(int userId)
        {
            List<RequestDTO> requests = new List<RequestDTO>();
            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();
                    string selectQuery = "SELECT * FROM tb_requests WHERE user_id = @userId"; // Filtrar solo registros activos
                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RequestDTO request = new RequestDTO();
                                request.Id = reader.GetInt32("Id");
                                request.Request_id = reader.GetInt32("request_id");
                                request.Type_request = reader.GetString("type_request");
                                request.Available_days = reader.GetInt32("available_days");
                                request.Start_date = reader.GetDateTime("start_date");
                                request.End_date = reader.GetDateTime("end_date");
                                request.Request_note = reader.GetString("note");

                                //FALTAN DATOS
                                requests.Add(request);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in nomina2.Models.DAO.RequestDAO.ReadActiveRequestByUserId: " + ex.Message);
            }
            return requests;
        }

        public string CreateRequest(RequestDTO request)



        {
            string response = "Failed";

            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();

                    string insertRequestQuery = "INSERT INTO tb_requests (user_id, Request_note, available_days, type_request, start_date, end_date) VALUES (@userId, @Requestnote, @availableDays, @typeRequest, @startDate, @endDate)";


                    using (MySqlCommand requestCommand = new MySqlCommand(insertRequestQuery, connection))

                    {

                        requestCommand.Parameters.AddWithValue("@userId", request.Id);
                        requestCommand.Parameters.AddWithValue("@Requestnote", request.Request_note);
                        requestCommand.Parameters.AddWithValue("@availableDays", request.Available_days);
                        requestCommand.Parameters.AddWithValue("@typeRequest", request.Type_request);
                        // Asignar el valor fijo "active" al parámetro @state
                        requestCommand.Parameters.AddWithValue("@startDate", request.Start_date);
                        requestCommand.Parameters.AddWithValue("@endDate", request.End_date);
                        requestCommand.Parameters.AddWithValue("@update_request", string.IsNullOrEmpty(request.Update_request) ? "MMORALES" : request.Update_request);

                        int rowsAffected = requestCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            response = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in RequestDAO.InsertRequest: " + ex.Message);
            }

            return response;

        }


        /*public bool SoftDeleteOvertime(int overtimeId)
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
        */




    }
}