using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WAQOOD.Properties;

namespace WAQOOD
{
	public class MainForm : Form
	{
		public static string str_conn = "Data Source=10.250.2.4,1433;Database=Waqood_db;User ID=sa;Password=Ncc@Hsa@Yemen@123";

		private SqlCommand mCommand = null;

		private static SqlDataAdapter mAdatpter = null;

		private static SqlConnection mConnection = new SqlConnection(MainForm.str_conn);

		private DataTable dtOrder;

		private IContainer components = null;

		private MenuStrip menuStrip1;

		private ToolStripMenuItem الترميزاتToolStripMenuItem;

		private ToolStripMenuItem الشركاتToolStripMenuItem;

		private ToolStripMenuItem الساراتToolStripMenuItem;

		private ToolStripMenuItem المحطاتToolStripMenuItem;

		private ToolStripMenuItem المستخدمينToolStripMenuItem;

		private ToolStripMenuItem الصلاحياتToolStripMenuItem;

		private ToolStripMenuItem ملفالحركةToolStripMenuItem;

		private ToolStripMenuItem صرفمحروقاتToolStripMenuItem;

		private ToolStripMenuItem التقاريرToolStripMenuItem;

		private ToolStripMenuItem تقريرالصرفوقودToolStripMenuItem;

		private ToolStripMenuItem تقريرالصرفشركاتToolStripMenuItem;

		private ToolStripMenuItem الاسعارToolStripMenuItem;

		private ToolStripMenuItem تغييركلمةالمرورToolStripMenuItem;

		private ToolStripMenuItem صلاحياتالمحطاتToolStripMenuItem;

		private ToolStripMenuItem العدادتToolStripMenuItem;

		private ToolStripMenuItem طلبوقودتثبيتToolStripMenuItem;

		private ToolStripMenuItem صرفالمحروقاتمحطةToolStripMenuItem;

		private ToolStripMenuItem طلبوقودجنوبترحيلToolStripMenuItem;

		private ToolStripMenuItem تقريرالطلباتشركاتToolStripMenuItem;

		private TableLayoutPanel tableLayoutPanel1;

		private Panel panel1;

		private Panel panel2;

		private PictureBox pictureBox3;

		private PictureBox pictureBox1;

		private PictureBox pictureBox2;

		private Label label1;

		private Panel panel3;

		private Label label_bra_name;

		public MainForm()
		{
			this.InitializeComponent();
			base.WindowState = FormWindowState.Maximized;
			MainForm.mConnection = new SqlConnection(MainForm.str_conn);
			this.dtOrder = new DataTable();
			this.label_bra_name.Text = LogonForm.braname + " - " + LogonForm.userName;
		}

		private void المستخدمينToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UsersForm usersForm = new UsersForm();
			usersForm.ShowDialog();
		}

