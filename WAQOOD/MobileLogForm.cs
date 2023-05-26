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
	public class MobileLogForm : Form
	{
		public static string userID2 = string.Empty;

		public static string userName2 = string.Empty;

		public static string actv2 = string.Empty;

		public static string staid2 = string.Empty;

		public static string staname2 = string.Empty;

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

		public MobileLogForm()
		{
			this.InitializeComponent();
			MobileLogForm.mConnection = new SqlConnection(MainForm.str_conn);
			this.dtUsers = new DataTable();
			try
			{
				bool flag = this.ReadMyData();
				if (flag)
				{
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
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

		private void button1_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void WriteMyData()
		{
			StreamWriter streamWriter = new StreamWriter("MyData2.txt");
			streamWriter.WriteLine(this.txtusername.Text);
			streamWriter.WriteLine(this.textBoxpass.Text);
			streamWriter.Close();
		}

		private bool ReadMyData()
		{
			StreamReader streamReader = new StreamReader("MyData2.txt");
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

		private void check_logon()
		{
			try
			{
				this.dtUsers.Clear();
				this.dtUsers.Clone();
				MobileLogForm.mAdatpter = new SqlDataAdapter(string.Concat(new string[]
				{
					"SELECT Users_t.[user_id],[user_name],[user_type],StationUsers.sta_id,sta_name,Users_t.[active] FROM [dbo].[Users_t],StationUsers,Stations where user_type='2' and StationUsers.sta_id=Stations.sta_id and StationUsers.[user_id]=Users_t.user_id and [user_log]='",
					this.txtusername.Text,
					"'  and [user_pass]='",
					this.textBoxpass.Text,
					"' "
				}), MobileLogForm.mConnection);
				MobileLogForm.mAdatpter.Fill(this.dtUsers);
				bool flag = this.dtUsers.Rows.Count >= 1;
				if (flag)
				{
					bool @checked = this.checkBoxSave.Checked;
					if (@checked)
					{
						this.WriteMyData();
					}
					MobileLogForm.userID2 = this.dtUsers.Rows[0]["user_id"].ToString();
					MobileLogForm.userName2 = this.dtUsers.Rows[0]["user_name"].ToString();
					MobileLogForm.staname2 = this.dtUsers.Rows[0]["sta_name"].ToString();
					MobileLogForm.actv2 = this.dtUsers.Rows[0]["active"].ToString();
					base.Hide();
					IssueForm issueForm = new IssueForm();
					issueForm.ShowDialog();
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

		private void MobileLogForm_Load(object sender, EventArgs e)
		{
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
			this.checkBoxSave = new CheckBox();
			this.pictureBox1 = new PictureBox();
			this.textBoxpass = new TextBox();
			this.button2 = new Button();
			this.button1 = new Button();
			this.label3 = new Label();
			this.txtusername = new TextBox();
			this.label2 = new Label();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.checkBoxSave.AutoSize = true;
			this.checkBoxSave.ForeColor = Color.FromArgb(64, 64, 64);
			this.checkBoxSave.Location = new Point(712, 383);
			this.checkBoxSave.Name = "checkBoxSave";
			this.checkBoxSave.Size = new Size(158, 23);
			this.checkBoxSave.TabIndex = 29;
			this.checkBoxSave.Text = "حفظ بيانات الدخول";
			this.checkBoxSave.UseVisualStyleBackColor = true;
			this.pictureBox1.Image = Resources.access;
			this.pictureBox1.Location = new Point(633, 163);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new Size(100, 102);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 23;
			this.pictureBox1.TabStop = false;
			this.textBoxpass.BackColor = Color.White;
			this.textBoxpass.Font = new Font("Arial", 14f, FontStyle.Bold);
			this.textBoxpass.Location = new Point(564, 339);
			this.textBoxpass.Multiline = true;
			this.textBoxpass.Name = "textBoxpass";
			this.textBoxpass.PasswordChar = '*';
			this.textBoxpass.Size = new Size(305, 38);
			this.textBoxpass.TabIndex = 26;
			this.textBoxpass.TextAlign = HorizontalAlignment.Center;
			this.button2.BackColor = Color.White;
			this.button2.Cursor = Cursors.Hand;
			this.button2.FlatStyle = FlatStyle.Flat;
			this.button2.Font = new Font("Arial", 16f, FontStyle.Bold);
			this.button2.ForeColor = Color.FromArgb(64, 64, 64);
			this.button2.Location = new Point(723, 420);
			this.button2.Name = "button2";
			this.button2.Size = new Size(146, 52);
			this.button2.TabIndex = 28;
			this.button2.Text = "دخــــول";
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.button1.BackColor = Color.FromArgb(255, 192, 128);
			this.button1.Cursor = Cursors.Hand;
			this.button1.FlatAppearance.BorderSize = 0;
			this.button1.FlatStyle = FlatStyle.Flat;
			this.button1.Font = new Font("Arial", 16f, FontStyle.Bold);
			this.button1.ForeColor = Color.Black;
			this.button1.Location = new Point(564, 420);
			this.button1.Name = "button1";
			this.button1.Size = new Size(137, 52);
			this.button1.TabIndex = 27;
			this.button1.Text = "الغاء";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.label3.AutoEllipsis = true;
			this.label3.AutoSize = true;
			this.label3.Font = new Font("Arial", 14f, FontStyle.Bold);
			this.label3.ForeColor = Color.FromArgb(64, 64, 64);
			this.label3.Location = new Point(875, 339);
			this.label3.Name = "label3";
			this.label3.Size = new Size(126, 33);
			this.label3.TabIndex = 25;
			this.label3.Text = "كلمة المرور";
			this.label3.TextAlign = ContentAlignment.TopRight;
			this.txtusername.BackColor = Color.White;
			this.txtusername.Font = new Font("Arial", 14f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.txtusername.Location = new Point(564, 292);
			this.txtusername.Multiline = true;
			this.txtusername.Name = "txtusername";
			this.txtusername.Size = new Size(306, 38);
			this.txtusername.TabIndex = 24;
			this.txtusername.TextAlign = HorizontalAlignment.Center;
			this.label2.AutoEllipsis = true;
			this.label2.AutoSize = true;
			this.label2.Font = new Font("Arial", 14f, FontStyle.Bold);
			this.label2.ForeColor = Color.FromArgb(64, 64, 64);
			this.label2.Location = new Point(875, 292);
			this.label2.Name = "label2";
			this.label2.Size = new Size(141, 33);
			this.label2.TabIndex = 22;
			this.label2.Text = "أسم المستخدم";
			this.label2.TextAlign = ContentAlignment.MiddleCenter;
			base.AutoScaleDimensions = new SizeF(9f, 19f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(1333, 647);
			base.Controls.Add(this.checkBoxSave);
			base.Controls.Add(this.pictureBox1);
			base.Controls.Add(this.textBoxpass);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.txtusername);
			base.Controls.Add(this.label2);
			base.Name = "MobileLogForm";
			this.RightToLeft = RightToLeft.Yes;
			this.Text = "تسجيل دخول المحطات";
			base.Load += new EventHandler(this.MobileLogForm_Load);
			((ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
