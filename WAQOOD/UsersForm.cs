using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WAQOOD.Properties;

namespace WAQOOD
{
	public class UsersForm : Form
	{
		private string userid = string.Empty;

		private string username = string.Empty;

		private int usertype;

		private int actv;

		private string staid = string.Empty;

		private string staname = string.Empty;

		private SqlCommand mCommand = null;

		private static SqlDataAdapter mAdatpter = null;

		private static SqlConnection mConnection = new SqlConnection(MainForm.str_conn);

		private DataTable dtOrder;

		private IContainer components = null;

		private TableLayoutPanel tableLayoutPanel1;

		private Panel panel1;

		private Label label8;

		private Button button3;

		private TextBox textBoxMobile;

		private Label label9;

		private TextBox textBoxUserID;

		private Label label10;

		private TextBox textBoxPass;

		private Label label11;

		private TextBox textBoxuserlog;

		private TextBox textBoxUsername;

		private Label label12;

		private Label label13;

		private GroupBox groupBox1;

		private RadioButton radioActive;

		private RadioButton radioStop;

		private Label label4;

		private ComboBox comboBoxBra;

		private Label label3;

		private ComboBox comboBoxComp;

		private Label label2;

		private Label label1;

		private Button button4;

		private DataGridView dataGridView1;

		private GroupBox groupBox2;

		private RadioButton radioCompany;

		private RadioButton radioStation;

		private PictureBox pictureBox1;

		private Button button1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn Column3;

		private DataGridViewTextBoxColumn Column4;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn Column5;

		private DataGridViewTextBoxColumn Column2;

		private DataGridViewTextBoxColumn Column7;

		private DataGridViewTextBoxColumn Column6;

		public UsersForm()
		{
			this.InitializeComponent();
			base.WindowState = FormWindowState.Maximized;
			UsersForm.mConnection = new SqlConnection(MainForm.str_conn);
			this.dtOrder = new DataTable();
			this.DisplayData();
			this.Fill_Company();
		}

		private void Fill_Company()
		{
			try
			{
				UsersForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT [comp_id],[comp_name] FROM [dbo].[company] order by [comp_id] asc", UsersForm.mConnection);
				DataTable dataTable = new DataTable();
				sqlDataAdapter.Fill(dataTable);
				DataRow dataRow = dataTable.NewRow();
				dataRow[0] = 0;
				dataRow[1] = "-الشركة-";
				dataTable.Rows.InsertAt(dataRow, 0);
				this.comboBoxComp.DataSource = dataTable;
				this.comboBoxComp.DisplayMember = "comp_name";
				this.comboBoxComp.ValueMember = "comp_id";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}
		}

		private void Fill_Branch()
		{
			try
			{
				UsersForm.mConnection = new SqlConnection(MainForm.str_conn);
				UsersForm.mAdatpter = new SqlDataAdapter("SELECT [bra_id],[bra_name] FROM [dbo].[Branchs] where comp_id=@compidd order by [bra_id] asc", UsersForm.mConnection);
				UsersForm.mAdatpter.SelectCommand.Parameters.AddWithValue("@compidd", this.comboBoxComp.SelectedValue);
				DataTable dataTable = new DataTable();
				UsersForm.mAdatpter.Fill(dataTable);
				this.comboBoxBra.DataSource = dataTable;
				this.comboBoxBra.DisplayMember = "bra_name";
				this.comboBoxBra.ValueMember = "bra_id";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}
		}

