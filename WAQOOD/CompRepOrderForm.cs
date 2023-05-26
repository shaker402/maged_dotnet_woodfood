using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WAQOOD.Properties;

namespace WAQOOD
{
	public class CompRepOrderForm : Form
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

		private string flagg = string.Empty;

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

		private PictureBox pictureBox1;

		private TableLayoutPanel tableLayoutPanel1;

		private Panel panel1;

		private Button button4;

		private Panel panel2;

		private Panel panel4;

		private DataGridView dataGridView1;

		private Panel panel5;

		private Panel panel3;

		private PictureBox pictureBox2;

		private ComboBox comboBoxCars;

		private Label label7;

		private DateTimePicker dateTimePicker1;

		private Panel panelCancel;

		private TextBox textBoxResons;

		private Button button2Cancel;

		private Label label8;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn Column3;

		private DataGridViewTextBoxColumn Column10;

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

		private DataGridViewTextBoxColumn Column2;

		private DataGridViewTextBoxColumn Column11;

		private DataGridViewTextBoxColumn Column13;

		private Label label2;

		private DateTimePicker dateTimePicker2;

		private Label label1;

		private void pictureBox2_Click(object sender, EventArgs e)
		{
			this.DisplayDataAfterSearch();
		}

		private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			bool flag = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value != null;
			if (flag)
			{
				this.transidbra = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
				this.carbra = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
				this.carno = this.dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
				this.caramnt = this.dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
				this.ordDate = this.dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
				this.caroil = this.dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
				this.carid = this.dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
				this.carcomp = this.dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
				this.transidbra = this.dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
				this.flagg = this.dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
				this.transid = this.dataGridView1.Rows[e.RowIndex].Cells[16].Value.ToString();
				bool flag2 = this.flagg == "3" || this.flagg == "1";
				if (flag2)
				{
					this.panelCancel.Visible = true;
				}
			}
		}

		private void button2Cancel_Click(object sender, EventArgs e)
		{
			try
			{
				CompRepOrderForm.mConnection.Close();
				bool flag = this.textBoxResons.Text != "" && (this.flagg == "3" || this.flagg == "1");
				if (flag)
				{
					this.mCommand = new SqlCommand(string.Concat(new object[]
					{
						"update [dbo].[Trans_table] set flag='4',cancel_date='",
						DateTime.Now,
						"',cancel_desc='",
						this.textBoxResons.Text,
						"-",
						MobileLogForm.userID2,
						"' where flag in(1,3) and return_flag='1' and trans_id_bra='",
						this.transidbra,
						"' and trans_id='",
						this.transid,
						"' and [tyear]='",
						DateTime.Now.Year.ToString(),
						"' and [tmonth]='",
						DateTime.Now.Month.ToString(),
						"' and [bra_id]='",
						LogonForm.braid,
						"' and [comp_id]='",
						LogonForm.compid,
						"'and CAST(ORDER_DATE AS DATE)= CAST('",
						this.ordDate,
						"' AS DATE) and prod_id='",
						this.caroil,
						"' and [car_id]='",
						this.carid,
						"' and [car_id_bra]='",
						this.carbra,
						"' and [car_id_comp]='",
						this.carcomp,
						"' and [car_no]='",
						this.carno,
						"' "
					}), CompRepOrderForm.mConnection);
					CompRepOrderForm.mConnection.Open();
					int num = this.mCommand.ExecuteNonQuery();
					CompRepOrderForm.mConnection.Close();
					bool flag2 = num >= 1;
					if (flag2)
					{
						MessageBox.Show("تم الحفظ بنجاح" + num.ToString());
						this.DisplayData();
						this.textBoxResons.Text = "";
					}
					else
					{
						MessageBox.Show("خطأ في حفظ الطلب ");
					}
				}
				else
				{
					MessageBox.Show("بيانات غير مكتملة!");
				}
			}
			catch (Exception ex)
			{
				CompRepOrderForm.mConnection.Close();
				MessageBox.Show(ex.Message);
			}
		}

		public CompRepOrderForm()
		{
			this.InitializeComponent();
			base.WindowState = FormWindowState.Maximized;
			CompRepOrderForm.mConnection = new SqlConnection(MainForm.str_conn);
			this.dtorders = new DataTable();
			this.DisplayData();
			this.Fill_Cars();
		}

		private void Fill_Cars()
		{
			try
			{
				CompRepOrderForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(string.Concat(new string[]
				{
					"SELECT [car_id_bra],[car_no]+[car_desc] car_no,[ID],[car_id_comp] FROM [dbo].[Cars]  where comp_id='",
					LogonForm.compid,
					"' and bra_id='",
					LogonForm.braid,
					"' order by [car_id_bra] asc"
				}), CompRepOrderForm.mConnection);
				DataTable dataTable = new DataTable();
				sqlDataAdapter.Fill(dataTable);
				DataRow dataRow = dataTable.NewRow();
				dataRow[0] = 0;
				dataRow[1] = "-رقم السيارة-";
				dataTable.Rows.InsertAt(dataRow, 0);
				this.comboBoxCars.DataSource = dataTable;
				this.comboBoxCars.DisplayMember = "car_no";
				this.comboBoxCars.ValueMember = "car_id_bra";
				this.comboBoxCars.SelectedIndex = 0;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}
		}

		public void DisplayDataAfterSearch()
		{
			try
			{
				this.panelCancel.Visible = false;
				this.dtorders.Clear();
				this.dtorders.Clone();
				this.dataGridView1.Rows.Clear();
				this.dataGridView1.Refresh();
				CompRepOrderForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlConnection.ClearPool(CompRepOrderForm.mConnection);
				bool flag = this.comboBoxCars.SelectedIndex == 0;
				if (flag)
				{
					CompRepOrderForm.mAdatpter = new SqlDataAdapter(string.Concat(new string[]
					{
						"select case Trans_table.flag when '0' then 'جديد' when '1' then 'قيد التثبيت' when '2' then 'في المحطة' when '3' then 'اعاده من المحطة' when '4' then 'ملغي' when '5' then 'تم الصرف'  else 'unknown' end status,flag,[Trans_table].regn,trans_id_sta,prc,sta_name,Trans_table.sta_id,sta_name,trans_id,trans_id_bra,CAST(ORDER_DATE AS DATE) ORDER_DATE,[car_id],[car_id_bra],car_id_comp,[car_no],[amt2],[amt],ISNULL(amt2,0)*ISNULL(prc,0) totprc,Trans_table.[prod_id],prod_name,car_driver,Trans_table.curr_id,curr_name,Branchs.bra_id,Branchs.bra_name,Branchs.comp_id from [dbo].[Trans_table],Products,Stations,Currency,Branchs where CAST([order_date] As date) between CAST('",
						this.dateTimePicker1.Value.ToString("yyyy-MM-dd"),
						"' As date) and CAST('",
						this.dateTimePicker2.Value.ToString("yyyy-MM-dd"),
						"' As date)and Trans_table.[bra_id]='",
						LogonForm.braid,
						"' and Trans_table.[comp_id]='",
						LogonForm.compid,
						"' and Branchs.bra_id=Trans_table.bra_id and Branchs.comp_id=Trans_table.comp_id and Currency.curr_id=Trans_table.curr_id and Trans_table.sta_id=Stations.sta_id and [trans_type] in(1,2,3) and flag in(0,1,2,3,4,5) and [Trans_table].prod_id=Products.prod_id order by [trans_id] asc"
					}), CompRepOrderForm.mConnection);
				}
				else
				{
					CompRepOrderForm.mAdatpter = new SqlDataAdapter(string.Concat(new object[]
					{
						"select case Trans_table.flag when '0' then 'جديد' when '1' then 'قيد التثبيت' when '2' then 'في المحطة' when '3' then 'اعاده من المحطة' when '4' then 'ملغي' when '5' then 'تم الصرف'  else 'unknown' end status,flag,[Trans_table].regn,trans_id_sta,prc,sta_name,Trans_table.sta_id,sta_name,trans_id,trans_id_bra,CAST(ORDER_DATE AS DATE) ORDER_DATE,[car_id],[car_id_bra],car_id_comp,[car_no],[amt2],[amt],ISNULL(amt2,0)*ISNULL(prc,0) totprc,Trans_table.[prod_id],prod_name,car_driver,Trans_table.curr_id,curr_name,Branchs.bra_id,Branchs.bra_name,Branchs.comp_id from [dbo].[Trans_table],Products,Stations,Currency,Branchs where Trans_table.[car_id_bra]='",
						this.comboBoxCars.SelectedValue,
						"' and CAST([order_date] As date) between CAST('",
						this.dateTimePicker1.Value.ToString("yyyy-MM-dd"),
						"' As date) and CAST('",
						this.dateTimePicker2.Value.ToString("yyyy-MM-dd"),
						"' As date)and Trans_table.[bra_id]='",
						LogonForm.braid,
						"' and Trans_table.[comp_id]='",
						LogonForm.compid,
						"' and Branchs.bra_id=Trans_table.bra_id and Branchs.comp_id=Trans_table.comp_id and Currency.curr_id=Trans_table.curr_id and Trans_table.sta_id=Stations.sta_id and [trans_type] in(1,2,3) and flag in(0,1,2,3,4,5) and [Trans_table].prod_id=Products.prod_id order by [trans_id] asc"
					}), CompRepOrderForm.mConnection);
				}
				CompRepOrderForm.mAdatpter.Fill(this.dtorders);
				bool flag2 = this.dtorders.Rows.Count == 0;
				if (!flag2)
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
				this.panelCancel.Visible = false;
				this.dtorders.Clear();
				this.dtorders.Clone();
				this.dataGridView1.Rows.Clear();
				this.dataGridView1.Refresh();
				CompRepOrderForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlConnection.ClearPool(CompRepOrderForm.mConnection);
				CompRepOrderForm.mAdatpter = new SqlDataAdapter(string.Concat(new string[]
				{
					"select case Trans_table.flag when '0' then 'جديد' when '1' then 'قيد التثبيت' when '2' then 'في المحطة' when '3' then 'اعاده من المحطة' when '4' then 'ملغي' when '5' then 'تم الصرف'  else 'unknown' end status,flag,[Trans_table].regn,trans_id_sta,prc,sta_name,Trans_table.sta_id,sta_name,trans_id,trans_id_bra,CAST(ORDER_DATE AS DATE) ORDER_DATE,[car_id],[car_id_bra],car_id_comp,[car_no],[amt2],[amt],ISNULL(amt2,0)*ISNULL(prc,0) totprc,Trans_table.[prod_id],prod_name,car_driver,Trans_table.curr_id,curr_name,Branchs.bra_id,Branchs.bra_name,Branchs.comp_id from [dbo].[Trans_table],Products,Stations,Currency,Branchs where Trans_table.[bra_id]='",
					LogonForm.braid,
					"' and Trans_table.[comp_id]='",
					LogonForm.compid,
					"' and CAST(ORDER_DATE AS DATE)=CAST(GetDate() AS DATE) and Branchs.bra_id=Trans_table.bra_id and Branchs.comp_id=Trans_table.comp_id and Currency.curr_id=Trans_table.curr_id and Trans_table.sta_id=Stations.sta_id and [trans_type] in(1,2,3) and flag in(0,1,2,3,4,5) and [Trans_table].prod_id=Products.prod_id order by [trans_id] asc"
				}), CompRepOrderForm.mConnection);
				CompRepOrderForm.mAdatpter.Fill(this.dtorders);
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
						this.dtorders.Rows[i]["trans_id_bra"].ToString(),
						this.dtorders.Rows[i]["car_id_bra"].ToString(),
						this.dtorders.Rows[i]["car_no"].ToString(),
						this.dtorders.Rows[i]["sta_name"].ToString(),
						this.dtorders.Rows[i]["prod_name"].ToString(),
						this.dtorders.Rows[i]["amt"].ToString(),
						this.dtorders.Rows[i]["order_date"].ToString(),
						this.dtorders.Rows[i]["prod_id"].ToString(),
						this.dtorders.Rows[i]["car_id"].ToString(),
						this.dtorders.Rows[i]["car_id_comp"].ToString(),
						this.dtorders.Rows[i]["curr_name"].ToString(),
						this.dtorders.Rows[i]["trans_id_bra"].ToString(),
						this.dtorders.Rows[i]["trans_id_bra"].ToString(),
						this.dtorders.Rows[i]["curr_id"].ToString(),
						this.dtorders.Rows[i]["flag"].ToString(),
						this.dtorders.Rows[i]["status"].ToString(),
						this.dtorders.Rows[i]["trans_id"].ToString()
					});
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void CompRepOrderForm_Load(object sender, EventArgs e)
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
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			this.pictureBox1 = new PictureBox();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.panel1 = new Panel();
			this.button4 = new Button();
			this.panel2 = new Panel();
			this.panel4 = new Panel();
			this.dataGridView1 = new DataGridView();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.Column3 = new DataGridViewTextBoxColumn();
			this.Column10 = new DataGridViewTextBoxColumn();
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
			this.Column2 = new DataGridViewTextBoxColumn();
			this.Column11 = new DataGridViewTextBoxColumn();
			this.Column13 = new DataGridViewTextBoxColumn();
			this.panel5 = new Panel();
			this.panelCancel = new Panel();
			this.textBoxResons = new TextBox();
			this.button2Cancel = new Button();
			this.label8 = new Label();
			this.panel3 = new Panel();
			this.pictureBox2 = new PictureBox();
			this.comboBoxCars = new ComboBox();
			this.label7 = new Label();
			this.dateTimePicker1 = new DateTimePicker();
			this.label1 = new Label();
			this.label2 = new Label();
			this.dateTimePicker2 = new DateTimePicker();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel4.SuspendLayout();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			this.panel5.SuspendLayout();
			this.panelCancel.SuspendLayout();
			this.panel3.SuspendLayout();
			((ISupportInitialize)this.pictureBox2).BeginInit();
			base.SuspendLayout();
			this.pictureBox1.BorderStyle = BorderStyle.FixedSingle;
			this.pictureBox1.Dock = DockStyle.Fill;
			this.pictureBox1.Image = Resources.waqood;
			this.pictureBox1.Location = new Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new Size(792, 69);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
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
			this.tableLayoutPanel1.Size = new Size(1596, 754);
			this.tableLayoutPanel1.TabIndex = 6;
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Dock = DockStyle.Fill;
			this.panel1.Location = new Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(792, 69);
			this.panel1.TabIndex = 2;
			this.button4.Dock = DockStyle.Fill;
			this.button4.Enabled = false;
			this.button4.FlatStyle = FlatStyle.Flat;
			this.button4.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.button4.Location = new Point(801, 3);
			this.button4.Name = "button4";
			this.button4.Size = new Size(792, 69);
			this.button4.TabIndex = 3;
			this.button4.Text = "تقرير الطلبات - شركات";
			this.button4.UseVisualStyleBackColor = true;
			this.tableLayoutPanel1.SetColumnSpan(this.panel2, 2);
			this.panel2.Controls.Add(this.panel4);
			this.panel2.Controls.Add(this.panel3);
			this.panel2.Dock = DockStyle.Fill;
			this.panel2.Location = new Point(3, 78);
			this.panel2.Name = "panel2";
			this.panel2.Size = new Size(1590, 673);
			this.panel2.TabIndex = 4;
			this.panel4.BackColor = SystemColors.GradientActiveCaption;
			this.panel4.BorderStyle = BorderStyle.Fixed3D;
			this.panel4.Controls.Add(this.dataGridView1);
			this.panel4.Controls.Add(this.panel5);
			this.panel4.Dock = DockStyle.Fill;
			this.panel4.Location = new Point(0, 63);
			this.panel4.Name = "panel4";
			this.panel4.Size = new Size(1590, 610);
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
				this.dataGridViewTextBoxColumn2,
				this.Column3,
				this.Column10,
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
				this.Column2,
				this.Column11,
				this.Column13
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
			this.dataGridView1.Size = new Size(1586, 555);
			this.dataGridView1.TabIndex = 148;
			this.dataGridView1.RowHeaderMouseClick += new DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
			this.dataGridViewTextBoxColumn2.HeaderText = "رقم الطلب";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.Column3.HeaderText = "رقم السيارة";
			this.Column3.Name = "Column3";
			this.Column10.HeaderText = "رقم اللوحة";
			this.Column10.Name = "Column10";
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
			this.Column2.HeaderText = "flag";
			this.Column2.Name = "Column2";
			this.Column2.Visible = false;
			this.Column11.HeaderText = "الحالة";
			this.Column11.Name = "Column11";
			this.Column13.HeaderText = "transid";
			this.Column13.Name = "Column13";
			this.Column13.Visible = false;
			this.panel5.BorderStyle = BorderStyle.FixedSingle;
			this.panel5.Controls.Add(this.panelCancel);
			this.panel5.Dock = DockStyle.Bottom;
			this.panel5.Location = new Point(0, 555);
			this.panel5.Name = "panel5";
			this.panel5.Size = new Size(1586, 51);
			this.panel5.TabIndex = 101;
			this.panelCancel.Controls.Add(this.textBoxResons);
			this.panelCancel.Controls.Add(this.button2Cancel);
			this.panelCancel.Controls.Add(this.label8);
			this.panelCancel.Dock = DockStyle.Fill;
			this.panelCancel.Location = new Point(0, 0);
			this.panelCancel.Name = "panelCancel";
			this.panelCancel.Size = new Size(1584, 49);
			this.panelCancel.TabIndex = 0;
			this.panelCancel.Visible = false;
			this.textBoxResons.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBoxResons.Location = new Point(740, 7);
			this.textBoxResons.Name = "textBoxResons";
			this.textBoxResons.Size = new Size(279, 33);
			this.textBoxResons.TabIndex = 100;
			this.textBoxResons.TextAlign = HorizontalAlignment.Center;
			this.button2Cancel.BackColor = Color.White;
			this.button2Cancel.Cursor = Cursors.Hand;
			this.button2Cancel.FlatStyle = FlatStyle.Flat;
			this.button2Cancel.Font = new Font("Arial", 12f, FontStyle.Bold);
			this.button2Cancel.ForeColor = Color.FromArgb(64, 64, 64);
			this.button2Cancel.Location = new Point(588, 5);
			this.button2Cancel.Name = "button2Cancel";
			this.button2Cancel.Size = new Size(146, 34);
			this.button2Cancel.TabIndex = 102;
			this.button2Cancel.Text = "الغاء";
			this.button2Cancel.UseVisualStyleBackColor = false;
			this.button2Cancel.Click += new EventHandler(this.button2Cancel_Click);
			this.label8.AutoSize = true;
			this.label8.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label8.Location = new Point(1023, 8);
			this.label8.Name = "label8";
			this.label8.Size = new Size(107, 29);
			this.label8.TabIndex = 101;
			this.label8.Text = "سبب الالغاء";
			this.panel3.BackColor = SystemColors.ControlLight;
			this.panel3.BorderStyle = BorderStyle.FixedSingle;
			this.panel3.Controls.Add(this.label2);
			this.panel3.Controls.Add(this.dateTimePicker2);
			this.panel3.Controls.Add(this.label1);
			this.panel3.Controls.Add(this.pictureBox2);
			this.panel3.Controls.Add(this.comboBoxCars);
			this.panel3.Controls.Add(this.label7);
			this.panel3.Controls.Add(this.dateTimePicker1);
			this.panel3.Dock = DockStyle.Top;
			this.panel3.Location = new Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new Size(1590, 63);
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
			this.comboBoxCars.AutoCompleteMode = AutoCompleteMode.Suggest;
			this.comboBoxCars.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.comboBoxCars.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxCars.FlatStyle = FlatStyle.Flat;
			this.comboBoxCars.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.comboBoxCars.FormattingEnabled = true;
			this.comboBoxCars.Location = new Point(1102, 11);
			this.comboBoxCars.Name = "comboBoxCars";
			this.comboBoxCars.Size = new Size(344, 37);
			this.comboBoxCars.TabIndex = 148;
			this.label7.AutoSize = true;
			this.label7.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label7.Location = new Point(1452, 16);
			this.label7.Name = "label7";
			this.label7.Size = new Size(70, 29);
			this.label7.TabIndex = 149;
			this.label7.Text = "السيارة";
			this.dateTimePicker1.Font = new Font("Arial", 10f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.dateTimePicker1.Location = new Point(478, 13);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new Size(316, 30);
			this.dateTimePicker1.TabIndex = 6;
			this.label1.AutoSize = true;
			this.label1.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label1.Location = new Point(800, 13);
			this.label1.Name = "label1";
			this.label1.Size = new Size(86, 29);
			this.label1.TabIndex = 151;
			this.label1.Text = "من تاريخ";
			this.label2.AutoSize = true;
			this.label2.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label2.Location = new Point(391, 11);
			this.label2.Name = "label2";
			this.label2.Size = new Size(87, 29);
			this.label2.TabIndex = 153;
			this.label2.Text = "الى تاريخ";
			this.dateTimePicker2.Font = new Font("Arial", 10f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.dateTimePicker2.Location = new Point(69, 11);
			this.dateTimePicker2.Name = "dateTimePicker2";
			this.dateTimePicker2.Size = new Size(316, 30);
			this.dateTimePicker2.TabIndex = 152;
			base.AutoScaleDimensions = new SizeF(9f, 19f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(1596, 754);
			base.Controls.Add(this.tableLayoutPanel1);
			base.Name = "CompRepOrderForm";
			this.RightToLeft = RightToLeft.Yes;
			this.Text = "تقرير الطلبات";
			base.Load += new EventHandler(this.CompRepOrderForm_Load);
			((ISupportInitialize)this.pictureBox1).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			((ISupportInitialize)this.dataGridView1).EndInit();
			this.panel5.ResumeLayout(false);
			this.panelCancel.ResumeLayout(false);
			this.panelCancel.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			((ISupportInitialize)this.pictureBox2).EndInit();
			base.ResumeLayout(false);
		}
	}
}
