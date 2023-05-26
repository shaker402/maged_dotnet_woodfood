using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WAQOOD.Properties;

namespace WAQOOD
{
	public class TransForm : Form
	{
		private int actv;

		private int transtype;

		private string carid = string.Empty;

		private string caroil = string.Empty;

		private string currid = string.Empty;

		private string regnn = string.Empty;

		private string prcid = string.Empty;

		private string carnotes = string.Empty;

		private string carcomp = string.Empty;

		private string carbra = string.Empty;

		private string cardriver = string.Empty;

		private string carno = string.Empty;

		private string transid = string.Empty;

		private string carbudget = string.Empty;

		private string transidbra = string.Empty;

		private string cardesc = string.Empty;

		private string carshasi = string.Empty;

		private string prcr = string.Empty;

		private string prcd = string.Empty;

		private SqlCommand mCommand = null;

		private static SqlDataAdapter mAdatpter = null;

		private static SqlConnection mConnection = new SqlConnection(MainForm.str_conn);

		private DataTable dtcars;

		private DataTable dtsta;

		private DataTable dtinfo;

		private IContainer components = null;

		private Panel panel1;

		private PictureBox pictureBox1;

		private Button button4;

		private TableLayoutPanel tableLayoutPanel1;

		private Panel panel2;

		private Panel panel3;

		private TextBox textBoxOrddate;

		private Label label1;

		private TextBox textBox_comp_id;

		private Label label3;

		private Panel panel4;

		private Label label4;

		private TextBox textBoxOrdNo;

		private Label label2;

		private ComboBox comboBoxCarNo;

		private ComboBox comboBoxProdType;

		private Label label5;

		private TextBox textBoxdriver;

		private Label label6;

		private ComboBox comboBoxStation;

		private Label label7;

		private Panel panel5;

		private TextBox textBoxcounter;

		private Label label9;

		private TextBox textBoxAmt;

		private Label label8;

		private Button button1;

		private Button button2;

		private GroupBox groupBox1;

		private RadioButton radioBudget;

		private RadioButton radioTask;

		private PictureBox pictureBoxQR;

		public TransForm()
		{
			this.InitializeComponent();
			base.WindowState = FormWindowState.Maximized;
			TransForm.mConnection = new SqlConnection(MainForm.str_conn);
			this.textBoxOrddate.Text = DateTime.Now.ToString();
			this.textBox_comp_id.Text = LogonForm.braname;
			this.dtcars = new DataTable();
			this.dtsta = new DataTable();
			this.dtinfo = new DataTable();
			this.Fill_Cars();
			this.comboBoxCarNo.Focus();
		}

		private void radioTask_CheckedChanged(object sender, EventArgs e)
		{
			bool @checked = this.radioTask.Checked;
			if (@checked)
			{
				this.transtype = 2;
			}
			else
			{
				this.transtype = 0;
			}
		}

		private void radioBudget_CheckedChanged(object sender, EventArgs e)
		{
			bool @checked = this.radioBudget.Checked;
			if (@checked)
			{
				this.transtype = 1;
			}
			else
			{
				this.transtype = 0;
			}
		}

		private void Fill_Cars()
		{
			try
			{
				TransForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(string.Concat(new string[]
				{
					"SELECT [car_id_bra],[car_no]+[car_desc] car_no,[ID],[car_id_comp] FROM [dbo].[Cars]  where comp_id='",
					LogonForm.compid,
					"' and bra_id='",
					LogonForm.braid,
					"' and active='1' order by [car_id_bra] asc"
				}), TransForm.mConnection);
				DataTable dataTable = new DataTable();
				sqlDataAdapter.Fill(dataTable);
				DataRow dataRow = dataTable.NewRow();
				dataRow[0] = 0;
				dataRow[1] = "-رقم السيارة-";
				dataTable.Rows.InsertAt(dataRow, 0);
				this.comboBoxCarNo.DataSource = dataTable;
				this.comboBoxCarNo.DisplayMember = "car_no";
				this.comboBoxCarNo.ValueMember = "car_id_bra";
				this.comboBoxCarNo.SelectedIndex = 0;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}
		}

