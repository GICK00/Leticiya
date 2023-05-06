using Leticiya.Class;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Windows.Forms;

namespace Leticiya.Interaction
{
    internal class InteractionDataUser
    {
        private readonly ServicesUser servicesUser = new ServicesUser();
        private int OrderId;
        private int AccounterId;


        public void AddUpdateDataOrder(string type, Order order, int order_id)
        {
            OrderId = order_id;
            AccounterId = servicesUser.SearchUser();
            string sql;
            if (type == "add")
                sql = "INSERT INTO public.\"Order\" (\"ORDER_STATUS\", \"ORDER_DATA\", \"CUSTOMER_ID\", \"ORDER_PRICE\", \"ORDER_PRICE_DELIVERY\", \"ORDER_ADDRESS\", \"ORDER_UNLOADING_DATA\", \"ORDER_COMMENTORDER_COMMENT\", \"ACCOUNTANT_ID\")" +
                $"\r\nVALUES (@ORDER_STATUS, @ORDER_DATA, @CUSTOMER_ID, '{order.OrderPrice()}', '{order.DeleveryPrice}', @ORDER_ADDRESS, @ORDER_UNLOADING_DATA, @ORDER_COMMENTORDER_COMMENT, @ACCOUNTANT_ID)" +
                "\r\nRETURNING \"ORDER_ID\"";
            else
                sql = "UPDATE public.\"Order\" " +
                    "\r\nSET \"ORDER_STATUS\" = @ORDER_STATUS, \"ORDER_DATA\" = @ORDER_DATA, \"CUSTOMER_ID\" =  @CUSTOMER_ID," +
                    $"\r\n\"ORDER_PRICE\" = {order.OrderPrice()},  \"ORDER_PRICE_DELIVERY\" = {order.DeleveryPrice}, \"ORDER_ADDRESS\" = @ORDER_ADDRESS," +
                    "\r\n\"ORDER_UNLOADING_DATA\" = @ORDER_UNLOADING_DATA, \"ORDER_COMMENTORDER_COMMENT\" = @ORDER_COMMENTORDER_COMMENT," +
                    $"\r\n\"ACCOUNTANT_ID\" = @ACCOUNTANT_ID \r\nWHERE \"ORDER_ID\" = {OrderId}";

            using (NpgsqlCommand sqlCommand = new NpgsqlCommand(sql, Program.connection))
            {
                Program.connection.Open();

                sqlCommand.Parameters.Add(new NpgsqlParameter<string>("@ORDER_STATUS", NpgsqlDbType.Text));
                sqlCommand.Parameters["@ORDER_STATUS"].Value = order.Status;

                sqlCommand.Parameters.Add(new NpgsqlParameter("ORDER_DATA", NpgsqlDbType.Date));
                sqlCommand.Parameters["ORDER_DATA"].Value = Convert.ToDateTime(order.DataOrder);

                sqlCommand.Parameters.Add(new NpgsqlParameter<int>("CUSTOMER_ID", NpgsqlDbType.Integer));
                sqlCommand.Parameters["CUSTOMER_ID"].Value = order.customer.Id;

                sqlCommand.Parameters.Add(new NpgsqlParameter<string>("ORDER_ADDRESS", NpgsqlDbType.Text));
                sqlCommand.Parameters["ORDER_ADDRESS"].Value = order.Address;

                if (order.DataDelevery != null)
                {
                    sqlCommand.Parameters.Add(new NpgsqlParameter("ORDER_UNLOADING_DATA", NpgsqlDbType.Date));
                    sqlCommand.Parameters["ORDER_UNLOADING_DATA"].Value = DBNull.Value;
                }
                else
                {
                    sqlCommand.Parameters.Add(new NpgsqlParameter("ORDER_UNLOADING_DATA", NpgsqlDbType.Date));
                    sqlCommand.Parameters["ORDER_UNLOADING_DATA"].Value = DateTime.Parse(order.DataDelevery);
                }

                sqlCommand.Parameters.Add(new NpgsqlParameter<string>("ORDER_COMMENTORDER_COMMENT", NpgsqlDbType.Text));
                sqlCommand.Parameters["ORDER_COMMENTORDER_COMMENT"].Value = order.Comment;

                sqlCommand.Parameters.Add(new NpgsqlParameter<int>("ACCOUNTANT_ID", NpgsqlDbType.Integer));
                sqlCommand.Parameters["ACCOUNTANT_ID"].Value = AccounterId;

                if (type == "add")
                    OrderId = Convert.ToInt32(sqlCommand.ExecuteScalar());
                else
                    sqlCommand.ExecuteNonQuery();
                Program.connection.Close();
            }

            if (order.products.Count == 0)
                return;

            if (type != "add")
            {
                sql = "DELETE FROM \"Order_Product\"" +
                    $"\r\nWHERE \"ORDER_ID\" = {OrderId}";
                using (NpgsqlCommand sqlCommand = new NpgsqlCommand(sql, Program.connection))
                {
                    Program.connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    Program.connection.Close();
                }
            }

            if (order.products.Count >= 1)
                sql = "INSERT INTO public.\"Order_Product\" (\"ORDER_ID\", \"PRODUCT_ID\", \"ORDER_PRODUCT_COUT\")" +
                    $"\r\nVALUES ('{OrderId}', '{order.products[0].Id}', '{order.products[0].Cout}')";
            if (order.products.Count > 1)
            {
                for (int i = 1; i < order.products.Count; i++)
                    sql += $",\r\n('{OrderId}', '{order.products[i].Id}', '{order.products[i].Cout}')";
            }
            using (NpgsqlCommand sqlCommand = new NpgsqlCommand(sql, Program.connection))
            {
                Program.connection.Open();
                sqlCommand.ExecuteNonQuery();
                Program.connection.Close();
            }
        }

        public void AddUpdateDataOther(string sql)
        {
            using (NpgsqlCommand sqlCommand = new NpgsqlCommand(sql, Program.connection))
            {
                Program.connection.Open();
                sqlCommand.ExecuteNonQuery();
                Program.connection.Close();
            }
        }

        public void Deleted(string sql)
        {
            DialogResult result = MessageBox.Show("Вы уверенны что хотите удалить строку данных?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                return;
            try
            {
                using (NpgsqlCommand sqlCommand = new NpgsqlCommand(sql, Program.connection))
                {
                    Program.connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
                Program.formMain.toolStripStatusLabel2.Text = $"Данные удалены";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.formMain.toolStripStatusLabel2.Text = $"Ошибка! {ex.Message}";
            }
            finally
            {
                Program.connection.Close();
                servicesUser.ReloadViewBD(FormMain.treeViewItemSelect);
            }
        }
    }
}