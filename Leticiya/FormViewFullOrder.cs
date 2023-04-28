using Leticiya.Class;
using Leticiya.Interaction;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Collections.Generic;
using System.Threading;

namespace Leticiya
{
    public partial class FormViewFullOrder : MaterialForm
    {
        private readonly ServicesUser servicesUser = new ServicesUser();
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

        private void FormViewFullOrder_Load(object sender, System.EventArgs e)
        {
            DataInForm();
        }

        private void DataInForm()
        {
            Order order = servicesUser.FullDataOrder(order_id);
            labelStatus.Text = order.Status;
            labelDataOrder.Text = order.DataOrder;
            labelFIOorgan.Text = $"{order.customer.Surname} {order.customer.Name} {order.customer.Patronymic} {order.customer.Organization}".Trim();
            labelTelepfone.Text = order.customer.Telephone;
            labelAddres.Text = order.Address;
            labelDataUpload.Text = order.DataDelevery;
            labelPriceDeliv.Text = order.DeleveryPrice.ToString();
            labelPriceOrder.Text = order.OrderPrice().ToString();
            labelComment.Text = order.Comment;

            List<string>[] orderList = servicesUser.DataOrderProduct(order_id);
            for (int i = 0; i < orderList.Length; i++)
                dataGridViewProduct.Rows.Add(orderList[i][4], orderList[i][1], orderList[i][0], orderList[i][2], orderList[i][3]);
        }
    }
}