using Leticiya.Interaction;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Leticiya
{
    public partial class FormDeletedSearch : MaterialForm
    {
        private readonly InteractionDataAdmin interactionData = new InteractionDataAdmin();
        private string type;

        public FormDeletedSearch(string t)
        {
            InitializeComponent();

            new Thread(() =>
            {
                var materialSkinManager = MaterialSkinManager.Instance;
                materialSkinManager.AddFormToManage(this);
                materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
                materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey300, Primary.Grey900, Primary.Grey200, Accent.LightBlue200, TextShade.BLACK);
            }).Start();

            new Thread(() =>
            {
                type = t;
                switch (type)
                {
                    case "del":
                        this.Text = "Удаление";
                        label1.Text = "Укажите номер в таблицы для удаление данных.";
                        break;
                    case "sea":
                        this.Text = "Поиск";
                        label1.Text = "Укажите данные для поиска.";
                        break;
                }
            }).Start();
        }

        //Обработчик выполнения операции удаления либо поиска
        public void buttonOk_Click(object sender, EventArgs e)
        {
            switch (type)
            {
                case "del":
                    DialogResult result = MessageBox.Show("Вы уверенны что хотите удалить строку данных?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result != DialogResult.No)
                        interactionData.Deleted(this, textBox1);
                    break;
                case "sea":
                    Search();
                    break;
            }
        }

        //Метод формирования запросов к БД для поиска
        private void Search()
        {
            string sql = null;
            /*switch (Program.formMain.comboBox.Text)
            {
                case "Employee":
                    sql = "SELECT * FROM Employee WHERE EMPLOYEE_SURNAME = @EMPLOYEE_SURNAME AND EMPLOYEE_NAME = @EMPLOYEE_NAME AND EMPLOYEE_PATRONYMIC = @EMPLOYEE_PATRONYMIC OR EMPLOYEE_PATRONYMIC IS NULL";
                    break;
                case "Programmer":
                    sql = "SELECT * FROM Programmer WHERE PROGRAMMER_SURNAME = @PROGRAMMER_SURNAME AND PROGRAMMER_NAME = @PROGRAMMER_NAME AND PROGRAMMER_PATRONYMIC = @PROGRAMMER_PATRONYMIC OR PROGRAMMER_PATRONYMIC IS NULL";
                    break;
                case "Task":
                    sql = "SELECT * FROM Task WHERE TASK_NAME = @Name";
                    break;
                case "Departament":
                    sql = "SELECT * FROM Departament WHERE DEPARTAMENT_NAME = @Name";
                    break;
                case "TaskDescription":
                    sql = "SELECT * FROM TaskDescription WHERE TASKDESCRIPTION_ID = @ID";
                    break;
                case "Employee_Task":
                    sql = "SELECT * FROM Employee_Task WHERE EMPLOYEE_TASK_ID = @ID";
                    break;
                case "Task_Programmer":
                    sql = "SELECT * FROM Task_Programmer WHERE TASK_PROGRAMMER_ID = @ID";
                    break;
                case "Autorization":
                    sql = "SELECT * FROM Autorization WHERE LOGIN = @Name";
                    break;
            }*/
            interactionData.Search(sql, textBox1, out bool res);
            if (res == true) this.Close();
        }
    }
}
