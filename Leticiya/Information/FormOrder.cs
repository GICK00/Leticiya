using Leticiya.Interaction;
using MaterialSkin;
using MaterialSkin.Controls;
using Npgsql;
using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Leticiya
{
    public partial class FormOrder : MaterialForm
    {
        private readonly ServicesAdmin servicesAdmin = new ServicesAdmin();

        public FormOrder()
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
    }
}
