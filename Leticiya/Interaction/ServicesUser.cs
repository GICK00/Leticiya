using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

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
                    sql = "SELECT \"ORDER_ID\" AS \"Номер заказа\", \"ORDER_STATUS\" AS \"Статус заказа\", \"ORDER_DATA\" AS \"Дата заказа\", \"CUSTOMER_SURNAME\" AS \"Фамилия заказчика\"," +
                        "\r\n\"CUSTOMER_NAME\" AS \"Имя заказчика\", \"CUSTOMER_PATRONYMIC\" AS \"Отчество заказчика\", \"ORDER_PRICE\" AS \"Стоимость заказа\"," +
                        "\r\n\"CUSTOMER_ORGANIZATION\" AS \"Организация заказчика\", \"ACCOUNTANT_SURNAME\" AS \"Фамилия бухгалтера\"" +
                        "\r\nFROM public.\"Order\" o, public.\"Customer\" cu, public.\"Accountant\" ac\r\nWHERE o.\"CUSTOMER_ID\" = cu.\"CUSTOMER_ID\"" +
                        "\r\nAND o.\"ACCOUNTANT_ID\" = ac.\"ACCOUNTANT_ID\"" +
                        "\r\nORDER BY \"ORDER_ID\" DESC" +
                        "\r\nLIMIT 41";
                    if (textBoxCoutPage > 1)
                        sql += $"\r\nOFFSET {offsetPage}";
                    break;
                case "Категории":
                    sql = "SELECT \"CATEGORY_ID\" AS \"Номер категории\", \"CATEGORY_NAME\" AS \"Название категории\"" +
                        "\r\nFROM public.\"Category\"" +
                        "\r\nLIMIT 41";
                    if (textBoxCoutPage > 1)
                        sql += $"\r\nOFFSET {offsetPage}";
                    break;
                case "Цеха":
                    sql = "SELECT \"WORKSHOP_ID\" AS \"Номер цеха\", \"WORKSHOP_NAME\" AS \"Название цеха\"" +
                        "\r\nFROM public.\"Workshop\"" +
                        "\r\nLIMIT 41";
                    if (textBoxCoutPage > 1)
                        sql += $"\r\nOFFSET {offsetPage}";
                    break;
                case "Товары":
                    sql = "SELECT \"PRODUCT_ID\" AS \"Номер товара\", \"PRODUCT_NAME\" AS \"Название товара\", \"CATEGORY_NAME\" AS \"Название категории\", \"PRODUCT_PRICE\" AS \"Цена товара\"," +
                        "\r\n\"WORKSHOP_NAME\" AS \"Название цеха\"" +
                        "\r\nFROM public.\"Product\" p, public.\"Category\" c, public.\"Workshop\" w" +
                        "\r\nWHERE P.\"CATEGORY_ID\" = c.\"CATEGORY_ID\" AND p.\"WORKSHOP_ID\" = w.\"WORKSHOP_ID\"" +
                        "\r\nORDER BY \"PRODUCT_ID\" DESC" +
                        "\r\nLIMIT 41";
                    if (textBoxCoutPage > 1)
                        sql += $"\r\nOFFSET {offsetPage}";
                    break;
                case "Заказчики":
                    sql = "SELECT \"CUSTOMER_ID\" AS \"Номер заказчика\", \"CUSTOMER_SURNAME\" AS \"Фамилия заказчика\", \"CUSTOMER_NAME\" AS \"Имя заказчика\", \"CUSTOMER_PATRONYMIC\"" +
                        "\r\nAS \"Отчество заказчика\", \"CUSTOMER_TELEPHONE\" AS \"Телефон заказчика\", \"CUSTOMER_ORGANIZATION\" AS \"Организация заказчика\"" +
                        "\r\nFROM public.\"Customer\"" +
                         "\r\nORDER BY \"CUSTOMER_ID\" DESC" +
                        "\r\nLIMIT 41";
                    if (textBoxCoutPage > 1)
                        sql += $"\r\nOFFSET {offsetPage}";
                    break;
                case "Пользователи":
                    sql = "SELECT \"ACCOUNTANT_ID\" AS \"Номер пользователя\", \"ACCOUNTANT_SURNAME\" AS \"Фамилия пользователя\", \"ACCOUNTANT_NAME\" AS \"Имя пользователя\", \"ACCOUNTANT_PATRONYMIC\" AS \"Отчество пользователя\"," +
                        "\r\n\"ACCOUNTANT_LOGIN\" AS \"Логин пользователя\"";
                    if (FormLogin.Position[0] == "admin")
                        sql += ", \"ACCOUNTANT_PASSWORD\" AS \"Пароль пользователя\", \"ACCOUNTANT_POSITION\" AS \"Права пользователя\"";
                    sql += "\r\nFROM public.\"Accountant\"" +
                        "\r\nORDER BY \"ACCOUNTANT_ID\" DESC" +
                        "\r\nLIMIT 41";
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

        public List<List<string>> DataTableCustomer()
        {
            List<List<string>> list = new List<List<string>>();
            List<string> names = new List<string>();
            List<string> dataCustomer = new List<string>();

            const string sql = "SELECT \"CUSTOMER_ID\", \"CUSTOMER_SURNAME\", \"CUSTOMER_NAME\", \"CUSTOMER_PATRONYMIC\", \"CUSTOMER_ORGANIZATION\", \"CUSTOMER_TELEPHONE\"" +
                "\r\nFROM public.\"Customer\"";
            using (NpgsqlCommand sqlCommand = new NpgsqlCommand(sql, Program.connection))
            {
                Program.connection.Open();
                using (NpgsqlDataReader dataReader = sqlCommand.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(dataReader);
                    foreach (DataRow row in dataTable.Rows)
                    {
                        names.Add(row["CUSTOMER_SURNAME"].ToString() + " " + row["CUSTOMER_NAME"].ToString() + " "
                            + row["CUSTOMER_PATRONYMIC"].ToString() + " " + row["CUSTOMER_ORGANIZATION"].ToString());
                        dataCustomer.Add(row["CUSTOMER_ID"].ToString() + "|" + row["CUSTOMER_SURNAME"].ToString() + " " + row["CUSTOMER_NAME"].ToString() + " "
                            + row["CUSTOMER_PATRONYMIC"].ToString() + "|" + row["CUSTOMER_ORGANIZATION"].ToString() + "|"
                            + row["CUSTOMER_TELEPHONE"].ToString());
                    }
                    dataReader.Close();
                }
                Program.connection.Close();
                list.Add(names);
                list.Add(dataCustomer);
            }
            return list;
        }

        //Выгрузка в comboBox товаров для добавления
        public List<List<string>> DataTableOrderProduct()
        {
            List<List<string>> list = new List<List<string>>();
            List<string> names = new List<string>();
            List<string> dataProducrt = new List<string>();

            const string sql = "SELECT \"PRODUCT_ID\", \"PRODUCT_NAME\", \"CATEGORY_NAME\", \"PRODUCT_PRICE\"" +
                "\r\nFROM public.\"Product\" p, public.\"Category\" c " +
                "\r\nWHERE p.\"CATEGORY_ID\" = c.\"CATEGORY_ID\"";
            using (NpgsqlCommand sqlCommand = new NpgsqlCommand(sql, Program.connection))
            {
                Program.connection.Open();
                using (NpgsqlDataReader dataReader = sqlCommand.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(dataReader);
                    foreach (DataRow row in dataTable.Rows)
                    {
                        names.Add(row["PRODUCT_NAME"].ToString());
                        dataProducrt.Add(row["PRODUCT_ID"].ToString() + "|" + row["PRODUCT_NAME"].ToString() + "|" + row["CATEGORY_NAME"].ToString() + "|" + row["PRODUCT_PRICE"].ToString());
                    }
                    dataReader.Close();
                }
                Program.connection.Close();
                list.Add(names);
                list.Add(dataProducrt);
            }
            return list;
        }

        //Поиск информации в БД
        public int SearchUser()
        {
            int id;
            string sql = "SELECT \"ACCOUNTANT_ID\"" +
                "\r\nFROM public.\"Accountant\" " +
                $"\r\nWHERE \"ACCOUNTANT_SURNAME\" = '{FormLogin.Position[1]}' AND \"ACCOUNTANT_NAME\" = '{FormLogin.Position[2]}'";
            using (NpgsqlCommand sqlCommand = new NpgsqlCommand(sql, Program.connection))
            {
                Program.connection.Open();
                id = (int?)sqlCommand.ExecuteScalar() ?? 0;
                Program.connection.Close();
            }
            return id;
        }

        // Заппрос на вывод данныз для заполнения их в таблиц Excel
        public List<string> DataOrder(int order_id)
        {
            string sql = "SELECT o.\"ORDER_ID\", \"CUSTOMER_SURNAME\", \"CUSTOMER_NAME\", \"CUSTOMER_PATRONYMIC\", \"ORDER_PRICE\"," +
                "\r\n\"CUSTOMER_ORGANIZATION\", \"ACCOUNTANT_SURNAME\", \"ACCOUNTANT_NAME\", \"ACCOUNTANT_PATRONYMIC\"" +
                "\r\nFROM public.\"Order\" o, public.\"Customer\" cu, public.\"Accountant\" ac" +
                $"\r\nWHERE o.\"ORDER_ID\" = {order_id} AND o.\"CUSTOMER_ID\" = cu.\"CUSTOMER_ID\" AND o.\"ACCOUNTANT_ID\" = ac.\"ACCOUNTANT_ID\"";
            List<string> list = new List<string>();
            using (NpgsqlCommand sqlCommand = new NpgsqlCommand(sql, Program.connection))
            {
                Program.connection.Open();
                using (NpgsqlDataReader dataReader = sqlCommand.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(dataReader);
                    dataReader.Close();
                    DataRow row = dataTable.Rows[0];

                    list.Add(row["CUSTOMER_ORGANIZATION"].ToString());
                    list.Add(row["CUSTOMER_SURNAME"].ToString());
                    list.Add(row["CUSTOMER_NAME"].ToString());
                    list.Add(row["CUSTOMER_PATRONYMIC"].ToString());
                    list.Add(row["ORDER_PRICE"].ToString());
                    list.Add(row["ACCOUNTANT_SURNAME"].ToString());
                    list.Add(row["ACCOUNTANT_NAME"].ToString());
                    list.Add(row["ACCOUNTANT_PATRONYMIC"].ToString());
                }
                Program.connection.Close();
            }
            return list;
        }

        public List<string>[] DataOrderProduct(int order_id)
        {
            string sql = "SELECT \"CATEGORY_NAME\", \"PRODUCT_NAME\", \"PRODUCT_PRICE\", \"ORDER_PRODUCT_COUT\"" +
                "\r\nFROM public.\"Order\" o, public.\"Product\" p, public.\"Order_Product\" op, public.\"Category\" c" +
                $"\r\nWHERE o.\"ORDER_ID\" = {order_id} AND o.\"ORDER_ID\" = op.\"ORDER_ID\" AND op.\"PRODUCT_ID\" = p.\"PRODUCT_ID\" AND p.\"CATEGORY_ID\" = c.\"CATEGORY_ID\"";

            List<string>[] mas;
            using (NpgsqlCommand sqlCommand = new NpgsqlCommand(sql, Program.connection))
            {
                Program.connection.Open();
                using (NpgsqlDataReader dataReader = sqlCommand.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(dataReader);
                    dataReader.Close();
                    int size = dataTable.Rows.Count;
                    mas = new List<string>[size];
                    for (int i = 0; i < size; i++)
                    {
                        List<string> list = new List<string>();
                        DataRow row = dataTable.Rows[i];
                        list.Add(row["CATEGORY_NAME"].ToString());
                        list.Add(row["PRODUCT_NAME"].ToString());
                        list.Add(row["ORDER_PRODUCT_COUT"].ToString());
                        list.Add(row["PRODUCT_PRICE"].ToString());
                        mas[i] = list;
                    }
                }
                Program.connection.Close();
            }
            return mas;
        }
    }
}