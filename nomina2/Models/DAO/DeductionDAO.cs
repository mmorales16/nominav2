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





        public List<DeductionDTO> ReadDeductionByUserId(int userId)
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
    }
}