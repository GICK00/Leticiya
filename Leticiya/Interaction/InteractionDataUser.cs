using Leticiya.Class;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Data;
using System.Windows.Forms;

namespace Leticiya.Interaction
{
    internal class InteractionDataUser
    {
        private readonly ServicesUser servicesUser = new ServicesUser();
        private int AccounterId;
        private int OrderId;

        public void AddDataOrder(Order order)
        {
            AccounterId = servicesUser.SearchUser();
            string sql = "INSERT INTO public.\"Order\" (\"ORDER_STATUS\", \"ORDER_DATA\", \"CUSTOMER_ID\", \"ORDER_PRICE\", \"ORDER_PRICE_DELIVERY\", \"ORDER_ADDRESS\", \"ORDER_UNLOADING_DATA\", \"ORDER_COMMENTORDER_COMMENT\", \"ACCOUNTANT_ID\")" +
                $"\r\nVALUES (@ORDER_STATUS, @ORDER_DATA, @CUSTOMER_ID, '{order.OrderPrice()}', '{order.DeleveryPrice}', @ORDER_ADDRESS, @ORDER_UNLOADING_DATA, @ORDER_COMMENTORDER_COMMENT, @ACCOUNTANT_ID)" +
                "\r\nRETURNING \"ORDER_ID\"";

            using (NpgsqlCommand sqlCommand = new NpgsqlCommand(sql, Program.connection))
            {
                Program.connection.Open();

                sqlCommand.Parameters.Add(new NpgsqlParameter<string>("@ORDER_STATUS", NpgsqlDbType.Text));
                sqlCommand.Parameters["@ORDER_STATUS"].Value = order.Status;

                sqlCommand.Parameters.Add(new NpgsqlParameter("ORDER_DATA", NpgsqlDbType.Date));
                sqlCommand.Parameters["ORDER_DATA"].Value = DateTime.Parse(order.DataOrder);

                sqlCommand.Parameters.Add(new NpgsqlParameter<int>("CUSTOMER_ID", NpgsqlDbType.Integer));
                sqlCommand.Parameters["CUSTOMER_ID"].Value = order.customer.Id;

                /*sqlCommand.Parameters.Add(new NpgsqlParameter("ORDER_PRICE", NpgsqlDbType.Money));
                sqlCommand.Parameters["ORDER_PRICE"].Value = ;

                sqlCommand.Parameters.Add(new NpgsqlParameter("ORDER_PRICE_DELIVERY", NpgsqlDbType.Money));
                sqlCommand.Parameters["ORDER_PRICE_DELIVERY"].Value = ;*/

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

                OrderId = Convert.ToInt32(sqlCommand.ExecuteScalar());
                Program.connection.Close();
            }


            if (order.products.Count == 0)
                return;
            sql = "INSERT INTO public.\"Order_Product\" (\"ORDER_ID\", \"PRODUCT_ID\", \"ORDER_PRODUCT_COUT\")" +
                $"\r\nVALUES ('{OrderId}', '{order.products[0].Id}', '{order.products[0].Cout}')";
            for (int i = 0; i < order.products.Count; i++)
                sql += $",\r\n('{OrderId}', '{order.products[i].Id}', '{order.products[i].Cout}')";

            using (NpgsqlCommand sqlCommand = new NpgsqlCommand(sql, Program.connection))
            {
                Program.connection.Open();
                sqlCommand.ExecuteNonQuery();
                Program.connection.Close();
            }
        }

        public void AddDataOther(string sql)
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