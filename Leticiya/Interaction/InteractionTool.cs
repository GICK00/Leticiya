using Npgsql;
using System.Windows.Forms;

namespace Leticiya.Interaction
{
    class InteractionTool
    {
        private readonly ServicesAdmin servicesAdmin = new ServicesAdmin();

        // Создает полную резерную копию всей БД.
        public void buttonCreateCopy()
        {
            NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder(Tools.connSrring);

            string path = ServicesAdmin.saveFileDialogBack.FileName;
            string sql = $@"BACKUP DATABASE[{builder["Database"]}] TO DISK = N'{path}' WITH NOFORMAT, NOINIT, NAME = N'{builder["Database"]}-Полная База данных Резервное копирование', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
            Tools.PanelLoad(sql, "back");
        }

        // Восстанавливает БД из выбранной резервной копии.
        public void buttonReestablish()
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите востановить базу данных?", "Восстановление базы данных.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;
            if (ServicesAdmin.openFileDialogRes.ShowDialog() == DialogResult.Cancel)
                return;

            NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder(Tools.connSrring);

            string path = ServicesAdmin.openFileDialogRes.FileName;
            string sql = $@"USE master RESTORE DATABASE [{builder["Database"]}] FROM  DISK = N'{path}' WITH REPLACE, FILE = 1,  NOUNLOAD,  STATS = 5";
            Tools.PanelLoad(sql, "res");
        }

        // Очищает все таблицы БД от данных кроме таблицы Autorization (она необходима для авторизации в приложении). 
        public void buttonClearBD()
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите очистить базу данных?", "Удаление данных.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;
            using (NpgsqlCommand sqlCommand = new NpgsqlCommand("DeletedAll", Program.connection))
            {
                Program.connection.Open();
                sqlCommand.ExecuteNonQuery();
                Program.connection.Close();
            }
            servicesAdmin.ReloadEditingBD(Program.formMain.comboBox.Text);
            servicesAdmin.ClearStr();
            Program.formMain.toolStripStatusLabel2.Text = "База данных очищенна";
        }
    }
}
