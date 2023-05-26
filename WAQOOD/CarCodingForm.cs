using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WAQOOD.Properties;

namespace WAQOOD
{
	public class CarCodingForm : Form
	{
		private int actv = -1;

		private string carid = string.Empty;

		private string caroil = string.Empty;

		private string carnotes = string.Empty;

		private string carcomp = string.Empty;

		private string carbra = string.Empty;

		private string cardriver = string.Empty;

		private string carno = string.Empty;

		private string cardesc = string.Empty;

		private string carshasi = string.Empty;

		private SqlCommand mCommand = null;

		private static SqlDataAdapter mAdatpter = null;

		private static SqlConnection mConnection = new SqlConnection(MainForm.str_conn);

		private DataTable dtOrder;

		private IContainer components = null;

		private TableLayoutPanel tableLayoutPanel1;

		private Panel panel1;

		private DataGridView dataGridView1;

		private TextBox textBox1carDesc;

		private Label label1;

		private TextBox textBoxCarID;

		private Label label2;

		private TextBox textBoxCarNo;

		private Label label4;

		private TextBox textBox2Shasi;

		private Label label3;

		private ComboBox comboBoxProd;

		private Label label6;

		private TextBox textBoxNotes;

		private Label label8;

		private TextBox textBoxDriverName;

		private Label label5;

		private Button buttonUpdateCars;

		private Button button3;

		private Label label9;

		private PictureBox pictureBox1;

		private Button button1;

		private Panel panel2;

		private Label label11;

		private Label label10;

		private Label label7;

		private TextBox textBoxamt;

		private TextBox textBoxmonth;

		private TextBox textBoxyear;

		private DataGridView dataGridView2;

		private Label label12;

		private GroupBox groupBox1;

		private RadioButton radioActive;

		private RadioButton radioStop;

		private Button button2;

		private DataGridViewTextBoxColumn Column12;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		private DataGridViewTextBoxColumn Column10;

		private DataGridViewTextBoxColumn Column11;

		private TextBox textBox1Budget;

		private Label label13;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn Column3;

		private DataGridViewTextBoxColumn Column4;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn Column2;

		private DataGridViewTextBoxColumn Column5;

		private DataGridViewTextBoxColumn Column6;

		private DataGridViewTextBoxColumn Column7;

		private DataGridViewTextBoxColumn Column8;

		private DataGridViewTextBoxColumn Column9;

		private DataGridViewTextBoxColumn Column13;

		private Label label14;

		private TextBox textBoxamt2;

		private TextBox textBox1;

		private Label label15;

		private TextBox textBox2;

		private Label label16;

		public CarCodingForm()
		{
			this.InitializeComponent();
			base.WindowState = FormWindowState.Maximized;
			CarCodingForm.mConnection = new SqlConnection(MainForm.str_conn);
			this.dtOrder = new DataTable();
			this.DisplayData();
			this.Fill_Products();
		}

		public void DisplayData()
		{
			try
			{
				this.dtOrder.Clear();
				this.dtOrder.Clone();
				this.dataGridView1.Rows.Clear();
				this.dataGridView1.Refresh();
				CarCodingForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlConnection.ClearPool(CarCodingForm.mConnection);
				CarCodingForm.mAdatpter = new SqlDataAdapter(string.Concat(new string[]
				{
					"SELECT budget,[ID],[car_id_comp],[car_id_bra],[car_desc],[car_no],[Sach_no],products.[prod_id],prod_name,[bra_id],[comp_id],[active],[driver_name],[notes] FROM [dbo].[Cars],products where bra_id='",
					LogonForm.braid,
					"' and comp_id='",
					LogonForm.compid,
					"' and [Cars].prod_id=products.prod_id order by [car_id_bra] asc"
				}), CarCodingForm.mConnection);
				CarCodingForm.mAdatpter.Fill(this.dtOrder);
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
						this.dtOrder.Rows[i]["car_id_bra"].ToString(),
						this.dtOrder.Rows[i]["car_no"].ToString(),
						this.dtOrder.Rows[i]["car_desc"].ToString(),
						this.dtOrder.Rows[i]["Sach_no"].ToString(),
						this.dtOrder.Rows[i]["prod_name"].ToString(),
						this.dtOrder.Rows[i]["driver_name"].ToString(),
						this.dtOrder.Rows[i]["active"].ToString(),
						this.dtOrder.Rows[i]["notes"].ToString(),
						this.dtOrder.Rows[i]["prod_id"].ToString(),
						this.dtOrder.Rows[i]["ID"].ToString(),
						this.dtOrder.Rows[i]["car_id_comp"].ToString(),
						this.dtOrder.Rows[i]["budget"].ToString()
					});
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
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

