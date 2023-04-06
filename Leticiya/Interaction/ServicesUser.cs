using Npgsql;
using System;
using System.Data;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;

namespace Leticiya.Interaction
{
    internal class ServicesUser
    {
        //Метод вывода содержимого таблицы БД в таблицу dataGridView1
        //Выбранная таблица определяется пользователем или находится по стандарту в comboBox
        //Вызов обновляет данные в dataGridView1 и сбрасывает выделенную строку

        public void ReloadViewBD(string element)
        {
            Program.formMain.label1.Text = element;
            string sql = null;

            int textBoxCoutPage = Convert.ToInt32(Program.formMain.textBoxCoutPage.Text);
            int offsetPage = (textBoxCoutPage - 1) * 41;

            switch (element)
            {
                case "Заказы":
                    sql = $"SELECT \"ORDER_ID\" AS \"Номер заказа\", \"ORDER_STATUS\" AS \"Статус заказа\", \"ORDER_DATA\" AS \"Дата заказа\", \"CUSTOMER_SURNAME\" AS \"Фамилия заказчика\"," +
                        $"\r\n\"CUSTOMER_NAME\" AS \"Имя заказчика\", \"CUSTOMER_PATRONYMIC\" AS \"Отчество заказчика\", \"ORDER_PRICE\" AS \"Стоимость заказа\"," +
                        $"\r\n\"CUSTOMER_ORGANIZATION\" AS \"Организация заказчика\", \"ACCOUNTANT_SURNAME\" AS \"Фамилия бухгалтера\"" +
                        $"\r\nFROM public.\"Order\" o, public.\"Customer\" cu, public.\"Accountant\" ac\r\nWHERE o.\"CUSTOMER_ID\" = cu.\"CUSTOMER_ID\"" +
                        $"\r\nAND o.\"ACCOUNTANT_ID\" = ac.\"ACCOUNTANT_ID\"" +
                        $"\r\nLIMIT 41";
                    if (textBoxCoutPage > 1)
                        sql += $"\r\nOFFSET {offsetPage}";
                    break;
                case "Категории":
                    sql = $"SELECT \"CATEGORY_ID\" AS \"Номер категории\", \"CATEGORY_NAME\" AS \"Название категории\"" +
                        $"\r\nFROM public.\"Category\"" +
                        $"\r\nLIMIT 41";
                    if (textBoxCoutPage > 1)
                        sql += $"\r\nOFFSET {offsetPage}";
                    break;
                case "Цеха":
                    sql = $"SELECT \"WORKSHOP_ID\" AS \"Номер цеха\", \"WORKSHOP_NAME\" AS \"Название цеха\"" +
                        $"\r\nFROM public.\"Workshop\"" +
                        $"\r\nLIMIT 41";
                    if (textBoxCoutPage > 1)
                        sql += $"\r\nOFFSET {offsetPage}";
                    break;
                case "Товары":
                    sql = $"SELECT \"PRODUCT_ID\" AS \"Номер товара\", \"PRODUCT_NAME\" AS \"Название товара\", \"CATEGORY_NAME\" AS \"Название категории\", \"PRODUCT_PRICE\" AS \"Цена товара\"," +
                        $"\r\n\"WORKSHOP_NAME\" AS \"Название цеха\"" +
                        $"\r\nFROM public.\"Product\" p, public.\"Category\" c, public.\"Workshop\" w" +
                        $"\r\nWHERE P.\"CATEGORY_ID\" = c.\"CATEGORY_ID\" AND p.\"WORKSHOP_ID\" = w.\"WORKSHOP_ID\"" +
                        $"\r\nLIMIT 41";
                    if (textBoxCoutPage > 1)
                        sql += $"\r\nOFFSET {offsetPage}";
                    break;
                case "Заказчики":
                    sql = $"SELECT \"CUSTOMER_ID\" AS \"Номер заказчика\", \"CUSTOMER_SURNAME\" AS \"Фамилия заказчика\", \"CUSTOMER_NAME\" AS \"Имя заказчика\", \"CUSTOMER_PATRONYMIC\"" +
                        $"\r\nAS \"Отчество заказчика\", \"CUSTOMER_TELEPHONE\" AS \"Телефон заказчика\", \"CUSTOMER_ORGANIZATION\" AS \"Организация заказчика\"" +
                        $"\r\nFROM public.\"Customer\"" +
                        $"\r\nLIMIT 41";
                    if (textBoxCoutPage > 1)
                        sql += $"\r\nOFFSET {offsetPage}";
                    break;
                default: return;
            }

            using (NpgsqlCommand sqlCommand = new NpgsqlCommand(sql, Program.connection))
            {
                Program.connection.Open();
                using (NpgsqlDataReader dataReader = sqlCommand.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(dataReader);
                    Program.formMain.dataGridViewUser.DataSource = dataTable;
                    dataReader.Close();
                }
                Program.connection.Close();
            }
        }
    }
}