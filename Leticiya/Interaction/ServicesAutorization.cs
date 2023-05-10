using Npgsql;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Leticiya.Interaction
{
    internal static class ServicesAutorization
    {
        public static string[] Position { get; set; }

        //Метод проверки поля login пользователя на соответсвие логину гостя
        public static bool LoginGuest()
        {
            if (Position != null)
            {
                if (Position[0] == "user")
                    return true;
                return true;
            }
            MessageBox.Show("Вы не вошли в систему!\r\nВойдите в систему во вкладке \"Авторизация\".", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        //Метод проверки поля login пользователя на соответсвие логину администратора
        //Администратор исеет все права для взаимодействия с БД через приложение
        public static bool LoginAdmin()
        {
            if (Position != null)
            {
                if (Position[0] == "admin")
                    return true;
                MessageBox.Show("Вы не являетесь Администратором!", "Ошибка доступа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            MessageBox.Show("Вы не вошли в систему!\r\nВойдите в систему во вкладке \"Авторизация\".", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
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
                        Position = (reader["ACCOUNTANT_POSITION"].ToString().Trim() + " " + reader["ACCOUNTANT_SURNAME"].ToString().Trim() + " " + reader["ACCOUNTANT_NAME"].ToString().Trim() + " " + reader["ACCOUNTANT_PATRONYMIC"].ToString().Trim()).Split();
                        reader.Close();
                    }
                }
                return Position;
            }
            catch
            {
                return Position = null;
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


        public static void VisiblAtAutorization()
        {
            if (Position[0] == "admin")
            {
                Program.formMain.treeView.Enabled = true;
                Program.formMain.dataGridViewUser.Enabled = true;
                Program.formMain.treeView.Select();
                ServicesUser.ReloadViewBD(FormMain.treeViewItemSelect);
            }
            else
            {
                if (Position[0] == "user")
                {
                    Program.formMain.treeView.Enabled = true;
                    Program.formMain.dataGridViewUser.Enabled = true;
                    Program.formMain.treeView.Select();
                    ServicesUser.ReloadViewBD(FormMain.treeViewItemSelect);
                }
            }

            Program.formMain.toolStripStatusLabel2.Text = "Произведен вход с правами " + Position[0];
            Program.formMain.Text = "Мебельная фабрика Leticiya - " + Position[0] + " " + Position[1] + " " + Position[2] + " " + Position[3];
        }
    }
}