		private void radioStop_CheckedChanged(object sender, EventArgs e)
		{
			bool @checked = this.radioStop.Checked;
			if (@checked)
			{
				this.actv = 0;
			}
			else
			{
				this.actv = 0;
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.InsertCarBudget();
		}

		private void InsertCarBudget()
		{
			try
			{
				this.mCommand = new SqlCommand(string.Concat(new string[]
				{
					"INSERT INTO [dbo].[Cars_budget]([car_id],[car_id_comp],[car_id_bra],[bra_id],[comp_id],[car_no],prod_id,[car_month],[car_year],[amt],[pc_log]) VALUES('",
					this.carid,
					"','",
					this.carcomp,
					"','",
					this.carbra,
					"','",
					LogonForm.braid,
					"', '",
					LogonForm.compid,
					"','",
					this.carno,
					"','",
					this.caroil,
					"',@mnthh,@yearr,@amntt,'",
					Environment.MachineName,
					"-",
					Environment.UserName,
					"')"
				}), CarCodingForm.mConnection);
				CarCodingForm.mConnection.Open();
				this.mCommand.Parameters.AddWithValue("@yearr", this.textBoxyear.Text);
				this.mCommand.Parameters.AddWithValue("@mnthh", this.textBoxmonth.Text);
				this.mCommand.Parameters.AddWithValue("@amntt", this.textBoxamt.Text);
				this.mCommand.ExecuteNonQuery();
				CarCodingForm.mConnection.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				CarCodingForm.mConnection.Close();
			}
		}

		private void ClearControls()
		{
			this.textBox1carDesc.Text = "";
			this.textBox2Shasi.Text = "";
			this.textBoxCarID.Text = "";
			this.textBoxCarNo.Text = "";
			this.textBoxDriverName.Text = "";
			this.textBoxNotes.Text = "";
			this.textBox1Budget.Text = "";
		}

		private void Fill_Products()
		{
			try
			{
				CarCodingForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT [prod_id],[prod_name] FROM [dbo].[products] order by [prod_id] asc", CarCodingForm.mConnection);
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

		private void textBox2proname_TextChanged(object sender, EventArgs e)
		{
		}

		private void CarCodingForm_Load(object sender, EventArgs e)
		{
		}

		private void button4_Click(object sender, EventArgs e)
		{
			try
			{
				bool flag = this.actv != -1 && this.textBox1Budget.Text != "" && this.textBoxCarNo.Text != "" && this.textBox2Shasi.Text != "" && this.textBox1carDesc.Text != "" && this.comboBoxProd.SelectedIndex.ToString() != "0";
				if (flag)
				{
					this.mCommand = new SqlCommand(string.Concat(new object[]
					{
						"update Cars set budget=@budget2,active='",
						this.actv,
						"' where bra_id='",
						LogonForm.braid,
						"' and comp_id='",
						LogonForm.compid,
						"' and ID='",
						this.carid,
						"' and car_id_comp='",
						this.carcomp,
						"' and car_id_bra='",
						this.carbra,
						"' and Sach_no=@shashi"
					}), CarCodingForm.mConnection);
					CarCodingForm.mConnection.Open();
					this.mCommand.Parameters.AddWithValue("@caridd", this.textBoxCarID.Text);
					this.mCommand.Parameters.AddWithValue("@shashi", this.textBox2Shasi.Text);
					this.mCommand.Parameters.AddWithValue("@budget2", this.textBox1Budget.Text);
					this.mCommand.ExecuteNonQuery();
					CarCodingForm.mConnection.Close();
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
				CarCodingForm.mConnection.Close();
				MessageBox.Show(ex.Message);
			}
		}

		private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			bool flag = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value != null;
			if (flag)
			{
				this.carid = this.dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
				this.carno = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
				this.textBoxCarNo.Text = this.carno;
				this.cardesc = this.dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
				this.textBox1carDesc.Text = this.cardesc;
				this.carshasi = this.dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
				this.textBox2Shasi.Text = this.carshasi;
				this.caroil = this.dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
				this.comboBoxProd.SelectedValue = this.caroil;
				this.cardriver = this.dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
				this.textBoxDriverName.Text = this.cardriver;
				this.actv = (int)Convert.ToInt16(this.dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());
				this.carnotes = this.dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
				this.textBoxNotes.Text = this.carnotes;
				this.carbra = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
				this.textBoxCarID.Text = this.carbra;
				this.carcomp = this.dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
				this.textBox1Budget.Text = this.dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
				this.CheckBudget();
			}
			else
			{
				this.textBox1Budget.Text = "";
				this.textBoxDriverName.Text = "";
				this.textBoxCarID.Text = "";
				this.textBoxCarNo.Text = "";
				this.textBox1carDesc.Text = "";
				this.textBox2Shasi.Text = "";
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			try
			{
				bool flag = this.actv != -1 && this.textBox1Budget.Text != "" && this.textBoxCarNo.Text != "" && this.textBox2Shasi.Text != "" && this.textBox1carDesc.Text != "" && this.comboBoxProd.SelectedIndex.ToString() != "0";
				if (flag)
				{
					this.mCommand = new SqlCommand(string.Concat(new object[]
					{
						"INSERT INTO [dbo].[Cars](budget,[car_desc],[car_no],[Sach_no],[prod_id],[bra_id],[comp_id],[driver_name],[notes],inuser,active) VALUES(@budgett,@cardesc, @carno, @shashi, @prodid, '",
						LogonForm.braid,
						"', '",
						LogonForm.compid,
						"', @drvname, @notes,@us,'",
						this.actv,
						"')"
					}), CarCodingForm.mConnection);
					CarCodingForm.mConnection.Open();
					this.mCommand.Parameters.AddWithValue("@budgett", this.textBox1Budget.Text);
					this.mCommand.Parameters.AddWithValue("@cardesc", this.textBox1carDesc.Text);
					this.mCommand.Parameters.AddWithValue("@carno", this.textBoxCarNo.Text);
					this.mCommand.Parameters.AddWithValue("@shashi", this.textBox2Shasi.Text);
					this.mCommand.Parameters.AddWithValue("@prodid", this.comboBoxProd.SelectedValue);
					this.mCommand.Parameters.AddWithValue("@drvname", this.textBoxDriverName.Text);
					this.mCommand.Parameters.AddWithValue("@notes", this.textBoxNotes.Text);
					this.mCommand.Parameters.AddWithValue("@us", Environment.MachineName + "-" + Environment.UserName);
					this.mCommand.ExecuteNonQuery();
					CarCodingForm.mConnection.Close();
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
				CarCodingForm.mConnection.Close();
			}
		}

		private void CheckBudget()
		{
			DataTable dataTable = new DataTable();
			try
			{
				dataTable.Clear();
				dataTable.Clone();
				CarCodingForm.mAdatpter = new SqlDataAdapter(string.Concat(new string[]
				{
					"select isnull(sum(amt2),0) totamt2 from [dbo].[Trans_table] where [tyear]='",
					DateTime.Now.Year.ToString(),
					"' and [tmonth]='",
					DateTime.Now.Month.ToString(),
					"' and [bra_id]='",
					LogonForm.braid,
					"' and [comp_id]='",
					LogonForm.compid,
					"' and [prod_id]=@prodidd2 and [trans_type] in(1) and flag in(0,5) and [car_id]='",
					this.carid,
					"' and [car_id_bra]='",
					this.carbra,
					"' and [car_id_comp]='",
					this.carcomp,
					"' and [car_no]='",
					this.carno,
					"'"
				}), CarCodingForm.mConnection);
				CarCodingForm.mAdatpter.SelectCommand.Parameters.AddWithValue("@prodidd2", this.comboBoxProd.SelectedValue);
				CarCodingForm.mAdatpter.Fill(dataTable);
				bool flag = dataTable.Rows.Count >= 1;
				if (flag)
				{
					decimal num = Convert.ToDecimal(dataTable.Rows[0]["totamt2"].ToString());
					decimal num2 = Convert.ToDecimal(this.textBox1Budget.Text);
					decimal num3 = Convert.ToDecimal(this.textBox1Budget.Text) - Convert.ToDecimal(dataTable.Rows[0]["totamt2"].ToString());
					this.textBoxyear.Text = DateTime.Now.Year.ToString();
					this.textBoxmonth.Text = DateTime.Now.Month.ToString();
					this.textBoxamt.Text = this.textBox1Budget.Text;
					this.textBoxamt2.Text = num3.ToString();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public void DisplayDataBudget()
		{
			try
			{
				this.dtOrder.Clear();
				this.dtOrder.Clone();
				this.dataGridView2.Rows.Clear();
				this.dataGridView2.Refresh();
				CarCodingForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlConnection.ClearPool(CarCodingForm.mConnection);
				CarCodingForm.mAdatpter = new SqlDataAdapter(string.Concat(new string[]
				{
					"select [car_id_bra],[car_month],[car_year],[amt] from [Cars_budget] where [car_id]='",
					this.carid,
					"' and [car_id_comp]='",
					this.carcomp,
					"' and [car_id_bra]='",
					this.carbra,
					"' and [car_no]='",
					this.carno,
					"' order by [car_month],[car_year]"
				}), CarCodingForm.mConnection);
				CarCodingForm.mAdatpter.Fill(this.dtOrder);
				bool flag = this.dtOrder.Rows.Count == 0;
				if (!flag)
				{
					this.add_coulmbudget();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public void add_coulmbudget()
		{
			try
			{
				for (int i = 0; i < this.dtOrder.Rows.Count; i++)
				{
					this.dataGridView2.Rows.Add(new object[]
					{
						this.dtOrder.Rows[i]["car_id_bra"].ToString(),
						this.dtOrder.Rows[i]["car_year"].ToString(),
						this.dtOrder.Rows[i]["car_month"].ToString(),
						this.dtOrder.Rows[i]["amt"].ToString()
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
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.button1 = new Button();
			this.dataGridView1 = new DataGridView();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.Column3 = new DataGridViewTextBoxColumn();
			this.Column4 = new DataGridViewTextBoxColumn();
			this.Column1 = new DataGridViewTextBoxColumn();
			this.Column2 = new DataGridViewTextBoxColumn();
			this.Column5 = new DataGridViewTextBoxColumn();
			this.Column6 = new DataGridViewTextBoxColumn();
			this.Column7 = new DataGridViewTextBoxColumn();
			this.Column8 = new DataGridViewTextBoxColumn();
			this.Column9 = new DataGridViewTextBoxColumn();
			this.Column13 = new DataGridViewTextBoxColumn();
			this.panel1 = new Panel();
			this.textBox1 = new TextBox();
			this.label15 = new Label();
			this.textBox2 = new TextBox();
			this.label16 = new Label();
			this.textBox1Budget = new TextBox();
			this.label13 = new Label();
			this.label12 = new Label();
			this.groupBox1 = new GroupBox();
			this.radioActive = new RadioButton();
			this.radioStop = new RadioButton();
			this.panel2 = new Panel();
			this.label14 = new Label();
			this.textBoxamt2 = new TextBox();
			this.button2 = new Button();
			this.dataGridView2 = new DataGridView();
			this.Column12 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
			this.Column10 = new DataGridViewTextBoxColumn();
			this.Column11 = new DataGridViewTextBoxColumn();
			this.label11 = new Label();
			this.label10 = new Label();
			this.label7 = new Label();
			this.textBoxamt = new TextBox();
			this.textBoxmonth = new TextBox();
			this.textBoxyear = new TextBox();
			this.label9 = new Label();
			this.buttonUpdateCars = new Button();
			this.button3 = new Button();
			this.textBoxNotes = new TextBox();
			this.label8 = new Label();
			this.textBoxDriverName = new TextBox();
			this.label5 = new Label();
			this.comboBoxProd = new ComboBox();
			this.label6 = new Label();
			this.textBox2Shasi = new TextBox();
			this.label3 = new Label();
			this.textBox1carDesc = new TextBox();
			this.label1 = new Label();
			this.textBoxCarID = new TextBox();
			this.label2 = new Label();
			this.textBoxCarNo = new TextBox();
			this.label4 = new Label();
			this.pictureBox1 = new PictureBox();
			this.tableLayoutPanel1.SuspendLayout();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			this.panel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.panel2.SuspendLayout();
			((ISupportInitialize)this.dataGridView2).BeginInit();
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
			this.tableLayoutPanel1.Size = new Size(1877, 959);
			this.tableLayoutPanel1.TabIndex = 1;
			this.button1.Dock = DockStyle.Fill;
			this.button1.Enabled = false;
			this.button1.FlatStyle = FlatStyle.Flat;
			this.button1.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.button1.ForeColor = SystemColors.ActiveCaptionText;
			this.button1.Location = new Point(1317, 3);
			this.button1.Name = "button1";
			this.button1.Size = new Size(557, 89);
			this.button1.TabIndex = 6;
			this.button1.Text = "ترميز السيارات";
			this.button1.UseVisualStyleBackColor = false;
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
				this.Column2,
				this.Column5,
				this.Column6,
				this.Column7,
				this.Column8,
				this.Column9,
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
			this.dataGridView1.Location = new Point(3, 98);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowTemplate.Height = 29;
			this.dataGridView1.Size = new Size(1308, 858);
			this.dataGridView1.TabIndex = 3;
			this.dataGridView1.RowHeaderMouseClick += new DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
			this.dataGridViewTextBoxColumn1.HeaderText = "التسلسل";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn2.HeaderText = "رقم اللوحة";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.Column3.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.Column3.HeaderText = "الوصف";
			this.Column3.Name = "Column3";
			this.Column3.Width = 108;
			this.Column4.HeaderText = "رقم الشاسية";
			this.Column4.Name = "Column4";
			this.Column1.HeaderText = "الوقود";
			this.Column1.Name = "Column1";
			this.Column1.Width = 50;
			this.Column2.HeaderText = "السائق";
			this.Column2.Name = "Column2";
			this.Column5.HeaderText = "الحالة";
			this.Column5.Name = "Column5";
			this.Column5.Width = 50;
			this.Column6.HeaderText = "ملاحظات";
			this.Column6.Name = "Column6";
			this.Column7.HeaderText = "prodid";
			this.Column7.Name = "Column7";
			this.Column7.Visible = false;
			this.Column8.HeaderText = "carid";
			this.Column8.Name = "Column8";
			this.Column8.Visible = false;
			this.Column9.HeaderText = "carcomp";
			this.Column9.Name = "Column9";
			this.Column9.Visible = false;
			this.Column13.HeaderText = "المخصص";
			this.Column13.Name = "Column13";
			this.panel1.BackColor = Color.LightSteelBlue;
			this.panel1.Controls.Add(this.textBox1);
			this.panel1.Controls.Add(this.label15);
			this.panel1.Controls.Add(this.textBox2);
			this.panel1.Controls.Add(this.label16);
			this.panel1.Controls.Add(this.textBox1Budget);
			this.panel1.Controls.Add(this.label13);
			this.panel1.Controls.Add(this.label12);
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.label9);
			this.panel1.Controls.Add(this.buttonUpdateCars);
			this.panel1.Controls.Add(this.button3);
			this.panel1.Controls.Add(this.textBoxNotes);
			this.panel1.Controls.Add(this.label8);
			this.panel1.Controls.Add(this.textBoxDriverName);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.comboBoxProd);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.textBox2Shasi);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.textBox1carDesc);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.textBoxCarID);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.textBoxCarNo);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Dock = DockStyle.Fill;
			this.panel1.Location = new Point(1317, 98);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(557, 858);
			this.panel1.TabIndex = 0;
			this.textBox1.BackColor = SystemColors.ButtonHighlight;
			this.textBox1.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBox1.Location = new Point(72, 495);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new Size(279, 33);
			this.textBox1.TabIndex = 149;
			this.textBox1.TextAlign = HorizontalAlignment.Center;
			this.label15.AutoSize = true;
			this.label15.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label15.Location = new Point(357, 496);
			this.label15.Name = "label15";
			this.label15.Size = new Size(109, 29);
			this.label15.TabIndex = 151;
			this.label15.Text = "رقم الموبايل";
			this.textBox2.BackColor = SystemColors.ButtonHighlight;
			this.textBox2.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBox2.Location = new Point(71, 457);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new Size(279, 33);
			this.textBox2.TabIndex = 148;
			this.textBox2.TextAlign = HorizontalAlignment.Center;
			this.label16.AutoSize = true;
			this.label16.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label16.Location = new Point(356, 458);
			this.label16.Name = "label16";
			this.label16.Size = new Size(65, 29);
			this.label16.TabIndex = 150;
			this.label16.Text = "الايميل";
			this.textBox1Budget.BackColor = SystemColors.ButtonHighlight;
			this.textBox1Budget.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBox1Budget.Location = new Point(72, 329);
			this.textBox1Budget.Name = "textBox1Budget";
			this.textBox1Budget.Size = new Size(279, 33);
			this.textBox1Budget.TabIndex = 131;
			this.textBox1Budget.TextAlign = HorizontalAlignment.Center;
			this.label13.AutoSize = true;
			this.label13.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label13.Location = new Point(357, 330);
			this.label13.Name = "label13";
			this.label13.Size = new Size(92, 29);
			this.label13.TabIndex = 147;
			this.label13.Text = "المخصص";
			this.label12.AutoSize = true;
			this.label12.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label12.Location = new Point(357, 380);
			this.label12.Name = "label12";
			this.label12.Size = new Size(112, 29);
			this.label12.TabIndex = 145;
			this.label12.Text = "حالة السيارة";
			this.groupBox1.BackColor = SystemColors.ButtonHighlight;
			this.groupBox1.Controls.Add(this.radioActive);
			this.groupBox1.Controls.Add(this.radioStop);
			this.groupBox1.Location = new Point(72, 368);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(279, 43);
			this.groupBox1.TabIndex = 132;
			this.groupBox1.TabStop = false;
			this.radioActive.AutoSize = true;
			this.radioActive.Font = new Font("Arial", 10f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.radioActive.Location = new Point(195, 12);
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
			this.radioStop.Text = "متوقف";
			this.radioStop.UseVisualStyleBackColor = true;
			this.radioStop.CheckedChanged += new EventHandler(this.radioStop_CheckedChanged);
			this.panel2.BorderStyle = BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.label14);
			this.panel2.Controls.Add(this.textBoxamt2);
			this.panel2.Controls.Add(this.button2);
			this.panel2.Controls.Add(this.dataGridView2);
			this.panel2.Controls.Add(this.label11);
			this.panel2.Controls.Add(this.label10);
			this.panel2.Controls.Add(this.label7);
			this.panel2.Controls.Add(this.textBoxamt);
			this.panel2.Controls.Add(this.textBoxmonth);
			this.panel2.Controls.Add(this.textBoxyear);
			this.panel2.Dock = DockStyle.Bottom;
			this.panel2.Location = new Point(0, 545);
			this.panel2.Name = "panel2";
			this.panel2.Size = new Size(557, 313);
			this.panel2.TabIndex = 143;
			this.label14.AutoSize = true;
			this.label14.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label14.Location = new Point(28, 7);
			this.label14.Name = "label14";
			this.label14.Size = new Size(70, 29);
			this.label14.TabIndex = 149;
			this.label14.Text = "المتبقي";
			this.textBoxamt2.Font = new Font("Arial", 10f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBoxamt2.ForeColor = Color.Blue;
			this.textBoxamt2.Location = new Point(6, 40);
			this.textBoxamt2.Name = "textBoxamt2";
			this.textBoxamt2.Size = new Size(100, 30);
			this.textBoxamt2.TabIndex = 148;
			this.textBoxamt2.TextAlign = HorizontalAlignment.Center;
			this.button2.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 0);
			this.button2.Font = new Font("Arial", 12f, FontStyle.Bold);
			this.button2.Location = new Point(418, 34);
			this.button2.Name = "button2";
			this.button2.Size = new Size(134, 37);
			this.button2.TabIndex = 146;
			this.button2.Text = "تحديث";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Visible = false;
			this.button2.Click += new EventHandler(this.button2_Click);
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
				this.Column12,
				this.dataGridViewTextBoxColumn3,
				this.dataGridViewTextBoxColumn4,
				this.Column10,
				this.Column11
			});
			dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle6.BackColor = SystemColors.Window;
			dataGridViewCellStyle6.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			dataGridViewCellStyle6.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
			this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle6;
			this.dataGridView2.Dock = DockStyle.Bottom;
			this.dataGridView2.Location = new Point(0, 90);
			this.dataGridView2.Name = "dataGridView2";
			this.dataGridView2.RowTemplate.Height = 29;
			this.dataGridView2.Size = new Size(555, 221);
			this.dataGridView2.TabIndex = 147;
			this.Column12.HeaderText = "رقم السيارة";
			this.Column12.Name = "Column12";
			this.dataGridViewTextBoxColumn3.HeaderText = "السنة";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn4.HeaderText = "الشهر";
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.Width = 112;
			this.Column10.HeaderText = "الكمية";
			this.Column10.Name = "Column10";
			this.Column11.HeaderText = "المنصرف";
			this.Column11.Name = "Column11";
			this.label11.AutoSize = true;
			this.label11.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label11.Location = new Point(126, 7);
			this.label11.Name = "label11";
			this.label11.Size = new Size(60, 29);
			this.label11.TabIndex = 146;
			this.label11.Text = "الكمية";
			this.label10.AutoSize = true;
			this.label10.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label10.Location = new Point(215, 7);
			this.label10.Name = "label10";
			this.label10.Size = new Size(60, 29);
			this.label10.TabIndex = 145;
			this.label10.Text = "الشهر";
			this.label7.AutoSize = true;
			this.label7.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label7.Location = new Point(304, 7);
			this.label7.Name = "label7";
			this.label7.Size = new Size(56, 29);
			this.label7.TabIndex = 144;
			this.label7.Text = "السنة";
			this.textBoxamt.Font = new Font("Arial", 10f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBoxamt.ForeColor = Color.Blue;
			this.textBoxamt.Location = new Point(112, 40);
			this.textBoxamt.Name = "textBoxamt";
			this.textBoxamt.Size = new Size(100, 30);
			this.textBoxamt.TabIndex = 2;
			this.textBoxamt.TextAlign = HorizontalAlignment.Center;
			this.textBoxmonth.Font = new Font("Arial", 10f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBoxmonth.ForeColor = Color.Blue;
			this.textBoxmonth.Location = new Point(218, 40);
			this.textBoxmonth.MaxLength = 2;
			this.textBoxmonth.Name = "textBoxmonth";
			this.textBoxmonth.Size = new Size(68, 30);
			this.textBoxmonth.TabIndex = 1;
			this.textBoxmonth.TextAlign = HorizontalAlignment.Center;
			this.textBoxyear.Font = new Font("Arial", 10f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBoxyear.ForeColor = Color.Blue;
			this.textBoxyear.Location = new Point(292, 40);
			this.textBoxyear.MaxLength = 4;
			this.textBoxyear.Name = "textBoxyear";
			this.textBoxyear.Size = new Size(99, 30);
			this.textBoxyear.TabIndex = 0;
			this.textBoxyear.TextAlign = HorizontalAlignment.Center;
			this.label9.AutoSize = true;
			this.label9.Font = new Font("Arial", 16f, FontStyle.Bold);
			this.label9.Location = new Point(141, 1);
			this.label9.Name = "label9";
			this.label9.Size = new Size(183, 37);
			this.label9.TabIndex = 140;
			this.label9.Text = "بيانات السيارات";
			this.buttonUpdateCars.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 0);
			this.buttonUpdateCars.Font = new Font("Arial", 12f, FontStyle.Bold);
			this.buttonUpdateCars.Location = new Point(76, 417);
			this.buttonUpdateCars.Name = "buttonUpdateCars";
			this.buttonUpdateCars.Size = new Size(134, 37);
			this.buttonUpdateCars.TabIndex = 134;
			this.buttonUpdateCars.Text = "تحديث";
			this.buttonUpdateCars.UseVisualStyleBackColor = true;
			this.buttonUpdateCars.Click += new EventHandler(this.button4_Click);
			this.button3.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 0);
			this.button3.Font = new Font("Arial", 12f, FontStyle.Bold);
			this.button3.Location = new Point(216, 417);
			this.button3.Name = "button3";
			this.button3.Size = new Size(134, 37);
			this.button3.TabIndex = 133;
			this.button3.Text = "اضافة";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new EventHandler(this.button3_Click);
			this.textBoxNotes.BackColor = SystemColors.ButtonHighlight;
			this.textBoxNotes.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBoxNotes.Location = new Point(71, 291);
			this.textBoxNotes.Name = "textBoxNotes";
			this.textBoxNotes.Size = new Size(279, 33);
			this.textBoxNotes.TabIndex = 130;
			this.textBoxNotes.TextAlign = HorizontalAlignment.Center;
			this.label8.AutoSize = true;
			this.label8.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label8.Location = new Point(356, 292);
			this.label8.Name = "label8";
			this.label8.Size = new Size(84, 29);
			this.label8.TabIndex = 136;
			this.label8.Text = "ملاحظات";
			this.textBoxDriverName.BackColor = SystemColors.ButtonHighlight;
			this.textBoxDriverName.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBoxDriverName.Location = new Point(71, 252);
			this.textBoxDriverName.Name = "textBoxDriverName";
			this.textBoxDriverName.Size = new Size(279, 33);
			this.textBoxDriverName.TabIndex = 129;
			this.textBoxDriverName.TextAlign = HorizontalAlignment.Center;
			this.label5.AutoSize = true;
			this.label5.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label5.Location = new Point(356, 253);
			this.label5.Name = "label5";
			this.label5.Size = new Size(103, 29);
			this.label5.TabIndex = 132;
			this.label5.Text = "اسم السائق";
			this.comboBoxProd.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxProd.FlatStyle = FlatStyle.Flat;
			this.comboBoxProd.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.comboBoxProd.FormattingEnabled = true;
			this.comboBoxProd.Location = new Point(71, 209);
			this.comboBoxProd.Name = "comboBoxProd";
			this.comboBoxProd.Size = new Size(279, 37);
			this.comboBoxProd.TabIndex = 128;
			this.label6.AutoSize = true;
			this.label6.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label6.Location = new Point(356, 209);
			this.label6.Name = "label6";
			this.label6.Size = new Size(99, 29);
			this.label6.TabIndex = 130;
			this.label6.Text = "نوع الوقود";
			this.textBox2Shasi.BackColor = SystemColors.ButtonHighlight;
			this.textBox2Shasi.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBox2Shasi.Location = new Point(71, 170);
			this.textBox2Shasi.Name = "textBox2Shasi";
			this.textBox2Shasi.Size = new Size(279, 33);
			this.textBox2Shasi.TabIndex = 127;
			this.textBox2Shasi.TextAlign = HorizontalAlignment.Center;
			this.label3.AutoSize = true;
			this.label3.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label3.Location = new Point(356, 171);
			this.label3.Name = "label3";
			this.label3.Size = new Size(110, 29);
			this.label3.TabIndex = 128;
			this.label3.Text = "رقم الشاسية";
			this.textBox1carDesc.BackColor = SystemColors.ButtonHighlight;
			this.textBox1carDesc.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBox1carDesc.Location = new Point(71, 131);
			this.textBox1carDesc.Name = "textBox1carDesc";
			this.textBox1carDesc.Size = new Size(279, 33);
			this.textBox1carDesc.TabIndex = 126;
			this.textBox1carDesc.TextAlign = HorizontalAlignment.Center;
			this.label1.AutoSize = true;
			this.label1.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label1.Location = new Point(356, 132);
			this.label1.Name = "label1";
			this.label1.Size = new Size(125, 29);
			this.label1.TabIndex = 126;
			this.label1.Text = "وصف السيارة";
			this.textBoxCarID.BackColor = SystemColors.ButtonHighlight;
			this.textBoxCarID.Enabled = false;
			this.textBoxCarID.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBoxCarID.Location = new Point(71, 51);
			this.textBoxCarID.Name = "textBoxCarID";
			this.textBoxCarID.ReadOnly = true;
			this.textBoxCarID.Size = new Size(279, 33);
			this.textBoxCarID.TabIndex = 124;
			this.textBoxCarID.TextAlign = HorizontalAlignment.Center;
			this.label2.AutoSize = true;
			this.label2.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label2.Location = new Point(356, 55);
			this.label2.Name = "label2";
			this.label2.Size = new Size(131, 29);
			this.label2.TabIndex = 123;
			this.label2.Text = "تسلسل السيارة";
			this.textBoxCarNo.BackColor = SystemColors.ButtonHighlight;
			this.textBoxCarNo.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBoxCarNo.Location = new Point(71, 91);
			this.textBoxCarNo.Name = "textBoxCarNo";
			this.textBoxCarNo.Size = new Size(279, 33);
			this.textBoxCarNo.TabIndex = 125;
			this.textBoxCarNo.TextAlign = HorizontalAlignment.Center;
			this.textBoxCarNo.TextChanged += new EventHandler(this.textBox2proname_TextChanged);
			this.label4.AutoSize = true;
			this.label4.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label4.Location = new Point(356, 92);
			this.label4.Name = "label4";
			this.label4.Size = new Size(97, 29);
			this.label4.TabIndex = 122;
			this.label4.Text = "رقم اللوحة";
			this.pictureBox1.Dock = DockStyle.Fill;
			this.pictureBox1.Image = Resources.waqood;
			this.pictureBox1.Location = new Point(3, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new Size(1308, 89);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 4;
			this.pictureBox1.TabStop = false;
			base.AutoScaleDimensions = new SizeF(9f, 19f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(1877, 959);
			base.Controls.Add(this.tableLayoutPanel1);
			base.Name = "CarCodingForm";
			this.RightToLeft = RightToLeft.Yes;
			this.Text = "ترميز السيارات";
			base.Load += new EventHandler(this.CarCodingForm_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			((ISupportInitialize)this.dataGridView1).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((ISupportInitialize)this.dataGridView2).EndInit();
			((ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
		}
	}
}
