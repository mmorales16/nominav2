using MySql.Data.MySqlClient;
using nomina2.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nomina2.Models.DAO
{
    public class DeductionDAO
    {
        public List<DeductionDTO> ReadDeduction()
        {
            List<DeductionDTO> deductions = new List<DeductionDTO>();
            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();
                    string selectQuery = "SELECT * FROM tb_deductions";
                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DeductionDTO deduction = new DeductionDTO();
                                //deductions.Id = reader.GetInt32("id");
                                deduction.Deduction_id = reader.GetInt32("id_deductions");
                                deduction.Deduction_description = reader.GetString("description");
                                //deductions.Type_action = reader.GetString("type_action");
                                deductions.Add(deduction);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in nomina.Models.DAO.Roles.InserUser:" + ex.Message);
            }
            return deductions;
        }





        public List<DeductionDTO> ReadActiveDeductionByUserId(int userId)
        {
            List<DeductionDTO> deductions = new List<DeductionDTO>();
            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();
                    string selectQuery = "SELECT * FROM tb_deductions WHERE user_id = @userId";
                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DeductionDTO deduction = new DeductionDTO();
                                deduction.Deduction_id = reader.GetInt32("id_deduction");
                                deduction.Id = reader.GetInt32("user_id");
                                deduction.Deduction_description = reader.GetString("description");
                                deduction.Type_action = reader.GetString("type_action");
                                deduction.Deduction_value = reader.GetDecimal("value");

                                deductions.Add(deduction);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in nomina2.Models.DAO.DeductionsDAO.ReadDeductionsByUserId: " + ex.Message);
            }
            return deductions;
        }


        public string InsertDeduction(DeductionDTO deduction)



        {
            string response = "Failed";

            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();

                    string insertDeductionQuery = "INSERT INTO tb_deductions (user_id, type_action, description, value) VALUES (@userId, @typeAction, @description, @value)";


                    using (MySqlCommand deductionCommand = new MySqlCommand(insertDeductionQuery, connection))

                    {

                        deductionCommand.Parameters.AddWithValue("@userId", deduction.Id);
                        deductionCommand.Parameters.AddWithValue("@typeAction", deduction.Type_action);
                        deductionCommand.Parameters.AddWithValue("@description", deduction.Deduction_description);
                        deductionCommand.Parameters.AddWithValue("@value", deduction.Deduction_value); 

                        int rowsAffected = deductionCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            response = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DeductionDAO.InsertDeduction: " + ex.Message);
            }

            return response;

        }

        public DeductionDTO GetDeductionById(int id)
        {
            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();
                                                   //Cambiar de id_deduction a Deduction_id
                    string selectQuery = "SELECT Deduction_id, Id, Type_action FROM tb_dedutions WHERE Id = @id";

                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DeductionDTO deduction = new DeductionDTO
                                {
                                    
                                    Deduction_id = Convert.ToInt32(reader["Deduction_id"]),
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Type_action = reader["Type_action"].ToString()
                                };

                                return deduction;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DeductionDAO.GetDeductionById: " + ex.Message);
            }

            return null;
        }

        public string DeleteDeduction(int id)
        {
            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();

                    string deleteQuery = "DELETE FROM tb_deductions WHERE Id = @id";

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
                Console.WriteLine("Error in DeductionDAO.DeleteDeduction: " + ex.Message);
            }

            return "Failed";
        }



    }
}

