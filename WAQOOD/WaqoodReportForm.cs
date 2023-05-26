using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WAQOOD.Properties;

namespace WAQOOD
{
	public class WaqoodReportForm : Form
	{
		private int actv;

		private string carid = string.Empty;

		private string regnn = string.Empty;

		private string curridd = string.Empty;

		private string caramnt = string.Empty;

		private string statid = string.Empty;

		private string caramnt2 = string.Empty;

		private string caroil = string.Empty;

		private string ordDate = string.Empty;

		private string carcomp = string.Empty;

		private string carbra = string.Empty;

		private string cardriver = string.Empty;

		private string carno = string.Empty;

		private string compid = string.Empty;

		private string braid = string.Empty;

		private string transid = string.Empty;

		private string transidsta = string.Empty;

		private string transidbra = string.Empty;

		private string cardesc = string.Empty;

		private string carshasi = string.Empty;

		private string prcr = string.Empty;

		private string prcd = string.Empty;

		private string prcid = string.Empty;

		private SqlCommand mCommand = null;

		private static SqlDataAdapter mAdatpter = null;

		private static SqlConnection mConnection = new SqlConnection(MainForm.str_conn);

		private DataTable dtorders;

		private IContainer components = null;

		private Panel panel1;

		private PictureBox pictureBox1;

		private Panel panel5;

		private Panel panel3;

		private TableLayoutPanel tableLayoutPanel1;

		private Button button4;

		private Panel panel2;

		private Panel panel4;

		private DataGridView dataGridView1;

		private DataGridViewTextBoxColumn Column11;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn Column18;

		private DataGridViewTextBoxColumn Column3;

		private DataGridViewTextBoxColumn Column5;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn Column4;

		private DataGridViewTextBoxColumn Column6;

		private DataGridViewTextBoxColumn Column7;

		private DataGridViewTextBoxColumn Column8;

		private DataGridViewTextBoxColumn Column9;

		private DataGridViewTextBoxColumn Column12;

		private DataGridViewTextBoxColumn Column14;

		private DataGridViewTextBoxColumn Column15;

		private DataGridViewTextBoxColumn Column17;

		private DataGridViewTextBoxColumn Column13;

		private DataGridViewTextBoxColumn Column2;

		private ComboBox comboBoxComp;

		private Label label3;

		private ComboBox comboBoxStation;

		private Label label7;

		private PictureBox pictureBox2;

		private Label label2;

		private DateTimePicker dateTimePicker2;

		private Label label1;

		private DateTimePicker dateTimePicker1;

		public WaqoodReportForm()
		{
			this.InitializeComponent();
			base.WindowState = FormWindowState.Maximized;
			WaqoodReportForm.mConnection = new SqlConnection(MainForm.str_conn);
			this.dtorders = new DataTable();
			this.DisplayData();
		}

		private void pictureBox2_Click(object sender, EventArgs e)
		{
			this.DisplayDataAfterSearch();
		}

