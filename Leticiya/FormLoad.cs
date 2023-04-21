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
                Action action = () =>
                {
                    switch (typeLoad)
                    {
                        case "res":
                            this.label1.Text = "Восстановление...";
                            break;
                        case "back":
                            this.label1.Text = "Создание резерной копии...";
                            break;
                        case "excel":
                            this.label1.Text = "Создание документа...";
                            break;
                    }
                };
                if (InvokeRequired)
                    Invoke(action);
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
            switch (typeLoad)
            {
                case "open":
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
                    break;
                case "res":
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
                    break;
                case "back":
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
                    break;
                case "excel":
                    await Task.Run(() =>
                    {
                        excelClass.ExpExcel(Convert.ToInt32(sqlLoad));
                        Task.Delay(300);
                    });
                    progressBar.Value += 50;
                    await Task.Delay(500);
                    Program.formMain.toolStripStatusLabel2.Text = $"Накладная сохранена {ExcelClass.saveFileDialogExp.FileName} ";
                    break;
            }
            this.Close();
        }
    }
}