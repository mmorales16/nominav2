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
                    string selectQuery = "SELECT * FROM tb_deductions WHERE user_id = @userId AND state = 1";
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

                    string insertDeductionQuery = "INSERT INTO tb_deductions (user_id, type_action, description, value, state) VALUES (@userId, @typeAction, @description, @value, 1)";


                    using (MySqlCommand deductionCommand = new MySqlCommand(insertDeductionQuery, connection))

                    {

                        deductionCommand.Parameters.AddWithValue("@userId", deduction.Id);
                        deductionCommand.Parameters.AddWithValue("@typeAction", deduction.Type_action);
                        deductionCommand.Parameters.AddWithValue("@description", deduction.Deduction_description);
                        deductionCommand.Parameters.AddWithValue("@value", deduction.Deduction_value);
                        // Asignar el valor fijo "active" al parámetro @state
                        deductionCommand.Parameters.AddWithValue("@state", "active");
                        //deductionCommand.Parameters.AddWithValue("@update_deduction", deduction.Update_deduction);
                        deductionCommand.Parameters.AddWithValue("@update_deduction", string.IsNullOrEmpty(deduction.Update_deduction) ? "MMORALES" : deduction.Update_deduction);

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


        public string UpdateDeduction(DeductionDTO deduction)
        {
            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();

                    string updateQuery = "UPDATE tb_deductions SET type_action = @typeAction, description = @description, value = @value WHERE Id = @id";

                    using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                    {
                        //command.Parameters.AddWithValue("@userId", deduction.Id);
                        command.Parameters.AddWithValue("@typeAction", deduction.Type_action);
                        command.Parameters.AddWithValue("@description", deduction.Deduction_description);
                        command.Parameters.AddWithValue("@value", deduction.Deduction_value);


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
                Console.WriteLine("Error in DeductionDAO.UpdateDeduction: " + ex.Message);
            }

            return "Failed";
        }


        public bool SoftDeleteDeduction(int deductionId)
        {
            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();
                    string softDeleteQuery = "UPDATE tb_deductions SET state = 0 WHERE id_deduction = @deductionId";
                    using (MySqlCommand command = new MySqlCommand(softDeleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@deductionId", deductionId);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in nomina2.Models.DAO.DeductionDAO.SoftDeleteDeduction: " + ex.Message);
                return false;
            }
        }



    }
}

