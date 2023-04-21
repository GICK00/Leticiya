using Leticiya.Interaction;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Path = System.IO.Path;

namespace Leticiya
{
    public partial class FormMain : MaterialForm
    {
        public static readonly MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
        private readonly InteractionDataAdmin interactionDataAdmin = new InteractionDataAdmin();
        private readonly InteractionDataUser interactionDataUser = new InteractionDataUser();
        private readonly InteractionTool interactionTool = new InteractionTool();
        private readonly ServicesAdmin servicesAdmin = new ServicesAdmin();
        private readonly ServicesUser servicesUser = new ServicesUser();
        private readonly Tools tools = new Tools();

        private readonly FormRequest formRequest = new FormRequest();
        private readonly FormInfo formInfo = new FormInfo();
        private readonly FormLogin formLogin = new FormLogin();
        private readonly FormSettings formSettings = new FormSettings();

        public static string treeViewItemSelect = null;

        public static bool flagSelectAdmin = false;
        public static bool flagSelectUser = false;
        public static int AdminGridSelect = 0;
        public static int UserGridSelect = 0;

        public FormMain()
        {
            Program.formMain = this;

            InitializeComponent();

            new Thread(() =>
            {
                materialSkinManager.AddFormToManage(this);
                materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
                materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey300, Primary.Grey900, Primary.Grey200, Accent.LightBlue200, TextShade.BLACK);
            }).Start();

            ServicesAdmin.saveFileDialogBack.Filter = "Bak files(*.bak)|*.bak";
            ExcelClass.saveFileDialogExp.Filter = "Excel files(*.xlsx)|*.xlsx";
            ServicesAdmin.openFileDialogSQL.Filter = "Sql files(*.sql)|*.sql| Text files(*.txt)|*.txt| All files(*.*)|*.*";
            ServicesAdmin.openFileDialogRes.Filter = "Bak files(*.bak)|*.bak";

            servicesAdmin.Visibl();

            dataGridViewAdmin.Enabled = false;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (tools.CheckConfig() != true)
                return;

            string[] settings = File.ReadAllLines($"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\\config.ini");
            Regex regex = new Regex(@"UpdateApp=True");
            if (regex.IsMatch(settings[1]))
            {
                new Thread(() =>
                {
                    tools.UpdateApp();
                }).Start();
            }

            Tools.PanelLoad("", "open");
            if (tools.Test() != true)
                return;
            toolStripStatusLabel2.Text = "Готово к работе";

            string path = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\\cahce";
            if (File.Exists(path) != false)
            {
                Tools.Autorization(Tools.AutorizationCache());
                if (FormLogin.Position != null)
                    tools.VisiblAtAutorization();
            }
        }

        //
        //Кнопки формы на page для admin
        //

