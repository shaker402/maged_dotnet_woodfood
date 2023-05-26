using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WAQOOD.Properties;

namespace WAQOOD
{
	public class LogonForm : Form
	{
		public static string userID = string.Empty;

		public static string actv = string.Empty;

		public static string userName = string.Empty;

		public static string braname = string.Empty;

		public static string compid = string.Empty;

		public static string compcurr1 = string.Empty;

		public static string compcurr2 = string.Empty;

		public static string braid = string.Empty;

		private SqlCommand mCommand = null;

		private static SqlDataAdapter mAdatpter = null;

		private static SqlConnection mConnection = new SqlConnection(MainForm.str_conn);

		private DataTable dtUsers;

		private IContainer components = null;

		private CheckBox checkBoxSave;

		private PictureBox pictureBox1;

		private TextBox textBoxpass;

		private Button button2;

		private Button button1;

		private Label label3;

		private TextBox txtusername;

		private Label label2;

		private Panel panel1;

		private Label label1;

		private PictureBox pictureBox2;

		public LogonForm()
		{
			this.InitializeComponent();
			LogonForm.mConnection = new SqlConnection(MainForm.str_conn);
			this.dtUsers = new DataTable();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Environment.Exit(0);
		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{
		}

		private void LogonForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Environment.Exit(0);
		}

		private void LogonForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Environment.Exit(0);
		}

		private void LogonForm_Load(object sender, EventArgs e)
		{
		}

		private void WriteMyData()
		{
			StreamWriter streamWriter = new StreamWriter("MyData.txt");
			streamWriter.WriteLine(this.txtusername.Text);
			streamWriter.WriteLine(this.textBoxpass.Text);
			streamWriter.Close();
		}

