using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WAQOOD.Properties;

namespace WAQOOD
{
	public class CompanyForm : Form
	{
		private string compid = string.Empty;

		private string compname = string.Empty;

		private SqlCommand mCommand = null;

		private static SqlDataAdapter mAdatpter = null;

		private static SqlConnection mConnection = new SqlConnection(MainForm.str_conn);

		private DataTable dtOrder;

		private IContainer components = null;

		private TableLayoutPanel tableLayoutPanel1;

		private SplitContainer splitContainer1;

		private Button button3;

		private TextBox textBox_comp_id;

		private Label label3;

		private TextBox textBox1_compname;

		private Label label1;

		private DataGridView dataGridView1;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn Column2;

		private SplitContainer splitContainer2;

		private Button button1;

		private TextBox textBox1BraId;

		private Label label2;

		private TextBox textBox2Braname;

		private Label label4;

		private Label label6;

		private TextBox textBox3AccountNo;

		private Label label5;

		private DataGridView dataGridView2;

		private TextBox textBox1comp;

		private Label label9;

		private Label label7;

		private Panel panel1;

		private PictureBox pictureBox1;

		private Button button4;

		private Label label8;

		private ComboBox comboBox2Currency;

		private Label label10;

		private ComboBox comboBox2Currency2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn Column3;

		private DataGridViewTextBoxColumn Column4;

		private DataGridViewTextBoxColumn Column5;

		private DataGridViewTextBoxColumn Column6;

		public CompanyForm()
		{
			this.InitializeComponent();
			base.WindowState = FormWindowState.Maximized;
			CompanyForm.mConnection = new SqlConnection(MainForm.str_conn);
			this.dtOrder = new DataTable();
			this.DisplayData();
			this.DisplayDataBranch();
		}