		public void DisplayData()
		{
			try
			{
				this.dtOrder.Clear();
				this.dtOrder.Clone();
				this.dataGridView1.Rows.Clear();
				this.dataGridView1.Refresh();
				UsersForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlConnection.ClearPool(UsersForm.mConnection);
				UsersForm.mAdatpter = new SqlDataAdapter("SELECT [user_id],[user_name],[user_log],[user_pass],[user_type],[Users_t].[bra_id] bra_id,bra_name,[Users_t].[comp_id] comp_id,[mobile_no],Users_t.[active] FROM [dbo].[Users_t],Branchs where [Users_t].bra_id=Branchs.bra_id order by [user_id] asc", UsersForm.mConnection);
				UsersForm.mAdatpter.Fill(this.dtOrder);
				bool flag = this.dtOrder.Rows.Count == 0;
				if (!flag)
				{
					this.add_coulm();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public void add_coulm()
		{
			try
			{
				for (int i = 0; i < this.dtOrder.Rows.Count; i++)
				{
					this.dataGridView1.Rows.Add(new object[]
					{
						this.dtOrder.Rows[i]["user_ID"].ToString(),
						this.dtOrder.Rows[i]["user_NAME"].ToString(),
						this.dtOrder.Rows[i]["user_log"].ToString(),
						this.dtOrder.Rows[i]["user_pass"].ToString(),
						this.dtOrder.Rows[i]["mobile_no"].ToString(),
						this.dtOrder.Rows[i]["bra_name"].ToString(),
						this.dtOrder.Rows[i]["user_type"].ToString(),
						this.dtOrder.Rows[i]["active"].ToString(),
						this.dtOrder.Rows[i]["bra_id"].ToString()
					});
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void comboBoxComp_SelectedValueChanged(object sender, EventArgs e)
		{
			bool flag = this.comboBoxComp.SelectedIndex.ToString() != "0";
			if (flag)
			{
				this.Fill_Branch();
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			try
			{
				bool flag = this.textBoxuserlog.Text != "" && this.textBoxUsername.Text != "" && this.textBoxPass.Text != "" && this.comboBoxComp.SelectedIndex.ToString() != "0" && this.comboBoxBra.SelectedValue.ToString() != "";
				if (flag)
				{
					this.mCommand = new SqlCommand("INSERT INTO [dbo].[Users_t]([user_name],[user_log],[user_pass],[user_type],[bra_id],[comp_id],[mobile_no],active) VALUES(@tname,@ulog,@upass,@ut,@braid,@compid,@mob,@actvv)", UsersForm.mConnection);
					UsersForm.mConnection.Open();
					this.mCommand.Parameters.AddWithValue("@tname", this.textBoxUsername.Text);
					this.mCommand.Parameters.AddWithValue("@ulog", this.textBoxuserlog.Text);
					this.mCommand.Parameters.AddWithValue("@upass", this.textBoxPass.Text);
					this.mCommand.Parameters.AddWithValue("@ut", this.usertype);
					this.mCommand.Parameters.AddWithValue("@compid", this.comboBoxComp.SelectedValue);
					this.mCommand.Parameters.AddWithValue("@braid", this.comboBoxBra.SelectedValue);
					this.mCommand.Parameters.AddWithValue("@mob", this.textBoxMobile.Text);
					this.mCommand.Parameters.AddWithValue("@actvv", this.actv);
					this.mCommand.ExecuteNonQuery();
					UsersForm.mConnection.Close();
					MessageBox.Show(" تم حفظ البيانات بنجاح");
					this.DisplayData();
					this.ClearControls();
				}
				else
				{
					MessageBox.Show("بيانات غير مكتملة!");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				UsersForm.mConnection.Close();
			}
		}

		private void UsersForm_Load(object sender, EventArgs e)
		{
		}

		private void radioCompany_CheckedChanged(object sender, EventArgs e)
		{
			bool @checked = this.radioCompany.Checked;
			if (@checked)
			{
				this.usertype = 1;
			}
			else
			{
				this.usertype = 0;
			}
		}

		private void radioStation_CheckedChanged(object sender, EventArgs e)
		{
			bool @checked = this.radioStation.Checked;
			if (@checked)
			{
				this.usertype = 2;
			}
			else
			{
				this.usertype = 0;
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			try
			{
				bool flag = this.textBoxMobile.Text != "" && this.textBoxUserID.Text != "" && this.textBoxUsername.Text != "" && this.textBoxPass.Text != "";
				if (flag)
				{
					this.mCommand = new SqlCommand("update Users_t set user_pass=@upass,user_type=@ut,mobile_no=@mob,active=@actvv where user_id=@tid and user_name=@tname and user_log=@ulog", UsersForm.mConnection);
					UsersForm.mConnection.Open();
					this.mCommand.Parameters.AddWithValue("@tid", this.textBoxUserID.Text);
					this.mCommand.Parameters.AddWithValue("@tname", this.textBoxUsername.Text);
					this.mCommand.Parameters.AddWithValue("@ulog", this.textBoxuserlog.Text);
					this.mCommand.Parameters.AddWithValue("@upass", this.textBoxPass.Text);
					this.mCommand.Parameters.AddWithValue("@ut", this.usertype);
					this.mCommand.Parameters.AddWithValue("@compid", this.comboBoxComp.SelectedValue);
					this.mCommand.Parameters.AddWithValue("@mob", this.textBoxMobile.Text);
					this.mCommand.Parameters.AddWithValue("@actvv", this.actv);
					this.mCommand.ExecuteNonQuery();
					UsersForm.mConnection.Close();
					MessageBox.Show("تم التعديل بنجاح");
					this.DisplayData();
					this.ClearControls();
				}
				else
				{
					MessageBox.Show("بيانات غير مكتملة!");
				}
			}
			catch (Exception ex)
			{
				UsersForm.mConnection.Close();
				MessageBox.Show(ex.Message);
			}
		}

		private void radioStop_CheckedChanged(object sender, EventArgs e)
		{
			bool @checked = this.radioStop.Checked;
			if (@checked)
			{
				this.actv = 0;
			}
			else
			{
				this.actv = 1;
			}
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
		}

		private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			bool flag = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value != null;
			if (flag)
			{
				this.userid = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
				this.username = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
				this.textBoxMobile.Text = this.dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
				this.textBoxPass.Text = this.dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
				this.textBoxuserlog.Text = this.dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
				this.comboBoxBra.SelectedValue = this.dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
				this.textBoxUsername.Text = this.username;
				this.textBoxUserID.Text = this.userid;
			}
		}

		private void radioActive_CheckedChanged(object sender, EventArgs e)
		{
			bool @checked = this.radioActive.Checked;
			if (@checked)
			{
				this.actv = 1;
			}
			else
			{
				this.actv = 0;
			}
		}

		private void ClearControls()
		{
			this.textBoxMobile.Text = "";
			this.textBoxPass.Text = "";
			this.textBoxuserlog.Text = "";
			this.textBoxUsername.Text = "";
			this.textBoxUserID.Text = "";
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
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.button1 = new Button();
			this.dataGridView1 = new DataGridView();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.Column3 = new DataGridViewTextBoxColumn();
			this.Column4 = new DataGridViewTextBoxColumn();
			this.Column1 = new DataGridViewTextBoxColumn();
			this.Column5 = new DataGridViewTextBoxColumn();
			this.Column2 = new DataGridViewTextBoxColumn();
			this.Column7 = new DataGridViewTextBoxColumn();
			this.Column6 = new DataGridViewTextBoxColumn();
			this.panel1 = new Panel();
			this.groupBox2 = new GroupBox();
			this.radioCompany = new RadioButton();
			this.radioStation = new RadioButton();
			this.button4 = new Button();
			this.groupBox1 = new GroupBox();
			this.radioActive = new RadioButton();
			this.radioStop = new RadioButton();
			this.label4 = new Label();
			this.comboBoxBra = new ComboBox();
			this.label3 = new Label();
			this.comboBoxComp = new ComboBox();
			this.label2 = new Label();
			this.label1 = new Label();
			this.label8 = new Label();
			this.button3 = new Button();
			this.textBoxMobile = new TextBox();
			this.label9 = new Label();
			this.textBoxUserID = new TextBox();
			this.label10 = new Label();
			this.textBoxPass = new TextBox();
			this.label11 = new Label();
			this.textBoxuserlog = new TextBox();
			this.textBoxUsername = new TextBox();
			this.label12 = new Label();
			this.label13 = new Label();
			this.pictureBox1 = new PictureBox();
			this.tableLayoutPanel1.SuspendLayout();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			this.panel1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70f));
			this.tableLayoutPanel1.Controls.Add(this.button1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 1, 0);
			this.tableLayoutPanel1.Dock = DockStyle.Fill;
			this.tableLayoutPanel1.Location = new Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 90f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel1.Size = new Size(1924, 940);
			this.tableLayoutPanel1.TabIndex = 1;
			this.button1.Dock = DockStyle.Fill;
			this.button1.Enabled = false;
			this.button1.FlatStyle = FlatStyle.Flat;
			this.button1.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.button1.Location = new Point(1350, 3);
			this.button1.Name = "button1";
			this.button1.Size = new Size(571, 88);
			this.button1.TabIndex = 6;
			this.button1.Text = "ترميز المستخدمين";
			this.button1.UseVisualStyleBackColor = true;
			this.dataGridView1.AllowUserToDeleteRows = false;
			dataGridViewCellStyle.BackColor = Color.WhiteSmoke;
			dataGridViewCellStyle.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle;
			this.dataGridView1.BackgroundColor = SystemColors.Control;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.BackColor = SystemColors.Control;
			dataGridViewCellStyle2.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
			this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn1,
				this.dataGridViewTextBoxColumn2,
				this.Column3,
				this.Column4,
				this.Column1,
				this.Column5,
				this.Column2,
				this.Column7,
				this.Column6
			});
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle3.BackColor = SystemColors.Window;
			dataGridViewCellStyle3.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
			this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridView1.Dock = DockStyle.Fill;
			this.dataGridView1.Location = new Point(3, 97);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowTemplate.Height = 29;
			this.dataGridView1.Size = new Size(1341, 840);
			this.dataGridView1.TabIndex = 4;
			this.dataGridView1.CellContentClick += new DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
			this.dataGridView1.RowHeaderMouseClick += new DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
			this.dataGridViewTextBoxColumn1.HeaderText = "التسلسل";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn2.HeaderText = "الاسم";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.Column3.HeaderText = "رمز الدخول";
			this.Column3.Name = "Column3";
			this.Column4.HeaderText = "كلمة المرور";
			this.Column4.Name = "Column4";
			this.Column1.HeaderText = "الموبايل";
			this.Column1.Name = "Column1";
			this.Column5.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.Column5.HeaderText = "الشركة";
			this.Column5.Name = "Column5";
			this.Column2.HeaderText = "النوع";
			this.Column2.Name = "Column2";
			this.Column7.HeaderText = "الحالة";
			this.Column7.Name = "Column7";
			this.Column6.HeaderText = "الفرع";
			this.Column6.Name = "Column6";
			this.Column6.Visible = false;
			this.panel1.BackColor = Color.LightSteelBlue;
			this.panel1.Controls.Add(this.groupBox2);
			this.panel1.Controls.Add(this.button4);
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.comboBoxBra);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.comboBoxComp);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.label8);
			this.panel1.Controls.Add(this.button3);
			this.panel1.Controls.Add(this.textBoxMobile);
			this.panel1.Controls.Add(this.label9);
			this.panel1.Controls.Add(this.textBoxUserID);
			this.panel1.Controls.Add(this.label10);
			this.panel1.Controls.Add(this.textBoxPass);
			this.panel1.Controls.Add(this.label11);
			this.panel1.Controls.Add(this.textBoxuserlog);
			this.panel1.Controls.Add(this.textBoxUsername);
			this.panel1.Controls.Add(this.label12);
			this.panel1.Controls.Add(this.label13);
			this.panel1.Dock = DockStyle.Fill;
			this.panel1.Location = new Point(1350, 97);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(571, 840);
			this.panel1.TabIndex = 2;
			this.groupBox2.BackColor = SystemColors.ButtonHighlight;
			this.groupBox2.Controls.Add(this.radioCompany);
			this.groupBox2.Controls.Add(this.radioStation);
			this.groupBox2.Location = new Point(79, 435);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(279, 47);
			this.groupBox2.TabIndex = 142;
			this.groupBox2.TabStop = false;
			this.radioCompany.AutoSize = true;
			this.radioCompany.Font = new Font("Arial", 10f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.radioCompany.Location = new Point(177, 14);
			this.radioCompany.Name = "radioCompany";
			this.radioCompany.Size = new Size(81, 28);
			this.radioCompany.TabIndex = 1;
			this.radioCompany.TabStop = true;
			this.radioCompany.Text = "شركات";
			this.radioCompany.UseVisualStyleBackColor = true;
			this.radioCompany.CheckedChanged += new EventHandler(this.radioCompany_CheckedChanged);
			this.radioStation.AutoSize = true;
			this.radioStation.Font = new Font("Arial", 10f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.radioStation.Location = new Point(78, 14);
			this.radioStation.Name = "radioStation";
			this.radioStation.Size = new Size(81, 28);
			this.radioStation.TabIndex = 0;
			this.radioStation.TabStop = true;
			this.radioStation.Text = "محطات";
			this.radioStation.UseVisualStyleBackColor = true;
			this.radioStation.CheckedChanged += new EventHandler(this.radioStation_CheckedChanged);
			this.button4.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 0);
			this.button4.Font = new Font("Arial", 12f, FontStyle.Bold);
			this.button4.Location = new Point(85, 537);
			this.button4.Name = "button4";
			this.button4.Size = new Size(134, 37);
			this.button4.TabIndex = 145;
			this.button4.Text = "تحديث";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new EventHandler(this.button4_Click);
			this.groupBox1.BackColor = SystemColors.ButtonHighlight;
			this.groupBox1.Controls.Add(this.radioActive);
			this.groupBox1.Controls.Add(this.radioStop);
			this.groupBox1.Location = new Point(80, 488);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(279, 43);
			this.groupBox1.TabIndex = 143;
			this.groupBox1.TabStop = false;
			this.radioActive.AutoSize = true;
			this.radioActive.Font = new Font("Arial", 10f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.radioActive.Location = new Point(195, 14);
			this.radioActive.Name = "radioActive";
			this.radioActive.Size = new Size(62, 28);
			this.radioActive.TabIndex = 1;
			this.radioActive.Text = "فعال";
			this.radioActive.UseVisualStyleBackColor = true;
			this.radioActive.CheckedChanged += new EventHandler(this.radioActive_CheckedChanged);
			this.radioStop.AutoSize = true;
			this.radioStop.Font = new Font("Arial", 10f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.radioStop.Location = new Point(80, 14);
			this.radioStop.Name = "radioStop";
			this.radioStop.Size = new Size(78, 28);
			this.radioStop.TabIndex = 0;
			this.radioStop.TabStop = true;
			this.radioStop.Text = "متوقف";
			this.radioStop.UseVisualStyleBackColor = true;
			this.radioStop.CheckedChanged += new EventHandler(this.radioStop_CheckedChanged);
			this.label4.AutoSize = true;
			this.label4.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label4.Location = new Point(365, 492);
			this.label4.Name = "label4";
			this.label4.Size = new Size(59, 29);
			this.label4.TabIndex = 147;
			this.label4.Text = "الحالة";
			this.comboBoxBra.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxBra.FlatStyle = FlatStyle.Flat;
			this.comboBoxBra.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.comboBoxBra.FormattingEnabled = true;
			this.comboBoxBra.Location = new Point(80, 391);
			this.comboBoxBra.Name = "comboBoxBra";
			this.comboBoxBra.Size = new Size(279, 37);
			this.comboBoxBra.TabIndex = 141;
			this.label3.AutoSize = true;
			this.label3.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label3.Location = new Point(360, 392);
			this.label3.Name = "label3";
			this.label3.Size = new Size(55, 29);
			this.label3.TabIndex = 145;
			this.label3.Text = "الفرع";
			this.comboBoxComp.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxComp.FlatStyle = FlatStyle.Flat;
			this.comboBoxComp.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.comboBoxComp.FormattingEnabled = true;
			this.comboBoxComp.Location = new Point(80, 348);
			this.comboBoxComp.Name = "comboBoxComp";
			this.comboBoxComp.Size = new Size(279, 37);
			this.comboBoxComp.TabIndex = 140;
			this.comboBoxComp.SelectedValueChanged += new EventHandler(this.comboBoxComp_SelectedValueChanged);
			this.label2.AutoSize = true;
			this.label2.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label2.Location = new Point(360, 349);
			this.label2.Name = "label2";
			this.label2.Size = new Size(68, 29);
			this.label2.TabIndex = 143;
			this.label2.Text = "الشركة";
			this.label1.AutoSize = true;
			this.label1.Font = new Font("Arial", 16f, FontStyle.Bold);
			this.label1.Location = new Point(80, 95);
			this.label1.Name = "label1";
			this.label1.Size = new Size(213, 37);
			this.label1.TabIndex = 142;
			this.label1.Text = "بيانات المستخدمين";
			this.label8.AutoSize = true;
			this.label8.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label8.Location = new Point(365, 434);
			this.label8.Name = "label8";
			this.label8.Size = new Size(124, 29);
			this.label8.TabIndex = 139;
			this.label8.Text = "نوع المستخدم";
			this.button3.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 0);
			this.button3.Font = new Font("Arial", 12f, FontStyle.Bold);
			this.button3.Location = new Point(225, 537);
			this.button3.Name = "button3";
			this.button3.Size = new Size(134, 37);
			this.button3.TabIndex = 144;
			this.button3.Text = "اضافة";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new EventHandler(this.button3_Click);
			this.textBoxMobile.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBoxMobile.Location = new Point(80, 311);
			this.textBoxMobile.MaxLength = 9;
			this.textBoxMobile.Name = "textBoxMobile";
			this.textBoxMobile.Size = new Size(279, 33);
			this.textBoxMobile.TabIndex = 139;
			this.textBoxMobile.TextAlign = HorizontalAlignment.Center;
			this.label9.AutoSize = true;
			this.label9.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label9.Location = new Point(365, 315);
			this.label9.Name = "label9";
			this.label9.Size = new Size(109, 29);
			this.label9.TabIndex = 136;
			this.label9.Text = "رقم الموبايل";
			this.textBoxUserID.Enabled = false;
			this.textBoxUserID.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBoxUserID.Location = new Point(80, 157);
			this.textBoxUserID.Name = "textBoxUserID";
			this.textBoxUserID.Size = new Size(279, 33);
			this.textBoxUserID.TabIndex = 135;
			this.textBoxUserID.TextAlign = HorizontalAlignment.Center;
			this.label10.AutoSize = true;
			this.label10.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label10.Location = new Point(365, 164);
			this.label10.Name = "label10";
			this.label10.Size = new Size(119, 29);
			this.label10.TabIndex = 134;
			this.label10.Text = "رقم المستخدم";
			this.textBoxPass.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBoxPass.Location = new Point(80, 272);
			this.textBoxPass.Name = "textBoxPass";
			this.textBoxPass.Size = new Size(279, 33);
			this.textBoxPass.TabIndex = 138;
			this.textBoxPass.TextAlign = HorizontalAlignment.Center;
			this.label11.AutoSize = true;
			this.label11.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label11.Location = new Point(365, 276);
			this.label11.Name = "label11";
			this.label11.Size = new Size(107, 29);
			this.label11.TabIndex = 132;
			this.label11.Text = "كلمة المرور";
			this.textBoxuserlog.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBoxuserlog.Location = new Point(80, 234);
			this.textBoxuserlog.Name = "textBoxuserlog";
			this.textBoxuserlog.Size = new Size(279, 33);
			this.textBoxuserlog.TabIndex = 137;
			this.textBoxuserlog.TextAlign = HorizontalAlignment.Center;
			this.textBoxUsername.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBoxUsername.Location = new Point(80, 195);
			this.textBoxUsername.Name = "textBoxUsername";
			this.textBoxUsername.Size = new Size(279, 33);
			this.textBoxUsername.TabIndex = 136;
			this.textBoxUsername.TextAlign = HorizontalAlignment.Center;
			this.label12.AutoSize = true;
			this.label12.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label12.Location = new Point(365, 239);
			this.label12.Name = "label12";
			this.label12.Size = new Size(104, 29);
			this.label12.TabIndex = 129;
			this.label12.Text = "رمز الدخول";
			this.label13.AutoSize = true;
			this.label13.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label13.Location = new Point(365, 202);
			this.label13.Name = "label13";
			this.label13.Size = new Size(54, 29);
			this.label13.TabIndex = 128;
			this.label13.Text = "الاسم";
			this.pictureBox1.Dock = DockStyle.Fill;
			this.pictureBox1.Image = Resources.waqood;
			this.pictureBox1.Location = new Point(3, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new Size(1341, 88);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 5;
			this.pictureBox1.TabStop = false;
			base.AutoScaleDimensions = new SizeF(9f, 19f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(1924, 940);
			base.Controls.Add(this.tableLayoutPanel1);
			base.Name = "UsersForm";
			this.RightToLeft = RightToLeft.Yes;
			this.Text = "ترميز المستخدمين";
			base.Load += new EventHandler(this.UsersForm_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			((ISupportInitialize)this.dataGridView1).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
		}
	}
}