		private bool ReadMyData()
		{
			StreamReader streamReader = new StreamReader("MyData.txt");
			bool flag = streamReader == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				this.txtusername.Text = streamReader.ReadLine();
				this.textBoxpass.Text = streamReader.ReadLine();
				streamReader.Close();
				result = true;
			}
			return result;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			bool flag = this.txtusername.Text == "" || this.textBoxpass.Text == "";
			if (flag)
			{
				MessageBox.Show("تاكد من ادخال كلمة المرور وكلمة المرور");
			}
			else
			{
				this.check_logon();
			}
		}

		private void check_logon()
		{
			try
			{
				this.dtUsers.Clear();
				this.dtUsers.Clone();
				LogonForm.mAdatpter = new SqlDataAdapter(string.Concat(new string[]
				{
					"SELECT [user_id],[user_name],[user_type],curr_id,curr_id2,Branchs.[bra_id],Users_t.[comp_id],bra_name,Users_t.[active] FROM [dbo].[Users_t],Branchs where user_type='1' and Branchs.[bra_id]=Users_t.bra_id and [user_log]='",
					this.txtusername.Text,
					"'  and [user_pass]='",
					this.textBoxpass.Text,
					"' "
				}), LogonForm.mConnection);
				LogonForm.mAdatpter.Fill(this.dtUsers);
				bool flag = this.dtUsers.Rows.Count == 1;
				if (flag)
				{
					bool @checked = this.checkBoxSave.Checked;
					if (@checked)
					{
						this.txtusername.Text = this.txtusername.Text;
						this.textBoxpass.Text = this.textBoxpass.Text;
					}
					LogonForm.userID = this.dtUsers.Rows[0]["user_id"].ToString();
					LogonForm.userName = this.dtUsers.Rows[0]["user_name"].ToString();
					LogonForm.compid = this.dtUsers.Rows[0]["comp_id"].ToString();
					LogonForm.braid = this.dtUsers.Rows[0]["bra_id"].ToString();
					LogonForm.braname = this.dtUsers.Rows[0]["bra_name"].ToString();
					LogonForm.actv = this.dtUsers.Rows[0]["active"].ToString();
					LogonForm.compcurr1 = this.dtUsers.Rows[0]["curr_id"].ToString();
					LogonForm.compcurr2 = this.dtUsers.Rows[0]["curr_id2"].ToString();
					base.Hide();
					MainForm mainForm = new MainForm();
					mainForm.ShowDialog();
					this.txtusername.Text = "";
					this.textBoxpass.Text = "";
				}
				else
				{
					MessageBox.Show("اسم المستخدم او كلمة المرور غير صحيحة");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(LogonForm));
			this.checkBoxSave = new CheckBox();
			this.pictureBox1 = new PictureBox();
			this.textBoxpass = new TextBox();
			this.button2 = new Button();
			this.button1 = new Button();
			this.label3 = new Label();
			this.txtusername = new TextBox();
			this.label2 = new Label();
			this.panel1 = new Panel();
			this.pictureBox2 = new PictureBox();
			this.label1 = new Label();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			this.panel1.SuspendLayout();
			((ISupportInitialize)this.pictureBox2).BeginInit();
			base.SuspendLayout();
			this.checkBoxSave.AutoSize = true;
			this.checkBoxSave.Font = new Font("Arial", 10f, FontStyle.Bold);
			this.checkBoxSave.ForeColor = Color.FromArgb(64, 64, 64);
			this.checkBoxSave.Location = new Point(542, 309);
			this.checkBoxSave.Name = "checkBoxSave";
			this.checkBoxSave.Size = new Size(156, 28);
			this.checkBoxSave.TabIndex = 18;
			this.checkBoxSave.Text = "حفظ بيانات الدخول";
			this.checkBoxSave.UseVisualStyleBackColor = true;
			this.pictureBox1.Image = Resources.waqood;
			this.pictureBox1.Location = new Point(553, 95);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new Size(100, 102);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 15;
			this.pictureBox1.TabStop = false;
			this.textBoxpass.BackColor = Color.White;
			this.textBoxpass.Font = new Font("Arial", 14f, FontStyle.Bold);
			this.textBoxpass.Location = new Point(394, 265);
			this.textBoxpass.Multiline = true;
			this.textBoxpass.Name = "textBoxpass";
			this.textBoxpass.PasswordChar = '*';
			this.textBoxpass.Size = new Size(305, 38);
			this.textBoxpass.TabIndex = 17;
			this.textBoxpass.TextAlign = HorizontalAlignment.Center;
			this.button2.BackColor = Color.White;
			this.button2.Cursor = Cursors.Hand;
			this.button2.FlatStyle = FlatStyle.Flat;
			this.button2.Font = new Font("Arial", 16f, FontStyle.Bold);
			this.button2.ForeColor = Color.FromArgb(64, 64, 64);
			this.button2.Location = new Point(553, 346);
			this.button2.Name = "button2";
			this.button2.Size = new Size(146, 52);
			this.button2.TabIndex = 20;
			this.button2.Text = "دخــــول";
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.button1.BackColor = Color.Red;
			this.button1.Cursor = Cursors.Hand;
			this.button1.FlatAppearance.BorderSize = 0;
			this.button1.FlatStyle = FlatStyle.Flat;
			this.button1.Font = new Font("Arial", 16f, FontStyle.Bold);
			this.button1.ForeColor = Color.Black;
			this.button1.Location = new Point(394, 346);
			this.button1.Name = "button1";
			this.button1.Size = new Size(137, 52);
			this.button1.TabIndex = 19;
			this.button1.Text = "الغاء";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.label3.AutoEllipsis = true;
			this.label3.AutoSize = true;
			this.label3.Font = new Font("Arial", 14f, FontStyle.Bold);
			this.label3.ForeColor = Color.FromArgb(64, 64, 64);
			this.label3.Location = new Point(705, 265);
			this.label3.Name = "label3";
			this.label3.Size = new Size(126, 33);
			this.label3.TabIndex = 17;
			this.label3.Text = "كلمة المرور";
			this.label3.TextAlign = ContentAlignment.TopRight;
			this.txtusername.BackColor = Color.White;
			this.txtusername.Font = new Font("Arial", 14f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.txtusername.Location = new Point(394, 218);
			this.txtusername.Multiline = true;
			this.txtusername.Name = "txtusername";
			this.txtusername.Size = new Size(306, 38);
			this.txtusername.TabIndex = 16;
			this.txtusername.TextAlign = HorizontalAlignment.Center;
			this.label2.AutoEllipsis = true;
			this.label2.AutoSize = true;
			this.label2.Font = new Font("Arial", 14f, FontStyle.Bold);
			this.label2.ForeColor = Color.FromArgb(64, 64, 64);
			this.label2.Location = new Point(705, 218);
			this.label2.Name = "label2";
			this.label2.Size = new Size(141, 33);
			this.label2.TabIndex = 14;
			this.label2.Text = "أسم المستخدم";
			this.label2.TextAlign = ContentAlignment.MiddleCenter;
			this.panel1.BackgroundImage = Resources.waqood;
			this.panel1.BackgroundImageLayout = ImageLayout.None;
			this.panel1.BorderStyle = BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.pictureBox2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.button2);
			this.panel1.Controls.Add(this.checkBoxSave);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Controls.Add(this.txtusername);
			this.panel1.Controls.Add(this.textBoxpass);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Dock = DockStyle.Fill;
			this.panel1.Location = new Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(1027, 582);
			this.panel1.TabIndex = 22;
			this.panel1.Paint += new PaintEventHandler(this.panel1_Paint);
			this.pictureBox2.Image = Resources.adroidlog;
			this.pictureBox2.Location = new Point(447, 95);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new Size(100, 102);
			this.pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox2.TabIndex = 23;
			this.pictureBox2.TabStop = false;
			this.label1.AutoSize = true;
			this.label1.BackColor = SystemColors.Control;
			this.label1.Font = new Font("Arial", 18f, FontStyle.Bold);
			this.label1.ForeColor = Color.Red;
			this.label1.Location = new Point(425, 34);
			this.label1.Name = "label1";
			this.label1.Size = new Size(228, 43);
			this.label1.TabIndex = 22;
			this.label1.Text = "نظام طلبات الوقود";
			this.label1.TextAlign = ContentAlignment.MiddleCenter;
			base.AutoScaleDimensions = new SizeF(9f, 19f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.WhiteSmoke;
			base.ClientSize = new Size(1027, 582);
			base.Controls.Add(this.panel1);
			base.FormBorderStyle = FormBorderStyle.None;
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "LogonForm";
			this.RightToLeft = RightToLeft.Yes;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "تسجيل الدخول";
			base.FormClosing += new FormClosingEventHandler(this.LogonForm_FormClosing);
			base.FormClosed += new FormClosedEventHandler(this.LogonForm_FormClosed);
			base.Load += new EventHandler(this.LogonForm_Load);
			((ISupportInitialize)this.pictureBox1).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((ISupportInitialize)this.pictureBox2).EndInit();
			base.ResumeLayout(false);
		}
	}
}
