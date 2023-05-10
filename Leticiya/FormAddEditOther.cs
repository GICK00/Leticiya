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
        private string Type;
        private string Name_tree;
        private int Id;

        public FormAddEditOther(string type, string name_tree, int id)
        {
            InitializeComponent();

            Type = type;
            Name_tree = name_tree;
            Id = id;

            if (Type != "add")
            {
                this.Text = $"{name_tree} №{id}";
                buttonAddEdit.Text = "Изменить";
            }
            else
                this.Text = name_tree;

            new Thread(() =>
            {
                var materialSkinManager = MaterialSkinManager.Instance;
                materialSkinManager.AddFormToManage(this);
                materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
                materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey300, Primary.Grey900, Primary.Grey200, Accent.LightBlue200, TextShade.BLACK);
            }).Start();

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
                    comboBoxCategory.DataSource = ServicesUser.DataTableCategory()[0];
                    comboBoxWorkshop.DataSource = ServicesUser.DataTableWorkshop()[0];
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
                            if (SearchCategory() != -1 && SearchWorkshop() != -1)
                                sql = "INSERT INTO public.\"Product\" (\"PRODUCT_NAME\", \"CATEGORY_ID\", \"PRODUCT_PRICE\", \"WORKSHOP_ID\")" +
                                    $"\r\n\tVALUES ('{textBoxPRODUCT_NAME.Text.Trim()}','{SearchCategory()}','{textBoxPRODUCT_PRICE.Text.Trim()}','{SearchWorkshop()}')";
                            else
                            {
                                MessageBox.Show("Нет такой категории или производителя, проверьте пожалуйста корректность введёных данных.", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            break;
                        case "Заказчики":
                            string[] FIO = textBoxCUSTOMER_NAME.Text.Trim().Split();
                            if (FIO.Length == 3)
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
                                $"\r\nWHERE \"CATEGORY_ID\" = {Id}";
                            break;
                        case "Цеха":
                            sql = $"UPDATE public.\"Workshop\" SET \"WORKSHOP_NAME\" = '{textBoxWorkshop.Text}'" +
                                $"\r\nWHERE \"WORKSHOP_ID\" = {Id}";
                            break;
                        case "Товары":
                            if (SearchCategory() != -1 && SearchWorkshop() != -1)
                                sql = $"UPDATE public.\"Product\" SET \"PRODUCT_NAME\" = '{textBoxPRODUCT_NAME.Text}' ,\"CATEGORY_ID\" = '{SearchCategory()}'," +
                                    $"\"PRODUCT_PRICE\" = '{textBoxPRODUCT_PRICE.Text}', \"WORKSHOP_ID\" = '{SearchWorkshop()}'" +
                                   $"\r\nWHERE \"PRODUCT_ID\" = {Id}";
                            else
                            {
                                MessageBox.Show("Нет такой категории или производителя, проверьте пожалуйста корректность введёных данных.", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            break;
                        case "Заказчики":
                            string[] FIO = textBoxCUSTOMER_NAME.Text.Trim().Split();
                            if (FIO.Length == 3)
                                sql = $"UPDATE public.\"Customer\" SET \"CUSTOMER_SURNAME\" = '{FIO[0]}', \"CUSTOMER_NAME\" = '{FIO[1]}', \"CUSTOMER_PATRONYMIC\" = '{FIO[2]}'," +
                                    $"\"CUSTOMER_TELEPHONE\" = '{textBoxCUSTOMER_TELEPHONE.Text}', \"CUSTOMER_ORGANIZATION\" = '{textBoxCUSTOMER_ORGANIZATION.Text}'" +
                                    $"\r\nWHERE \"CUSTOMER_ID\" = {Id}";
                            else
                                sql = $"UPDATE public.\"Customer\" SET \"CUSTOMER_SURNAME\" = '{FIO[0]}', \"CUSTOMER_NAME\" = '{FIO[1]}', \"CUSTOMER_PATRONYMIC\" = ''," +
                                    $"\"CUSTOMER_TELEPHONE\" = '{textBoxCUSTOMER_TELEPHONE.Text}', \"CUSTOMER_ORGANIZATION\" = '{textBoxCUSTOMER_ORGANIZATION.Text}'" +
                                    $"\r\nWHERE \"CUSTOMER_ID\" = {Id}";
                            break;
                        case "Пользователи":
                            FIO = textBoxAccountentName.Text.Trim().Split();
                            switch (FIO.Length)
                            {
                                case 3:
                                    sql = $"UPDATE public.\"Accountant\" SET \"ACCOUNTANT_SURNAME\" = '{FIO[0]}', \"ACCOUNTANT_NAME\" = '{FIO[1]}', \"ACCOUNTANT_PATRONYMIC\" = '{FIO[2]}'," +
                                        $"\"ACCOUNTANT_LOGIN\" = '{textBoxLogin.Text}', \"ACCOUNTANT_PASSWORD\" = '{textBoxPassword.Text}', \"ACCOUNTANT_POSITION\" = '{comboBoxPosition.Text}'" +
                                        $"WHERE \"ACCOUNTANT_ID\" = {Id}";
                                    break;
                                case 2:
                                    sql = $"UPDATE public.\"Accountant\" SET \"ACCOUNTANT_SURNAME\" = '{FIO[0]}', \"ACCOUNTANT_NAME\" = '{FIO[1]}', \"ACCOUNTANT_PATRONYMIC\" = ''," +
                                        $"\"ACCOUNTANT_LOGIN\" = '{textBoxLogin.Text}', \"ACCOUNTANT_PASSWORD\" = '{textBoxPassword.Text}', \"ACCOUNTANT_POSITION\" = '{comboBoxPosition.Text}'" +
                                        $"WHERE \"ACCOUNTANT_ID\" = {Id}";
                                    break;
                                case 1:
                                    sql = $"UPDATE public.\"Accountant\" SET \"ACCOUNTANT_SURNAME\" = '{FIO[0]}', \"ACCOUNTANT_NAME\" = '', \"ACCOUNTANT_PATRONYMIC\" = ''," +
                                        $"\"ACCOUNTANT_LOGIN\" = '{textBoxLogin.Text}', \"ACCOUNTANT_PASSWORD\" = '{textBoxPassword.Text}', \"ACCOUNTANT_POSITION\" = '{comboBoxPosition.Text}'" +
                                        $"WHERE \"ACCOUNTANT_ID\" = {Id}";
                                    break;
                                case 0:
                                    sql = $"UPDATE public.\"Accountant\" SET \"ACCOUNTANT_SURNAME\" = '', \"ACCOUNTANT_NAME\" = '', \"ACCOUNTANT_PATRONYMIC\" = ''," +
                                        $"\"ACCOUNTANT_LOGIN\" = '{textBoxLogin.Text}', \"ACCOUNTANT_PASSWORD\" = '{textBoxPassword.Text}', \"ACCOUNTANT_POSITION\" = '{comboBoxPosition.Text}'" +
                                        $"WHERE \"ACCOUNTANT_ID\" = {Id}";
                                    break;
                            }
                            break;
                    }
                    Program.formMain.toolStripStatusLabel2.Text = "Запись изменена";
                }
                InteractionDataUser.AddUpdateDataOther(sql);
                ServicesUser.ReloadViewBD(Name_tree);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка ввода, проверьте пожалуйста корректность введёных данных. {ex.Message}", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                Program.connection.Close();
            }
        }

        //Доделать выгрузку данных при откртии формы для изменения
        private void DataInForm()
        {
            List<string> list = ServicesUser.DataOther(Name_tree, Id);
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
                    textBoxCUSTOMER_TELEPHONE.Text = list[3];
                    textBoxCUSTOMER_ORGANIZATION.Text = list[4];
                    break;
                case "Пользователи":
                    textBoxAccountentName.Text = $"{list[0]} {list[1]} {list[2]}";
                    textBoxLogin.Text = list[3];
                    textBoxPassword.Text = list[4];
                    comboBoxPosition.Text = list[5];
                    break;
            }
        }

        private int SearchCategory()
        {
            List<string> list = ServicesUser.DataTableCategory()[1];

            string[] data;

            foreach (string row in list)
            {
                data = row.Split("|".ToCharArray());
                string dam = data[1];
                if (dam.Trim() == comboBoxCategory.Text.Trim())
                    return Convert.ToInt32(data[0]);
            }
            return -1;
        }

        private int SearchWorkshop()
        {
            List<string> list = ServicesUser.DataTableWorkshop()[1];

            string[] data;

            foreach (string row in list)
            {
                data = row.Split("|".ToCharArray());
                string dam = data[1];
                if (dam.Trim() == comboBoxWorkshop.Text.Trim())
                    return Convert.ToInt32(data[0]);
            }
            return -1;
        }

        private void textBoxCUSTOMER_NAME_TextChanged(object sender, EventArgs e)
        {
            if (textBoxCUSTOMER_NAME.Text.Trim().Split().Length > 3)
            {
                textBoxCUSTOMER_NAME.MaxLength = textBoxCUSTOMER_NAME.Text.Trim().Length - 2;
                textBoxCUSTOMER_NAME.Text = textBoxCUSTOMER_NAME.Text.Remove(textBoxCUSTOMER_NAME.Text.Length - 2);
                MessageBox.Show("Введите Ф.И.О.", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                textBoxAccountentName.MaxLength = 200;
        }

        private void textBoxAccountentName_TextChanged(object sender, EventArgs e)
        {
            if (textBoxAccountentName.Text.Trim().Split().Length > 3)
            {
                textBoxAccountentName.MaxLength = textBoxAccountentName.Text.Trim().Length - 2;
                textBoxAccountentName.Text = textBoxAccountentName.Text.Remove(textBoxAccountentName.Text.Length - 2);
                MessageBox.Show("Введите Ф.И.О.", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                textBoxAccountentName.MaxLength = 200;
        }

        private void buttonExit_Click(object sender, EventArgs e) => this.Close();
    }
}