using MySql.Data.MySqlClient;
using nomina2.Models.DTO;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace nomina2.Models.DAO
{
    public class RecordDAO
    {

        public List<RecordDTO> ReadAllRecords()
        {
            List<RecordDTO> records = new List<RecordDTO>();
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
                                RecordDTO record = new RecordDTO();
                                record.Request_id = reader.GetInt32("id_request");
                                record.Id = reader.GetInt32("user_id");
                                record.Request_note = reader.GetString("note");
                                record.Type_request = reader.GetString("type_request");
                                record.Start_date = reader.GetDateTime("start_date");
                                record.End_date = reader.GetDateTime("end_date");
                                record.Active = reader.GetInt32("active");
                                records.Add(record);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in RecordDAO.ReadAllRequests: " + ex.Message);
            }
            return records;
        }

        public List<RecordDTO> ReadRecordsByUserId(int userId)
        {
            List<RecordDTO> userRecords = new List<RecordDTO>();
            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();
                    string selectQuery = "SELECT * FROM tb_requests WHERE user_id = @userId";
                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RecordDTO record = new RecordDTO();
                                record.Request_id = reader.GetInt32("id_request");
                                record.Id = reader.GetInt32("user_id");
                                record.Request_note = reader.GetString("note");
                                record.Type_request = reader.GetString("type_request");
                                record.Start_date = reader.GetDateTime("start_date");
                                record.End_date = reader.GetDateTime("end_date");
                                userRecords.Add(record);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in RecordDAO.ReadRecordsByUserId: " + ex.Message);
            }
            return userRecords;
        }
    }
}