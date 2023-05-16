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

        private readonly FormInfo formInfo = new FormInfo();
        private readonly FormLogin formLogin = new FormLogin();
        private readonly FormSettings formSettings = new FormSettings();

        public static string treeViewItemSelect = null;

        private static bool flagSelectUser = false;
        private static int UserGridSelect = 0;

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

            ExcelClass.saveFileDialogExp.Filter = "Excel files(*.xlsx)|*.xlsx";
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (Tools.CheckConfig() != true)
                return;

            string[] settings = File.ReadAllLines($"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\\config.ini");
            Regex regex = new Regex(@"UpdateApp=True");
            if (regex.IsMatch(settings[1]))
            {
                new Thread(() =>
                {
                    Tools.UpdateApp();
                }).Start();
            }

            Tools.PanelLoad("", "open");
            if (Tools.TestConnect() != true)
                return;
            toolStripStatusLabel2.Text = "Готово к работе";

            string path = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\\cahce";
            if (File.Exists(path) != false)
            {
                ServicesAutorization.Autorization(ServicesAutorization.AutorizationCache());
                if (ServicesAutorization.Position != null)
                    ServicesAutorization.VisiblAtAutorization();
            }
        }

        //
        //ToolStrip
        //

        //Обработчик переподключения к БД
        private void buttonReconnection_Click(object sender, EventArgs e)
        {
            if (Tools.CheckConfig() != true)
                return;
            Tools.PanelLoad("", "open");
            if (Program.SQLStat != false)
            {
                MessageBox.Show("Подключение к базе данных установленно", "Проверка подключения", MessageBoxButtons.OK);
                toolStripStatusLabel2.Text = "Готово к работе";
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
        private void buttonUpdateApp_Click(object sender, EventArgs e) => new Thread(() => Tools.UpdateApp()).Start();

        //Обработчик открывающая форму авторизации
        private void buttonAuthorization_Click(object sender, EventArgs e)
        {
            if (Tools.CheckConfig() != true || Tools.TestConnect() != true)
                return;
            formLogin.ShowDialog();
        }

        //Обработчик выхода, полностью сбрасывает приложени (запрещает все функции) и производит выход из учетной записи
        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ServicesAutorization.Position != null)
            {
                ServicesAutorization.Position = null;
                this.Text = "Мебельная фабрика Leticiya";
                materialSkinManager.AddFormToManage(this);
                treeView.Enabled = false;

                dataGridViewUser.Enabled = false;
                dataGridViewUser.DataSource = null;

                dataGridViewUser.ClearSelection();
                flagSelectUser = false;

                toolStripStatusLabel2.Text = "Произведен выход из системы";
                string path = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\\cahce";
                File.Delete(path);
            }
            else
                MessageBox.Show("Не выполнен вход в систему!", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //Обработчик открывающий форму настроек БД (в ней можно прописать строку подключения к БД)
        private void toolStripButtonSettings_Click(object sender, EventArgs e) => formSettings.ShowDialog();

        //Обработчик для создания накладной на заказ
        private void CreateInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ServicesAutorization.LoginGuest() != true)
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

            ExcelClass.saveFileDialogExp.FileName = $"Накладаная_№{UserGridSelect}_{DateTime.Now:dd.MM.yyyy}.xlsx";
            if (Tools.TestConnect() != true || ExcelClass.saveFileDialogExp.ShowDialog() == DialogResult.Cancel)
                return;
            Tools.PanelLoad(UserGridSelect.ToString(), "excel");
        }

        //Обработчик выбора вкладки на TreeView
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            treeViewItemSelect = e.Node.Text;
            ServicesUser.ReloadViewBD(treeViewItemSelect);
            UserGridSelect = 0;
            flagSelectUser = false;
            toolStripStatusLabel2.Text = $"Выбран раздел {treeViewItemSelect}";
        }

        //Обработчки добавления
        private void buttonAddUser_Click(object sender, EventArgs e)
        {
            if (ServicesAutorization.LoginGuest() != true)
                return;
            if (treeViewItemSelect == "Пользователи")
                if (ServicesAutorization.LoginAdmin() != true)
                    return;

            if (treeViewItemSelect == "Заказы")
            {
                FormAddEditOrder formAddEditDel = new FormAddEditOrder("add", 0);
                formAddEditDel.ShowDialog();
            }
            else
            {
                FormAddEditOther formAddEditDelOther = new FormAddEditOther("add", treeViewItemSelect, 0);
                formAddEditDelOther.ShowDialog();
            }
            flagSelectUser = false;
        }

        //Обработчик изменения 
        private void buttonEditUser_Click(object sender, EventArgs e)
        {
            if (ServicesAutorization.LoginGuest() != true)
                return;
            if (treeViewItemSelect == "Пользователи")
                if (ServicesAutorization.LoginAdmin() != true)
                    return;

            if (flagSelectUser == false)
            {
                MessageBox.Show("Выберете, что нужно изменить", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (treeViewItemSelect == "Заказы")
            {
                FormAddEditOrder formAddEditDel = new FormAddEditOrder("edit", UserGridSelect);
                formAddEditDel.ShowDialog();
            }
            else
            {
                FormAddEditOther formAddEditDelOther = new FormAddEditOther("edit", treeViewItemSelect, UserGridSelect);
                formAddEditDelOther.ShowDialog();
            }
            flagSelectUser = false;
        }

        //Обработчик удаления
        private void buttonDelUser_Click(object sender, EventArgs e)
        {
            if (ServicesAutorization.LoginGuest() != true)
                return;
            if (treeViewItemSelect == "Пользователи")
                if (ServicesAutorization.LoginAdmin() != true)
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
            InteractionDataUser.Deleted(sql);
            flagSelectUser = false;
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
                ServicesUser.ReloadViewBD(treeViewItemSelect);
        }

        //
        //DataGriedView на page для user
        //

        //Обработчик обрабатывающий двойное нажатие на строку в dataGridView на tabControl для админов
        private void dataGridViewUser_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridViewUser.CurrentRow == null)
                return;
            FormMain.flagSelectUser = true;
            UserGridSelect = (int)dataGridViewUser.CurrentRow.Cells[0].Value;
            toolStripStatusLabel2.Text = $"Выбрана строка № {dataGridViewUser.CurrentRow.Cells[0].Value}";
        }

        //Контекстное меня для DataGriedViewUser для раздела Заказы
        private void toolStripButtonViewFullOrder_Click(object sender, EventArgs e)
        {
            if (flagSelectUser == false)
            {
                MessageBox.Show("Выберете заказ для просмотра.", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            FormViewFullOrder formViewFullOrder = new FormViewFullOrder(UserGridSelect);
            formViewFullOrder.Show();
        }

        private void contextMenuStripGriedViewUser_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (treeViewItemSelect != "Заказы")
                e.Cancel = true;
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
        private void FormMain_ResizeBegin(object sender, EventArgs e) => dataGridViewUser.DataSource = null;

        private void FormMain_ResizeEnd(object sender, EventArgs e)
        {
            if (ServicesAutorization.Position == null)
                return;
            ServicesUser.ReloadViewBD(treeViewItemSelect);
        }
    }
}