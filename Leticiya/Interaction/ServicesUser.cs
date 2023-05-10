using Leticiya.Class;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace Leticiya.Interaction
{
    internal static class ServicesUser
    {
        //Вывод кратких данных в главную таблицу по каждому разделу
        public static void ReloadViewBD(string element)
        {
            Program.formMain.label1.Text = element;
            string sql;

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
                        "\r\nORDER BY \"CATEGORY_ID\" DESC" +
                        "\r\nLIMIT 41";
                    if (textBoxCoutPage > 1)
                        sql += $"\r\nOFFSET {offsetPage}";
                    break;
                case "Цеха":
                    sql = "SELECT \"WORKSHOP_ID\" AS \"Номер цеха\", \"WORKSHOP_NAME\" AS \"Название цеха\"" +
                        "\r\nFROM public.\"Workshop\"" +
                        "\r\nORDER BY \"WORKSHOP_ID\" DESC" +
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
                    if (ServicesAutorization.Position[0] == "admin")
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

        //Вывод всех заказчиков
        public static List<List<string>> DataTableCustomer()
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

        //Вывод всех товаров
        public static List<List<string>> DataTableOrderProduct()
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

        //Вывод всех категорий
        public static List<List<string>> DataTableCategory()
        {
            List<List<string>> list = new List<List<string>>();
            List<string> names = new List<string>();
            List<string> dataCategory = new List<string>();

            const string sql = "SELECT \"CATEGORY_ID\", \"CATEGORY_NAME\"" +
                "\r\nFROM public.\"Category\"";
            using (NpgsqlCommand sqlCommand = new NpgsqlCommand(sql, Program.connection))
            {
                Program.connection.Open();
                using (NpgsqlDataReader dataReader = sqlCommand.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(dataReader);
                    foreach (DataRow row in dataTable.Rows)
                    {
                        names.Add(row["CATEGORY_NAME"].ToString());
                        dataCategory.Add(row["CATEGORY_ID"].ToString() + "|" + row["CATEGORY_NAME"].ToString());
                    }
                    dataReader.Close();
                }
                Program.connection.Close();
                list.Add(names);
                list.Add(dataCategory);
            }
            return list;
        }

        //Вывод всех цехов
        public static List<List<string>> DataTableWorkshop()
        {
            List<List<string>> list = new List<List<string>>();
            List<string> names = new List<string>();
            List<string> dataWorkshop = new List<string>();

            const string sql = "SELECT \"WORKSHOP_ID\", \"WORKSHOP_NAME\"" +
                "\r\nFROM public.\"Workshop\"";
            using (NpgsqlCommand sqlCommand = new NpgsqlCommand(sql, Program.connection))
            {
                Program.connection.Open();
                using (NpgsqlDataReader dataReader = sqlCommand.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(dataReader);
                    foreach (DataRow row in dataTable.Rows)
                    {
                        names.Add(row["WORKSHOP_NAME"].ToString());
                        dataWorkshop.Add(row["WORKSHOP_ID"].ToString() + "|" + row["WORKSHOP_NAME"].ToString());
                    }
                    dataReader.Close();
                }
                Program.connection.Close();
                list.Add(names);
                list.Add(dataWorkshop);
            }
            return list;
        }

        //Поиск Id авторизованного пользователя
        public static int SearchUser()
        {
            int id;
            string sql = "SELECT \"ACCOUNTANT_ID\"" +
                "\r\nFROM public.\"Accountant\" " +
                $"\r\nWHERE \"ACCOUNTANT_SURNAME\" = '{ServicesAutorization.Position[1]}' AND \"ACCOUNTANT_NAME\" = '{ServicesAutorization.Position[2]}'";
            using (NpgsqlCommand sqlCommand = new NpgsqlCommand(sql, Program.connection))
            {
                Program.connection.Open();
                id = (int?)sqlCommand.ExecuteScalar() ?? 0;
                Program.connection.Close();
            }
            return id;
        }

        //Данные о товарах для указанного заказа
        public static List<string>[] DataOrderProduct(int order_id)
        {
            string sql = "SELECT p.\"PRODUCT_ID\", \"CATEGORY_NAME\", \"PRODUCT_NAME\", \"PRODUCT_PRICE\", \"ORDER_PRODUCT_COUT\"" +
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
                        list.Add(row["PRODUCT_ID"].ToString());
                        mas[i] = list;
                    }
                }
                Program.connection.Close();
            }
            return mas;
        }

        //Подробная информация о заказе
        public static Order FullDataOrder(int order_id)
        {
            string sql = "SELECT o.\"ORDER_ID\", \"ORDER_STATUS\", \"ORDER_DATA\", cu.\"CUSTOMER_ID\", \"ORDER_PRICE\", \"ORDER_PRICE_DELIVERY\", \"ORDER_ADDRESS\", \"ORDER_UNLOADING_DATA\", \"ORDER_COMMENTORDER_COMMENT\", ac.\"ACCOUNTANT_ID\"," +
                "\r\n\"CUSTOMER_SURNAME\", \"CUSTOMER_NAME\", \"CUSTOMER_PATRONYMIC\", \"CUSTOMER_ORGANIZATION\", \"CUSTOMER_TELEPHONE\"" +
                "\r\nFROM public.\"Order\" o, public.\"Customer\" cu, public.\"Accountant\" ac" +
                $"\r\nWHERE o.\"ORDER_ID\" = {order_id} AND o.\"CUSTOMER_ID\" = cu.\"CUSTOMER_ID\" AND o.\"ACCOUNTANT_ID\" = ac.\"ACCOUNTANT_ID\"";
            DataTable dataTable;
            DataRow row;

            using (NpgsqlCommand sqlCommand = new NpgsqlCommand(sql, Program.connection))
            {
                Program.connection.Open();
                using (NpgsqlDataReader dataReader = sqlCommand.ExecuteReader())
                {
                    dataTable = new DataTable();
                    dataTable.Load(dataReader);
                    dataReader.Close();
                    row = dataTable.Rows[0];
                }
                Program.connection.Close();
            }

            Order order = new Order();

            string[] Customer = $"{row["CUSTOMER_SURNAME"]}|{row["CUSTOMER_NAME"]}|{row["CUSTOMER_PATRONYMIC"]}|{row["CUSTOMER_ORGANIZATION"]}".Trim().Split("|".ToCharArray());

            if (Customer.Length > 3)
                order.AddCustomer(Convert.ToInt32(row["CUSTOMER_ID"]), Customer[0], Customer[1], Customer[2], Customer[3], row["CUSTOMER_TELEPHONE"].ToString());
            else if (Customer.Length > 2)
                order.AddCustomer(Convert.ToInt32(row["CUSTOMER_ID"]), Customer[0], Customer[1], Customer[2], null, row["CUSTOMER_TELEPHONE"].ToString());
            else
                order.AddCustomer(Convert.ToInt32(row["CUSTOMER_ID"]), Customer[0], Customer[1], null, null, row["CUSTOMER_TELEPHONE"].ToString());


            order.Address = row["ORDER_ADDRESS"].ToString();
            order.Status = row["ORDER_STATUS"].ToString();
            order.DataOrder = Convert.ToDateTime(row["ORDER_DATA"]).ToString("dd.MM.yyyy");
            if (row["ORDER_UNLOADING_DATA"].ToString().Length != 0)
                order.DataDelevery = Convert.ToDateTime(row["ORDER_UNLOADING_DATA"]).ToString("dd.MM.yyyy");
            else
                order.DataDelevery = null;
            order.DeleveryPrice = Convert.ToDouble(row["ORDER_PRICE_DELIVERY"]);

            List<string>[] dataOrderProduct = DataOrderProduct(order_id);

            for (int i = 0; i < dataOrderProduct.Length; i++)
                order.AddProduct(Convert.ToInt32(dataOrderProduct[i][4]), dataOrderProduct[i][1], Convert.ToDouble(dataOrderProduct[i][3]), Convert.ToInt32(dataOrderProduct[i][2]));

            order.Comment = row["ORDER_COMMENTORDER_COMMENT"].ToString();
            return order;
        }

        //Подробная информация в выбраном разделе.
        public static List<string> DataOther(string Name_tree, int Id)
        {
            List<string> list = new List<string>();
            string sql = null;
            switch (Name_tree)
            {
                case "Категории":
                    sql = $"SELECT \"CATEGORY_NAME\" FROM public.\"Category\"" +
                        $"\r\nWHERE \"CATEGORY_ID\" = {Id}";
                    break;
                case "Цеха":
                    sql = "SELECT \"WORKSHOP_NAME\" FROM public.\"Workshop\"" +
                        $"\r\nWHERE \"WORKSHOP_ID\" = {Id}";
                    break;
                case "Товары":
                    sql = "SELECT \"PRODUCT_NAME\", \"CATEGORY_NAME\", \"PRODUCT_PRICE\", \"WORKSHOP_NAME\"" +
                        "\r\nFROM public.\"Product\" p, public.\"Category\" c, public.\"Workshop\" w" +
                       $"\r\nWHERE \"PRODUCT_ID\" = {Id} AND p.\"CATEGORY_ID\" = c.\"CATEGORY_ID\" AND p.\"WORKSHOP_ID\" = w.\"WORKSHOP_ID\"";
                    break;
                case "Заказчики":
                    sql = "SELECT \"CUSTOMER_SURNAME\", \"CUSTOMER_NAME\", \"CUSTOMER_PATRONYMIC\", \"CUSTOMER_TELEPHONE\", \"CUSTOMER_ORGANIZATION\" FROM public.\"Customer\"" +
                        $"\r\nWHERE \"CUSTOMER_ID\" = {Id}";
                    break;
                case "Пользователи":
                    sql = "SELECT \"ACCOUNTANT_SURNAME\", \"ACCOUNTANT_NAME\", \"ACCOUNTANT_PATRONYMIC\", \"ACCOUNTANT_LOGIN\", \"ACCOUNTANT_PASSWORD\", \"ACCOUNTANT_POSITION\" FROM public.\"Accountant\"" +
                       $"\r\nWHERE \"ACCOUNTANT_ID\" = {Id}";
                    break;
            }

            using (NpgsqlCommand sqlCommand = new NpgsqlCommand(sql, Program.connection))
            {
                Program.connection.Open();
                using (NpgsqlDataReader dataReader = sqlCommand.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(dataReader);
                    dataReader.Close();
                    DataRow row = dataTable.Rows[0];
                    for (int i = 0; i < row.Table.Columns.Count; i++)
                        list.Add(row[i].ToString());
                }
                Program.connection.Close();
            }
            return list;
        }

        //Вывод данных о заказе для заполнения их в таблиц Excel
        public static List<string> DataOrderInExcel(int order_id)
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
    }
}