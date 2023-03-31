using Leticiya.Interaction;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Leticiya
{
    public partial class FormLogin : MaterialForm
    {
        private readonly ServicesAdmin servicesAdmin = new ServicesAdmin();
        private readonly ServicesUser servicesUser = new ServicesUser();
        private readonly Tools tools = new Tools();


        public static string[] Position;

        public FormLogin()
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

        //Обработчик авторизации
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (Position != null)
            {
                MessageBox.Show("Вы уже вошли в систему!", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            else
            {
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    Position = null;
                    string sql = $"SELECT \"ACCOUNTANT_SURNAME\", \"ACCOUNTANT_NAME\", \"ACCOUNTANT_PATRONYMIC\", \"ACCOUNTANT_POSITION\" FROM public.\"Accountant\" WHERE \"ACCOUNTANT_LOGIN\" = '{textBox1.Text}' AND \"ACCOUNTANT_PASSWORD\" = '{textBox2.Text}'";
                    if (Position != Tools.Autorization(sql))
                    {
                        tools.VisiblAtAutorization();
                        Tools.SaveCache(textBox1.Text, textBox2.Text);

                        FormMain.materialSkinManager.AddFormToManage(this);
                        textBox1.Clear();
                        textBox2.Clear();
                        this.Close();
                    }
                    else
                        MessageBox.Show("Нет пользователя с таким логином и паролем!", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Введите логин и пароль!", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}