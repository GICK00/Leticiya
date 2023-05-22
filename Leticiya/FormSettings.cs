using Leticiya.Interaction;
using MaterialSkin;
using MaterialSkin.Controls;
using Npgsql;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Leticiya
{
    public partial class FormSettings : MaterialForm
    {
        private readonly string path = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\\config.ini";

        public FormSettings()
        {
            InitializeComponent();

            new Thread(() =>
            {
                var materialSkinManager = MaterialSkinManager.Instance;
                materialSkinManager.AddFormToManage(this);
                materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
                materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey300, Primary.Grey900, Primary.Grey200, Accent.LightBlue200, TextShade.BLACK);
            }).Start();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            //Проверка файла конфигурации на наличи
            if (Tools.CheckConfig() != true)
            {
                this.Close();
                return;
            }

            string[] settings = File.ReadAllLines(path);
            NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder(Tools.connSrring);

            //Выгрузка даннх из файла конфигурации в textBox на форме настроек
            textBox1.Text = builder["Host"].ToString();
            textBox5.Text = builder["Port"].ToString();
            textBox4.Text = builder["Database"].ToString();
            textBox2.Text = builder["Username"].ToString();
            textBox3.Text = builder["Password"].ToString();
            textBox6.Text = builder["CancellationTimeout"].ToString();
            Regex regex = new Regex(@"UpdateApp=True");
            if (regex.IsMatch(settings[1]))
                checkBoxUpdate.Checked = true;
            else
                checkBoxUpdate.Checked = false;
        }

        //Обработчик сохранения введеных данных в файл конфигурации 
        private void buttonSaves_Click(object sender, EventArgs e)
        {
            if (Tools.CheckConfig() != true)
                return;
            try
            {
                NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder(Tools.connSrring)
                {
                    ["Host"] = textBox1.Text.Trim(),
                    ["Port"] = textBox5.Text.Trim(),
                    ["Database"] = textBox4.Text.Trim(),
                    ["Username"] = textBox2.Text.Trim(),
                    ["Password"] = textBox3.Text.Trim(),
                    ["CancellationTimeout"] = textBox6.Text.Trim()
                };

                string settings = builder.ConnectionString + "\r\nUpdateApp=" + checkBoxUpdate.Checked;
                File.WriteAllText(path, settings);

                DialogResult dialogResult = MessageBox.Show("Параметры успешно сохранены.", "Настройки", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.OK)
                    this.Close();
            }
            catch (NpgsqlException)
            {
                MessageBox.Show("Неверно введены данные!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex.Message}", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Обработчик вызова дополнительной программы (программа работы с БД, ее подключение и отсоединение от сервера)
        private void buttonSettingsExe_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("Settings.exe");
            }
            catch
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