using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WAQOOD.Properties;

namespace WAQOOD
{
	public class StationForm : Form
	{
		private string staid = string.Empty;

		private string staname = string.Empty;

		private string staoil = string.Empty;

		private SqlCommand mCommand = null;

		private static SqlDataAdapter mAdatpter = null;

		private static SqlConnection mConnection = new SqlConnection(MainForm.str_conn);

		private DataTable dtOrder;

		private IContainer components = null;

		private DataGridView dataGridView2;

		private SplitContainer splitContainer2;

		private Label label5;

		private Label label6;

		private Button button1;

		private TextBox textBoxsta_t_id;

		private Label label2;

		private TextBox textBoxsta_t_name;

		private Label label4;

		private TableLayoutPanel tableLayoutPanel1;

		private SplitContainer splitContainer1;

		private Button button3;

		private TextBox textBox_sta_id;

		private Label label3;

		private TextBox textBox1_sta_name;

		private Label label1;

		private DataGridView dataGridView1;

		private Label label8;

		private ComboBox comboBox2Currency;

		private Label label7;

		private ComboBox comboBox1Gov;

		private TextBox textBox1staname;

		private ComboBox comboBoxProd;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn Column3;

		private PictureBox pictureBox1;

		private Button button4;

		private ComboBox comboBoxprodst;

		private Label label9;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn Column2;

		private DataGridViewTextBoxColumn Column4;

		private DataGridViewTextBoxColumn Column5;

		private DataGridViewTextBoxColumn Column6;

		private DataGridViewTextBoxColumn Column7;

		public StationForm()
		{
			this.InitializeComponent();
			base.WindowState = FormWindowState.Maximized;
			StationForm.mConnection = new SqlConnection(MainForm.str_conn);
			this.dtOrder = new DataTable();
			this.DisplayData();
			this.Fill_Goverments();
			this.Fill_ProductsSta();
			this.Fill_Regn();
		}

		private void Fill_Regn()
		{
			List<Regn> list = new List<Regn>();
			list.Add(new Regn
			{
				ID = 0,
				Name = "-المنطقة-"
			});
			list.Add(new Regn
			{
				ID = 1,
				Name = "شمال"
			});
			list.Add(new Regn
			{
				ID = 2,
				Name = "جنوب"
			});
			this.comboBox2Currency.DataSource = list;
			this.comboBox2Currency.ValueMember = "ID";
			this.comboBox2Currency.DisplayMember = "Name";
			this.comboBox2Currency.SelectedIndex = 0;
		}

		public void DisplayData()
		{
			try
			{
				this.dtOrder.Clear();
				this.dtOrder.Clone();
				this.dataGridView1.Rows.Clear();
				this.dataGridView1.Refresh();
				StationForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlConnection.ClearPool(StationForm.mConnection);
				StationForm.mAdatpter = new SqlDataAdapter("SELECT [sta_id],[sta_name],[gov_name],CASE regn WHEN '1' then 'شمال' when 2 then 'جنوب' end regn,Stations.prod_id,prod_name FROM Products,[dbo].[Stations],Goverments where Products.prod_id=Stations.prod_id and [Stations].[gov_id]=Goverments.[gov_id] order by [sta_id] asc", StationForm.mConnection);
				StationForm.mAdatpter.Fill(this.dtOrder);
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
						this.dtOrder.Rows[i]["sta_ID"].ToString(),
						this.dtOrder.Rows[i]["sta_NAME"].ToString(),
						this.dtOrder.Rows[i]["prod_name"].ToString(),
						this.dtOrder.Rows[i]["gov_NAME"].ToString(),
						this.dtOrder.Rows[i]["regn"].ToString(),
						this.dtOrder.Rows[i]["prod_id"].ToString()
					});
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Fill_Goverments()
		{
			try
			{
				StationForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT [gov_id],[gov_name] FROM [dbo].[Goverments] order by gov_id asc", StationForm.mConnection);
				DataTable dataTable = new DataTable();
				sqlDataAdapter.Fill(dataTable);
				DataRow dataRow = dataTable.NewRow();
				dataRow[0] = 0;
				dataRow[1] = "-المحافظة-";
				dataTable.Rows.InsertAt(dataRow, 0);
				this.comboBox1Gov.DataSource = dataTable;
				this.comboBox1Gov.DisplayMember = "gov_name";
				this.comboBox1Gov.ValueMember = "gov_id";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}
		}

