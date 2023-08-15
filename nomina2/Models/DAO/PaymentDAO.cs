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
            List<UserDTO> users = new List<UserDTO>();

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
                            command.Parameters.AddWithValue("@searchKeyword", searchKeyword);


                        }

                        using (MySqlDataReader readerPayments = command.ExecuteReader())
                        {
                            while (readerPayments.Read())
                            {
                                PaymentDTO payment = new PaymentDTO();
                                payment.Id_payment = readerPayments.GetInt32("id_payment");
                                payment.Id_user = readerPayments.GetInt32("user_id");
                                payment.Salary = readerPayments.GetDecimal("salary");
                                payment.Date = readerPayments.GetDateTime("date").Date; // Obtener solo fecha
                                payment.Type = readerPayments.GetString("type");
                                payment.Worked_days = readerPayments.GetInt32("worked_days");
                                payment.Regular_Hours = readerPayments.GetInt32("regular_Hours");
                                payment.Overtime_hours = readerPayments.GetInt32("overtime_hours");
                                payment.Gross_Salary = readerPayments.GetDecimal("gross_Salary");
                                payment.Detail = readerPayments.GetString("detail");
                                payment.Observation = readerPayments.GetString("observation");
                                payment.Update_date = readerPayments.GetDateTime("update_date");
                                payment.Update_user = readerPayments.GetString("update_user");
                                payment.Create_date = readerPayments.GetDateTime("create_date");
                                payment.Create_user = readerPayments.GetString("create_user");
                                payment.UserName = readerPayments.GetString("user_name");
                                payment.LastName = readerPayments.GetString("user_last_name");
                                payment.AmountSalary = readerPayments.GetDecimal("amount_salary");
                                // Obtener el nombre del usuario correspondiente al user_id
                                int userId = readerPayments.GetInt32("user_id");

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

                        paymentCommand.Parameters.AddWithValue("@userId", payment.Id_user);
                        paymentCommand.Parameters.AddWithValue("@salary", payment.AmountSalary);
                        paymentCommand.Parameters.AddWithValue("@date", payment.Date);
                        paymentCommand.Parameters.AddWithValue("@type", payment.Type);
                        // Verificar si worked_days es 0, y asignar 0 si es así
                        if (payment.Worked_days == 0)
                        {
                            paymentCommand.Parameters.AddWithValue("@worked_days", 0);
                        }
                        else
                        {
                            paymentCommand.Parameters.AddWithValue("@worked_days", payment.Worked_days);
                        }
                        // Verificar si regular_Hours es 0, y asignar 0 si es así
                        if (payment.Regular_Hours == 0)
                        {
                            paymentCommand.Parameters.AddWithValue("@regular_Hours", 0);
                        }
                        else
                        {
                            paymentCommand.Parameters.AddWithValue("@regular_Hours", payment.Regular_Hours);
                        }
                        // Verificar si overtime_hours es 0, y asignar 0 si es así
                        if (payment.Overtime_hours == 0)
                        {
                            paymentCommand.Parameters.AddWithValue("@overtime_hours", 0);
                        }
                        else
                        {
                            paymentCommand.Parameters.AddWithValue("@overtime_hours", payment.Overtime_hours);
                        }

                        // Calcula el valor de regularGross multiplicando Regular_Hours por AmountSalary
                        decimal regularGrossx = payment.Regular_Hours * payment.AmountSalary;

                        // Calcula el valor de overtimeGross multiplicando Overtime_hours por AmountSalary y luego por 1.5
                        decimal overtimeGrossx = payment.Overtime_hours * payment.AmountSalary * 1.5M;

                        // Calcula el valor de workedDaysGross multiplicando Worked_days por AmountSalary
                        decimal workedDaysGrossx = payment.Worked_days * payment.AmountSalary / 22M;

                        // Calcula el valor total de grossSalary sumando regularGross, overtimeGross y workedDaysGross
                        decimal grossSalary = regularGrossx + overtimeGrossx + workedDaysGrossx;

                        if (payment.Type == "A")
                        {
                            grossSalary = 0;
                        }

                        paymentCommand.Parameters.AddWithValue("@gross_Salary", grossSalary);


                        // paymentCommand.Parameters.AddWithValue("@detail", payment.Detail);
                        paymentCommand.Parameters.AddWithValue("@detail", string.IsNullOrEmpty(payment.Detail) ? " " : payment.Detail);
                        // paymentCommand.Parameters.AddWithValue("@observation", payment.Observation);
                        paymentCommand.Parameters.AddWithValue("@observation", string.IsNullOrEmpty(payment.Observation) ? " " : payment.Observation);
                        paymentCommand.Parameters.AddWithValue("@update_date", DateTime.Now);
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


        public PaymentDTO GetPaymentById(int id_payment)
        {
            PaymentDTO payment = null;

            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();

                    string getPaymentQuery = "SELECT * FROM tb_payments WHERE id_payment = @id_payment";

                    using (MySqlCommand paymentCommand = new MySqlCommand(getPaymentQuery, connection))
                    {
                        paymentCommand.Parameters.AddWithValue("@id_payment", id_payment);

                        using (MySqlDataReader reader = paymentCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                payment = new PaymentDTO
                                {
                                    Id_payment = Convert.ToInt32(reader["id_payment"]),
                                    Id_user = Convert.ToInt32(reader["user_id"]),
                                    AmountSalary = Convert.ToDecimal(reader["salary"]),
                                    Date = Convert.ToDateTime(reader["date"]),
                                    Type = Convert.ToString(reader["type"]),
                                    Worked_days = Convert.ToInt32(reader["worked_days"]),
                                    Regular_Hours = Convert.ToInt32(reader["regular_hours"]),
                                    Overtime_hours = Convert.ToInt32(reader["overtime_hours"]),
                                    Gross_Salary = Convert.ToInt32(reader["gross_salary"]),
                                    Detail = Convert.ToString(reader["detail"]),
                                    Observation = Convert.ToString(reader["observation"]),
                                    Update_date = Convert.ToDateTime(reader["update_date"]),
                                    Update_user = Convert.ToString(reader["update_user"]),
                                    Create_date = Convert.ToDateTime(reader["create_date"]),
                                    Create_user = Convert.ToString(reader["create_user"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in PaymentDAO.GetPaymentById: " + ex.Message);
            }

            return payment;
        }


        public string UpdatePayment(PaymentDTO payment)
        {
            string response = "Failed";

            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();

                    string updatePaymentQuery = "UPDATE tb_payments SET salary = @salary, date = @date, type = @type, worked_days = @worked_days, regular_hours = @regular_hours, overtime_hours = @overtime_hours, gross_salary = @gross_salary, update_date = @update_date, update_user = @update_user  WHERE id_payment = @id_payment";

                    using (MySqlCommand paymentCommand = new MySqlCommand(updatePaymentQuery, connection))
                    {


                        paymentCommand.Parameters.AddWithValue("@salary", payment.AmountSalary);
                        paymentCommand.Parameters.AddWithValue("@date", payment.Date);
                        paymentCommand.Parameters.AddWithValue("@type", payment.Type);
                        paymentCommand.Parameters.AddWithValue("@worked_days", payment.Worked_days);
                        paymentCommand.Parameters.AddWithValue("@regular_hours", payment.Regular_Hours);
                        paymentCommand.Parameters.AddWithValue("@overtime_hours", payment.Overtime_hours);

                        // Calcula el valor de regularGross multiplicando Regular_Hours por AmountSalary
                        decimal regularGrossx = payment.Regular_Hours * payment.AmountSalary;

                        // Calcula el valor de overtimeGross multiplicando Overtime_hours por AmountSalary y luego por 1.5
                        decimal overtimeGrossx = payment.Overtime_hours * payment.AmountSalary * 1.5M;

                        // Calcula el valor de workedDaysGross multiplicando Worked_days por AmountSalary
                        decimal workedDaysGrossx = payment.Worked_days * payment.AmountSalary / 22M;

                        // Calcula el valor total de grossSalary sumando regularGross, overtimeGross y workedDaysGross
                        decimal grossSalary = regularGrossx + overtimeGrossx + workedDaysGrossx;
                        if (payment.Type == "A")
                        {
                            grossSalary = 0;
                        }
                        paymentCommand.Parameters.AddWithValue("@gross_salary", grossSalary);
                        paymentCommand.Parameters.AddWithValue("@update_date", DateTime.Now);
                        paymentCommand.Parameters.AddWithValue("@update_user", string.IsNullOrEmpty(payment.Update_user) ? "MMORALES" : payment.Update_user);
                        paymentCommand.Parameters.AddWithValue("@id_payment", payment.Id_payment);


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
                Console.WriteLine("Error in PaymentDAO.UpdatePayment: " + ex.Message);
            }

            return response;
        }


        public string DeletePayment(int id_payment)
        {
            string response = "Failed";

            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();

                    string deletePaymentQuery = "DELETE FROM tb_payments WHERE id_payment = @id_payment";

                    using (MySqlCommand paymentCommand = new MySqlCommand(deletePaymentQuery, connection))
                    {
                        paymentCommand.Parameters.AddWithValue("@id_payment", id_payment);

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
                Console.WriteLine("Error in PaymentDAO.DeletePayment: " + ex.Message);
            }

            return response;
        }








    }
}
