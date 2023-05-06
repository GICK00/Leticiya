
namespace Leticiya
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Заказы");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Категории");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Цеха");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Товары");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Заказчики");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Пользователи");
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.buttonAuthorization = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.buttonInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonUpdateApp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonReconnectionBD = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSettings = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.CreateInvoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStripGriedViewUser = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripButtonViewFullOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonPrevPage = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.textBoxCoutPage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonNextpage = new System.Windows.Forms.Button();
            this.dataGridViewUser = new System.Windows.Forms.DataGridView();
            this.buttonAddUser = new System.Windows.Forms.Button();
            this.treeView = new System.Windows.Forms.TreeView();
            this.buttonEditUser = new System.Windows.Forms.Button();
            this.buttonDelUser = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStripGriedViewUser.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUser)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.toolStripDropDownButton2,
            this.toolStripSeparator3,
            this.toolStripLabel4,
            this.toolStripDropDownButton3,
            this.toolStripLabel1,
            this.buttonReconnectionBD,
            this.toolStripLabel3,
            this.toolStripSeparator2,
            this.toolStripLabel5,
            this.toolStripButtonSettings,
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 55);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(1237, 27);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(13, 24);
            this.toolStripLabel2.Text = "  ";
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonAuthorization,
            this.buttonExit});
            this.toolStripDropDownButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(96, 24);
            this.toolStripDropDownButton2.Text = "Авторизация";
            // 
            // buttonAuthorization
            // 
            this.buttonAuthorization.Name = "buttonAuthorization";
            this.buttonAuthorization.Size = new System.Drawing.Size(225, 22);
            this.buttonAuthorization.Text = "Войти под своим логином";
            this.buttonAuthorization.Click += new System.EventHandler(this.buttonAuthorization_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(225, 22);
            this.buttonExit.Text = "Выйти";
            this.buttonExit.Click += new System.EventHandler(this.выйтиToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(22, 24);
            this.toolStripLabel4.Text = "     ";
            // 
            // toolStripDropDownButton3
            // 
            this.toolStripDropDownButton3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripDropDownButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonInfo,
            this.buttonUpdateApp});
            this.toolStripDropDownButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripDropDownButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton3.Image")));
            this.toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton3.Name = "toolStripDropDownButton3";
            this.toolStripDropDownButton3.Size = new System.Drawing.Size(69, 24);
            this.toolStripDropDownButton3.Text = "Справка";
            // 
            // buttonInfo
            // 
            this.buttonInfo.BackColor = System.Drawing.SystemColors.Control;
            this.buttonInfo.Name = "buttonInfo";
            this.buttonInfo.Size = new System.Drawing.Size(263, 22);
            this.buttonInfo.Text = "О приложении";
            this.buttonInfo.Click += new System.EventHandler(this.buttonInfo_Click);
            // 
            // buttonUpdateApp
            // 
            this.buttonUpdateApp.Name = "buttonUpdateApp";
            this.buttonUpdateApp.Size = new System.Drawing.Size(263, 22);
            this.buttonUpdateApp.Text = "Проверить наличие обновлений";
            this.buttonUpdateApp.Click += new System.EventHandler(this.buttonUpdateApp_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(6, 27);
            // 
            // buttonReconnectionBD
            // 
            this.buttonReconnectionBD.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.buttonReconnectionBD.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonReconnectionBD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonReconnectionBD.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonReconnectionBD.Name = "buttonReconnectionBD";
            this.buttonReconnectionBD.Size = new System.Drawing.Size(156, 24);
            this.buttonReconnectionBD.Text = "Проверить подключение";
            this.buttonReconnectionBD.Click += new System.EventHandler(this.buttonReconnection_Click);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(0, 24);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(0, 24);
            this.toolStripSeparator2.Visible = false;
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButtonSettings
            // 
            this.toolStripButtonSettings.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSettings.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSettings.Image")));
            this.toolStripButtonSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSettings.Name = "toolStripButtonSettings";
            this.toolStripButtonSettings.Size = new System.Drawing.Size(80, 24);
            this.toolStripButtonSettings.Text = "Параметры";
            this.toolStripButtonSettings.Click += new System.EventHandler(this.toolStripButtonSettings_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateInvoiceToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(86, 24);
            this.toolStripDropDownButton1.Text = "Документы";
            // 
            // CreateInvoiceToolStripMenuItem
            // 
            this.CreateInvoiceToolStripMenuItem.Name = "CreateInvoiceToolStripMenuItem";
            this.CreateInvoiceToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.CreateInvoiceToolStripMenuItem.Text = "Создать накладную на заказ";
            this.CreateInvoiceToolStripMenuItem.Click += new System.EventHandler(this.CreateInvoiceToolStripMenuItem_Click);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(50, 19);
            this.toolStripStatusLabel1.Text = "Статус:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.statusStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(4, 673);
            this.statusStrip1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1230, 24);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 19);
            // 
            // contextMenuStripGriedViewUser
            // 
            this.contextMenuStripGriedViewUser.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonViewFullOrder});
            this.contextMenuStripGriedViewUser.Name = "contextMenuStrip1";
            this.contextMenuStripGriedViewUser.Size = new System.Drawing.Size(132, 26);
            this.contextMenuStripGriedViewUser.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripGriedViewUser_Opening);
            // 
            // toolStripButtonViewFullOrder
            // 
            this.toolStripButtonViewFullOrder.Name = "toolStripButtonViewFullOrder";
            this.toolStripButtonViewFullOrder.Size = new System.Drawing.Size(131, 22);
            this.toolStripButtonViewFullOrder.Text = "Подробно";
            this.toolStripButtonViewFullOrder.Click += new System.EventHandler(this.toolStripButtonViewFullOrder_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(54, 618);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 90;
            this.label4.Text = "Страница";
            // 
            // buttonPrevPage
            // 
            this.buttonPrevPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPrevPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonPrevPage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonPrevPage.FlatAppearance.BorderSize = 0;
            this.buttonPrevPage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.buttonPrevPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.buttonPrevPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPrevPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonPrevPage.Location = new System.Drawing.Point(28, 639);
            this.buttonPrevPage.Margin = new System.Windows.Forms.Padding(0);
            this.buttonPrevPage.Name = "buttonPrevPage";
            this.buttonPrevPage.Size = new System.Drawing.Size(23, 23);
            this.buttonPrevPage.TabIndex = 86;
            this.buttonPrevPage.Text = "<";
            this.buttonPrevPage.UseVisualStyleBackColor = false;
            this.buttonPrevPage.Click += new System.EventHandler(this.buttonPrevPage_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(20, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 24);
            this.label2.TabIndex = 87;
            this.label2.Text = "Ресурсы";
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel5.Controls.Add(this.textBoxCoutPage);
            this.panel5.Location = new System.Drawing.Point(54, 639);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(55, 23);
            this.panel5.TabIndex = 89;
            // 
            // textBoxCoutPage
            // 
            this.textBoxCoutPage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCoutPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBoxCoutPage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxCoutPage.Location = new System.Drawing.Point(4, 5);
            this.textBoxCoutPage.Name = "textBoxCoutPage";
            this.textBoxCoutPage.ReadOnly = true;
            this.textBoxCoutPage.Size = new System.Drawing.Size(48, 13);
            this.textBoxCoutPage.TabIndex = 50;
            this.textBoxCoutPage.Text = "1";
            this.textBoxCoutPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxCoutPage.TextChanged += new System.EventHandler(this.textBoxCoutPage_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(174, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 24);
            this.label1.TabIndex = 85;
            this.label1.Text = "Заказы";
            // 
            // buttonNextpage
            // 
            this.buttonNextpage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonNextpage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonNextpage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonNextpage.FlatAppearance.BorderSize = 0;
            this.buttonNextpage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.buttonNextpage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.buttonNextpage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNextpage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonNextpage.Location = new System.Drawing.Point(112, 639);
            this.buttonNextpage.Margin = new System.Windows.Forms.Padding(0);
            this.buttonNextpage.Name = "buttonNextpage";
            this.buttonNextpage.Size = new System.Drawing.Size(23, 23);
            this.buttonNextpage.TabIndex = 88;
            this.buttonNextpage.Text = ">";
            this.buttonNextpage.UseVisualStyleBackColor = false;
            this.buttonNextpage.Click += new System.EventHandler(this.buttonNextpage_Click);
            // 
            // dataGridViewUser
            // 
            this.dataGridViewUser.AllowUserToAddRows = false;
            this.dataGridViewUser.AllowUserToDeleteRows = false;
            this.dataGridViewUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewUser.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewUser.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridViewUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewUser.CausesValidation = false;
            this.dataGridViewUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUser.ContextMenuStrip = this.contextMenuStripGriedViewUser;
            this.dataGridViewUser.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.dataGridViewUser.Enabled = false;
            this.dataGridViewUser.EnableHeadersVisualStyles = false;
            this.dataGridViewUser.Location = new System.Drawing.Point(177, 122);
            this.dataGridViewUser.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridViewUser.MultiSelect = false;
            this.dataGridViewUser.Name = "dataGridViewUser";
            this.dataGridViewUser.ReadOnly = true;
            this.dataGridViewUser.RowHeadersVisible = false;
            this.dataGridViewUser.RowHeadersWidth = 20;
            this.dataGridViewUser.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewUser.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewUser.Size = new System.Drawing.Size(1052, 540);
            this.dataGridViewUser.TabIndex = 81;
            this.dataGridViewUser.TabStop = false;
            this.dataGridViewUser.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridViewUser_MouseDoubleClick);
            // 
            // buttonAddUser
            // 
            this.buttonAddUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonAddUser.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonAddUser.FlatAppearance.BorderSize = 0;
            this.buttonAddUser.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.buttonAddUser.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.buttonAddUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAddUser.Location = new System.Drawing.Point(907, 91);
            this.buttonAddUser.Margin = new System.Windows.Forms.Padding(0);
            this.buttonAddUser.Name = "buttonAddUser";
            this.buttonAddUser.Size = new System.Drawing.Size(103, 23);
            this.buttonAddUser.TabIndex = 84;
            this.buttonAddUser.Text = "Добавить";
            this.buttonAddUser.UseVisualStyleBackColor = false;
            this.buttonAddUser.Click += new System.EventHandler(this.buttonAddUser_Click);
            // 
            // treeView
            // 
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView.Enabled = false;
            this.treeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.treeView.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.treeView.ItemHeight = 20;
            this.treeView.Location = new System.Drawing.Point(19, 122);
            this.treeView.Name = "treeView";
            treeNode1.Name = "Заказы";
            treeNode1.Text = "Заказы";
            treeNode2.Name = "Категории";
            treeNode2.Text = "Категории";
            treeNode3.Name = "Цеха";
            treeNode3.Text = "Цеха";
            treeNode4.Name = "Товары";
            treeNode4.Text = "Товары";
            treeNode5.Name = "Заказчики";
            treeNode5.Text = "Заказчики";
            treeNode6.Name = "Пользователи";
            treeNode6.Text = "Пользователи";
            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6});
            this.treeView.Scrollable = false;
            this.treeView.Size = new System.Drawing.Size(132, 122);
            this.treeView.TabIndex = 80;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // buttonEditUser
            // 
            this.buttonEditUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonEditUser.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonEditUser.FlatAppearance.BorderSize = 0;
            this.buttonEditUser.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.buttonEditUser.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.buttonEditUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEditUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonEditUser.Location = new System.Drawing.Point(1017, 91);
            this.buttonEditUser.Margin = new System.Windows.Forms.Padding(0);
            this.buttonEditUser.Name = "buttonEditUser";
            this.buttonEditUser.Size = new System.Drawing.Size(103, 23);
            this.buttonEditUser.TabIndex = 83;
            this.buttonEditUser.Text = "Изменить";
            this.buttonEditUser.UseVisualStyleBackColor = false;
            this.buttonEditUser.Click += new System.EventHandler(this.buttonEditUser_Click);
            // 
            // buttonDelUser
            // 
            this.buttonDelUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonDelUser.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonDelUser.FlatAppearance.BorderSize = 0;
            this.buttonDelUser.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.buttonDelUser.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.buttonDelUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDelUser.Location = new System.Drawing.Point(1126, 91);
            this.buttonDelUser.Margin = new System.Windows.Forms.Padding(0);
            this.buttonDelUser.Name = "buttonDelUser";
            this.buttonDelUser.Size = new System.Drawing.Size(103, 23);
            this.buttonDelUser.TabIndex = 82;
            this.buttonDelUser.Text = "Удалить";
            this.buttonDelUser.UseVisualStyleBackColor = false;
            this.buttonDelUser.Click += new System.EventHandler(this.buttonDelUser_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(1237, 700);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonPrevPage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonNextpage);
            this.Controls.Add(this.dataGridViewUser);
            this.Controls.Add(this.buttonAddUser);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.buttonEditUser);
            this.Controls.Add(this.buttonDelUser);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MinimumSize = new System.Drawing.Size(1237, 700);
            this.Name = "FormMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Мебельная фабрика Leticiya";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResizeBegin += new System.EventHandler(this.FormMain_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.FormMain_ResizeEnd);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStripGriedViewUser.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonReconnectionBD;
        private System.Windows.Forms.ToolStripSeparator toolStripLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem buttonAuthorization;
        private System.Windows.Forms.ToolStripMenuItem buttonExit;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton3;
        private System.Windows.Forms.ToolStripMenuItem buttonInfo;
        private System.Windows.Forms.ToolStripMenuItem buttonUpdateApp;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripLabel5;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem CreateInvoiceToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripGriedViewUser;
        private System.Windows.Forms.ToolStripMenuItem toolStripButtonViewFullOrder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonPrevPage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel5;
        public System.Windows.Forms.TextBox textBoxCoutPage;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonNextpage;
        public System.Windows.Forms.DataGridView dataGridViewUser;
        private System.Windows.Forms.Button buttonAddUser;
        public System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.Button buttonEditUser;
        private System.Windows.Forms.Button buttonDelUser;
    }
}