		private void Fill_Currency()
		{
			try
			{
				StationForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT [curr_id],[curr_name] FROM [dbo].[Currency] order by [curr_id] asc", StationForm.mConnection);
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

		private void Fill_Products()
		{
			try
			{
				StationForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT [prod_id],[prod_name] FROM [dbo].[products] order by [prod_id] asc", StationForm.mConnection);
				DataTable dataTable = new DataTable();
				sqlDataAdapter.Fill(dataTable);
				DataRow dataRow = dataTable.NewRow();
				dataRow[0] = 0;
				dataRow[1] = "-نوع الوقود-";
				dataTable.Rows.InsertAt(dataRow, 0);
				this.comboBoxProd.DataSource = dataTable;
				this.comboBoxProd.DisplayMember = "prod_name";
				this.comboBoxProd.ValueMember = "prod_id";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}
		}

		private void Fill_ProductsSta()
		{
			try
			{
				StationForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT [prod_id],[prod_name] FROM [dbo].[products] order by [prod_id] asc", StationForm.mConnection);
				DataTable dataTable = new DataTable();
				sqlDataAdapter.Fill(dataTable);
				DataRow dataRow = dataTable.NewRow();
				dataRow[0] = 0;
				dataRow[1] = "-نوع الوقود-";
				dataTable.Rows.InsertAt(dataRow, 0);
				this.comboBoxprodst.DataSource = dataTable;
				this.comboBoxprodst.DisplayMember = "prod_name";
				this.comboBoxprodst.ValueMember = "prod_id";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			try
			{
				bool flag = this.textBox1_sta_name.Text != "" && this.comboBox1Gov.SelectedIndex.ToString() != "0" && this.comboBox2Currency.SelectedIndex.ToString() != "0";
				if (flag)
				{
					this.mCommand = new SqlCommand("INSERT INTO [dbo].[Stations](prod_id,[sta_name],[gov_id],[regn]) VALUES(@prodd,@sname,@gov,@regnn)", StationForm.mConnection);
					StationForm.mConnection.Open();
					this.mCommand.Parameters.AddWithValue("@sname", this.textBox1_sta_name.Text);
					this.mCommand.Parameters.AddWithValue("@gov", this.comboBox1Gov.SelectedValue);
					this.mCommand.Parameters.AddWithValue("@regnn", this.comboBox2Currency.SelectedValue);
					this.mCommand.Parameters.AddWithValue("@prodd", this.comboBoxprodst.SelectedValue);
					this.mCommand.ExecuteNonQuery();
					StationForm.mConnection.Close();
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
				StationForm.mConnection.Close();
			}
		}

		private void ClearControls()
		{
			this.textBox1_sta_name.Text = "";
			this.textBox_sta_id.Text = "";
			this.textBoxsta_t_id.Text = "";
			this.textBoxsta_t_name.Text = "";
		}

		private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			bool flag = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() != null;
			if (flag)
			{
				this.staid = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
				this.staname = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
				this.textBox1staname.Text = this.staname;
				this.staoil = this.dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
				this.Fill_Products();
				this.comboBoxProd.SelectedValue = this.staoil;
				this.comboBoxProd.Enabled = false;
				this.DisplayDataT();
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				bool flag = this.textBoxsta_t_id.Text != "" && this.textBoxsta_t_name.Text != "" && this.textBox1staname.Text != "" && this.comboBoxProd.SelectedIndex.ToString() != "0";
				if (flag)
				{
					this.mCommand = new SqlCommand("INSERT INTO [dbo].[Station_t](sta_t_id,[sta_t_name],[sta_id],[prod_id]) VALUES(@tid,@tname,@sid,@prod)", StationForm.mConnection);
					StationForm.mConnection.Open();
					this.mCommand.Parameters.AddWithValue("@tid", this.textBoxsta_t_id.Text);
					this.mCommand.Parameters.AddWithValue("@tname", this.textBoxsta_t_name.Text);
					this.mCommand.Parameters.AddWithValue("@sid", this.staid);
					this.mCommand.Parameters.AddWithValue("@prod", this.comboBoxProd.SelectedValue);
					this.mCommand.ExecuteNonQuery();
					StationForm.mConnection.Close();
					MessageBox.Show(" تم حفظ البيانات بنجاح");
					this.DisplayDataT();
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
				StationForm.mConnection.Close();
			}
		}

		private void StationForm_Load(object sender, EventArgs e)
		{
		}

		public void DisplayDataT()
		{
			try
			{
				this.dtOrder.Clear();
				this.dtOrder.Clone();
				this.dataGridView2.Rows.Clear();
				this.dataGridView2.Refresh();
				StationForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlConnection.ClearPool(StationForm.mConnection);
				StationForm.mAdatpter = new SqlDataAdapter("SELECT [sta_t_id],[sta_t_name],[prod_name] FROM [dbo].[Station_t],products where [sta_id]=@stid and [Station_t].prod_id=products.prod_id order by [sta_t_id] asc", StationForm.mConnection);
				StationForm.mAdatpter.SelectCommand.Parameters.AddWithValue("@stid", this.staid);
				StationForm.mAdatpter.Fill(this.dtOrder);
				bool flag = this.dtOrder.Rows.Count == 0;
				if (!flag)
				{
					this.add_coulmT();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public void add_coulmT()
		{
			try
			{
				for (int i = 0; i < this.dtOrder.Rows.Count; i++)
				{
					this.dataGridView2.Rows.Add(new object[]
					{
						this.dtOrder.Rows[i]["sta_t_ID"].ToString(),
						this.dtOrder.Rows[i]["sta_t_NAME"].ToString(),
						this.dtOrder.Rows[i]["prod_NAME"].ToString()
					});
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
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
			this.dataGridView2 = new DataGridView();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.Column3 = new DataGridViewTextBoxColumn();
			this.splitContainer2 = new SplitContainer();
			this.comboBoxProd = new ComboBox();
			this.textBox1staname = new TextBox();
			this.label5 = new Label();
			this.label6 = new Label();
			this.button1 = new Button();
			this.textBoxsta_t_id = new TextBox();
			this.label2 = new Label();
			this.textBoxsta_t_name = new TextBox();
			this.label4 = new Label();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.button4 = new Button();
			this.splitContainer1 = new SplitContainer();
			this.comboBoxprodst = new ComboBox();
			this.label9 = new Label();
			this.label8 = new Label();
			this.comboBox2Currency = new ComboBox();
			this.label7 = new Label();
			this.comboBox1Gov = new ComboBox();
			this.button3 = new Button();
			this.textBox_sta_id = new TextBox();
			this.label3 = new Label();
			this.textBox1_sta_name = new TextBox();
			this.label1 = new Label();
			this.dataGridView1 = new DataGridView();
			this.Column1 = new DataGridViewTextBoxColumn();
			this.Column2 = new DataGridViewTextBoxColumn();
			this.Column4 = new DataGridViewTextBoxColumn();
			this.Column5 = new DataGridViewTextBoxColumn();
			this.Column6 = new DataGridViewTextBoxColumn();
			this.Column7 = new DataGridViewTextBoxColumn();
			this.pictureBox1 = new PictureBox();
			((ISupportInitialize)this.dataGridView2).BeginInit();
			((ISupportInitialize)this.splitContainer2).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((ISupportInitialize)this.splitContainer1).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.dataGridView2.AllowUserToDeleteRows = false;
			dataGridViewCellStyle.BackColor = Color.WhiteSmoke;
			dataGridViewCellStyle.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.dataGridView2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle;
			this.dataGridView2.BackgroundColor = SystemColors.Control;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.BackColor = SystemColors.Control;
			dataGridViewCellStyle2.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
			this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView2.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn1,
				this.dataGridViewTextBoxColumn2,
				this.Column3
			});
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle3.BackColor = SystemColors.Window;
			dataGridViewCellStyle3.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
			this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridView2.Dock = DockStyle.Fill;
			this.dataGridView2.Location = new Point(0, 0);
			this.dataGridView2.Name = "dataGridView2";
			this.dataGridView2.RowTemplate.Height = 29;
			this.dataGridView2.Size = new Size(901, 529);
			this.dataGridView2.TabIndex = 1;
			this.dataGridViewTextBoxColumn1.HeaderText = "الرقم";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn2.HeaderText = "الاسم";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.Column3.HeaderText = "الصنف";
			this.Column3.Name = "Column3";
			this.splitContainer2.BorderStyle = BorderStyle.FixedSingle;
			this.splitContainer2.Dock = DockStyle.Fill;
			this.splitContainer2.Location = new Point(3, 96);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = Orientation.Horizontal;
			this.splitContainer2.Panel1.BackColor = Color.LightSteelBlue;
			this.splitContainer2.Panel1.Controls.Add(this.comboBoxProd);
			this.splitContainer2.Panel1.Controls.Add(this.textBox1staname);
			this.splitContainer2.Panel1.Controls.Add(this.label5);
			this.splitContainer2.Panel1.Controls.Add(this.label6);
			this.splitContainer2.Panel1.Controls.Add(this.button1);
			this.splitContainer2.Panel1.Controls.Add(this.textBoxsta_t_id);
			this.splitContainer2.Panel1.Controls.Add(this.label2);
			this.splitContainer2.Panel1.Controls.Add(this.textBoxsta_t_name);
			this.splitContainer2.Panel1.Controls.Add(this.label4);
			this.splitContainer2.Panel1.RightToLeft = RightToLeft.Yes;
			this.splitContainer2.Panel2.Controls.Add(this.dataGridView2);
			this.splitContainer2.Panel2.RightToLeft = RightToLeft.Yes;
			this.splitContainer2.Size = new Size(903, 835);
			this.splitContainer2.SplitterDistance = 300;
			this.splitContainer2.TabIndex = 1;
			this.comboBoxProd.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxProd.FlatStyle = FlatStyle.Flat;
			this.comboBoxProd.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.comboBoxProd.FormattingEnabled = true;
			this.comboBoxProd.Location = new Point(161, 149);
			this.comboBoxProd.Name = "comboBoxProd";
			this.comboBoxProd.Size = new Size(279, 37);
			this.comboBoxProd.TabIndex = 123;
			this.textBox1staname.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBox1staname.Location = new Point(161, 26);
			this.textBox1staname.Name = "textBox1staname";
			this.textBox1staname.ReadOnly = true;
			this.textBox1staname.Size = new Size(279, 33);
			this.textBox1staname.TabIndex = 114;
			this.textBox1staname.TextAlign = HorizontalAlignment.Center;
			this.label5.AutoSize = true;
			this.label5.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label5.Location = new Point(448, 152);
			this.label5.Name = "label5";
			this.label5.Size = new Size(99, 29);
			this.label5.TabIndex = 109;
			this.label5.Text = "نوع الوقود";
			this.label6.AutoSize = true;
			this.label6.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label6.Location = new Point(446, 30);
			this.label6.Name = "label6";
			this.label6.Size = new Size(70, 29);
			this.label6.TabIndex = 107;
			this.label6.Text = "المحطة";
			this.button1.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 0);
			this.button1.Font = new Font("Arial", 12f, FontStyle.Bold);
			this.button1.Location = new Point(229, 188);
			this.button1.Name = "button1";
			this.button1.Size = new Size(134, 37);
			this.button1.TabIndex = 112;
			this.button1.Text = "اضافة";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.textBoxsta_t_id.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBoxsta_t_id.Location = new Point(161, 69);
			this.textBoxsta_t_id.Name = "textBoxsta_t_id";
			this.textBoxsta_t_id.Size = new Size(279, 33);
			this.textBoxsta_t_id.TabIndex = 109;
			this.textBoxsta_t_id.TextAlign = HorizontalAlignment.Center;
			this.label2.AutoSize = true;
			this.label2.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label2.Location = new Point(446, 73);
			this.label2.Name = "label2";
			this.label2.Size = new Size(107, 29);
			this.label2.TabIndex = 91;
			this.label2.Text = "رقم الطرمبة";
			this.textBoxsta_t_name.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBoxsta_t_name.Location = new Point(161, 110);
			this.textBoxsta_t_name.Name = "textBoxsta_t_name";
			this.textBoxsta_t_name.Size = new Size(279, 33);
			this.textBoxsta_t_name.TabIndex = 110;
			this.textBoxsta_t_name.TextAlign = HorizontalAlignment.Center;
			this.label4.AutoSize = true;
			this.label4.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label4.Location = new Point(446, 111);
			this.label4.Name = "label4";
			this.label4.Size = new Size(110, 29);
			this.label4.TabIndex = 90;
			this.label4.Text = "اسم الطرمبة";
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel1.Controls.Add(this.button4, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.splitContainer2, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 1, 0);
			this.tableLayoutPanel1.Dock = DockStyle.Fill;
			this.tableLayoutPanel1.Location = new Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 90f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel1.Size = new Size(1817, 934);
			this.tableLayoutPanel1.TabIndex = 1;
			this.button4.Dock = DockStyle.Fill;
			this.button4.Enabled = false;
			this.button4.FlatStyle = FlatStyle.Flat;
			this.button4.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.button4.Location = new Point(912, 3);
			this.button4.Name = "button4";
			this.button4.Size = new Size(902, 87);
			this.button4.TabIndex = 4;
			this.button4.Text = "المحطات";
			this.button4.UseVisualStyleBackColor = true;
			this.splitContainer1.BorderStyle = BorderStyle.FixedSingle;
			this.splitContainer1.Dock = DockStyle.Fill;
			this.splitContainer1.Location = new Point(912, 96);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = Orientation.Horizontal;
			this.splitContainer1.Panel1.BackColor = Color.LightSteelBlue;
			this.splitContainer1.Panel1.Controls.Add(this.comboBoxprodst);
			this.splitContainer1.Panel1.Controls.Add(this.label9);
			this.splitContainer1.Panel1.Controls.Add(this.label8);
			this.splitContainer1.Panel1.Controls.Add(this.comboBox2Currency);
			this.splitContainer1.Panel1.Controls.Add(this.label7);
			this.splitContainer1.Panel1.Controls.Add(this.comboBox1Gov);
			this.splitContainer1.Panel1.Controls.Add(this.button3);
			this.splitContainer1.Panel1.Controls.Add(this.textBox_sta_id);
			this.splitContainer1.Panel1.Controls.Add(this.label3);
			this.splitContainer1.Panel1.Controls.Add(this.textBox1_sta_name);
			this.splitContainer1.Panel1.Controls.Add(this.label1);
			this.splitContainer1.Panel1.RightToLeft = RightToLeft.Yes;
			this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
			this.splitContainer1.Panel2.RightToLeft = RightToLeft.Yes;
			this.splitContainer1.Size = new Size(902, 835);
			this.splitContainer1.SplitterDistance = 300;
			this.splitContainer1.TabIndex = 0;
			this.comboBoxprodst.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxprodst.FlatStyle = FlatStyle.Flat;
			this.comboBoxprodst.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.comboBoxprodst.FormattingEnabled = true;
			this.comboBoxprodst.Location = new Point(236, 177);
			this.comboBoxprodst.Name = "comboBoxprodst";
			this.comboBoxprodst.Size = new Size(279, 37);
			this.comboBoxprodst.TabIndex = 91;
			this.label9.AutoSize = true;
			this.label9.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label9.Location = new Point(523, 180);
			this.label9.Name = "label9";
			this.label9.Size = new Size(99, 29);
			this.label9.TabIndex = 124;
			this.label9.Text = "نوع الوقود";
			this.label8.AutoSize = true;
			this.label8.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label8.Location = new Point(524, 137);
			this.label8.Name = "label8";
			this.label8.Size = new Size(72, 29);
			this.label8.TabIndex = 112;
			this.label8.Text = "المنطقة";
			this.comboBox2Currency.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox2Currency.FlatStyle = FlatStyle.Flat;
			this.comboBox2Currency.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.comboBox2Currency.FormattingEnabled = true;
			this.comboBox2Currency.Location = new Point(236, 134);
			this.comboBox2Currency.Name = "comboBox2Currency";
			this.comboBox2Currency.Size = new Size(279, 37);
			this.comboBox2Currency.TabIndex = 90;
			this.label7.AutoSize = true;
			this.label7.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label7.Location = new Point(521, 94);
			this.label7.Name = "label7";
			this.label7.Size = new Size(83, 29);
			this.label7.TabIndex = 110;
			this.label7.Text = "المحافظة";
			this.comboBox1Gov.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox1Gov.FlatStyle = FlatStyle.Flat;
			this.comboBox1Gov.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.comboBox1Gov.FormattingEnabled = true;
			this.comboBox1Gov.Location = new Point(236, 91);
			this.comboBox1Gov.Name = "comboBox1Gov";
			this.comboBox1Gov.Size = new Size(279, 37);
			this.comboBox1Gov.TabIndex = 89;
			this.button3.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 0);
			this.button3.Font = new Font("Arial", 12f, FontStyle.Bold);
			this.button3.Location = new Point(303, 220);
			this.button3.Name = "button3";
			this.button3.Size = new Size(134, 37);
			this.button3.TabIndex = 91;
			this.button3.Text = "اضافة";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new EventHandler(this.button3_Click);
			this.textBox_sta_id.BackColor = SystemColors.ButtonHighlight;
			this.textBox_sta_id.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBox_sta_id.Location = new Point(236, 13);
			this.textBox_sta_id.Name = "textBox_sta_id";
			this.textBox_sta_id.ReadOnly = true;
			this.textBox_sta_id.Size = new Size(279, 33);
			this.textBox_sta_id.TabIndex = 87;
			this.textBox_sta_id.TextAlign = HorizontalAlignment.Center;
			this.label3.AutoSize = true;
			this.label3.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label3.Location = new Point(524, 13);
			this.label3.Name = "label3";
			this.label3.Size = new Size(103, 29);
			this.label3.TabIndex = 86;
			this.label3.Text = "رقم المحطة";
			this.textBox1_sta_name.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBox1_sta_name.Location = new Point(236, 52);
			this.textBox1_sta_name.Name = "textBox1_sta_name";
			this.textBox1_sta_name.Size = new Size(279, 33);
			this.textBox1_sta_name.TabIndex = 88;
			this.textBox1_sta_name.TextAlign = HorizontalAlignment.Center;
			this.label1.AutoSize = true;
			this.label1.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label1.Location = new Point(521, 53);
			this.label1.Name = "label1";
			this.label1.Size = new Size(106, 29);
			this.label1.TabIndex = 84;
			this.label1.Text = "اسم المحطة";
			this.dataGridView1.AllowUserToDeleteRows = false;
			dataGridViewCellStyle4.BackColor = Color.WhiteSmoke;
			dataGridViewCellStyle4.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridView1.BackgroundColor = SystemColors.Control;
			dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle5.BackColor = SystemColors.Control;
			dataGridViewCellStyle5.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
			this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
			this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new DataGridViewColumn[]
			{
				this.Column1,
				this.Column2,
				this.Column4,
				this.Column5,
				this.Column6,
				this.Column7
			});
			dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle6.BackColor = SystemColors.Window;
			dataGridViewCellStyle6.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			dataGridViewCellStyle6.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
			this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle6;
			this.dataGridView1.Dock = DockStyle.Fill;
			this.dataGridView1.Location = new Point(0, 0);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowTemplate.Height = 29;
			this.dataGridView1.Size = new Size(900, 529);
			this.dataGridView1.TabIndex = 0;
			this.dataGridView1.RowHeaderMouseClick += new DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
			this.Column1.HeaderText = "الرقم";
			this.Column1.Name = "Column1";
			this.Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.Column2.HeaderText = "اسم المحطة";
			this.Column2.Name = "Column2";
			this.Column4.HeaderText = "الوقود";
			this.Column4.Name = "Column4";
			this.Column5.HeaderText = "المحافظة";
			this.Column5.Name = "Column5";
			this.Column6.HeaderText = "المنطقة";
			this.Column6.Name = "Column6";
			this.Column7.HeaderText = "prodid";
			this.Column7.Name = "Column7";
			this.Column7.Visible = false;
			this.pictureBox1.BorderStyle = BorderStyle.FixedSingle;
			this.pictureBox1.Dock = DockStyle.Fill;
			this.pictureBox1.Image = Resources.waqood;
			this.pictureBox1.Location = new Point(3, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new Size(903, 87);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			base.AutoScaleDimensions = new SizeF(9f, 19f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(1817, 934);
			base.Controls.Add(this.tableLayoutPanel1);
			base.Name = "StationForm";
			this.RightToLeft = RightToLeft.Yes;
			this.Text = "ترميز المحطات";
			base.Load += new EventHandler(this.StationForm_Load);
			((ISupportInitialize)this.dataGridView2).EndInit();
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel1.PerformLayout();
			this.splitContainer2.Panel2.ResumeLayout(false);
			((ISupportInitialize)this.splitContainer2).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((ISupportInitialize)this.splitContainer1).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((ISupportInitialize)this.dataGridView1).EndInit();
			((ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
		}
	}
}
