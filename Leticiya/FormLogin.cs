using Leticiya.Interaction;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Leticiya
{
    public partial class FormLogin : MaterialForm
    {
        private readonly ServicesAdmin servicesAdmin = new ServicesAdmin();
        private readonly ServicesUser servicesUser = new ServicesUser();


        public static string Login;

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
            if (Login != null)
            {
                MessageBox.Show("Вы уже вошли в систему!", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            else
            {
                if (textBox1.Text != "" && textBox2.Text != "")
                {

                    Login = null;
                    string sql = $"SELECT POSITION FROM Autorization WHERE LOGIN = '{textBox1.Text}' AND PASSWORD = '{textBox2.Text}'";

                    if (Login != Tools.Autorization(sql))
                    {
                        if (Login == "admin")
                        {
                            servicesAdmin.DataTableAdmin();
                            Program.formMain.dataGridViewAdmin.Enabled = true;
                            servicesAdmin.Visibl();
                            servicesAdmin.ReloadEditingBD(Program.formMain.comboBox.Text);

                            string path = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\\cahce";
                            if (File.Exists(path) != true)
                                File.Create(path).Close();

                            using (StreamWriter writer = new StreamWriter(File.Open(path, FileMode.Create)))
                            {
                                writer.Write(Encrypt.EncryptText(textBox1.Text.Trim() + " " + textBox2.Text.Trim(), Program.key));
                            }
                        }
                        else
                        {

                        }

                        Program.formMain.toolStripStatusLabel2.Text = "Произведен вход под логином " + textBox1.Text;
                        Program.formMain.Text = "Мебельная фабрика Leticiya - " + textBox1.Text;
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