		public void DisplayDataAfterSearch()
		{
			try
			{
				this.dtorders.Clear();
				this.dtorders.Clone();
				this.dataGridView1.Rows.Clear();
				this.dataGridView1.Refresh();
				WaqoodReportForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlConnection.ClearPool(WaqoodReportForm.mConnection);
				bool flag = this.comboBoxComp.SelectedIndex != 0 && this.comboBoxStation.SelectedIndex != 0;
				if (flag)
				{
					WaqoodReportForm.mAdatpter = new SqlDataAdapter(string.Concat(new object[]
					{
						"select [Trans_table].regn,trans_id_sta,prc,sta_name,Trans_table.sta_id,sta_name,trans_id,trans_id_bra,CAST(trans_date AS DATE) trans_date,CAST(ORDER_DATE AS DATE) ORDER_DATE,[car_id],[car_id_bra],car_id_comp,[car_no],[amt2],[amt],ISNULL(amt2,0)*ISNULL(prc,0) totprc,Trans_table.[prod_id],prod_name,car_driver,Trans_table.curr_id,curr_name,Branchs.bra_id,Branchs.bra_name,Branchs.comp_id from [dbo].[Trans_table],Products,Stations,Currency,Branchs where [Trans_table].sta_id='",
						this.comboBoxStation.SelectedValue,
						"' and [Trans_table].bra_id='",
						this.comboBoxComp.SelectedValue,
						"' and CAST([trans_date] As date) between CAST('",
						this.dateTimePicker1.Value.ToString("yyyy-MM-dd"),
						"' As date) and CAST('",
						this.dateTimePicker2.Value.ToString("yyyy-MM-dd"),
						"' As date) and Branchs.bra_id=Trans_table.bra_id and Branchs.comp_id=Trans_table.comp_id and Currency.curr_id=Trans_table.curr_id and Trans_table.sta_id=Stations.sta_id and [trans_type] in(1,2,3) and flag='5' and [Trans_table].prod_id=Products.prod_id order by[trans_id] asc"
					}), WaqoodReportForm.mConnection);
				}
				else
				{
					bool flag2 = this.comboBoxStation.SelectedIndex != 0;
					if (flag2)
					{
						WaqoodReportForm.mAdatpter = new SqlDataAdapter(string.Concat(new object[]
						{
							"select [Trans_table].regn,trans_id_sta,prc,sta_name,Trans_table.sta_id,sta_name,trans_id,trans_id_bra,CAST(trans_date AS DATE) trans_date,CAST(ORDER_DATE AS DATE) ORDER_DATE,[car_id],[car_id_bra],car_id_comp,[car_no],[amt2],[amt],ISNULL(amt2,0)*ISNULL(prc,0) totprc,Trans_table.[prod_id],prod_name,car_driver,Trans_table.curr_id,curr_name,Branchs.bra_id,Branchs.bra_name,Branchs.comp_id from [dbo].[Trans_table],Products,Stations,Currency,Branchs where [Trans_table].sta_id='",
							this.comboBoxStation.SelectedValue,
							"' and CAST([trans_date] As date) between CAST('",
							this.dateTimePicker1.Value.ToString("yyyy-MM-dd"),
							"' As date) and CAST('",
							this.dateTimePicker2.Value.ToString("yyyy-MM-dd"),
							"' As date) and Branchs.bra_id=Trans_table.bra_id and Branchs.comp_id=Trans_table.comp_id and Currency.curr_id=Trans_table.curr_id and Trans_table.sta_id=Stations.sta_id and [trans_type] in(1,2,3) and flag='5' and [Trans_table].prod_id=Products.prod_id order by[trans_id] asc"
						}), WaqoodReportForm.mConnection);
					}
					else
					{
						bool flag3 = this.comboBoxComp.SelectedIndex != 0;
						if (flag3)
						{
							WaqoodReportForm.mAdatpter = new SqlDataAdapter(string.Concat(new object[]
							{
								"select [Trans_table].regn,trans_id_sta,prc,sta_name,Trans_table.sta_id,sta_name,trans_id,trans_id_bra,CAST(trans_date AS DATE) trans_date,CAST(ORDER_DATE AS DATE) ORDER_DATE,[car_id],[car_id_bra],car_id_comp,[car_no],[amt2],[amt],ISNULL(amt2,0)*ISNULL(prc,0) totprc,Trans_table.[prod_id],prod_name,car_driver,Trans_table.curr_id,curr_name,Branchs.bra_id,Branchs.bra_name,Branchs.comp_id from [dbo].[Trans_table],Products,Stations,Currency,Branchs where [Trans_table].bra_id='",
								this.comboBoxComp.SelectedValue,
								"' and CAST([trans_date] As date) between CAST('",
								this.dateTimePicker1.Value.ToString("yyyy-MM-dd"),
								"' As date) and CAST('",
								this.dateTimePicker2.Value.ToString("yyyy-MM-dd"),
								"' As date) and Branchs.bra_id=Trans_table.bra_id and Branchs.comp_id=Trans_table.comp_id and Currency.curr_id=Trans_table.curr_id and Trans_table.sta_id=Stations.sta_id and [trans_type] in(1,2,3) and flag='5' and [Trans_table].prod_id=Products.prod_id order by[trans_id] asc"
							}), WaqoodReportForm.mConnection);
						}
						else
						{
							WaqoodReportForm.mAdatpter = new SqlDataAdapter(string.Concat(new string[]
							{
								"select [Trans_table].regn,trans_id_sta,prc,sta_name,Trans_table.sta_id,sta_name,trans_id,trans_id_bra,CAST(trans_date AS DATE) trans_date,CAST(ORDER_DATE AS DATE) ORDER_DATE,[car_id],[car_id_bra],car_id_comp,[car_no],[amt2],[amt],ISNULL(amt2,0)*ISNULL(prc,0) totprc,Trans_table.[prod_id],prod_name,car_driver,Trans_table.curr_id,curr_name,Branchs.bra_id,Branchs.bra_name,Branchs.comp_id from [dbo].[Trans_table],Products,Stations,Currency,Branchs where CAST([trans_date] As date) between CAST('",
								this.dateTimePicker1.Value.ToString("yyyy-MM-dd"),
								"' As date) and CAST('",
								this.dateTimePicker2.Value.ToString("yyyy-MM-dd"),
								"' As date) and Branchs.bra_id=Trans_table.bra_id and Branchs.comp_id=Trans_table.comp_id and Currency.curr_id=Trans_table.curr_id and Trans_table.sta_id=Stations.sta_id and [trans_type] in(1,2,3) and flag='5' and [Trans_table].prod_id=Products.prod_id order by[trans_id] asc"
							}), WaqoodReportForm.mConnection);
						}
					}
				}
				WaqoodReportForm.mAdatpter.Fill(this.dtorders);
				bool flag4 = this.dtorders.Rows.Count == 0;
				if (!flag4)
				{
					this.add_coulm();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public void DisplayData()
		{
			try
			{
				this.dtorders.Clear();
				this.dtorders.Clone();
				this.dataGridView1.Rows.Clear();
				this.dataGridView1.Refresh();
				WaqoodReportForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlConnection.ClearPool(WaqoodReportForm.mConnection);
				WaqoodReportForm.mAdatpter = new SqlDataAdapter("select [Trans_table].regn,trans_id_sta,prc,sta_name,Trans_table.sta_id,sta_name,trans_id,trans_id_bra,CAST(trans_date AS DATE) trans_date,CAST(ORDER_DATE AS DATE) ORDER_DATE,[car_id],[car_id_bra],car_id_comp,[car_no],[amt2],[amt],ISNULL(amt2,0)*ISNULL(prc,0) totprc,Trans_table.[prod_id],prod_name,car_driver,Trans_table.curr_id,curr_name,Branchs.bra_id,Branchs.bra_name,Branchs.comp_id from [dbo].[Trans_table],Products,Stations,Currency,Branchs where CAST(trans_date AS DATE)=CAST(GetDate() AS DATE) and Branchs.bra_id=Trans_table.bra_id and Branchs.comp_id=Trans_table.comp_id and Currency.curr_id=Trans_table.curr_id and Trans_table.sta_id=Stations.sta_id and [trans_type] in(1,2,3) and flag='5' and [Trans_table].prod_id=Products.prod_id  order by [trans_id] asc", WaqoodReportForm.mConnection);
				WaqoodReportForm.mAdatpter.Fill(this.dtorders);
				bool flag = this.dtorders.Rows.Count == 0;
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
				for (int i = 0; i < this.dtorders.Rows.Count; i++)
				{
					this.dataGridView1.Rows.Add(new object[]
					{
						this.dtorders.Rows[i]["bra_name"].ToString(),
						this.dtorders.Rows[i]["trans_id_bra"].ToString(),
						this.dtorders.Rows[i]["trans_id_sta"].ToString(),
						this.dtorders.Rows[i]["car_id_bra"].ToString(),
						this.dtorders.Rows[i]["sta_name"].ToString(),
						this.dtorders.Rows[i]["prod_name"].ToString(),
						this.dtorders.Rows[i]["amt2"].ToString(),
						this.dtorders.Rows[i]["trans_date"].ToString(),
						this.dtorders.Rows[i]["prod_id"].ToString(),
						this.dtorders.Rows[i]["car_id"].ToString(),
						this.dtorders.Rows[i]["car_id_comp"].ToString(),
						this.dtorders.Rows[i]["curr_name"].ToString(),
						this.dtorders.Rows[i]["comp_id"].ToString(),
						this.dtorders.Rows[i]["bra_id"].ToString(),
						this.dtorders.Rows[i]["curr_id"].ToString(),
						this.dtorders.Rows[i]["prc"].ToString(),
						this.dtorders.Rows[i]["totprc"].ToString()
					});
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void WaqoodReportForm_Load(object sender, EventArgs e)
		{
			this.Fill_Stations();
			this.Fill_Branch();
		}

		private void Fill_Branch()
		{
			try
			{
				WaqoodReportForm.mConnection = new SqlConnection(MainForm.str_conn);
				WaqoodReportForm.mAdatpter = new SqlDataAdapter("SELECT [bra_id],[bra_name] FROM [dbo].[Branchs] order by [bra_id] asc", WaqoodReportForm.mConnection);
				DataTable dataTable = new DataTable();
				WaqoodReportForm.mAdatpter.Fill(dataTable);
				DataRow dataRow = dataTable.NewRow();
				dataRow[0] = 0;
				dataRow[1] = "-الشركة-";
				dataTable.Rows.InsertAt(dataRow, 0);
				this.comboBoxComp.DataSource = dataTable;
				this.comboBoxComp.DisplayMember = "bra_name";
				this.comboBoxComp.ValueMember = "bra_id";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}
		}

		private void Fill_Stations()
		{
			try
			{
				WaqoodReportForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT [sta_id],[sta_name] FROM [dbo].[Stations] order by [sta_id] asc", WaqoodReportForm.mConnection);
				DataTable dataTable = new DataTable();
				sqlDataAdapter.Fill(dataTable);
				DataRow dataRow = dataTable.NewRow();
				dataRow[0] = 0;
				dataRow[1] = "-المحطة-";
				dataTable.Rows.InsertAt(dataRow, 0);
				this.comboBoxStation.DataSource = dataTable;
				this.comboBoxStation.DisplayMember = "sta_name";
				this.comboBoxStation.ValueMember = "sta_id";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
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
			this.panel1 = new Panel();
			this.pictureBox1 = new PictureBox();
			this.panel5 = new Panel();
			this.panel3 = new Panel();
			this.pictureBox2 = new PictureBox();
			this.comboBoxStation = new ComboBox();
			this.label7 = new Label();
			this.comboBoxComp = new ComboBox();
			this.label3 = new Label();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.button4 = new Button();
			this.panel2 = new Panel();
			this.panel4 = new Panel();
			this.dataGridView1 = new DataGridView();
			this.Column11 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.Column18 = new DataGridViewTextBoxColumn();
			this.Column3 = new DataGridViewTextBoxColumn();
			this.Column5 = new DataGridViewTextBoxColumn();
			this.Column1 = new DataGridViewTextBoxColumn();
			this.Column4 = new DataGridViewTextBoxColumn();
			this.Column6 = new DataGridViewTextBoxColumn();
			this.Column7 = new DataGridViewTextBoxColumn();
			this.Column8 = new DataGridViewTextBoxColumn();
			this.Column9 = new DataGridViewTextBoxColumn();
			this.Column12 = new DataGridViewTextBoxColumn();
			this.Column14 = new DataGridViewTextBoxColumn();
			this.Column15 = new DataGridViewTextBoxColumn();
			this.Column17 = new DataGridViewTextBoxColumn();
			this.Column13 = new DataGridViewTextBoxColumn();
			this.Column2 = new DataGridViewTextBoxColumn();
			this.label2 = new Label();
			this.dateTimePicker2 = new DateTimePicker();
			this.label1 = new Label();
			this.dateTimePicker1 = new DateTimePicker();
			this.panel1.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			this.panel3.SuspendLayout();
			((ISupportInitialize)this.pictureBox2).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel4.SuspendLayout();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			base.SuspendLayout();
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Dock = DockStyle.Fill;
			this.panel1.Location = new Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(889, 92);
			this.panel1.TabIndex = 2;
			this.pictureBox1.BorderStyle = BorderStyle.FixedSingle;
			this.pictureBox1.Dock = DockStyle.Fill;
			this.pictureBox1.Image = Resources.waqood;
			this.pictureBox1.Location = new Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new Size(889, 92);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.panel5.BorderStyle = BorderStyle.FixedSingle;
			this.panel5.Dock = DockStyle.Bottom;
			this.panel5.Location = new Point(0, 737);
			this.panel5.Name = "panel5";
			this.panel5.Size = new Size(1780, 78);
			this.panel5.TabIndex = 101;
			this.panel3.BackColor = SystemColors.ControlLight;
			this.panel3.BorderStyle = BorderStyle.FixedSingle;
			this.panel3.Controls.Add(this.label2);
			this.panel3.Controls.Add(this.dateTimePicker2);
			this.panel3.Controls.Add(this.label1);
			this.panel3.Controls.Add(this.dateTimePicker1);
			this.panel3.Controls.Add(this.pictureBox2);
			this.panel3.Controls.Add(this.comboBoxStation);
			this.panel3.Controls.Add(this.label7);
			this.panel3.Controls.Add(this.comboBoxComp);
			this.panel3.Controls.Add(this.label3);
			this.panel3.Dock = DockStyle.Top;
			this.panel3.Location = new Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new Size(1784, 63);
			this.panel3.TabIndex = 0;
			this.pictureBox2.Cursor = Cursors.Hand;
			this.pictureBox2.Dock = DockStyle.Left;
			this.pictureBox2.Image = Resources.icons8_search_30;
			this.pictureBox2.Location = new Point(0, 0);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new Size(58, 61);
			this.pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox2.TabIndex = 150;
			this.pictureBox2.TabStop = false;
			this.pictureBox2.Click += new EventHandler(this.pictureBox2_Click);
			this.comboBoxStation.AutoCompleteMode = AutoCompleteMode.Suggest;
			this.comboBoxStation.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.comboBoxStation.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxStation.FlatStyle = FlatStyle.Flat;
			this.comboBoxStation.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.comboBoxStation.FormattingEnabled = true;
			this.comboBoxStation.Location = new Point(1324, 12);
			this.comboBoxStation.Name = "comboBoxStation";
			this.comboBoxStation.Size = new Size(366, 37);
			this.comboBoxStation.TabIndex = 148;
			this.label7.AutoSize = true;
			this.label7.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label7.Location = new Point(1696, 20);
			this.label7.Name = "label7";
			this.label7.Size = new Size(70, 29);
			this.label7.TabIndex = 149;
			this.label7.Text = "المحطة";
			this.comboBoxComp.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxComp.FlatStyle = FlatStyle.Flat;
			this.comboBoxComp.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.comboBoxComp.FormattingEnabled = true;
			this.comboBoxComp.Location = new Point(910, 12);
			this.comboBoxComp.Name = "comboBoxComp";
			this.comboBoxComp.Size = new Size(322, 37);
			this.comboBoxComp.TabIndex = 146;
			this.label3.AutoSize = true;
			this.label3.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label3.Location = new Point(1238, 20);
			this.label3.Name = "label3";
			this.label3.Size = new Size(68, 29);
			this.label3.TabIndex = 147;
			this.label3.Text = "الشركة";
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.button4, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
			this.tableLayoutPanel1.Dock = DockStyle.Fill;
			this.tableLayoutPanel1.Location = new Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 90f));
			this.tableLayoutPanel1.Size = new Size(1790, 986);
			this.tableLayoutPanel1.TabIndex = 4;
			this.button4.Dock = DockStyle.Fill;
			this.button4.Enabled = false;
			this.button4.FlatStyle = FlatStyle.Flat;
			this.button4.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.button4.Location = new Point(898, 3);
			this.button4.Name = "button4";
			this.button4.Size = new Size(889, 92);
			this.button4.TabIndex = 3;
			this.button4.Text = "تقرير الصرف - وقود";
			this.button4.UseVisualStyleBackColor = true;
			this.tableLayoutPanel1.SetColumnSpan(this.panel2, 2);
			this.panel2.Controls.Add(this.panel4);
			this.panel2.Controls.Add(this.panel3);
			this.panel2.Dock = DockStyle.Fill;
			this.panel2.Location = new Point(3, 101);
			this.panel2.Name = "panel2";
			this.panel2.Size = new Size(1784, 882);
			this.panel2.TabIndex = 4;
			this.panel4.BackColor = SystemColors.GradientActiveCaption;
			this.panel4.BorderStyle = BorderStyle.Fixed3D;
			this.panel4.Controls.Add(this.dataGridView1);
			this.panel4.Controls.Add(this.panel5);
			this.panel4.Dock = DockStyle.Fill;
			this.panel4.Location = new Point(0, 63);
			this.panel4.Name = "panel4";
			this.panel4.Size = new Size(1784, 819);
			this.panel4.TabIndex = 1;
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
				this.Column11,
				this.dataGridViewTextBoxColumn2,
				this.Column18,
				this.Column3,
				this.Column5,
				this.Column1,
				this.Column4,
				this.Column6,
				this.Column7,
				this.Column8,
				this.Column9,
				this.Column12,
				this.Column14,
				this.Column15,
				this.Column17,
				this.Column13,
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
			this.dataGridView1.Size = new Size(1780, 737);
			this.dataGridView1.TabIndex = 148;
			this.Column11.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.Column11.HeaderText = "الشركة";
			this.Column11.Name = "Column11";
			this.dataGridViewTextBoxColumn2.HeaderText = "رقم الطلب";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.Column18.HeaderText = "رقم السند";
			this.Column18.Name = "Column18";
			this.Column3.HeaderText = "رقم السيارة";
			this.Column3.Name = "Column3";
			this.Column5.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.Column5.HeaderText = "المحطة";
			this.Column5.Name = "Column5";
			this.Column1.HeaderText = "الوقود";
			this.Column1.Name = "Column1";
			this.Column4.HeaderText = "الكمية";
			this.Column4.Name = "Column4";
			this.Column6.HeaderText = "التاريخ";
			this.Column6.Name = "Column6";
			this.Column6.Width = 110;
			this.Column7.HeaderText = "prodid";
			this.Column7.Name = "Column7";
			this.Column7.Visible = false;
			this.Column8.HeaderText = "carid";
			this.Column8.Name = "Column8";
			this.Column8.Visible = false;
			this.Column9.HeaderText = "carcomp";
			this.Column9.Name = "Column9";
			this.Column9.Visible = false;
			this.Column12.HeaderText = "العملة";
			this.Column12.Name = "Column12";
			this.Column14.HeaderText = "compid";
			this.Column14.Name = "Column14";
			this.Column14.Visible = false;
			this.Column15.HeaderText = "braid";
			this.Column15.Name = "Column15";
			this.Column15.Visible = false;
			this.Column17.HeaderText = "currid";
			this.Column17.Name = "Column17";
			this.Column17.Visible = false;
			this.Column13.HeaderText = "السعر";
			this.Column13.Name = "Column13";
			this.Column2.HeaderText = "الاجمالي";
			this.Column2.Name = "Column2";
			this.label2.AutoSize = true;
			this.label2.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label2.Location = new Point(381, 20);
			this.label2.Name = "label2";
			this.label2.Size = new Size(87, 29);
			this.label2.TabIndex = 157;
			this.label2.Text = "الى تاريخ";
			this.dateTimePicker2.Font = new Font("Arial", 10f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.dateTimePicker2.Location = new Point(59, 19);
			this.dateTimePicker2.Name = "dateTimePicker2";
			this.dateTimePicker2.Size = new Size(316, 30);
			this.dateTimePicker2.TabIndex = 156;
			this.label1.AutoSize = true;
			this.label1.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label1.Location = new Point(790, 20);
			this.label1.Name = "label1";
			this.label1.Size = new Size(86, 29);
			this.label1.TabIndex = 155;
			this.label1.Text = "من تاريخ";
			this.dateTimePicker1.Font = new Font("Arial", 10f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.dateTimePicker1.Location = new Point(468, 19);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new Size(316, 30);
			this.dateTimePicker1.TabIndex = 154;
			base.AutoScaleDimensions = new SizeF(9f, 19f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(1790, 986);
			base.Controls.Add(this.tableLayoutPanel1);
			base.Name = "WaqoodReportForm";
			this.RightToLeft = RightToLeft.Yes;
			this.Text = "تقرير الصرف - وقود";
			base.Load += new EventHandler(this.WaqoodReportForm_Load);
			this.panel1.ResumeLayout(false);
			((ISupportInitialize)this.pictureBox1).EndInit();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			((ISupportInitialize)this.pictureBox2).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			((ISupportInitialize)this.dataGridView1).EndInit();
			base.ResumeLayout(false);
		}
	}
}
