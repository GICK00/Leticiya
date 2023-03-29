using Leticiya.Interaction;
using MaterialSkin;
using MaterialSkin.Controls;
using Npgsql;
using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Leticiya
{
    public partial class FormRequest : MaterialForm
    {
        private readonly ServicesAdmin servicesAdmin = new ServicesAdmin();

        public FormRequest()
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

        //Обработчик очистки строки статуса
        private void FormRequest_Load(object sender, EventArgs e) => toolStripStatusLabel2.Text = "";

        //Обработчик открывающий файл с sql запаросом для его выполнения к БД
        private void toolStripButtonOpenSQL_Click(object sender, EventArgs e)
        {
            if (ServicesAdmin.openFileDialogSQL.ShowDialog() == DialogResult.Cancel)
                return;
            try
            {
                string filename = ServicesAdmin.openFileDialogSQL.FileName;
                string sql = System.IO.File.ReadAllText(filename, Encoding.GetEncoding(1251));
                textBoxSQLReader.Text = sql;
            }
            catch (Exception ex)
            {
                toolStripStatusLabel2.Text = $"Запрос не выполнен! Ошибка {ex.Message}";
                Program.formMain.toolStripStatusLabel2.Text = $"Запрос не выполнен! Ошибка {ex.Message}";
            }
            finally
            {
                Program.connection.Close();
            }
        }

        //Обработчик выполнения запроса написанного в textBox
        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                using (NpgsqlCommand sqlCommand = new NpgsqlCommand(textBoxSQLReader.Text, Program.connection))
                {
                    Program.connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
                toolStripStatusLabel2.Text = "Запрос выполнен";
                Program.formMain.toolStripStatusLabel2.Text = "Запрос выполнен";
            }
            catch (Exception ex)
            {
                toolStripStatusLabel2.Text = $"Ошибка! {ex.Message}";
                Program.formMain.toolStripStatusLabel2.Text = $"Ошибка! {ex.Message}";
            }
            finally
            {
                Program.connection.Close();
                servicesAdmin.ReloadEditingBD(Program.formMain.comboBox.Text);
            }
        }

        //Обработчик закрытия формы
        private void buttonExit_Click(object sender, EventArgs e) => this.Close();
    }
}