        //Обработчик проверки на привилегии учетной записи для переключения tabControl в режим для адимна (режим полного доступа над БД)
        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tools.LoginAdmin() != true)
                e.Cancel = true;
        }

        //Обработчик обрабатывающий переключение таблиц в comboBox для дальнейшей работы с ними
        private void comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (tools.Test() != true)
                return;
            servicesAdmin.ClearStr();
            servicesAdmin.ReloadEditingBD(comboBox.Text);
            servicesAdmin.comboBoxFilter(comboBox.Text);
            servicesAdmin.Visibl();
        }

        //Обработчик добавления информации для выбранной таблицы в comboBox
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (tools.Test() != true)
                return;
            interactionDataAdmin.AddAndUpdate("Add");
        }

        //Обработчик изменения выбранной информации для выбранной таблицы в comboBox
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (tools.Test() != true)
                return;
            interactionDataAdmin.AddAndUpdate("Update");
        }

        //Обработчик поиска информации в выбранной таблицы в comboBox
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (tools.Test() != true)
                return;
            FormDeletedSearch formDeletedAndSearch = new FormDeletedSearch("sea");
            formDeletedAndSearch.ShowDialog();
        }

        //Обработчик удаления информации в выбранной таблицы в comboBox
        private void buttonDeleted_Click(object sender, EventArgs e)
        {
            if (tools.Test() != true)
                return;

            if (flagSelectAdmin == false)
            {
                MessageBox.Show("Выберете, что нужно изменить", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            interactionDataAdmin.Deleted(AdminGridSelect);
        }

        //Обработчик обновления информации в dataGriedView (выгрузка информации из таблиц БД в dataGriedView)
        private void buttonReload_Click(object sender, EventArgs e)
        {
            if (tools.Test() != true)
                return;
            servicesAdmin.ReloadEditingBD(comboBox.Text);
        }

        //Обработчик очищения всех полей имеющихся на tabControl
        public void buttonClearStr_Click(object sender, EventArgs e) => servicesAdmin.ClearStr();

        //Обработчик фильтрации информации в таблице
        private void buttonFilter_Click(object sender, EventArgs e)
        {
            if (tools.Test() != true)
                return;
            servicesAdmin.Filter(comboBox.Text, comboBoxFilter.Text);
        }

        //Кнопки переключения между страницами для admin
        private void buttonPrevPage2_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBoxCoutPageAdmin.Text) > 1)
                textBoxCoutPageAdmin.Text = (Convert.ToInt32(textBoxCoutPageAdmin.Text) - 1).ToString();
        }

        private void buttonNextPage2_Click(object sender, EventArgs e) => textBoxCoutPageAdmin.Text = (Convert.ToInt32(textBoxCoutPageAdmin.Text) + 1).ToString();

        private void textBoxCoutPageAdmin_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBoxCoutPageAdmin.Text) >= 1)
                servicesAdmin.ReloadEditingBD(comboBox.Text);
        }

        //
        //ToolStrip
        //

        //Обработчик переподключения к БД
        private void buttonReconnection_Click(object sender, EventArgs e)
        {
            if (tools.CheckConfig() != true)
                return;
            Tools.PanelLoad("", "open");
            if (Program.SQLStat != false)
            {
                MessageBox.Show("Подключение к базе данных установленно", "Проверка подключения", MessageBoxButtons.OK);
                toolStripStatusLabel2.Text = "Готово к работе";

                if (FormLogin.Position != null)
                {
                    servicesAdmin.DataTableAdmin();
                    servicesAdmin.Visibl();
                    servicesAdmin.ReloadEditingBD(comboBox.Text);
                }
            }
            else
            {
                toolStripStatusLabel2.Text = $"Ошибка подключения к базе данных!";
                MessageBox.Show("Ошибка подключения к базе данных!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Обработчик открытия формы с информацией о приложени
        private void buttonInfo_Click(object sender, EventArgs e) => formInfo.ShowDialog();

        //Обработчик проверки версии приложения (верся риложения находится на GitHub)
        private void buttonUpdateApp_Click(object sender, EventArgs e) => new Thread(() => tools.UpdateApp()).Start();

        //Обработчик открывающая форму авторизации
        private void buttonAuthorization_Click(object sender, EventArgs e)
        {
            if (tools.CheckConfig() != true || tools.Test() != true)
                return;
            formLogin.ShowDialog();
        }

        //Обработчик выхода, полностью сбрасывает приложени (запрещает все функции) и производит выход из учетной записи
        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormLogin.Position != null)
            {
                tabControl.SelectTab(tabPage2);
                FormLogin.Position = null;
                this.Text = "Мебельная фабрика Leticiya";
                materialSkinManager.AddFormToManage(this);

                servicesAdmin.Visibl();

                comboBox.DataSource = null;
                comboBoxFilter.DataSource = null;
                treeView.Enabled = false;

                dataGridViewAdmin.Enabled = false;
                dataGridViewAdmin.DataSource = null;

                dataGridViewAdmin.ClearSelection();
                FormMain.flagSelectAdmin = false;

                dataGridViewUser.Enabled = false;
                dataGridViewUser.DataSource = null;

                dataGridViewUser.ClearSelection();
                FormMain.flagSelectUser = false;

                toolStripStatusLabel2.Text = "Произведен выход из системы";
                string path = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\\cahce";
                File.Delete(path);
            }
            else
                MessageBox.Show("Не выполнен вход в систему!", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //Обработчик открывающий форму настроек БД (в ней можно прописать строку подключения к БД)
        private void toolStripButtonSettings_Click(object sender, EventArgs e) => formSettings.ShowDialog();

        //Обработчик открывает форму в которой можно выполнять запросы к БД (открывать существующие файлы или писать запрос в онне)
        private void buttonRunRequest_Click(object sender, EventArgs e)
        {
            if (tools.CheckConfig() != true || tools.Test() != true)
                return;
            FormMain.flagSelectAdmin = false;
            AdminGridSelect = 0;
            dataGridViewAdmin.ClearSelection();
            formRequest.ShowDialog();
        }

        //Обработчик создания полной резервной копии БД
        private void buttonCreateCopy_Click(object sender, EventArgs e)
        {
            ServicesAdmin.saveFileDialogBack.FileName = "Leticiya.bak";
            if (tools.CheckConfig() != true || tools.Test() != true || ServicesAdmin.saveFileDialogBack.ShowDialog() == DialogResult.Cancel)
                return;
            interactionTool.buttonCreateCopy();
        }

        //Обработчик восстановления резервной копии БД
        private void buttonReestablish_Click(object sender, EventArgs e)
        {
            if (tools.CheckConfig() != true || tools.Test() != true)
                return;
            interactionTool.buttonReestablish();
        }

        //Обработчик очищающий строки данных во всех таблицах БД
        private void buttonClearBD_Click(object sender, EventArgs e)
        {
            if (tools.CheckConfig() != true || tools.Test() != true)
                return;
            FormMain.flagSelectAdmin = false;
            AdminGridSelect = 0;
            dataGridViewAdmin.ClearSelection();
            interactionTool.buttonClearBD();
        }

        //Обработчик для создания накладной на заказ
        private void CreateInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tools.LoginGuest() != true)
                return;
            if (treeViewItemSelect != "Заказы")
            {
                MessageBox.Show("Откройте раздел \"Заказы\" и выберети заказ для создания накладной.", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            if (flagSelectUser == false)
            {
                MessageBox.Show("Выберете заказ на который нужно создать накладную.", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ExcelClass.saveFileDialogExp.FileName = $"Накладаная_№{UserGridSelect}_{DateTime.Now.ToString("dd.MM.yyyy")}.xlsx";
            if (tools.Test() != true || ExcelClass.saveFileDialogExp.ShowDialog() == DialogResult.Cancel)
                return;
            Tools.PanelLoad(UserGridSelect.ToString(), "excel");
        }

        //
        //DataGridView1 на page для admin
        //

        //Обработчик обрабатывающий двойное нажатие на строку в dataGridView на tabControl для админов
        private void dataGridViewAdmin_MouseDoubleClick(object sender, EventArgs e)
        {
            servicesAdmin.TextViewTextBox(servicesAdmin.ArrayUpdate());
            FormMain.flagSelectAdmin = true;

            AdminGridSelect = dataGridViewAdmin.CurrentRow.Index + 1;

            toolStripStatusLabel2.Text = $"Выбрана строка № {dataGridViewAdmin.CurrentRow.Index + 1}";
        }

        //
        //Кнопки формы на page для user
        //

        //Обработчик выбора вкладки на TreeView
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            treeViewItemSelect = e.Node.Text;
            servicesUser.ReloadViewBD(treeViewItemSelect);
            UserGridSelect = 0;
            flagSelectUser = false;
            toolStripStatusLabel2.Text = $"Выбран раздел {treeViewItemSelect}";

        }

        //Обработчки добавления
        private void buttonAddUser_Click(object sender, EventArgs e)
        {
            if (tools.LoginGuest() != true)
                return;

            if (treeViewItemSelect == "Заказы")
            {
                FormAddEditOrder formAddEditDel = new FormAddEditOrder("add");
                formAddEditDel.ShowDialog();
            }
            else
            {
                FormAddEditOther formAddEditDelOther = new FormAddEditOther(treeViewItemSelect, "add");
                formAddEditDelOther.ShowDialog();
            }
        }

        //Обработчик изменения 
        private void buttonEditUser_Click(object sender, EventArgs e)
        {
            if (tools.LoginGuest() != true)
                return;

            if (flagSelectUser == false)
            {
                MessageBox.Show("Выберете, что нужно изменить", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (treeViewItemSelect == "Заказы")
            {
                FormAddEditOrder formAddEditDel = new FormAddEditOrder("edit");
                formAddEditDel.ShowDialog();
            }
            else
            {
                FormAddEditOther formAddEditDelOther = new FormAddEditOther(treeViewItemSelect, "edit");
                formAddEditDelOther.ShowDialog();
            }
        }

        //Обработчик удаления
        private void buttonDelUser_Click(object sender, EventArgs e)
        {
            if (tools.LoginGuest() != true)
                return;

            if (flagSelectUser == false)
            {
                MessageBox.Show("Выберете, что нужно удалить", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string sql = null;
            switch (treeViewItemSelect)
            {
                case "Заказы":
                    sql = "DELETE FROM \"Order_Product\"" +
                    $"\r\nWHERE \"ORDER_ID\" = {UserGridSelect};" +
                    "\r\nDELETE FROM \"Order\" " +
                    $"\r\nWHERE \"ORDER_ID\" = {UserGridSelect};";
                    break;
                case "Категории":
                    sql = "DELETE FROM \"Category\"" +
                    $"\r\nWHERE \"CATEGORY_ID\" = {UserGridSelect};";
                    break;
                case "Цеха":
                    sql = "DELETE FROM \"Workshop\"" +
                    $"\r\nWHERE \"WORKSHOP_ID\" = {UserGridSelect};";
                    break;
                case "Товары":
                    sql = "DELETE FROM \"Product\"" +
                    $"\r\nWHERE \"PRODUCT_ID\" = {UserGridSelect};";
                    break;
                case "Заказчики":
                    sql = "DELETE FROM \"Customer\"" +
                    $"\r\nWHERE \"CUSTOMER_ID\" = {UserGridSelect};";
                    break;
                case "Пользователи":
                    sql = "DELETE FROM \"Accountant\"" +
                    $"\r\nWHERE \"ACCOUNTANT_ID\" = {UserGridSelect};";
                    break;

            }

            interactionDataUser.Deleted(sql);
        }

        //Кнопки переключения между страницами для user
        private void buttonNextpage_Click(object sender, EventArgs e) => textBoxCoutPage.Text = (Convert.ToInt32(textBoxCoutPage.Text) + 1).ToString();

        private void buttonPrevPage_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBoxCoutPage.Text) > 1)
                textBoxCoutPage.Text = (Convert.ToInt32(textBoxCoutPage.Text) - 1).ToString();
        }

        private void textBoxCoutPage_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBoxCoutPage.Text) >= 1)
                servicesUser.ReloadViewBD(treeViewItemSelect);
        }

        //
        //DataGriedView на page для user
        //

        //Обработчик обрабатывающий двойное нажатие на строку в dataGridView на tabControl для админов
        private void dataGridViewUser_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FormMain.flagSelectUser = true;

            UserGridSelect = (int)dataGridViewUser.CurrentRow.Cells[0].Value;

            toolStripStatusLabel2.Text = $"Выбрана строка № {dataGridViewUser.CurrentRow.Cells[0].Value}";
        }

        //
        //Настройки формы
        //

        //Обработчик вызова диалогового окна при закрытие главной формы приложения
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите закрыть приложение?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                e.Cancel = true;
        }

        //Оптимизация отображения таблиц при изменение размеров окна
        private void FormMain_ResizeBegin(object sender, EventArgs e)
        {
            dataGridViewAdmin.Visible = false;
            dataGridViewUser.Visible = false;
        }

        private void FormMain_ResizeEnd(object sender, EventArgs e)
        {
            dataGridViewAdmin.Visible = true;
            dataGridViewUser.Visible = true;
        }
    }
}