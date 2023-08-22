using MySql.Data.MySqlClient;
using nomina2.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Web;

namespace nomina2.Models.DAO
{
    public class RequestDAO
    {

        public List<RequestDTO> ReadAllRequests()
        {
            List<RequestDTO> requests = new List<RequestDTO>();
            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();
                    string selectQuery = "SELECT * FROM tb_requests"; // Obtener todas las solicitudes sin filtrar
                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RequestDTO request = new RequestDTO();
                                request.Request_id = reader.GetInt32("id_request");
                                request.Id = reader.GetInt32("user_id");
                                request.Request_note = reader.GetString("note");
                                request.Type_request = reader.GetString("type_request");
                                request.Active = reader.GetInt32("active");
                                request.Start_date = reader.GetDateTime("start_date");
                                request.End_date = reader.GetDateTime("end_date");
                                
                                requests.Add(request);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in RequestDAO.ReadAllRequests: " + ex.Message);
            }
            return requests;
        }

        public List<RequestDTO> ReadAllRequestsAdmin()
        {
            List<RequestDTO> requests = new List<RequestDTO>();
            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();
                    string selectQuery = "SELECT * FROM tb_requests"; // Obtener todas las solicitudes sin filtrar
                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RequestDTO request = new RequestDTO();
                                request.Request_id = reader.GetInt32("id_request");
                                request.Id = reader.GetInt32("user_id");
                                request.Request_note = reader.GetString("note");
                                request.Type_request = reader.GetString("type_request");
                                request.Start_date = reader.GetDateTime("start_date");
                                request.End_date = reader.GetDateTime("end_date");
                                requests.Add(request);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in RequestDAO.ReadAllRequests: " + ex.Message);
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

                    string insertRequestQuery = "INSERT INTO tb_requests (user_id, note, available_days, type_request, start_date, end_date, active, pending) VALUES (@userId, @Requestnote, @availableDays, @typeRequest, @startDate, @endDate, @Active, @Pending)";


                    using (MySqlCommand requestCommand = new MySqlCommand(insertRequestQuery, connection))

                    {

                        requestCommand.Parameters.AddWithValue("@userId", request.Id);
                        requestCommand.Parameters.AddWithValue("@Requestnote", request.Request_note);
                        requestCommand.Parameters.AddWithValue("@availableDays", request.Available_days);
                        requestCommand.Parameters.AddWithValue("@typeRequest", request.Type_request);
                        // Asignar el valor fijo "active" al parámetro @state
                        requestCommand.Parameters.AddWithValue("@startDate", request.Start_date);
                        requestCommand.Parameters.AddWithValue("@endDate", request.End_date);
                        requestCommand.Parameters.AddWithValue("@Active", request.Active);
                        requestCommand.Parameters.AddWithValue("@Pending", request.Pending);
                        requestCommand.Parameters.AddWithValue("@update_request", string.IsNullOrEmpty(request.Pending_request) ? "MMORALES" : request.Pending_request);

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

        public string PendingRequest(RequestDTO request)
        {
            string response = "Failed";

            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();

                    string updateRequestQuery = "UPDATE tb_requests SET note = @note, available_days = @availableDays, type_request = @typeRequest, start_date = @startDate, end_date = @endDate, active = @Active, pending = @Pending, pending_request = @pendingRequest WHERE id_request = @requestId";

                    using (MySqlCommand requestCommand = new MySqlCommand(updateRequestQuery, connection))
                    {
                        requestCommand.Parameters.AddWithValue("@note", request.Request_note);
                        requestCommand.Parameters.AddWithValue("@availableDays", request.Available_days);
                        requestCommand.Parameters.AddWithValue("@typeRequest", request.Type_request);
                        requestCommand.Parameters.AddWithValue("@startDate", request.Start_date);
                        requestCommand.Parameters.AddWithValue("@endDate", request.End_date);
                        requestCommand.Parameters.AddWithValue("@Active", request.Active);
                        requestCommand.Parameters.AddWithValue("@pendingRequest", string.IsNullOrEmpty(request.Pending_request) ? "MMORALES" : request.Pending_request);
                        requestCommand.Parameters.AddWithValue("@requestId", request.Request_id);                        
                        requestCommand.Parameters.AddWithValue("@Pending", request.Pending);

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
                Console.WriteLine("Error in RequestDAO.UpdateRequest: " + ex.Message);
            }

            return response;
        }


    }
}