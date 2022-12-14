using System;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace Leticiya.Interaction
{
    internal class Tools
    {
        private readonly WebClient client = new WebClient();

        public static string path;
        private static StreamReader streamReader;
        public static string connSrring;

        //Метод проверки поля login пользователя на соответсвие логину гостя
        public bool LoginGuest()
        {
            if (FormLogin.Login != null)
            {
                if (FormLogin.Login == "user")
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
            if (FormLogin.Login != null)
            {
                if (FormLogin.Login == "admin")
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
            if (FormMain.SQLStat != true)
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
            path = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\\config.ini";
            if (File.Exists(path) != true)
            {
                MessageBox.Show("Файл конфигурации отсуствует! Будет создан новый файл шаблон в корне программы.", "Критическая ошибка конфигурации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                File.Create(path).Close();
                File.WriteAllText(path, $"Data Source= ;Initial Catalog= ;Integrated Security= ;Connect Timeout= ");
                streamReader = new StreamReader(path);
                connSrring = streamReader.ReadToEnd();
                streamReader.Close();
                FormMain.connection = new SqlConnection(connSrring);
                Program.formMain.toolStripStatusLabel2.Text = "Критическая ошибка конфигурации!";
                return false;
            }
            else
            {
                //Сделать обработку исключения при файле с неверной конфигурацией
                streamReader = new StreamReader(path);
                connSrring = streamReader.ReadToEnd();
                streamReader.Close();
                FormMain.connection = new SqlConnection(connSrring);
                return true;
            }
        }

        //Метод Выполняет загрузку текстового файла Var.txt находящегося на GitHub
        public void UpdateApp()
        {
            try
            {
                Uri uri = new Uri("https://github.com/GICK00/Leticiya/blob/main/Ver.txt");
                if (client.DownloadString(uri).Contains(FormMain.ver))
                {
                    Program.formMain.toolStripStatusLabel2.Text = $"Устновленна послденяя версия приложения {FormMain.ver}";
                    return;
                }
                else
                {
                    string text = $"Доступна новая версия приложения.\r\nВаша текущая версия {FormMain.ver}. \r\nОбновить программу?";
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
    }
}