		public void DisplayData()
		{
			try
			{
				this.dtOrder.Clear();
				this.dtOrder.Clone();
				this.dataGridView1.Rows.Clear();
				this.dataGridView1.Refresh();
				CompanyForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlConnection.ClearPool(CompanyForm.mConnection);
				CompanyForm.mAdatpter = new SqlDataAdapter("SELECT [comp_id],[comp_name] FROM [dbo].[Company] order by comp_id asc", CompanyForm.mConnection);
				CompanyForm.mAdatpter.Fill(this.dtOrder);
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
						this.dtOrder.Rows[i]["COMP_ID"].ToString(),
						this.dtOrder.Rows[i]["COMP_NAME"].ToString()
					});
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public void DisplayDataBranch()
		{
			try
			{
				this.dtOrder.Clear();
				this.dtOrder.Clone();
				this.dataGridView2.Rows.Clear();
				this.dataGridView2.Refresh();
				CompanyForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlConnection.ClearPool(CompanyForm.mConnection);
				CompanyForm.mAdatpter = new SqlDataAdapter("SELECT [bra_id],[bra_name],[comp_name],[account_no],[Branchs].curr_id,(select curr_name from Currency where Currency.curr_id=[Branchs].curr_id2) curr_name2,curr_name FROM [dbo].[Branchs],company,Currency where Currency.curr_id=Branchs.curr_id and company.comp_id=[Branchs].comp_id and [Branchs].comp_id=@compid order by bra_id asc", CompanyForm.mConnection);
				CompanyForm.mAdatpter.SelectCommand.Parameters.AddWithValue("@compid", this.compid);
				CompanyForm.mAdatpter.Fill(this.dtOrder);
				bool flag = this.dtOrder.Rows.Count == 0;
				if (!flag)
				{
					this.add_coulmBranch();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public void add_coulmBranch()
		{
			try
			{
				for (int i = 0; i < this.dtOrder.Rows.Count; i++)
				{
					this.dataGridView2.Rows.Add(new object[]
					{
						this.dtOrder.Rows[i]["BRA_ID"].ToString(),
						this.dtOrder.Rows[i]["BRA_NAME"].ToString(),
						this.dtOrder.Rows[i]["COMP_NAME"].ToString(),
						this.dtOrder.Rows[i]["ACCOUNT_NO"].ToString(),
						this.dtOrder.Rows[i]["curr_name"].ToString(),
						this.dtOrder.Rows[i]["curr_name2"].ToString()
					});
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void ClearControls()
		{
			this.textBox1_compname.Text = "";
			this.textBox_comp_id.Text = "";
			this.textBox1BraId.Text = "";
			this.textBox2Braname.Text = "";
			this.textBox3AccountNo.Text = "";
		}

		private void Fill_Currency()
		{
			try
			{
				CompanyForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT [curr_id],[curr_name] FROM [dbo].[Currency] order by [curr_id] asc", CompanyForm.mConnection);
				DataTable dataTable = new DataTable();
				sqlDataAdapter.Fill(dataTable);
				DataRow dataRow = dataTable.NewRow();
				dataRow[0] = 0;
				dataRow[1] = "-العملة-";
				dataTable.Rows.InsertAt(dataRow, 0);
				this.comboBox2Currency.DataSource = dataTable;
				this.comboBox2Currency.DisplayMember = "curr_name";
				this.comboBox2Currency.ValueMember = "curr_id";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}
		}

		private void Fill_Currency2()
		{
			try
			{
				CompanyForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT [curr_id],[curr_name] FROM [dbo].[Currency] order by [curr_id] asc", CompanyForm.mConnection);
				DataTable dataTable = new DataTable();
				sqlDataAdapter.Fill(dataTable);
				DataRow dataRow = dataTable.NewRow();
				dataRow[0] = 0;
				dataRow[1] = "-العملة-";
				dataTable.Rows.InsertAt(dataRow, 0);
				this.comboBox2Currency2.DataSource = dataTable;
				this.comboBox2Currency2.DisplayMember = "curr_name";
				this.comboBox2Currency2.ValueMember = "curr_id";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}
		}

		private void CompanyForm_Load(object sender, EventArgs e)
		{
		}

		private void button3_Click(object sender, EventArgs e)
		{
			try
			{
				bool flag = this.textBox1_compname.Text != "" && this.textBox_comp_id.Text != "";
				if (flag)
				{
					this.mCommand = new SqlCommand("INSERT INTO [dbo].[Company] ([comp_id],[comp_name]) VALUES(@id,@name)", CompanyForm.mConnection);
					CompanyForm.mConnection.Open();
					this.mCommand.Parameters.AddWithValue("@id", this.textBox_comp_id.Text);
					this.mCommand.Parameters.AddWithValue("@name", this.textBox1_compname.Text);
					this.mCommand.ExecuteNonQuery();
					CompanyForm.mConnection.Close();
					CompanyForm.mConnection.Dispose();
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
				CompanyForm.mConnection.Close();
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				bool flag = this.textBox2Braname.Text != "" && this.compid.Length != 0 && this.comboBox2Currency.SelectedIndex.ToString() != "0" && this.comboBox2Currency2.SelectedIndex.ToString() != "0";
				if (flag)
				{
					this.mCommand = new SqlCommand("INSERT INTO [dbo].[Branchs](bra_id,[bra_name],[comp_id],[account_no],curr_id,curr_id2) VALUES(@id,@name,@comp,@accno,@curr1,@curr2)", CompanyForm.mConnection);
					CompanyForm.mConnection.Open();
					this.mCommand.Parameters.AddWithValue("@id", this.textBox1BraId.Text);
					this.mCommand.Parameters.AddWithValue("@name", this.textBox2Braname.Text);
					this.mCommand.Parameters.AddWithValue("@comp", this.compid);
					this.mCommand.Parameters.AddWithValue("@accno", this.textBox3AccountNo.Text);
					this.mCommand.Parameters.AddWithValue("@curr1", this.comboBox2Currency.SelectedValue);
					this.mCommand.Parameters.AddWithValue("@curr2", this.comboBox2Currency2.SelectedValue);
					this.mCommand.ExecuteNonQuery();
					CompanyForm.mConnection.Close();
					MessageBox.Show(" تم حفظ البيانات بنجاح");
					this.DisplayDataBranch();
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
				CompanyForm.mConnection.Close();
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.DisplayDataBranch();
		}

		private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			this.compid = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
			this.compname = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
			this.textBox1comp.Text = this.compname;
			this.Fill_Currency();
			this.Fill_Currency2();
			this.DisplayDataBranch();
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
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.splitContainer1 = new SplitContainer();
			this.label9 = new Label();
			this.button3 = new Button();
			this.textBox_comp_id = new TextBox();
			this.label3 = new Label();
			this.textBox1_compname = new TextBox();
			this.label1 = new Label();
			this.dataGridView1 = new DataGridView();
			this.Column1 = new DataGridViewTextBoxColumn();
			this.Column2 = new DataGridViewTextBoxColumn();
			this.splitContainer2 = new SplitContainer();
			this.label10 = new Label();
			this.comboBox2Currency2 = new ComboBox();
			this.label8 = new Label();
			this.comboBox2Currency = new ComboBox();
			this.label7 = new Label();
			this.textBox1comp = new TextBox();
			this.textBox3AccountNo = new TextBox();
			this.label5 = new Label();
			this.label6 = new Label();
			this.button1 = new Button();
			this.textBox1BraId = new TextBox();
			this.label2 = new Label();
			this.textBox2Braname = new TextBox();
			this.label4 = new Label();
			this.dataGridView2 = new DataGridView();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.Column3 = new DataGridViewTextBoxColumn();
			this.Column4 = new DataGridViewTextBoxColumn();
			this.Column5 = new DataGridViewTextBoxColumn();
			this.Column6 = new DataGridViewTextBoxColumn();
			this.panel1 = new Panel();
			this.pictureBox1 = new PictureBox();
			this.button4 = new Button();
			this.tableLayoutPanel1.SuspendLayout();
			((ISupportInitialize)this.splitContainer1).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			((ISupportInitialize)this.splitContainer2).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((ISupportInitialize)this.dataGridView2).BeginInit();
			this.panel1.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.splitContainer2, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.button4, 0, 0);
			this.tableLayoutPanel1.Dock = DockStyle.Fill;
			this.tableLayoutPanel1.Location = new Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 90f));
			this.tableLayoutPanel1.Size = new Size(1850, 1021);
			this.tableLayoutPanel1.TabIndex = 0;
			this.splitContainer1.BorderStyle = BorderStyle.FixedSingle;
			this.splitContainer1.Dock = DockStyle.Fill;
			this.splitContainer1.Location = new Point(928, 105);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = Orientation.Horizontal;
			this.splitContainer1.Panel1.BackColor = Color.LightSteelBlue;
			this.splitContainer1.Panel1.Controls.Add(this.label9);
			this.splitContainer1.Panel1.Controls.Add(this.button3);
			this.splitContainer1.Panel1.Controls.Add(this.textBox_comp_id);
			this.splitContainer1.Panel1.Controls.Add(this.label3);
			this.splitContainer1.Panel1.Controls.Add(this.textBox1_compname);
			this.splitContainer1.Panel1.Controls.Add(this.label1);
			this.splitContainer1.Panel1.RightToLeft = RightToLeft.Yes;
			this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
			this.splitContainer1.Panel2.RightToLeft = RightToLeft.Yes;
			this.splitContainer1.Size = new Size(919, 913);
			this.splitContainer1.SplitterDistance = 338;
			this.splitContainer1.TabIndex = 0;
			this.label9.AutoSize = true;
			this.label9.Font = new Font("Arial", 16f, FontStyle.Bold);
			this.label9.Location = new Point(279, 30);
			this.label9.Name = "label9";
			this.label9.Size = new Size(179, 37);
			this.label9.TabIndex = 141;
			this.label9.Text = "بيانات الشركات";
			this.button3.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 0);
			this.button3.Font = new Font("Arial", 12f, FontStyle.Bold);
			this.button3.Location = new Point(321, 165);
			this.button3.Name = "button3";
			this.button3.Size = new Size(134, 37);
			this.button3.TabIndex = 89;
			this.button3.Text = "اضافة";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new EventHandler(this.button3_Click);
			this.textBox_comp_id.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBox_comp_id.Location = new Point(120, 80);
			this.textBox_comp_id.Name = "textBox_comp_id";
			this.textBox_comp_id.Size = new Size(395, 33);
			this.textBox_comp_id.TabIndex = 87;
			this.textBox_comp_id.TextAlign = HorizontalAlignment.Center;
			this.label3.AutoSize = true;
			this.label3.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label3.Location = new Point(524, 80);
			this.label3.Name = "label3";
			this.label3.Size = new Size(101, 29);
			this.label3.TabIndex = 86;
			this.label3.Text = "رقم الشركة";
			this.textBox1_compname.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBox1_compname.Location = new Point(120, 126);
			this.textBox1_compname.Name = "textBox1_compname";
			this.textBox1_compname.Size = new Size(395, 33);
			this.textBox1_compname.TabIndex = 88;
			this.textBox1_compname.TextAlign = HorizontalAlignment.Center;
			this.label1.AutoSize = true;
			this.label1.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label1.Location = new Point(521, 127);
			this.label1.Name = "label1";
			this.label1.Size = new Size(104, 29);
			this.label1.TabIndex = 84;
			this.label1.Text = "اسم الشركة";
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
				this.Column1,
				this.Column2
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
			this.dataGridView1.Location = new Point(0, 0);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowTemplate.Height = 29;
			this.dataGridView1.Size = new Size(917, 569);
			this.dataGridView1.TabIndex = 0;
			this.dataGridView1.RowHeaderMouseClick += new DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
			this.Column1.HeaderText = "الرقم";
			this.Column1.Name = "Column1";
			this.Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.Column2.HeaderText = "الاسم";
			this.Column2.Name = "Column2";
			this.splitContainer2.BorderStyle = BorderStyle.FixedSingle;
			this.splitContainer2.Dock = DockStyle.Fill;
			this.splitContainer2.Location = new Point(3, 105);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = Orientation.Horizontal;
			this.splitContainer2.Panel1.BackColor = Color.LightSteelBlue;
			this.splitContainer2.Panel1.Controls.Add(this.label10);
			this.splitContainer2.Panel1.Controls.Add(this.comboBox2Currency2);
			this.splitContainer2.Panel1.Controls.Add(this.label8);
			this.splitContainer2.Panel1.Controls.Add(this.comboBox2Currency);
			this.splitContainer2.Panel1.Controls.Add(this.label7);
			this.splitContainer2.Panel1.Controls.Add(this.textBox1comp);
			this.splitContainer2.Panel1.Controls.Add(this.textBox3AccountNo);
			this.splitContainer2.Panel1.Controls.Add(this.label5);
			this.splitContainer2.Panel1.Controls.Add(this.label6);
			this.splitContainer2.Panel1.Controls.Add(this.button1);
			this.splitContainer2.Panel1.Controls.Add(this.textBox1BraId);
			this.splitContainer2.Panel1.Controls.Add(this.label2);
			this.splitContainer2.Panel1.Controls.Add(this.textBox2Braname);
			this.splitContainer2.Panel1.Controls.Add(this.label4);
			this.splitContainer2.Panel1.RightToLeft = RightToLeft.Yes;
			this.splitContainer2.Panel2.Controls.Add(this.dataGridView2);
			this.splitContainer2.Panel2.RightToLeft = RightToLeft.Yes;
			this.splitContainer2.Size = new Size(919, 913);
			this.splitContainer2.SplitterDistance = 337;
			this.splitContainer2.TabIndex = 1;
			this.label10.AutoSize = true;
			this.label10.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label10.Location = new Point(446, 239);
			this.label10.Name = "label10";
			this.label10.Size = new Size(114, 29);
			this.label10.TabIndex = 146;
			this.label10.Text = "العملة-جنوب";
			this.comboBox2Currency2.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox2Currency2.FlatStyle = FlatStyle.Flat;
			this.comboBox2Currency2.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.comboBox2Currency2.FormattingEnabled = true;
			this.comboBox2Currency2.Location = new Point(161, 236);
			this.comboBox2Currency2.Name = "comboBox2Currency2";
			this.comboBox2Currency2.Size = new Size(279, 37);
			this.comboBox2Currency2.TabIndex = 145;
			this.label8.AutoSize = true;
			this.label8.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label8.Location = new Point(446, 196);
			this.label8.Name = "label8";
			this.label8.Size = new Size(109, 29);
			this.label8.TabIndex = 144;
			this.label8.Text = "العملة-شمال";
			this.comboBox2Currency.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox2Currency.FlatStyle = FlatStyle.Flat;
			this.comboBox2Currency.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.comboBox2Currency.FormattingEnabled = true;
			this.comboBox2Currency.Location = new Point(161, 193);
			this.comboBox2Currency.Name = "comboBox2Currency";
			this.comboBox2Currency.Size = new Size(279, 37);
			this.comboBox2Currency.TabIndex = 143;
			this.label7.AutoSize = true;
			this.label7.Font = new Font("Arial", 16f, FontStyle.Bold);
			this.label7.Location = new Point(211, -5);
			this.label7.Name = "label7";
			this.label7.Size = new Size(160, 37);
			this.label7.TabIndex = 142;
			this.label7.Text = "بيانات الفروع";
			this.textBox1comp.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBox1comp.Location = new Point(161, 38);
			this.textBox1comp.Name = "textBox1comp";
			this.textBox1comp.ReadOnly = true;
			this.textBox1comp.Size = new Size(279, 33);
			this.textBox1comp.TabIndex = 108;
			this.textBox1comp.TextAlign = HorizontalAlignment.Center;
			this.textBox3AccountNo.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBox3AccountNo.Location = new Point(161, 157);
			this.textBox3AccountNo.Name = "textBox3AccountNo";
			this.textBox3AccountNo.Size = new Size(279, 33);
			this.textBox3AccountNo.TabIndex = 111;
			this.textBox3AccountNo.TextAlign = HorizontalAlignment.Center;
			this.label5.AutoSize = true;
			this.label5.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label5.Location = new Point(446, 158);
			this.label5.Name = "label5";
			this.label5.Size = new Size(107, 29);
			this.label5.TabIndex = 109;
			this.label5.Text = "رقم الحساب";
			this.label6.AutoSize = true;
			this.label6.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label6.Location = new Point(446, 42);
			this.label6.Name = "label6";
			this.label6.Size = new Size(68, 29);
			this.label6.TabIndex = 107;
			this.label6.Text = "الشركة";
			this.button1.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 0);
			this.button1.Font = new Font("Arial", 12f, FontStyle.Bold);
			this.button1.Location = new Point(230, 278);
			this.button1.Name = "button1";
			this.button1.Size = new Size(134, 37);
			this.button1.TabIndex = 112;
			this.button1.Text = "اضافة";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.textBox1BraId.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBox1BraId.Location = new Point(161, 77);
			this.textBox1BraId.Name = "textBox1BraId";
			this.textBox1BraId.Size = new Size(279, 33);
			this.textBox1BraId.TabIndex = 109;
			this.textBox1BraId.TextAlign = HorizontalAlignment.Center;
			this.label2.AutoSize = true;
			this.label2.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label2.Location = new Point(446, 81);
			this.label2.Name = "label2";
			this.label2.Size = new Size(88, 29);
			this.label2.TabIndex = 91;
			this.label2.Text = "رقم الفرع";
			this.textBox2Braname.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBox2Braname.Location = new Point(161, 118);
			this.textBox2Braname.Name = "textBox2Braname";
			this.textBox2Braname.Size = new Size(279, 33);
			this.textBox2Braname.TabIndex = 110;
			this.textBox2Braname.TextAlign = HorizontalAlignment.Center;
			this.label4.AutoSize = true;
			this.label4.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label4.Location = new Point(446, 119);
			this.label4.Name = "label4";
			this.label4.Size = new Size(54, 29);
			this.label4.TabIndex = 90;
			this.label4.Text = "الاسم";
			this.dataGridView2.AllowUserToDeleteRows = false;
			dataGridViewCellStyle4.BackColor = Color.WhiteSmoke;
			dataGridViewCellStyle4.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.dataGridView2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridView2.BackgroundColor = SystemColors.Control;
			dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle5.BackColor = SystemColors.Control;
			dataGridViewCellStyle5.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
			this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
			this.dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView2.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn1,
				this.dataGridViewTextBoxColumn2,
				this.Column3,
				this.Column4,
				this.Column5,
				this.Column6
			});
			dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle6.BackColor = SystemColors.Window;
			dataGridViewCellStyle6.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			dataGridViewCellStyle6.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
			this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle6;
			this.dataGridView2.Dock = DockStyle.Fill;
			this.dataGridView2.Location = new Point(0, 0);
			this.dataGridView2.Name = "dataGridView2";
			this.dataGridView2.RowTemplate.Height = 29;
			this.dataGridView2.Size = new Size(917, 570);
			this.dataGridView2.TabIndex = 1;
			this.dataGridViewTextBoxColumn1.HeaderText = "الرقم";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn2.HeaderText = "الاسم";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.Column3.HeaderText = "الشركة";
			this.Column3.Name = "Column3";
			this.Column3.Visible = false;
			this.Column4.HeaderText = "رقم الحساب";
			this.Column4.Name = "Column4";
			this.Column5.HeaderText = "العملة-شمال";
			this.Column5.Name = "Column5";
			this.Column6.HeaderText = "العملة-جنوب";
			this.Column6.Name = "Column6";
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Dock = DockStyle.Fill;
			this.panel1.Location = new Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(919, 96);
			this.panel1.TabIndex = 2;
			this.pictureBox1.BorderStyle = BorderStyle.FixedSingle;
			this.pictureBox1.Dock = DockStyle.Fill;
			this.pictureBox1.Image = Resources.waqood;
			this.pictureBox1.Location = new Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new Size(919, 96);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.button4.Dock = DockStyle.Fill;
			this.button4.Enabled = false;
			this.button4.FlatStyle = FlatStyle.Flat;
			this.button4.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.button4.Location = new Point(928, 3);
			this.button4.Name = "button4";
			this.button4.Size = new Size(919, 96);
			this.button4.TabIndex = 3;
			this.button4.Text = "الشركات";
			this.button4.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new SizeF(9f, 19f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(1850, 1021);
			base.Controls.Add(this.tableLayoutPanel1);
			base.Name = "CompanyForm";
			this.RightToLeft = RightToLeft.Yes;
			this.Text = "ترميز الشركات والفروع";
			base.Load += new EventHandler(this.CompanyForm_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((ISupportInitialize)this.splitContainer1).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((ISupportInitialize)this.dataGridView1).EndInit();
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel1.PerformLayout();
			this.splitContainer2.Panel2.ResumeLayout(false);
			((ISupportInitialize)this.splitContainer2).EndInit();
			this.splitContainer2.ResumeLayout(false);
			((ISupportInitialize)this.dataGridView2).EndInit();
			this.panel1.ResumeLayout(false);
			((ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
		}
	}
}
