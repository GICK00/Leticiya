using Npgsql;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace Leticiya.Interaction
{
    internal class Tools
    {
        private readonly WebClient client = new WebClient();
        private readonly ServicesAdmin servicesAdmin = new ServicesAdmin();

        private static StreamReader streamReader;
        public static string connSrring;

        //Метод проверки поля login пользователя на соответсвие логину гостя
        public bool LoginGuest()
        {
            if (FormLogin.Position != null)
            {
                if (FormLogin.Position[0] == "user")
                    return true;
                return true;
            }
            MessageBox.Show("Вы не вошли в систему!\r\nВойдите в систему во вкладке \"Авторизация\".", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        //Метод проверки поля login пользователя на соответсвие логину администратора
        //Администратор исеет все права для взаимодействия с БД через приложение
        public bool LoginAdmin()
        {
            if (FormLogin.Position != null)
            {
                if (FormLogin.Position[0] == "admin")
                    return true;
                MessageBox.Show("Вы не являетесь Администратором!", "Ошибка доступа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            MessageBox.Show("Вы не вошли в систему!\r\nВойдите в систему во вкладке \"Авторизация\".", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }



        //Функция проверки поля SQLStat на bool значение для вывода соответвующих уведомлений для пользоватля
        public bool Test()
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
        public bool CheckConfig()
        {
            string path = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\\config.ini";
            if (File.Exists(path) != true)
            {
                MessageBox.Show("Файл конфигурации отсуствует! Будет создан новый файл шаблон в корне программы.", "Критическая ошибка конфигурации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                File.Create(path).Close();
                File.WriteAllText(path, $"Host=localhost;Port=5432;Database=;Username= ; Password= ; Cancellation Timeout=2000\r\nUpdateApp=False");
                streamReader = new StreamReader(path);
                connSrring = streamReader.ReadLine();
                streamReader.Close();
                Program.connection = new NpgsqlConnection(connSrring);
                Program.formMain.toolStripStatusLabel2.Text = "Критическая ошибка конфигурации!";
                return false;
            }
            else
            {
                //Сделать обработку исключения при файле с неверной конфигурацией
                streamReader = new StreamReader(path);
                connSrring = streamReader.ReadLine();
                streamReader.Close();
                Program.connection = new NpgsqlConnection(connSrring);
                return true;
            }
        }

        //Метод Выполняет загрузку текстового файла Vеr.txt находящегося на GitHub
        public void UpdateApp()
        {
            try
            {
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
                    {
                        System.Diagnostics.Process.Start("https://github.com/GICK00/Leticiya");
                        Environment.Exit(0);
                    }
                }
            }
            catch (Exception ex)
            {
                Program.formMain.toolStripStatusLabel2.Text = $"Ошибка проверки обновлений! {ex.Message}";
            }
        }

        //Метод проверающий привелегии введенного пользователя
        public static string[] Autorization(string sql)
        {
            try
            {
                using (NpgsqlCommand sqlCommand = new NpgsqlCommand(sql, Program.connection))
                {
                    Program.connection.Open();
                    using (NpgsqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        reader.Read();
                        FormLogin.Position = (reader["ACCOUNTANT_POSITION"].ToString().Trim() + " " + reader["ACCOUNTANT_SURNAME"].ToString().Trim() + " " + reader["ACCOUNTANT_NAME"].ToString().Trim() + " " + reader["ACCOUNTANT_PATRONYMIC"].ToString().Trim()).Split();
                        reader.Close();
                    }
                }
                return FormLogin.Position;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return FormLogin.Position = null;
            }
            finally
            {
                Program.connection.Close();
            }
        }

        public static string AutorizationCache()
        {
            string path = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\\cahce";
            string data;
            using (StreamReader reader = new StreamReader(File.Open(path, FileMode.Open)))
            {
                data = reader.ReadToEnd();
            }

            string[] mas = Encrypt.DecryptText(data, Program.key).Split();
            return $"SELECT \"ACCOUNTANT_SURNAME\", \"ACCOUNTANT_NAME\", \"ACCOUNTANT_PATRONYMIC\", \"ACCOUNTANT_POSITION\" FROM public.\"Accountant\" WHERE \"ACCOUNTANT_LOGIN\" = '{mas[0]}' AND \"ACCOUNTANT_PASSWORD\" = '{mas[1]}'";
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

        public void VisiblAtAutorization()
        {
            if (FormLogin.Position[0] == "admin")
            {
                servicesAdmin.DataTableAdmin();
                Program.formMain.dataGridViewAdmin.Enabled = true;
                servicesAdmin.Visibl();
                servicesAdmin.ReloadEditingBD(Program.formMain.comboBox.Text);

                Program.formMain.treeView.Enabled = true;
                Program.formMain.dataGridViewUser.Enabled = true;
            }
            else
            {
                if (FormLogin.Position[0] == "user")
                {
                    Program.formMain.treeView.Enabled = true;
                    Program.formMain.dataGridViewUser.Enabled = true;
                }
            }

            Program.formMain.toolStripStatusLabel2.Text = "Произведен вход с правами " + FormLogin.Position[0];
            Program.formMain.Text = "Мебельная фабрика Leticiya - " + FormLogin.Position[0] + " " + FormLogin.Position[1] + " " + FormLogin.Position[2] + " " + FormLogin.Position[3];
        }
    }
}
