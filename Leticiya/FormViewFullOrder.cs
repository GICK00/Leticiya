using MaterialSkin;
using MaterialSkin.Controls;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace Leticiya
{
    public partial class FormViewFullOrder : MaterialForm
    {
        private int order_id;
        public FormViewFullOrder(int order_id)
        {
            InitializeComponent();

            this.order_id = order_id;
            this.Text += $" №{order_id}";

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