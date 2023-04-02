using Npgsql;
using System.Data;
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
            switch (element)
            {
                case "Заказы":
                    sql = $"SELECT \"ORDER_ID\" AS \"Номер заказа\", \"ORDER_STATUS\" AS \"Статус заказа\", \"ORDER_DATA\" AS \"Дата заказа\", \"CUSTOMER_SURNAME\" AS \"Фамилия заказчика\", \"CUSTOMER_NAME\" AS \"Имя заказчика\"," +
                        $"\r\n\t\t\"CUSTOMER_PATRONYMIC\" AS \"Отчество заказчика\", \"ORDER_PRICE\" AS \"Стоимость заказа\", \"CUSTOMER_ORGANIZATION\" AS \"Организация заказчика\", \"ACCOUNTANT_SURNAME\" AS \"Фамилия бухгалтера\"" +
                        $"\r\nFROM public.\"Order\" o, public.\"Customer\" cu, public.\"Accountant\" ac\r\nWHERE o.\"CUSTOMER_ID\" = cu.\"CUSTOMER_ID\" AND o.\"ACCOUNTANT_ID\" = ac.\"ACCOUNTANT_ID\"";
                    break;
                case "Категории":
                    //sql = $"SELECT * FROM \"\"";
                    break;
                case "Цеха":
                    //sql = $"SELECT * FROM \"\"";
                    break;
                case "Товары":
                    //sql = $"SELECT * FROM \"\"";
                    break;
                case "Заказчики":
                    //sql = $"SELECT * FROM \"\"";
                    break;
            }

            // Говно потом нужно удалить, это затычка
            if(element != "Заказы")
            {
                Program.formMain.dataGridViewUser.DataSource = null;
                return;
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