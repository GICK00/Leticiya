using Leticiya.Interaction;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Leticiya
{
    public partial class FormAddEditOther : MaterialForm
    {
        ServicesUser ServicesUser = new ServicesUser();
        InteractionDataUser interactionDataUser = new InteractionDataUser();

        private string type;
        public FormAddEditOther(string name, string type)
        {
            InitializeComponent();

            this.type = type;
            this.Text = name;

            new Thread(() =>
            {
                var materialSkinManager = MaterialSkinManager.Instance;
                materialSkinManager.AddFormToManage(this);
                materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
                materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey300, Primary.Grey900, Primary.Grey200, Accent.LightBlue200, TextShade.BLACK);
            }).Start();

            if (type != "add")
                buttonAddEdit.Text = "Изменить";

            switch (this.Text)
            {
                case "Категории":
                    panelCategory.Visible = true;
                    panelCategory.Enabled = true;
                    break;
                case "Цеха":
                    panelWorkshop.Visible = true;
                    panelWorkshop.Enabled = true;
                    break;
                case "Товары":
                    panelProduct.Visible = true;
                    panelProduct.Enabled = true;
                    break;
                case "Заказчики":
                    panelCustomer.Visible = true;
                    panelCustomer.Enabled = true;
                    break;
                case "Пользователи":
                    panelAccountant.Visible = true;
                    panelAccountant.Enabled = true;
                    break;
            }
        }

        private void FormAddEditOther_Load(object sender, EventArgs e)
        {

        }

        private void buttonAddEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (type == "add")
                {
                    string sql = null;
                    switch (this.Text)
                    {
                        case "Категории":
                            sql = "INSERT INTO public.\"Category\" (\"CATEGORY_NAME\")" +
                                $"\r\nVALUES ('{textBoxCategory.Text.Trim()}')";
                            break;
                        case "Цеха":
                            sql = "INSERT INTO \"Workshop\" (\"WORKSHOP_NAME\")" +
                                $"\r\n\tVALUES ('{textBoxWorkshop.Text.Trim()}')";
                            break;
                        case "Товары":
                            sql = "INSERT INTO \"Product\" (\"PRODUCT_NAME\", \"CATEGORY_ID\", \"PRODUCT_PRICE\", \"WORKSHOP_ID\")" +
                                $"\r\n\tVALUES ('{textBoxPRODUCT_NAME.Text.Trim()}','{comboBoxCategory.Text}','{textBoxPRODUCT_PRICE.Text.Trim()}','{comboBoxWorkshop.Text}')";
                            break;
                        case "Заказчики":
                            string[] FIO = textBoxCUSTOMER_NAME.Text.Trim().Split();
                            if (FIO.Length > 3)
                                sql = "INSERT INTO \"Customer\" (\"CUSTOMER_SURNAME\", \"CUSTOMER_NAME\", \"CUSTOMER_PATRONYMIC\", \"CUSTOMER_TELEPHONE\", \"CUSTOMER_ORGANIZATION\")" +
                                    $"\r\n\tVALUES ('{FIO[0]}','{FIO[1]}','{FIO[2]}','{textBoxCUSTOMER_TELEPHONE.Text.Trim()}','{textBoxCUSTOMER_ORGANIZATION.Text.Trim()}')";
                            else
                                sql = "INSERT INTO \"Customer\" (\"CUSTOMER_SURNAME\", \"CUSTOMER_NAME\", \"CUSTOMER_PATRONYMIC\", \"CUSTOMER_TELEPHONE\", \"CUSTOMER_ORGANIZATION\")" +
                                    $"\r\n\tVALUES ('{FIO[0]}','{FIO[1]}','','{textBoxCUSTOMER_TELEPHONE.Text.Trim()}','{textBoxCUSTOMER_ORGANIZATION.Text.Trim()}')";
                            break;
                        case "Пользователи":
                            FIO = textBoxAccountentName.Text.Trim().Split();
                            switch(FIO.Length)
                            {
                                case 3:
                                    sql = "INSERT INTO \"Accountant\" (\"ACCOUNTANT_SURNAME\", \"ACCOUNTANT_NAME\", \"ACCOUNTANT_PATRONYMIC\", \"ACCOUNTANT_LOGIN\", \"ACCOUNTANT_PASSWORD\", \"ACCOUNTANT_POSITION\")" +
                                        $"\r\n\tVALUES ('{FIO[0]}','{FIO[1]}','{FIO[2]}','{textBoxLogin.Text.Trim()}','{textBoxPassword.Text.Trim()}','{comboBoxPosition.Text}')";
                                    break;
                                case 2:
                                    sql = "INSERT INTO \"Accountant\" (\"ACCOUNTANT_SURNAME\", \"ACCOUNTANT_NAME\", \"ACCOUNTANT_PATRONYMIC\", \"ACCOUNTANT_LOGIN\", \"ACCOUNTANT_PASSWORD\", \"ACCOUNTANT_POSITION\")" +
                                        $"\r\n\tVALUES ('{FIO[0]}','{FIO[1]}','','{textBoxLogin.Text.Trim()}','{textBoxPassword.Text.Trim()}','{comboBoxPosition.Text}')";
                                    break;
                                case 1:
                                    sql = "INSERT INTO \"Accountant\" (\"ACCOUNTANT_SURNAME\", \"ACCOUNTANT_NAME\", \"ACCOUNTANT_PATRONYMIC\", \"ACCOUNTANT_LOGIN\", \"ACCOUNTANT_PASSWORD\", \"ACCOUNTANT_POSITION\")" +
                                        $"\r\n\tVALUES ('{FIO[0]}','','','{textBoxLogin.Text.Trim()}','{textBoxPassword.Text.Trim()}','{comboBoxPosition.Text}')";
                                    break;
                                case 0:
                                    sql = "INSERT INTO \"Accountant\" (\"ACCOUNTANT_SURNAME\", \"ACCOUNTANT_NAME\", \"ACCOUNTANT_PATRONYMIC\", \"ACCOUNTANT_LOGIN\", \"ACCOUNTANT_PASSWORD\", \"ACCOUNTANT_POSITION\")" +
                                        $"\r\n\tVALUES ('','','','{textBoxLogin.Text.Trim()}','{textBoxPassword.Text.Trim()}','{comboBoxPosition.Text}')";
                                    break;
                            }
                            break;
                    }
                    interactionDataUser.AddDataOther(sql);
                    ServicesUser.ReloadViewBD(this.Text);
                }
                else
                {

                }
            }
            catch
            {
                MessageBox.Show("Неверное введено Ф.И.О", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonExit_Click(object sender, EventArgs e) => this.Close();
    }
}