		private void GetCarInfo()
		{
			try
			{
				this.dtcars.Clear();
				this.dtcars.Clone();
				TransForm.mAdatpter = new SqlDataAdapter(string.Concat(new string[]
				{
					"SELECT budget,[ID],[car_id_comp],[car_id_bra],[car_desc],[car_no],[Sach_no],products.[prod_id],prod_name,[bra_id],[comp_id],[active],[driver_name],[notes] FROM [dbo].[Cars],products where bra_id='",
					LogonForm.braid,
					"' and comp_id='",
					LogonForm.compid,
					"' and [Cars].prod_id=products.prod_id and car_id_bra=@caridbra order by [car_id_bra] asc"
				}), TransForm.mConnection);
				TransForm.mAdatpter.SelectCommand.Parameters.AddWithValue("@caridbra", this.comboBoxCarNo.SelectedValue);
				TransForm.mAdatpter.Fill(this.dtcars);
				bool flag = this.dtcars.Rows.Count == 1;
				if (flag)
				{
					this.carid = this.dtcars.Rows[0]["ID"].ToString();
					this.carcomp = this.dtcars.Rows[0]["car_id_comp"].ToString();
					this.carbra = this.dtcars.Rows[0]["car_id_bra"].ToString();
					this.caroil = this.dtcars.Rows[0]["prod_id"].ToString();
					this.cardriver = this.dtcars.Rows[0]["driver_name"].ToString();
					this.carno = this.dtcars.Rows[0]["car_no"].ToString();
					this.carbudget = this.dtcars.Rows[0]["budget"].ToString();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void GetStationInfo()
		{
			try
			{
				this.dtsta.Clear();
				this.dtsta.Clone();
				TransForm.mAdatpter = new SqlDataAdapter("SELECT [sta_id],[sta_name],[Stations].gov_id,regn,Stations.prod_id,prc_ry,prc_do FROM [dbo].[Stations],Goverments,Products_prc where [Stations].[gov_id]=Goverments.[gov_id] and Products_prc.gov_id=Stations.[gov_id] and Stations.prod_id=@prodidd and [Stations].sta_id=@staidd and Products_prc.prod_id=Stations.[prod_id] order by [sta_id] asc", TransForm.mConnection);
				TransForm.mAdatpter.SelectCommand.Parameters.AddWithValue("@staidd", this.comboBoxStation.SelectedValue);
				TransForm.mAdatpter.SelectCommand.Parameters.AddWithValue("@prodidd", this.comboBoxProdType.SelectedValue);
				TransForm.mAdatpter.Fill(this.dtsta);
				bool flag = this.dtsta.Rows.Count == 1;
				if (flag)
				{
					this.regnn = this.dtsta.Rows[0]["regn"].ToString();
					bool flag2 = this.regnn == "1";
					if (flag2)
					{
						this.currid = LogonForm.compcurr1;
						bool flag3 = this.currid == "1";
						if (flag3)
						{
							this.prcid = this.dtsta.Rows[0]["prc_ry"].ToString();
						}
						else
						{
							bool flag4 = this.currid == "2";
							if (flag4)
							{
								this.prcid = this.dtsta.Rows[0]["prc_do"].ToString();
							}
						}
					}
					else
					{
						bool flag5 = this.regnn == "2";
						if (flag5)
						{
							this.currid = LogonForm.compcurr2;
							bool flag6 = this.currid == "1";
							if (flag6)
							{
								this.prcid = this.dtsta.Rows[0]["prc_ry"].ToString();
							}
							else
							{
								bool flag7 = this.currid == "2";
								if (flag7)
								{
									this.prcid = this.dtsta.Rows[0]["prc_do"].ToString();
								}
							}
						}
						else
						{
							MessageBox.Show("لايوجد اسعار لهذه المحطة");
						}
					}
				}
				else
				{
					MessageBox.Show("لايوجد اسعار لهذه المحطة");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void comboBoxStation_SelectedValueChanged(object sender, EventArgs e)
		{
			bool flag = this.comboBoxStation.SelectedIndex != 0;
			if (flag)
			{
				this.GetStationInfo();
			}
		}

		private void comboBoxCarNo_SelectedValueChanged(object sender, EventArgs e)
		{
			bool flag = this.comboBoxCarNo.SelectedIndex != 0;
			if (flag)
			{
				this.GetCarInfo();
				this.Fill_Products();
				this.comboBoxProdType.SelectedValue = this.caroil;
				this.comboBoxProdType.Enabled = false;
				this.textBoxdriver.Text = this.cardriver;
				this.Fill_Stations();
			}
		}

		private void Fill_Products()
		{
			try
			{
				TransForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT [prod_id],[prod_name] FROM [dbo].[products] order by [prod_id] asc", TransForm.mConnection);
				DataTable dataTable = new DataTable();
				sqlDataAdapter.Fill(dataTable);
				DataRow dataRow = dataTable.NewRow();
				dataRow[0] = 0;
				dataRow[1] = "-نوع الوقود-";
				dataTable.Rows.InsertAt(dataRow, 0);
				this.comboBoxProdType.DataSource = dataTable;
				this.comboBoxProdType.DisplayMember = "prod_name";
				this.comboBoxProdType.ValueMember = "prod_id";
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
				TransForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT [sta_id],[sta_name],regn,[Stations].gov_id,Stations.prod_id,prc_ry,prc_do FROM [dbo].[Stations],Goverments,Products_prc where [Stations].[gov_id]=Goverments.[gov_id] and Products_prc.gov_id=Stations.[gov_id] and Stations.prod_id='" + this.caroil + "' and Products_prc.prod_id=Stations.[prod_id] and active='1' order by [sta_id] asc", TransForm.mConnection);
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

		private void button1_Click(object sender, EventArgs e)
		{
			bool flag = this.transtype != 0 && this.textBoxcounter.Text != "" && this.textBoxdriver.Text != "" && this.textBoxAmt.Text != "" && this.comboBoxCarNo.SelectedIndex != 0 && this.comboBoxProdType.SelectedIndex != 0 && this.comboBoxStation.SelectedIndex != 0;
			if (flag)
			{
				bool flag2 = int.Parse(this.textBoxAmt.Text) <= 0;
				if (flag2)
				{
					MessageBox.Show("كمية الطلب اقل او تساوي 0 تاكد من الكمية");
				}
				else
				{
					this.CheckOrders();
				}
			}
			else
			{
				MessageBox.Show("بيانات غير مكتملة!");
			}
		}

		private void InsertOrder()
		{
			try
			{
				bool flag = this.transtype != 0 && this.textBoxcounter.Text != "" && this.textBoxdriver.Text != "" && this.textBoxAmt.Text != "" && this.comboBoxCarNo.SelectedIndex != 0 && this.comboBoxProdType.SelectedIndex != 0 && this.comboBoxStation.SelectedIndex != 0;
				if (flag)
				{
					DialogResult dialogResult = MessageBox.Show("كمية الطلب:\n" + this.textBoxAmt.Text.ToString() + "\nهل تريد الحفظ؟", "Confirmation", MessageBoxButtons.YesNo);
					if (dialogResult == DialogResult.Yes)
					{
						this.mCommand = new SqlCommand(string.Concat(new object[]
						{
							"INSERT INTO [dbo].[Trans_table](flag,[tyear],[tmonth],[bra_id],[comp_id],[sta_id],[prod_id],[trans_type],[car_id],[car_id_bra],[car_id_comp],[car_no],[car_driver],budget,[car_counter],regn,[amt],curr_id,prc_t,[inuser_comp1],[pc_logs])VALUES('1','",
							DateTime.Now.Year,
							"','",
							DateTime.Now.Month,
							"','",
							LogonForm.braid,
							"','",
							LogonForm.compid,
							"',@sta,@prodid,'",
							this.transtype,
							"','",
							this.carid,
							"',@caridbra,'",
							this.carcomp,
							"','",
							this.carno,
							"',@driverr,'",
							this.carbudget,
							"',@counterr,'",
							this.regnn,
							"',@amntt,'",
							this.currid,
							"','",
							this.prcid,
							"','",
							LogonForm.userID,
							"','",
							Environment.MachineName,
							"-",
							Environment.UserName,
							"')"
						}), TransForm.mConnection);
						TransForm.mConnection.Open();
						this.mCommand.Parameters.AddWithValue("@driverr", this.textBoxdriver.Text);
						this.mCommand.Parameters.AddWithValue("@counterr", this.textBoxcounter.Text);
						this.mCommand.Parameters.AddWithValue("@amntt", this.textBoxAmt.Text);
						this.mCommand.Parameters.AddWithValue("@caridbra", this.comboBoxCarNo.SelectedValue);
						this.mCommand.Parameters.AddWithValue("@prodid", this.caroil);
						this.mCommand.Parameters.AddWithValue("@sta", this.comboBoxStation.SelectedValue);
						int num = this.mCommand.ExecuteNonQuery();
						TransForm.mConnection.Close();
						bool flag2 = num >= 1;
						if (flag2)
						{
							MessageBox.Show(" تم حفظ البيانات بنجاح" + num.ToString());
							this.textBoxdriver.Text = "";
							this.textBoxcounter.Text = "";
							this.textBoxAmt.Text = "";
							this.textBoxcounter.Text = "";
							this.comboBoxCarNo.ResetText();
							this.comboBoxProdType.ResetText();
						}
						else
						{
							MessageBox.Show("يوجد خطأ في حفظ الطلب");
						}
					}
				}
				else
				{
					MessageBox.Show("بيانات غير مكتملة!");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				TransForm.mConnection.Close();
			}
		}

		private void GetMaxOrder()
		{
			try
			{
				this.dtinfo.Clear();
				this.dtinfo.Clone();
				TransForm.mAdatpter = new SqlDataAdapter(string.Concat(new string[]
				{
					"select max(trans_id_sta) trans_id,max(trans_id_bra) trans_id_bra from [dbo].[Trans_table] where [tyear]='",
					DateTime.Now.Year.ToString(),
					"' and [tmonth]='",
					DateTime.Now.Month.ToString(),
					"' and [bra_id]='",
					LogonForm.braid,
					"' and [comp_id]='",
					LogonForm.compid,
					"' and [sta_id]=@staidd2 and [prod_id]=@prodidd2 and [trans_type] in(1,2,3) and CAST(ORDER_DATE AS DATE)=CAST(GetDate() As date) and flag=5 and [car_id]='",
					this.carid,
					"' and [car_id_bra]='",
					this.carbra,
					"' and [car_id_comp]='",
					this.carcomp,
					"' and [car_no]='",
					this.carno,
					"' and [amt]='",
					this.textBoxAmt.Text,
					"'"
				}), TransForm.mConnection);
				TransForm.mAdatpter.SelectCommand.Parameters.AddWithValue("@staidd2", this.comboBoxStation.SelectedValue);
				TransForm.mAdatpter.SelectCommand.Parameters.AddWithValue("@prodidd2", this.comboBoxProdType.SelectedValue);
				TransForm.mAdatpter.Fill(this.dtinfo);
				bool flag = this.dtinfo.Rows.Count == 1;
				if (flag)
				{
					this.transid = this.dtinfo.Rows[0]["trans_id"].ToString();
					this.transidbra = this.dtinfo.Rows[0]["trans_id_bra"].ToString();
					this.textBoxOrdNo.Text = this.transidbra;
				}
				else
				{
					MessageBox.Show("لايوجد رقم طلب");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void CheckOrders()
		{
			DataTable dataTable = new DataTable();
			try
			{
				dataTable.Clear();
				dataTable.Clone();
				TransForm.mAdatpter = new SqlDataAdapter(string.Concat(new object[]
				{
					"select CAST(ORDER_DATE AS DATE) ORDER_DATE from [dbo].[Trans_table] where [tyear]='",
					DateTime.Now.Year,
					"' and [tmonth]='",
					DateTime.Now.Month,
					"' and [bra_id]='",
					LogonForm.braid,
					"' and [comp_id]='",
					LogonForm.compid,
					"' and [prod_id]=@prodidd2 and [trans_type] in(1,2,3)  and flag in(0,1,2,3) and [car_id]='",
					this.carid,
					"' and [car_id_bra]='",
					this.carbra,
					"' and [car_id_comp]='",
					this.carcomp,
					"' and [car_no]='",
					this.carno,
					"'"
				}), TransForm.mConnection);
				TransForm.mAdatpter.SelectCommand.Parameters.AddWithValue("@prodidd2", this.comboBoxProdType.SelectedValue);
				TransForm.mAdatpter.Fill(dataTable);
				bool flag = dataTable.Rows.Count >= 1;
				if (flag)
				{
					string text = dataTable.Rows[0]["ORDER_DATE"].ToString();
					MessageBox.Show("يوجد طلب سابق للسيارة بتاريخ:" + ((IConvertible)text).ToDateTime(null).ToString("dd/MM/yyyy"));
				}
				else
				{
					bool flag2 = this.transtype == 2;
					if (flag2)
					{
						this.InsertOrder();
					}
					else
					{
						this.CheckBudget();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void CheckBudget()
		{
			DataTable dataTable = new DataTable();
			try
			{
				dataTable.Clear();
				dataTable.Clone();
				TransForm.mAdatpter = new SqlDataAdapter(string.Concat(new object[]
				{
					"select isnull(sum(amt2),0) totamt2 from [dbo].[Trans_table] where [tyear]='",
					DateTime.Now.Year,
					"' and [tmonth]='",
					DateTime.Now.Month,
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
				}), TransForm.mConnection);
				TransForm.mAdatpter.SelectCommand.Parameters.AddWithValue("@prodidd2", this.comboBoxProdType.SelectedValue);
				TransForm.mAdatpter.Fill(dataTable);
				bool flag = dataTable.Rows.Count >= 1;
				if (flag)
				{
					decimal d = Convert.ToDecimal(dataTable.Rows[0]["totamt2"].ToString()) + Convert.ToDecimal(this.textBoxAmt.Text.ToString());
					decimal d2 = Convert.ToDecimal(this.carbudget);
					decimal num = Convert.ToDecimal(this.carbudget) - Convert.ToDecimal(dataTable.Rows[0]["totamt2"].ToString());
					bool flag2 = d > d2 || Convert.ToDecimal(dataTable.Rows[0]["totamt2"].ToString()) < decimal.Zero;
					if (flag2)
					{
						MessageBox.Show(" تجاوز في المخصص لهذا الشهر المتبقي=" + num.ToString());
					}
					else
					{
						bool flag3 = d < d2;
						if (flag3)
						{
							this.InsertOrder();
						}
						else
						{
							MessageBox.Show("خطأ في الاستعلام عن المخصص");
						}
					}
				}
				else
				{
					this.InsertOrder();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void TransForm_Load(object sender, EventArgs e)
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(TransForm));
			this.panel1 = new Panel();
			this.pictureBox1 = new PictureBox();
			this.button4 = new Button();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.panel2 = new Panel();
			this.panel4 = new Panel();
			this.pictureBoxQR = new PictureBox();
			this.groupBox1 = new GroupBox();
			this.radioBudget = new RadioButton();
			this.radioTask = new RadioButton();
			this.panel5 = new Panel();
			this.button1 = new Button();
			this.button2 = new Button();
			this.textBoxcounter = new TextBox();
			this.label9 = new Label();
			this.textBoxAmt = new TextBox();
			this.label8 = new Label();
			this.comboBoxStation = new ComboBox();
			this.label7 = new Label();
			this.textBoxdriver = new TextBox();
			this.label6 = new Label();
			this.comboBoxProdType = new ComboBox();
			this.label5 = new Label();
			this.comboBoxCarNo = new ComboBox();
			this.label4 = new Label();
			this.panel3 = new Panel();
			this.textBoxOrdNo = new TextBox();
			this.label2 = new Label();
			this.textBoxOrddate = new TextBox();
			this.label1 = new Label();
			this.textBox_comp_id = new TextBox();
			this.label3 = new Label();
			this.panel1.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel4.SuspendLayout();
			((ISupportInitialize)this.pictureBoxQR).BeginInit();
			this.groupBox1.SuspendLayout();
			this.panel5.SuspendLayout();
			this.panel3.SuspendLayout();
			base.SuspendLayout();
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Dock = DockStyle.Fill;
			this.panel1.Location = new Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(691, 59);
			this.panel1.TabIndex = 2;
			this.pictureBox1.BorderStyle = BorderStyle.FixedSingle;
			this.pictureBox1.Dock = DockStyle.Fill;
			this.pictureBox1.Image = Resources.waqood;
			this.pictureBox1.Location = new Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new Size(691, 59);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.button4.Dock = DockStyle.Fill;
			this.button4.Enabled = false;
			this.button4.FlatStyle = FlatStyle.Flat;
			this.button4.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.button4.Location = new Point(700, 3);
			this.button4.Name = "button4";
			this.button4.Size = new Size(691, 59);
			this.button4.TabIndex = 3;
			this.button4.Text = "طلب وقود";
			this.button4.UseVisualStyleBackColor = true;
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
			this.tableLayoutPanel1.Size = new Size(1394, 657);
			this.tableLayoutPanel1.TabIndex = 1;
			this.tableLayoutPanel1.SetColumnSpan(this.panel2, 2);
			this.panel2.Controls.Add(this.panel4);
			this.panel2.Controls.Add(this.panel3);
			this.panel2.Dock = DockStyle.Fill;
			this.panel2.Location = new Point(3, 68);
			this.panel2.Name = "panel2";
			this.panel2.Size = new Size(1388, 586);
			this.panel2.TabIndex = 4;
			this.panel4.BackColor = SystemColors.GradientActiveCaption;
			this.panel4.BorderStyle = BorderStyle.Fixed3D;
			this.panel4.Controls.Add(this.pictureBoxQR);
			this.panel4.Controls.Add(this.groupBox1);
			this.panel4.Controls.Add(this.panel5);
			this.panel4.Controls.Add(this.textBoxcounter);
			this.panel4.Controls.Add(this.label9);
			this.panel4.Controls.Add(this.textBoxAmt);
			this.panel4.Controls.Add(this.label8);
			this.panel4.Controls.Add(this.comboBoxStation);
			this.panel4.Controls.Add(this.label7);
			this.panel4.Controls.Add(this.textBoxdriver);
			this.panel4.Controls.Add(this.label6);
			this.panel4.Controls.Add(this.comboBoxProdType);
			this.panel4.Controls.Add(this.label5);
			this.panel4.Controls.Add(this.comboBoxCarNo);
			this.panel4.Controls.Add(this.label4);
			this.panel4.Dock = DockStyle.Fill;
			this.panel4.Location = new Point(0, 63);
			this.panel4.Name = "panel4";
			this.panel4.Size = new Size(1388, 523);
			this.panel4.TabIndex = 1;
			this.pictureBoxQR.Image = (Image)componentResourceManager.GetObject("pictureBoxQR.Image");
			this.pictureBoxQR.Location = new Point(5, 202);
			this.pictureBoxQR.Name = "pictureBoxQR";
			this.pictureBoxQR.Size = new Size(200, 200);
			this.pictureBoxQR.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBoxQR.TabIndex = 147;
			this.pictureBoxQR.TabStop = false;
			this.groupBox1.BackColor = SystemColors.ButtonHighlight;
			this.groupBox1.Controls.Add(this.radioBudget);
			this.groupBox1.Controls.Add(this.radioTask);
			this.groupBox1.Font = new Font("Arial", 10f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.groupBox1.Location = new Point(876, 137);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(404, 45);
			this.groupBox1.TabIndex = 97;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "نوع الصرف";
			this.radioBudget.AutoSize = true;
			this.radioBudget.Font = new Font("Arial", 10f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.radioBudget.Location = new Point(202, 14);
			this.radioBudget.Name = "radioBudget";
			this.radioBudget.Size = new Size(89, 28);
			this.radioBudget.TabIndex = 1;
			this.radioBudget.Text = "مخصص";
			this.radioBudget.UseVisualStyleBackColor = true;
			this.radioBudget.CheckedChanged += new EventHandler(this.radioBudget_CheckedChanged);
			this.radioTask.AutoSize = true;
			this.radioTask.Font = new Font("Arial", 10f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.radioTask.Location = new Point(80, 14);
			this.radioTask.Name = "radioTask";
			this.radioTask.Size = new Size(101, 28);
			this.radioTask.TabIndex = 0;
			this.radioTask.Text = "مهمة سفر";
			this.radioTask.UseVisualStyleBackColor = true;
			this.radioTask.CheckedChanged += new EventHandler(this.radioTask_CheckedChanged);
			this.panel5.BorderStyle = BorderStyle.FixedSingle;
			this.panel5.Controls.Add(this.button1);
			this.panel5.Controls.Add(this.button2);
			this.panel5.Dock = DockStyle.Bottom;
			this.panel5.Location = new Point(0, 419);
			this.panel5.Name = "panel5";
			this.panel5.Size = new Size(1384, 100);
			this.panel5.TabIndex = 101;
			this.button1.BackColor = Color.White;
			this.button1.Cursor = Cursors.Hand;
			this.button1.FlatStyle = FlatStyle.Flat;
			this.button1.Font = new Font("Arial", 16f, FontStyle.Bold);
			this.button1.ForeColor = Color.FromArgb(64, 64, 64);
			this.button1.Location = new Point(618, 27);
			this.button1.Name = "button1";
			this.button1.Size = new Size(146, 52);
			this.button1.TabIndex = 98;
			this.button1.Text = "حفظ";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.button2.BackColor = Color.White;
			this.button2.Cursor = Cursors.Hand;
			this.button2.FlatStyle = FlatStyle.Flat;
			this.button2.Font = new Font("Arial", 16f, FontStyle.Bold);
			this.button2.ForeColor = Color.FromArgb(64, 64, 64);
			this.button2.Location = new Point(875, 27);
			this.button2.Name = "button2";
			this.button2.Size = new Size(146, 52);
			this.button2.TabIndex = 99;
			this.button2.Text = "الغاء";
			this.button2.UseVisualStyleBackColor = false;
			this.textBoxcounter.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBoxcounter.Location = new Point(48, 86);
			this.textBoxcounter.MaxLength = 10;
			this.textBoxcounter.Name = "textBoxcounter";
			this.textBoxcounter.Size = new Size(279, 33);
			this.textBoxcounter.TabIndex = 96;
			this.textBoxcounter.TextAlign = HorizontalAlignment.Center;
			this.label9.AutoSize = true;
			this.label9.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label9.Location = new Point(336, 87);
			this.label9.Name = "label9";
			this.label9.Size = new Size(87, 29);
			this.label9.TabIndex = 99;
			this.label9.Text = "رقم العداد";
			this.textBoxAmt.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBoxAmt.Location = new Point(468, 85);
			this.textBoxAmt.MaxLength = 4;
			this.textBoxAmt.Name = "textBoxAmt";
			this.textBoxAmt.Size = new Size(297, 33);
			this.textBoxAmt.TabIndex = 95;
			this.textBoxAmt.TextAlign = HorizontalAlignment.Center;
			this.label8.AutoSize = true;
			this.label8.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label8.Location = new Point(781, 85);
			this.label8.Name = "label8";
			this.label8.Size = new Size(75, 29);
			this.label8.TabIndex = 97;
			this.label8.Text = "الكـمــية";
			this.comboBoxStation.AutoCompleteMode = AutoCompleteMode.Suggest;
			this.comboBoxStation.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.comboBoxStation.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxStation.FlatStyle = FlatStyle.Flat;
			this.comboBoxStation.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.comboBoxStation.FormattingEnabled = true;
			this.comboBoxStation.Location = new Point(876, 82);
			this.comboBoxStation.Name = "comboBoxStation";
			this.comboBoxStation.Size = new Size(377, 37);
			this.comboBoxStation.TabIndex = 94;
			this.comboBoxStation.SelectedValueChanged += new EventHandler(this.comboBoxStation_SelectedValueChanged);
			this.label7.AutoSize = true;
			this.label7.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label7.Location = new Point(1252, 85);
			this.label7.Name = "label7";
			this.label7.Size = new Size(70, 29);
			this.label7.TabIndex = 95;
			this.label7.Text = "المحطة";
			this.textBoxdriver.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBoxdriver.Location = new Point(48, 29);
			this.textBoxdriver.Name = "textBoxdriver";
			this.textBoxdriver.Size = new Size(279, 33);
			this.textBoxdriver.TabIndex = 93;
			this.textBoxdriver.TextAlign = HorizontalAlignment.Center;
			this.label6.AutoSize = true;
			this.label6.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label6.Location = new Point(336, 32);
			this.label6.Name = "label6";
			this.label6.Size = new Size(67, 29);
			this.label6.TabIndex = 92;
			this.label6.Text = "السائق";
			this.comboBoxProdType.AutoCompleteMode = AutoCompleteMode.Suggest;
			this.comboBoxProdType.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.comboBoxProdType.BackColor = SystemColors.InactiveBorder;
			this.comboBoxProdType.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxProdType.FlatStyle = FlatStyle.Flat;
			this.comboBoxProdType.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.comboBoxProdType.FormattingEnabled = true;
			this.comboBoxProdType.Location = new Point(468, 25);
			this.comboBoxProdType.Name = "comboBoxProdType";
			this.comboBoxProdType.Size = new Size(297, 37);
			this.comboBoxProdType.TabIndex = 94;
			this.label5.AutoSize = true;
			this.label5.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label5.Location = new Point(771, 25);
			this.label5.Name = "label5";
			this.label5.Size = new Size(99, 29);
			this.label5.TabIndex = 93;
			this.label5.Text = "نوع الوقود";
			this.comboBoxCarNo.AutoCompleteMode = AutoCompleteMode.Suggest;
			this.comboBoxCarNo.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.comboBoxCarNo.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxCarNo.FlatStyle = FlatStyle.Flat;
			this.comboBoxCarNo.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.comboBoxCarNo.FormattingEnabled = true;
			this.comboBoxCarNo.Location = new Point(876, 22);
			this.comboBoxCarNo.Name = "comboBoxCarNo";
			this.comboBoxCarNo.Size = new Size(377, 37);
			this.comboBoxCarNo.TabIndex = 92;
			this.comboBoxCarNo.SelectedValueChanged += new EventHandler(this.comboBoxCarNo_SelectedValueChanged);
			this.label4.AutoSize = true;
			this.label4.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label4.Location = new Point(1252, 25);
			this.label4.Name = "label4";
			this.label4.Size = new Size(103, 29);
			this.label4.TabIndex = 90;
			this.label4.Text = "رقم السيارة";
			this.panel3.BackColor = SystemColors.ControlLight;
			this.panel3.BorderStyle = BorderStyle.FixedSingle;
			this.panel3.Controls.Add(this.textBoxOrdNo);
			this.panel3.Controls.Add(this.label2);
			this.panel3.Controls.Add(this.textBoxOrddate);
			this.panel3.Controls.Add(this.label1);
			this.panel3.Controls.Add(this.textBox_comp_id);
			this.panel3.Controls.Add(this.label3);
			this.panel3.Dock = DockStyle.Top;
			this.panel3.Location = new Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new Size(1388, 63);
			this.panel3.TabIndex = 0;
			this.textBoxOrdNo.BackColor = SystemColors.ButtonHighlight;
			this.textBoxOrdNo.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBoxOrdNo.Location = new Point(52, 10);
			this.textBoxOrdNo.Name = "textBoxOrdNo";
			this.textBoxOrdNo.Size = new Size(279, 33);
			this.textBoxOrdNo.TabIndex = 89;
			this.textBoxOrdNo.TextAlign = HorizontalAlignment.Center;
			this.label2.AutoSize = true;
			this.label2.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label2.Location = new Point(337, 10);
			this.label2.Name = "label2";
			this.label2.Size = new Size(92, 29);
			this.label2.TabIndex = 88;
			this.label2.Text = "رقم الطلب";
			this.textBoxOrddate.BackColor = SystemColors.ButtonHighlight;
			this.textBoxOrddate.Enabled = false;
			this.textBoxOrddate.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBoxOrddate.Location = new Point(455, 9);
			this.textBoxOrddate.Name = "textBoxOrddate";
			this.textBoxOrddate.ReadOnly = true;
			this.textBoxOrddate.Size = new Size(297, 33);
			this.textBoxOrddate.TabIndex = 91;
			this.textBoxOrddate.TextAlign = HorizontalAlignment.Center;
			this.label1.AutoSize = true;
			this.label1.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label1.Location = new Point(772, 13);
			this.label1.Name = "label1";
			this.label1.Size = new Size(67, 29);
			this.label1.TabIndex = 90;
			this.label1.Text = "التاريخ";
			this.textBox_comp_id.BackColor = SystemColors.ButtonHighlight;
			this.textBox_comp_id.Enabled = false;
			this.textBox_comp_id.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBox_comp_id.Location = new Point(877, 10);
			this.textBox_comp_id.Name = "textBox_comp_id";
			this.textBox_comp_id.ReadOnly = true;
			this.textBox_comp_id.Size = new Size(297, 33);
			this.textBox_comp_id.TabIndex = 89;
			this.textBox_comp_id.TextAlign = HorizontalAlignment.Center;
			this.label3.AutoSize = true;
			this.label3.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label3.Location = new Point(1180, 13);
			this.label3.Name = "label3";
			this.label3.Size = new Size(68, 29);
			this.label3.TabIndex = 88;
			this.label3.Text = "الشركة";
			base.AutoScaleDimensions = new SizeF(9f, 19f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(1394, 657);
			base.Controls.Add(this.tableLayoutPanel1);
			base.Name = "TransForm";
			this.RightToLeft = RightToLeft.Yes;
			this.Text = "طلب وقود";
			base.Load += new EventHandler(this.TransForm_Load);
			this.panel1.ResumeLayout(false);
			((ISupportInitialize)this.pictureBox1).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			((ISupportInitialize)this.pictureBoxQR).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.panel5.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