		private void الشركاتToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CompanyForm companyForm = new CompanyForm();
			companyForm.ShowDialog();
		}

		private void الساراتToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CarCodingForm carCodingForm = new CarCodingForm();
			carCodingForm.ShowDialog();
		}

		private void المحطاتToolStripMenuItem_Click(object sender, EventArgs e)
		{
			StationForm stationForm = new StationForm();
			stationForm.ShowDialog();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
		}

		private void صرفمحروقاتToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TransForm transForm = new TransForm();
			transForm.ShowDialog();
		}

		private void الاسعارToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PrciceForm prciceForm = new PrciceForm();
			prciceForm.ShowDialog();
		}

		private void صلاحياتالمحطاتToolStripMenuItem_Click(object sender, EventArgs e)
		{
			StaPrivForm staPrivForm = new StaPrivForm();
			staPrivForm.ShowDialog();
		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Environment.Exit(0);
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Environment.Exit(0);
		}

		private void صرفالمحروقاتمحطةToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MobileLogForm mobileLogForm = new MobileLogForm();
			mobileLogForm.ShowDialog();
		}

		private void طلبوقودتثبيتToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ConfirmForm confirmForm = new ConfirmForm();
			confirmForm.ShowDialog();
		}

		private void طلبوقودجنوبترحيلToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SouthTransForm southTransForm = new SouthTransForm();
			southTransForm.ShowDialog();
		}

		private void تقريرالصرفوقودToolStripMenuItem_Click(object sender, EventArgs e)
		{
			WaqoodReportForm waqoodReportForm = new WaqoodReportForm();
			waqoodReportForm.ShowDialog();
		}

		private void تقريرالصرفشركاتToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CompReportForm compReportForm = new CompReportForm();
			compReportForm.ShowDialog();
		}

		private void تقريرالطلباتشركاتToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CompRepOrderForm compRepOrderForm = new CompRepOrderForm();
			compRepOrderForm.ShowDialog();
		}

		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(MainForm));
			this.menuStrip1 = new MenuStrip();
			this.الترميزاتToolStripMenuItem = new ToolStripMenuItem();
			this.الشركاتToolStripMenuItem = new ToolStripMenuItem();
			this.الاسعارToolStripMenuItem = new ToolStripMenuItem();
			this.المحطاتToolStripMenuItem = new ToolStripMenuItem();
			this.الساراتToolStripMenuItem = new ToolStripMenuItem();
			this.صلاحياتالمحطاتToolStripMenuItem = new ToolStripMenuItem();
			this.المستخدمينToolStripMenuItem = new ToolStripMenuItem();
			this.الصلاحياتToolStripMenuItem = new ToolStripMenuItem();
			this.العدادتToolStripMenuItem = new ToolStripMenuItem();
			this.ملفالحركةToolStripMenuItem = new ToolStripMenuItem();
			this.صرفمحروقاتToolStripMenuItem = new ToolStripMenuItem();
			this.طلبوقودجنوبترحيلToolStripMenuItem = new ToolStripMenuItem();
			this.طلبوقودتثبيتToolStripMenuItem = new ToolStripMenuItem();
			this.التقاريرToolStripMenuItem = new ToolStripMenuItem();
			this.تقريرالصرفوقودToolStripMenuItem = new ToolStripMenuItem();
			this.تقريرالصرفشركاتToolStripMenuItem = new ToolStripMenuItem();
			this.تقريرالطلباتشركاتToolStripMenuItem = new ToolStripMenuItem();
			this.تغييركلمةالمرورToolStripMenuItem = new ToolStripMenuItem();
			this.صرفالمحروقاتمحطةToolStripMenuItem = new ToolStripMenuItem();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.panel1 = new Panel();
			this.label1 = new Label();
			this.panel2 = new Panel();
			this.pictureBox3 = new PictureBox();
			this.pictureBox1 = new PictureBox();
			this.pictureBox2 = new PictureBox();
			this.panel3 = new Panel();
			this.label_bra_name = new Label();
			this.menuStrip1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			((ISupportInitialize)this.pictureBox3).BeginInit();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			((ISupportInitialize)this.pictureBox2).BeginInit();
			this.panel3.SuspendLayout();
			base.SuspendLayout();
			this.menuStrip1.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.menuStrip1.GripStyle = ToolStripGripStyle.Visible;
			this.menuStrip1.ImageScalingSize = new Size(24, 24);
			this.menuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.الترميزاتToolStripMenuItem,
				this.ملفالحركةToolStripMenuItem,
				this.التقاريرToolStripMenuItem,
				this.تغييركلمةالمرورToolStripMenuItem,
				this.صرفالمحروقاتمحطةToolStripMenuItem
			});
			this.menuStrip1.Location = new Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new Size(1281, 37);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			this.الترميزاتToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.الشركاتToolStripMenuItem,
				this.الاسعارToolStripMenuItem,
				this.المحطاتToolStripMenuItem,
				this.الساراتToolStripMenuItem,
				this.صلاحياتالمحطاتToolStripMenuItem,
				this.المستخدمينToolStripMenuItem,
				this.الصلاحياتToolStripMenuItem,
				this.العدادتToolStripMenuItem
			});
			this.الترميزاتToolStripMenuItem.Name = "الترميزاتToolStripMenuItem";
			this.الترميزاتToolStripMenuItem.Size = new Size(101, 33);
			this.الترميزاتToolStripMenuItem.Text = "الترميزات";
			this.الشركاتToolStripMenuItem.Name = "الشركاتToolStripMenuItem";
			this.الشركاتToolStripMenuItem.Size = new Size(248, 34);
			this.الشركاتToolStripMenuItem.Text = "الشركات";
			this.الشركاتToolStripMenuItem.Click += new EventHandler(this.الشركاتToolStripMenuItem_Click);
			this.الاسعارToolStripMenuItem.Name = "الاسعارToolStripMenuItem";
			this.الاسعارToolStripMenuItem.Size = new Size(248, 34);
			this.الاسعارToolStripMenuItem.Text = "الاسعار";
			this.الاسعارToolStripMenuItem.Click += new EventHandler(this.الاسعارToolStripMenuItem_Click);
			this.المحطاتToolStripMenuItem.Name = "المحطاتToolStripMenuItem";
			this.المحطاتToolStripMenuItem.Size = new Size(248, 34);
			this.المحطاتToolStripMenuItem.Text = "المحطات";
			this.المحطاتToolStripMenuItem.Click += new EventHandler(this.المحطاتToolStripMenuItem_Click);
			this.الساراتToolStripMenuItem.Name = "الساراتToolStripMenuItem";
			this.الساراتToolStripMenuItem.Size = new Size(248, 34);
			this.الساراتToolStripMenuItem.Text = "السيارات";
			this.الساراتToolStripMenuItem.Click += new EventHandler(this.الساراتToolStripMenuItem_Click);
			this.صلاحياتالمحطاتToolStripMenuItem.Name = "صلاحياتالمحطاتToolStripMenuItem";
			this.صلاحياتالمحطاتToolStripMenuItem.Size = new Size(248, 34);
			this.صلاحياتالمحطاتToolStripMenuItem.Text = "صلاحيات المحطات";
			this.صلاحياتالمحطاتToolStripMenuItem.Click += new EventHandler(this.صلاحياتالمحطاتToolStripMenuItem_Click);
			this.المستخدمينToolStripMenuItem.Name = "المستخدمينToolStripMenuItem";
			this.المستخدمينToolStripMenuItem.Size = new Size(248, 34);
			this.المستخدمينToolStripMenuItem.Text = "المستخدمين";
			this.المستخدمينToolStripMenuItem.Click += new EventHandler(this.المستخدمينToolStripMenuItem_Click);
			this.الصلاحياتToolStripMenuItem.Name = "الصلاحياتToolStripMenuItem";
			this.الصلاحياتToolStripMenuItem.Size = new Size(248, 34);
			this.الصلاحياتToolStripMenuItem.Text = "الصلاحيات";
			this.العدادتToolStripMenuItem.Name = "العدادتToolStripMenuItem";
			this.العدادتToolStripMenuItem.Size = new Size(248, 34);
			this.العدادتToolStripMenuItem.Text = "العدادت";
			this.ملفالحركةToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.صرفمحروقاتToolStripMenuItem,
				this.طلبوقودجنوبترحيلToolStripMenuItem,
				this.طلبوقودتثبيتToolStripMenuItem
			});
			this.ملفالحركةToolStripMenuItem.Name = "ملفالحركةToolStripMenuItem";
			this.ملفالحركةToolStripMenuItem.Size = new Size(119, 33);
			this.ملفالحركةToolStripMenuItem.Text = "ملف الحركة";
			this.صرفمحروقاتToolStripMenuItem.Name = "صرفمحروقاتToolStripMenuItem";
			this.صرفمحروقاتToolStripMenuItem.Size = new Size(301, 34);
			this.صرفمحروقاتToolStripMenuItem.Text = "طلب وقود شمال - ترحيل";
			this.صرفمحروقاتToolStripMenuItem.Click += new EventHandler(this.صرفمحروقاتToolStripMenuItem_Click);
			this.طلبوقودجنوبترحيلToolStripMenuItem.Name = "طلبوقودجنوبترحيلToolStripMenuItem";
			this.طلبوقودجنوبترحيلToolStripMenuItem.Size = new Size(301, 34);
			this.طلبوقودجنوبترحيلToolStripMenuItem.Text = "طلب وقود جنوب - ترحيل";
			this.طلبوقودجنوبترحيلToolStripMenuItem.Click += new EventHandler(this.طلبوقودجنوبترحيلToolStripMenuItem_Click);
			this.طلبوقودتثبيتToolStripMenuItem.Name = "طلبوقودتثبيتToolStripMenuItem";
			this.طلبوقودتثبيتToolStripMenuItem.Size = new Size(301, 34);
			this.طلبوقودتثبيتToolStripMenuItem.Text = "طلب وقود - تثبيت";
			this.طلبوقودتثبيتToolStripMenuItem.Click += new EventHandler(this.طلبوقودتثبيتToolStripMenuItem_Click);
			this.التقاريرToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.تقريرالصرفوقودToolStripMenuItem,
				this.تقريرالصرفشركاتToolStripMenuItem,
				this.تقريرالطلباتشركاتToolStripMenuItem
			});
			this.التقاريرToolStripMenuItem.Name = "التقاريرToolStripMenuItem";
			this.التقاريرToolStripMenuItem.Size = new Size(84, 33);
			this.التقاريرToolStripMenuItem.Text = "التقارير";
			this.تقريرالصرفوقودToolStripMenuItem.Name = "تقريرالصرفوقودToolStripMenuItem";
			this.تقريرالصرفوقودToolStripMenuItem.Size = new Size(284, 34);
			this.تقريرالصرفوقودToolStripMenuItem.Text = "تقرير الصرف-وقود";
			this.تقريرالصرفوقودToolStripMenuItem.Click += new EventHandler(this.تقريرالصرفوقودToolStripMenuItem_Click);
			this.تقريرالصرفشركاتToolStripMenuItem.Name = "تقريرالصرفشركاتToolStripMenuItem";
			this.تقريرالصرفشركاتToolStripMenuItem.Size = new Size(284, 34);
			this.تقريرالصرفشركاتToolStripMenuItem.Text = "تقرير الصرف -شركات";
			this.تقريرالصرفشركاتToolStripMenuItem.Click += new EventHandler(this.تقريرالصرفشركاتToolStripMenuItem_Click);
			this.تقريرالطلباتشركاتToolStripMenuItem.Name = "تقريرالطلباتشركاتToolStripMenuItem";
			this.تقريرالطلباتشركاتToolStripMenuItem.Size = new Size(284, 34);
			this.تقريرالطلباتشركاتToolStripMenuItem.Text = "تقرير الطلبات - شركات";
			this.تقريرالطلباتشركاتToolStripMenuItem.Click += new EventHandler(this.تقريرالطلباتشركاتToolStripMenuItem_Click);
			this.تغييركلمةالمرورToolStripMenuItem.Name = "تغييركلمةالمرورToolStripMenuItem";
			this.تغييركلمةالمرورToolStripMenuItem.Size = new Size(166, 33);
			this.تغييركلمةالمرورToolStripMenuItem.Text = "تغيير كلمة المرور";
			this.صرفالمحروقاتمحطةToolStripMenuItem.Name = "صرفالمحروقاتمحطةToolStripMenuItem";
			this.صرفالمحروقاتمحطةToolStripMenuItem.Size = new Size(231, 33);
			this.صرفالمحروقاتمحطةToolStripMenuItem.Text = "صرف المحروقات - محطة";
			this.صرفالمحروقاتمحطةToolStripMenuItem.Click += new EventHandler(this.صرفالمحروقاتمحطةToolStripMenuItem_Click);
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.pictureBox2, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 0);
			this.tableLayoutPanel1.Dock = DockStyle.Fill;
			this.tableLayoutPanel1.Location = new Point(0, 37);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 70f));
			this.tableLayoutPanel1.Size = new Size(1281, 608);
			this.tableLayoutPanel1.TabIndex = 1;
			this.panel1.BorderStyle = BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = DockStyle.Fill;
			this.panel1.Location = new Point(644, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(634, 54);
			this.panel1.TabIndex = 0;
			this.label1.AutoSize = true;
			this.label1.BackColor = SystemColors.Control;
			this.label1.Font = new Font("Arial", 14f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label1.ForeColor = Color.Red;
			this.label1.Location = new Point(246, 10);
			this.label1.Name = "label1";
			this.label1.Size = new Size(184, 33);
			this.label1.TabIndex = 2;
			this.label1.Text = "نظام طلبات الوقود";
			this.label1.TextAlign = ContentAlignment.MiddleCenter;
			this.tableLayoutPanel1.SetColumnSpan(this.panel2, 2);
			this.panel2.Controls.Add(this.pictureBox3);
			this.panel2.Dock = DockStyle.Fill;
			this.panel2.Location = new Point(3, 184);
			this.panel2.Name = "panel2";
			this.panel2.Size = new Size(1275, 421);
			this.panel2.TabIndex = 1;
			this.pictureBox3.BorderStyle = BorderStyle.Fixed3D;
			this.pictureBox3.Dock = DockStyle.Fill;
			this.pictureBox3.Image = Resources.waqoodimage;
			this.pictureBox3.Location = new Point(0, 0);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new Size(1275, 421);
			this.pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox3.TabIndex = 0;
			this.pictureBox3.TabStop = false;
			this.pictureBox1.BorderStyle = BorderStyle.Fixed3D;
			this.pictureBox1.Dock = DockStyle.Fill;
			this.pictureBox1.Image = Resources.adroidlog;
			this.pictureBox1.Location = new Point(644, 63);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new Size(634, 115);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			this.pictureBox2.BorderStyle = BorderStyle.Fixed3D;
			this.pictureBox2.Dock = DockStyle.Fill;
			this.pictureBox2.Image = Resources.waqood;
			this.pictureBox2.Location = new Point(3, 63);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new Size(635, 115);
			this.pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox2.TabIndex = 3;
			this.pictureBox2.TabStop = false;
			this.panel3.BorderStyle = BorderStyle.FixedSingle;
			this.panel3.Controls.Add(this.label_bra_name);
			this.panel3.Dock = DockStyle.Fill;
			this.panel3.Location = new Point(3, 3);
			this.panel3.Name = "panel3";
			this.panel3.Size = new Size(635, 54);
			this.panel3.TabIndex = 4;
			this.label_bra_name.AutoSize = true;
			this.label_bra_name.BackColor = SystemColors.Control;
			this.label_bra_name.Font = new Font("Arial", 14f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label_bra_name.ForeColor = Color.Red;
			this.label_bra_name.Location = new Point(247, 11);
			this.label_bra_name.Name = "label_bra_name";
			this.label_bra_name.Size = new Size(140, 33);
			this.label_bra_name.TabIndex = 2;
			this.label_bra_name.Text = "Welcome";
			base.AutoScaleDimensions = new SizeF(9f, 19f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(1281, 645);
			base.Controls.Add(this.tableLayoutPanel1);
			base.Controls.Add(this.menuStrip1);
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.MainMenuStrip = this.menuStrip1;
			base.Name = "MainForm";
			this.RightToLeft = RightToLeft.Yes;
			this.RightToLeftLayout = true;
			this.Text = "شركة وقود للاستثمار المحدودة";
			base.FormClosing += new FormClosingEventHandler(this.MainForm_FormClosing);
			base.FormClosed += new FormClosedEventHandler(this.MainForm_FormClosed);
			base.Load += new EventHandler(this.MainForm_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			((ISupportInitialize)this.pictureBox3).EndInit();
			((ISupportInitialize)this.pictureBox1).EndInit();
			((ISupportInitialize)this.pictureBox2).EndInit();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
