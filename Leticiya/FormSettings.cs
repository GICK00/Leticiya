using MaterialSkin;
using MaterialSkin.Controls;
using Leticiya.Interaction;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Leticiya
{
    public partial class FormSettings : MaterialForm
    {
        private readonly Tools tools = new Tools();

        public FormSettings()
        {
            InitializeComponent();

            new Thread(() =>
            {
                Action action = () =>
                {
                    var materialSkinManager = MaterialSkinManager.Instance;
                    materialSkinManager.AddFormToManage(this);
                    materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
                    materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey300, Primary.Grey900, Primary.Grey200, Accent.LightBlue200, TextShade.BLACK);
                };
                if (InvokeRequired)
                    Invoke(action);
                else
                    action();
            }).Start();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            //Проверка файла конфигурации на наличи
            tools.CheckConfig();

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(Tools.connSrring);

            //Выгрузка даннх из файла конфигурации в textBox на форме настроек
            textBox1.Text = builder["Server"].ToString();
            textBox4.Text = builder["Initial Catalog"].ToString();
            comboBox.Text = builder["Integrated Security"].ToString();
            textBox6.Text = builder["Connect Timeout"].ToString();
        }

        //Обработчик сохранения введеных данных в файл конфигурации 
        private void buttonSaves_Click(object sender, EventArgs e)
        {
            if (tools.CheckConfig() != true)
                return;

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(Tools.connSrring);

            builder["Server"] = textBox1.Text.Trim();
            builder["Initial Catalog"] = textBox4.Text.Trim();
            builder["Integrated Security"] = comboBox.Text.Trim();
            builder["Connect Timeout"] = textBox6.Text.Trim();

            File.WriteAllText(Tools.path, builder.ConnectionString);

            DialogResult dialogResult = MessageBox.Show("Параметры успешно сохранены.", "Настройки", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.OK)
                this.Close();
        }

        //Обработчик вызова дополнительной программы (программа работы с БД, ее подключение и отсоединение от сервера)
        private void buttonSettingsExe_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("Settings.exe");
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка зауска службы!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Обработчик запрещающий вводить все символы кроме цифр
        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }
    }
}