using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Leticiya
{
    public partial class FormAddEditDelOther : MaterialForm
    {
        private string type;
        public FormAddEditDelOther(string name, string type)
        {
            InitializeComponent();

            this.type = type;
            this.Text = name;

            new Thread(() =>
            {
                var materialSkinManager = MaterialSkinManager.Instance;
                materialSkinManager.AddFormToManage(this);
                materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
                materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey300, Primary.Grey900, Primary.Grey200, Accent.LightBlue200, TextShade.BLACK);
            }).Start();

            if (type != "add")
            {
                button1Add.Visible = false;
                button1Add.Enabled = false;
                buttonEdit.Visible = true;
                buttonEdit.Enabled = true;
            }

            switch (this.Text)
            {
                case "Категории":
                    panelCategory.Visible = true;
                    panelCategory.Enabled = true;
                    break;
                case "Цеха":
                    panelWorkshop.Visible = true;
                    panelWorkshop.Enabled = true;
                    break;
                case "Товары":
                    panelProduct.Visible = true;
                    panelProduct.Enabled = true;
                    break;
                case "Заказчики":
                    panelCustomer.Visible = true;
                    panelCustomer.Enabled = true;
                    break;
                case "Пользователи":
                    panelAccountant.Visible = true;
                    panelAccountant.Enabled = true;
                    break;
            }
        }

        private void FormAddEditDelOther_Load(object sender, EventArgs e)
        {
            
        }

        private void buttonExit_Click(object sender, EventArgs e) => this.Close();
    }
}