using Leticiya.Interaction;
using Npgsql;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Leticiya
{
    public partial class FormLoad : Form
    {
        private readonly ServicesAdmin servicesAdmin = new ServicesAdmin();
        private readonly ExcelClass excelClass = new ExcelClass();

        private string sqlLoad;
        private string typeLoad;

        public FormLoad(string sql, string type)
        {
            InitializeComponent();

            new Thread(() =>
            {
                sqlLoad = sql;
                typeLoad = type;
                if (typeLoad == "res")
                    this.label1.Text = "Восстановление...";
                else if (typeLoad == "back")
                    this.label1.Text = "Создание резерной копии...";
                else if (typeLoad == "update")
                    this.label1.Text = "Проверка версии...";
            }).Start();
        }

        private void FormLoad_Load(object sender, EventArgs e)
        {
            Selection();
            progressBar.Value += 50;
        }

        //Метод выпоолняющий функции в зависимости от задачи (в том числе и для красивой загрузки)
        private async void Selection()
        {
            if (sqlLoad == null)
            {
                await Task.Run(() =>
                {
                    try
                    {
                        Program.connection.Open();
                        Program.SQLStat = true;
                    }
                    catch
                    {
                        Program.SQLStat = false;
                    }
                    finally
                    {
                        Task.Delay(1000);
                        Program.connection.Close();
                    }
                });
                progressBar.Value += 50;
                await Task.Delay(500);
            }
            else if (typeLoad == "res")
            {
                await Task.Run(() =>
                {
                    try
                    {
                        using (NpgsqlCommand sqlCommand = new NpgsqlCommand(sqlLoad, Program.connection))
                        {
                            Program.connection.Open();
                            sqlCommand.ExecuteNonQuery();
                        }
                        Task.Delay(500);
                        Program.formMain.toolStripStatusLabel2.Text = "База данных успешно востановленна";
                    }
                    catch (Exception ex)
                    {
                        Program.formMain.toolStripStatusLabel2.Text = $"Ошибка востановления базы данных! {ex.Message}";
                    }
                    finally
                    {
                        Program.connection.Close();
                    }
                });
                progressBar.Value += 50;
                servicesAdmin.ReloadEditingBD(Program.formMain.comboBox.Text);
                await Task.Delay(500);
            }
            else if (typeLoad == "back")
            {
                await Task.Run(() =>
                {
                    try
                    {
                        using (NpgsqlCommand sqlCommand = new NpgsqlCommand(sqlLoad, Program.connection))
                        {
                            Program.connection.Open();
                            sqlCommand.ExecuteNonQuery();
                        }
                        Task.Delay(500);
                        Program.formMain.toolStripStatusLabel2.Text = "Резерная копия успешно создана и сохранена";
                    }
                    catch (Exception ex)
                    {
                        Program.formMain.toolStripStatusLabel2.Text = $"Ошибка сохранения резервной копии базы данных! {ex.Message}";
                    }
                    finally
                    {
                        Program.connection.Close();
                    }
                });
                progressBar.Value += 50;
                await Task.Delay(500);
            }
            this.Close();
        }
    }
}