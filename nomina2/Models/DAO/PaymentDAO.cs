using MySql.Data.MySqlClient;
using nomina2.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nomina2.Models.DAO
{
    public class PaymentDAO
    {
        public List<PaymentDTO> ReadPayments(string searchKeyword)
        {
            List<PaymentDTO> payments = new List<PaymentDTO>();

            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();
                    // string selectQuery = "SELECT * FROM tb_payments WHERE user_id LIKE @searchKeyword";
                    string selectQuery = "SELECT p.*, u.amount_salary, u.name AS user_name, u.last_name AS user_last_name FROM tb_payments p INNER JOIN tb_users u ON p.user_id = u.id WHERE p.user_id LIKE @searchKeyword";

                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        if (!string.IsNullOrWhiteSpace(searchKeyword))
                        {
                            command.Parameters.AddWithValue("@searchKeyword", "%" + searchKeyword + "%");
                        }

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PaymentDTO payment = new PaymentDTO();
                                payment.Id_payment = reader.GetInt32("id_payment");
                                payment.Id = reader.GetInt32("user_id");
                                payment.Salary = reader.GetDecimal("salary");
                                payment.Date = reader.GetDateTime("date").Date; // Obtener solo fecha
                                payment.Type = reader.GetString("type");
                                payment.Worked_days = reader.GetInt32("worked_days");
                                payment.Regular_Hours = reader.GetInt32("regular_Hours");
                                payment.Overtime_hours = reader.GetInt32("overtime_hours");
                                payment.Gross_Salary = reader.GetDecimal("gross_Salary");
                                payment.Detail = reader.GetString("detail");
                                payment.Observation = reader.GetString("observation");
                                payment.Update_date = reader.GetDateTime("update_date");
                                payment.Update_user = reader.GetString("update_user");
                                payment.Create_date = reader.GetDateTime("create_date");
                                payment.Create_user = reader.GetString("create_user");
                                payment.UserName = reader.GetString("user_name");
                                payment.LastName = reader.GetString("user_last_name");
                                payment.AmountSalary = reader.GetDecimal("amount_salary");
                                // Obtener el nombre del usuario correspondiente al user_id
                                int userId = reader.GetInt32("user_id");

                                payments.Add(payment);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in nomina.Models.DAO.UserDAO.ReadPayments:" + ex.Message);
            }
            return payments;
        }



        public string InsertPayment(PaymentDTO payment)
        {
            string response = "Failed";

            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();

                    string insertOvertimeQuery = "INSERT INTO tb_payments (user_id, salary, date, type, worked_days, regular_Hours, overtime_hours, gross_Salary, detail, observation, update_date, update_user, create_date, create_user) VALUES (@userId, @salary, @date, @type, @worked_days, @regular_Hours, @overtime_hours, @gross_Salary, @detail, @observation,  @update_date, @update_user, @create_date, @create_user )";


                    using (MySqlCommand paymentCommand = new MySqlCommand(insertOvertimeQuery, connection))

                    {

                        paymentCommand.Parameters.AddWithValue("@userId", payment.Id);
                        paymentCommand.Parameters.AddWithValue("@salary", payment.Salary);
                        paymentCommand.Parameters.AddWithValue("@date", payment.Date);
                        paymentCommand.Parameters.AddWithValue("@type", payment.Type);
                        paymentCommand.Parameters.AddWithValue("@worked_days", payment.Worked_days);
                        paymentCommand.Parameters.AddWithValue("@regular_Hours", payment.Regular_Hours);
                        paymentCommand.Parameters.AddWithValue("@overtime_hours", payment.Overtime_hours);
                        paymentCommand.Parameters.AddWithValue("@gross_Salary", payment.Gross_Salary);
                        paymentCommand.Parameters.AddWithValue("@detail", payment.Detail);
                        paymentCommand.Parameters.AddWithValue("@Observation", payment.Observation);
                        paymentCommand.Parameters.AddWithValue("@update_date", payment.Update_date);
                        paymentCommand.Parameters.AddWithValue("@update_user", string.IsNullOrEmpty(payment.Update_user) ? "MMORALES" : payment.Update_user);
                        // Asignar automáticamente la fecha y hora actual al parámetro @create_date
                        paymentCommand.Parameters.AddWithValue("@create_date", DateTime.Now);
                        paymentCommand.Parameters.AddWithValue("@create_user", string.IsNullOrEmpty(payment.Create_user) ? "ADMIN" : payment.Create_user);

                        int rowsAffected = paymentCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            response = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in PaymentDAO.InsertPayment: " + ex.Message);
            }

            return response;

        }








    }
}
