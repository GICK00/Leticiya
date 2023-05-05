using Leticiya.Class;
using Leticiya.Interaction;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Leticiya
{
    public partial class FormAddEditOrder : MaterialForm
    {
        private readonly ServicesUser servicesUser = new ServicesUser();
        private readonly InteractionDataUser interactionDataUser = new InteractionDataUser();

        private int CustomerId;
        private int OrderId;

        private string type;

        public FormAddEditOrder(string type, int order_id)
        {
            InitializeComponent();

            this.type = type;
            this.OrderId = order_id;

            if (type == "add")
                buttonAddEdit.Text = "Добавить заказ";
            else
                buttonAddEdit.Text = "Изменить заказ";

            new Thread(() =>
            {
                var materialSkinManager = MaterialSkinManager.Instance;
                materialSkinManager.AddFormToManage(this);
                materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
                materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey300, Primary.Grey900, Primary.Grey200, Accent.LightBlue200, TextShade.BLACK);
            }).Start();
        }

        private void FormAddEditOrder_Load(object sender, EventArgs e)
        {
            if (Program.SQLStat != true)
                return;
            comboBoxCustomer.DataSource = servicesUser.DataTableCustomer()[0];
            comboBoxProduct.DataSource = servicesUser.DataTableOrderProduct()[0];
            if (type != "edit")
                return;
            DataInForm();
        }

        private void buttonAddEdit_Click(object sender, EventArgs e)
        {
            if (CustomerId == -1)
            {
                MessageBox.Show("Нет такого заказчика, проверьте пожалуйста корректность введёных данных.");
                return;
            }

            Order order = new Order();

            string[] Customer = comboBoxCustomer.Text.Trim().Split();

            if (Customer.Length > 3)
                order.AddCustomer(CustomerId, Customer[0], Customer[1], Customer[2], Customer[3], textBoxTelephone.Text.Trim());
            else if (Customer.Length > 2)
                order.AddCustomer(CustomerId, Customer[0], Customer[1], Customer[2], null, textBoxTelephone.Text.Trim());
            else
                order.AddCustomer(CustomerId, Customer[0], Customer[1], null, null, textBoxTelephone.Text.Trim());


            order.Address = textBoxAddres.Text.Trim();
            order.Status = comboBoxStatus.Text;
            order.DataOrder = textBoxDataOrder.Text.Trim();
            order.DataDelevery = textBoxDeleveryData.Text.Trim();
            order.DeleveryPrice = Convert.ToDouble(textBoxDeleveryPrice.Text.Trim());

            for (int i = 0; i < dataGridViewProduct.Rows.Count; i++)
                order.AddProduct(Convert.ToInt32(dataGridViewProduct["IdProduct", i].Value), dataGridViewProduct["NameProduct", i].Value.ToString(), Convert.ToDouble(dataGridViewProduct["Price", i].Value), Convert.ToInt32(dataGridViewProduct["Cout", i].Value));

            order.Comment = textBoxComment.Text.Trim();

            if (type == "add")
            {
                interactionDataUser.AddUpdateDataOrder("add", order, -1);
                Program.formMain.toolStripStatusLabel2.Text = "Заказ оформлен";
            }
            else
            {
                interactionDataUser.AddUpdateDataOrder("edit", order, OrderId);
                Program.formMain.toolStripStatusLabel2.Text = "Заказ обновлен";
            }                
            servicesUser.ReloadViewBD("Заказы");
        }

        private void comboBoxCustomer_TextChanged(object sender, EventArgs e)
        {
            List<string> list = servicesUser.DataTableCustomer()[1];

            string[] data;

            foreach (string row in list)
            {
                data = row.Split("|".ToCharArray());
                string dam = data[1] + " " + data[2];
                if (dam == comboBoxCustomer.Text)
                {
                    textBoxTelephone.Text = data[3];
                    CustomerId = Convert.ToInt32(data[0]);
                    return;
                }
            }
            CustomerId = -1;
        }

        private void buttonAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> list = servicesUser.DataTableOrderProduct()[1];

                string[] data;

                foreach (string row in list)
                {
                    data = row.Split("|".ToCharArray());
                    if (data[1] == comboBoxProduct.Text)
                    {
                        dataGridViewProduct.Rows.Add(data[0], data[1], data[2], Convert.ToInt32(textBoxCout.Text), data[3]);
                        return;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Введите количество товара!", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DataInForm()
        {
            Order order = servicesUser.FullDataOrder(OrderId);

            comboBoxCustomer.Text = $"{order.customer.Surname} {order.customer.Name} {order.customer.Patronymic} {order.customer.Organization}".Trim();

            textBoxAddres.Text = order.Address;
            comboBoxStatus.Text = order.Status;
            textBoxDataOrder.Text = order.DataOrder;
            textBoxDeleveryData.Text = order.DataDelevery;
            textBoxDeleveryPrice.Text = order.DeleveryPrice.ToString();

            List<string>[] orderList = servicesUser.DataOrderProduct(OrderId);
            for (int i = 0; i < orderList.Length; i++)
                dataGridViewProduct.Rows.Add(orderList[i][4], orderList[i][1], orderList[i][0], orderList[i][2], orderList[i][3]);

            textBoxComment.Text = order.Comment;
        }

        private void dataGridViewProduct_MouseDoubleClick(object sender, MouseEventArgs e) => dataGridViewProduct.Rows.RemoveAt(dataGridViewProduct.CurrentRow.Index);

        private void textBoxCout_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
                e.Handled = true;
        }
    }
}