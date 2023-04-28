using Leticiya.Interaction;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Leticiya
{
    public partial class FormAddEditOther : MaterialForm
    {
        ServicesUser servicesUser = new ServicesUser();
        InteractionDataUser interactionDataUser = new InteractionDataUser();

        private string Type;
        private string Name_tree;
        private int Id;
        public FormAddEditOther(string type, string name_tree, int id)
        {
            InitializeComponent();

            Type = type;
            Name_tree = name_tree;
            Id = id;

            new Thread(() =>
            {
                var materialSkinManager = MaterialSkinManager.Instance;
                materialSkinManager.AddFormToManage(this);
                materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
                materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey300, Primary.Grey900, Primary.Grey200, Accent.LightBlue200, TextShade.BLACK);
            }).Start();

            if (Type != "add")
            {
                this.Text = $"{name_tree} №{id}";
                buttonAddEdit.Text = "Изменить";
            }
                
            switch (Name_tree)
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
            if (Type == "add")
                return;
            DataInForm();            
        }

        private void buttonAddEdit_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = null;
                if (Type == "add")
                {   
                    switch (Name_tree)
                    {
                        case "Категории":
                            sql = "INSERT INTO public.\"Category\" (\"CATEGORY_NAME\")" +
                                $"\r\nVALUES ('{textBoxCategory.Text.Trim()}')";
                            break;
                        case "Цеха":
                            sql = "INSERT INTO public.\"Workshop\" (\"WORKSHOP_NAME\")" +
                                $"\r\n\tVALUES ('{textBoxWorkshop.Text.Trim()}')";
                            break;
                        case "Товары":
                            sql = "INSERT INTO public.\"Product\" (\"PRODUCT_NAME\", \"CATEGORY_ID\", \"PRODUCT_PRICE\", \"WORKSHOP_ID\")" +
                                $"\r\n\tVALUES ('{textBoxPRODUCT_NAME.Text.Trim()}','{comboBoxCategory.Text}','{textBoxPRODUCT_PRICE.Text.Trim()}','{comboBoxWorkshop.Text}')";
                            break;
                        case "Заказчики":
                            string[] FIO = textBoxCUSTOMER_NAME.Text.Trim().Split();
                            if (FIO.Length > 3)
                                sql = "INSERT INTO public.\"Customer\" (\"CUSTOMER_SURNAME\", \"CUSTOMER_NAME\", \"CUSTOMER_PATRONYMIC\", \"CUSTOMER_TELEPHONE\", \"CUSTOMER_ORGANIZATION\")" +
                                    $"\r\n\tVALUES ('{FIO[0]}','{FIO[1]}','{FIO[2]}','{textBoxCUSTOMER_TELEPHONE.Text.Trim()}','{textBoxCUSTOMER_ORGANIZATION.Text.Trim()}')";
                            else
                                sql = "INSERT INTO public.\"Customer\" (\"CUSTOMER_SURNAME\", \"CUSTOMER_NAME\", \"CUSTOMER_PATRONYMIC\", \"CUSTOMER_TELEPHONE\", \"CUSTOMER_ORGANIZATION\")" +
                                    $"\r\n\tVALUES ('{FIO[0]}','{FIO[1]}','','{textBoxCUSTOMER_TELEPHONE.Text.Trim()}','{textBoxCUSTOMER_ORGANIZATION.Text.Trim()}')";
                            break;
                        case "Пользователи":
                            FIO = textBoxAccountentName.Text.Trim().Split();
                            switch (FIO.Length)
                            {
                                case 3:
                                    sql = "INSERT INTO public.\"Accountant\" (\"ACCOUNTANT_SURNAME\", \"ACCOUNTANT_NAME\", \"ACCOUNTANT_PATRONYMIC\", \"ACCOUNTANT_LOGIN\", \"ACCOUNTANT_PASSWORD\", \"ACCOUNTANT_POSITION\")" +
                                        $"\r\n\tVALUES ('{FIO[0]}','{FIO[1]}','{FIO[2]}','{textBoxLogin.Text.Trim()}','{textBoxPassword.Text.Trim()}','{comboBoxPosition.Text}')";
                                    break;
                                case 2:
                                    sql = "INSERT INTO public.\"Accountant\" (\"ACCOUNTANT_SURNAME\", \"ACCOUNTANT_NAME\", \"ACCOUNTANT_PATRONYMIC\", \"ACCOUNTANT_LOGIN\", \"ACCOUNTANT_PASSWORD\", \"ACCOUNTANT_POSITION\")" +
                                        $"\r\n\tVALUES ('{FIO[0]}','{FIO[1]}','','{textBoxLogin.Text.Trim()}','{textBoxPassword.Text.Trim()}','{comboBoxPosition.Text}')";
                                    break;
                                case 1:
                                    sql = "INSERT INTO public.\"Accountant\" (\"ACCOUNTANT_SURNAME\", \"ACCOUNTANT_NAME\", \"ACCOUNTANT_PATRONYMIC\", \"ACCOUNTANT_LOGIN\", \"ACCOUNTANT_PASSWORD\", \"ACCOUNTANT_POSITION\")" +
                                        $"\r\n\tVALUES ('{FIO[0]}','','','{textBoxLogin.Text.Trim()}','{textBoxPassword.Text.Trim()}','{comboBoxPosition.Text}')";
                                    break;
                                case 0:
                                    sql = "INSERT INTO public.\"Accountant\" (\"ACCOUNTANT_SURNAME\", \"ACCOUNTANT_NAME\", \"ACCOUNTANT_PATRONYMIC\", \"ACCOUNTANT_LOGIN\", \"ACCOUNTANT_PASSWORD\", \"ACCOUNTANT_POSITION\")" +
                                        $"\r\n\tVALUES ('','','','{textBoxLogin.Text.Trim()}','{textBoxPassword.Text.Trim()}','{comboBoxPosition.Text}')";
                                    break;
                            }
                            break;
                    }
                    Program.formMain.toolStripStatusLabel2.Text = "Запись добавлена";
                }
                else
                {
                    //Доделать изменение для каждого раздела
                    switch (Name_tree)
                    {
                        case "Категории":
                            sql = $"UPDATE public.\"Category\" SET \"CATEGORY_NAME\" = '{textBoxCategory.Text}'" +
                                $"WHERE \"CATEGORY_ID\" = {Id}";
                            break;
                        case "Цеха":
                            sql = "";
                            break;
                        case "Товары":
                            sql = "";
                            break;
                        case "Заказчики":
                            sql = "";
                            break;
                        case "Пользователи":
                            sql = "";
                            break;
                    }
                    Program.formMain.toolStripStatusLabel2.Text = "Запись изменена";
                }
                interactionDataUser.AddUpdateDataOther(sql);
                servicesUser.ReloadViewBD(Name_tree);
            }
            catch
            {
                MessageBox.Show("Неверное введено Ф.И.О", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonExit_Click(object sender, EventArgs e) => this.Close();

        //Доделать выгрузку данных при откртии формы для изменения
        private void DataInForm()
        {
            List<string> list = servicesUser.DataOther(Name_tree, Id);
            switch (Name_tree)
            {
                case "Категории":
                    textBoxCategory.Text = list[0];
                    break;
                case "Цеха":
                    textBoxWorkshop.Text = list[0];
                    break;
                case "Товары":
                    textBoxPRODUCT_NAME.Text = list[0];
                    comboBoxCategory.Text = list[1];
                    textBoxPRODUCT_PRICE.Text = list[2];
                    comboBoxWorkshop.Text = list[3];
                    break;
                case "Заказчики":
                    textBoxCUSTOMER_NAME.Text = $"{list[0]} {list[1]} {list[2]}";
                    textBoxCUSTOMER_ORGANIZATION.Text = list[3];
                    textBoxCUSTOMER_TELEPHONE.Text = list[4];
                    break;
                case "Пользователи":
                    textBoxAccountentName.Text = $"{list[0]} {list[1]} {list[2]}";
                    textBoxLogin.Text = list[3];
                    textBoxPassword.Text = list[4];
                    comboBoxPosition.Text = list[5];
                    break;
            }
        }
    }
}