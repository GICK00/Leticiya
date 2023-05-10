using Npgsql;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace Leticiya.Interaction
{
    internal static class Tools
    {
        private static StreamReader streamReader;
        public static string connSrring;

        //Метод вызова панели загрузки
        public static void PanelLoad(string sql, string type)
        {
            FormLoad formLoad = new FormLoad(sql, type);
            formLoad.progressBar.Value = 0;
            formLoad.ShowDialog();
        }

        //Функция проверки поля SQLStat на bool значение для вывода соответвующих уведомлений для пользоватля
        public static bool TestConnect()
        {
            if (Program.SQLStat != true)
            {
                Program.formMain.toolStripStatusLabel2.Text = $"Ошибка подключения к базе данных!";
                MessageBox.Show("Ошибка подключения к базе данных!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        //Функиця проверки файл конфигурации на доступность. Если файл не найден то создается новый файл,
        //в который записывается стандартный тип записи данного файла конфигурации
        public static bool CheckConfig()
        {
            string path = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\\config.ini";
            if (File.Exists(path) != true)
            {
                MessageBox.Show("Файл конфигурации отсуствует! Будет создан новый файл шаблон в корне программы.", "Критическая ошибка конфигурации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                File.Create(path).Close();
                File.WriteAllText(path, $"Host=localhost;Port=5432;Database=;Username=;Password=;Cancellation Timeout=2000\r\nUpdateApp=False");
                streamReader = new StreamReader(path);
                connSrring = streamReader.ReadLine();
                streamReader.Close();
                Program.connection = new NpgsqlConnection(connSrring);
                Program.formMain.toolStripStatusLabel2.Text = "Критическая ошибка конфигурации!";
                return false;
            }
            else
            {
                try
                {
                    //Сделать обработку исключения при файле с неверной конфигурацией
                    streamReader = new StreamReader(path);
                    connSrring = streamReader.ReadLine();
                    streamReader.Close();
                    Program.connection = new NpgsqlConnection(connSrring);
                    return true;
                }
                catch
                {
                    File.Delete(path);
                    MessageBox.Show("Файл конфигурации заполнен не верно, файл будет удален.", "Критическая ошибка конфигурации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        //Метод Выполняет загрузку текстового файла Vеr.txt находящегося на GitHub
        public static void UpdateApp()
        {
            try
            {
                WebClient client = new WebClient();
                Uri uri = new Uri("https://github.com/GICK00/Leticiya/blob/master/Ver.txt");
                if (client.DownloadString(uri).Contains(Program.ver))
                {
                    Program.formMain.toolStripStatusLabel2.Text = $"Устновленна послденяя версия приложения {Program.ver}";
                    return;
                }
                else
                {
                    string text = $"Доступна новая версия приложения.\r\nВаша текущая версия {Program.ver}. \r\nОбновить программу?";
                    DialogResult dialogResult = MessageBox.Show(text, "Достуно новое обновление", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                        System.Diagnostics.Process.Start("https://github.com/GICK00/Leticiya/releases/tag/Ver-Beta-2.0.0-L");
                }
            }
            catch (Exception ex)
            {
                Program.formMain.toolStripStatusLabel2.Text = $"Ошибка проверки обновлений! {ex.Message}";
            }
        }

        public static void SaveCache(string login, string password)
        {
            string path = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\\cahce";
            if (File.Exists(path) != true)
                File.Create(path).Close();

            using (StreamWriter writer = new StreamWriter(File.Open(path, FileMode.Create)))
            {
                writer.Write(Encrypt.EncryptText(login.Trim() + " " + password.Trim(), Program.key));
            }
        }
    }
}
