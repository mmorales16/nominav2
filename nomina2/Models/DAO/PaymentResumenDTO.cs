using MySql.Data.MySqlClient;
using nomina2.Models.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace nomina2.Models.DAO

{
    public class PaymentResumenDAO
    {


        public List<PaymentResumenDTO> ReadPaymentResumen()
        {
            List<PaymentResumenDTO> paymenresumens = new List<PaymentResumenDTO>();

            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();
                    string selectQuery = "SELECT\r\n    p.user_id,\r\n    u.name,\r\n    p.gross_salary,\r\n    p.worked_days,\r\n    COALESCE(grouped_deductions.total_monto_deduction, 0) AS total_monto_deduction,\r\n    COALESCE(grouped_overtime.total_monto_overtime, 0) AS total_monto_overtime,\r\n    COALESCE(grouped_deductions.total_deduction_porcentaje, 0) AS total_deduction_porcentaje,\r\n    COALESCE(grouped_overtime.total_overtime_porcentaje, 0) AS total_overtime_porcentaje,\r\n    (p.gross_salary + (COALESCE(grouped_overtime.total_overtime_porcentaje, 0) * p.gross_salary) + COALESCE(grouped_overtime.total_monto_overtime, 0)) - ((COALESCE(grouped_deductions.total_deduction_porcentaje, 0) * p.gross_salary) + COALESCE(grouped_deductions.total_monto_deduction, 0)) AS total_to_pay  \r\nFROM tb_payments p\r\nINNER JOIN tb_users u ON p.user_id = u.id\r\nLEFT JOIN (\r\n    SELECT user_id,\r\n\t        SUM(CASE WHEN type_action = 'porcentaje' THEN value ELSE 0 END) AS total_overtime_porcentaje,\r\n\t        SUM(CASE WHEN type_action = 'Monto' THEN value ELSE 0 END) AS total_monto_overtime\r\n    FROM tb_overtime\r\n    GROUP BY user_id\r\n) grouped_overtime ON p.user_id = grouped_overtime.user_id\r\nLEFT JOIN (\r\n    SELECT user_id,\r\n            SUM(CASE WHEN type_action = 'porcentaje' THEN value ELSE 0 END) AS total_deduction_porcentaje,\r\n            SUM(CASE WHEN type_action = 'Monto' THEN value ELSE 0 END) AS total_monto_deduction\r\n    FROM tb_deductions\r\n    GROUP BY user_id \r\n) grouped_deductions ON p.user_id = grouped_deductions.user_id\r\nWHERE p.date BETWEEN '2023-07-01' AND '2023-08-31'\r\nGROUP BY p.user_id, u.name,  grouped_deductions.total_deduction_porcentaje, grouped_overtime.total_overtime_porcentaje, grouped_overtime.total_monto_overtime, grouped_deductions.total_monto_deduction;\r\n";

                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PaymentResumenDTO paymentResumen = new PaymentResumenDTO
                                {
                                    UserId = Convert.ToInt32(reader["user_id"]),
                                    UserName = reader["name"].ToString(),
                                    GrossSalary = Convert.ToDecimal(reader["gross_salary"]),
                                    WorkedDays = Convert.ToInt32(reader["worked_days"]),
                                    TotalMontoDeduction = Convert.ToDecimal(reader["total_monto_deduction"]),
                                    TotalMontoOvertime = Convert.ToDecimal(reader["total_monto_overtime"]),
                                    TotalDeductionPorcentaje = Convert.ToDecimal(reader["total_deduction_porcentaje"]),
                                    TotalOvertimePorcentaje = Convert.ToDecimal(reader["total_overtime_porcentaje"]),
                                    TotalToPay = Convert.ToDecimal(reader["total_to_pay"])
                                };


                                paymenresumens.Add(paymentResumen);
                            }
                        }
                    }
                }
            }


            catch (Exception ex)
            {
                Console.WriteLine("Error in nomina.Models.DAO.Roles.InserUser:" + ex.Message);
            }
            return paymenresumens;
        }



    }
}