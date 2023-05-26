using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WAQOOD.Properties;

namespace WAQOOD
{
	public class SouthTransForm : Form
	{
		private int actv;

		private string carid = string.Empty;

		private string caroil = string.Empty;

		private string currid = string.Empty;

		private string prcid = string.Empty;

		private string carnotes = string.Empty;

		private string carcomp = string.Empty;

		private string carbra = string.Empty;

		private string cardriver = string.Empty;

		private string carno = string.Empty;

		private string transid = string.Empty;

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

		private IContainer components = null;

		private TextBox textBoxOrdNo;

		private Label label2;

		private TextBox textBoxOrddate;

		private Label label1;

		private Label label3;

		private Panel panel3;

		private TextBox textBox_comp_id;

		private PictureBox pictureBoxQR;

		private GroupBox groupBox1;

		private RadioButton radioActive;

		private RadioButton radioStop;

		private Panel panel5;

		private Button button3;

		private Button button1;

		private Button button2;

		private TextBox textBoxcounter;

		private Label label9;

		private TextBox textBoxAmt;

		private Panel panel4;

		private Label label8;

		private ComboBox comboBoxStation;

		private Label label7;

		private TextBox textBoxdriver;

		private Label label6;

		private ComboBox comboBoxProdType;

		private Label label5;

		private ComboBox comboBoxCarNo;

		private Label label4;

		private Panel panel1;

		private PictureBox pictureBox1;

		private Button button4;

		private TableLayoutPanel tableLayoutPanel1;

		private Panel panel2;

		public SouthTransForm()
		{
			this.InitializeComponent();
			base.WindowState = FormWindowState.Maximized;
			SouthTransForm.mConnection = new SqlConnection(MainForm.str_conn);
			this.textBoxOrddate.Text = DateTime.Now.ToString();
			this.textBox_comp_id.Text = LogonForm.braname;
			this.dtcars = new DataTable();
			this.dtsta = new DataTable();
			this.Fill_Cars();
		}

		private void comboBoxStation_SelectedValueChanged(object sender, EventArgs e)
		{
			bool flag = this.comboBoxStation.SelectedIndex != 0;
			if (flag)
			{
				this.GetStationInfo();
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			try
			{
				SouthTransForm.mConnection.Close();
				bool flag = this.transidbra.Length != 0;
				if (flag)
				{
					this.mCommand = new SqlCommand(string.Concat(new string[]
					{
						"update [dbo].[Trans_table] set flag='1' where flag='0' and trans_id_bra='",
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
						"'and prod_id='",
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
					}), SouthTransForm.mConnection);
					SouthTransForm.mConnection.Open();
					int num = this.mCommand.ExecuteNonQuery();
					SouthTransForm.mConnection.Close();
					MessageBox.Show("تم الترحيل بنجاح" + num.ToString());
				}
				else
				{
					MessageBox.Show("بيانات غير مكتملة!");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				SouthTransForm.mConnection.Close();
			}
		}

		private void GetStationInfo()
		{
			try
			{
				this.dtsta.Clear();
				this.dtsta.Clone();
				SouthTransForm.mAdatpter = new SqlDataAdapter("SELECT [sta_id],[sta_name],[Stations].gov_id,Currency.curr_id,Stations.prod_id,prc_ry,prc_do FROM [dbo].[Stations],Goverments,Products_prc,Currency where Currency.curr_id!=[Stations].curr_id and [Stations].[gov_id]=Goverments.[gov_id] and Products_prc.gov_id=Stations.[gov_id] and Stations.prod_id=@prodidd and [Stations].sta_id=@staidd and Products_prc.prod_id=Stations.[prod_id] order by [sta_id] asc", SouthTransForm.mConnection);
				SouthTransForm.mAdatpter.SelectCommand.Parameters.AddWithValue("@staidd", this.comboBoxStation.SelectedValue);
				SouthTransForm.mAdatpter.SelectCommand.Parameters.AddWithValue("@prodidd", this.comboBoxProdType.SelectedValue);
				SouthTransForm.mAdatpter.Fill(this.dtsta);
				bool flag = this.dtsta.Rows.Count == 1;
				if (flag)
				{
					this.currid = this.dtsta.Rows[0]["curr_id"].ToString();
					bool flag2 = this.currid == "1";
					if (flag2)
					{
						bool flag3 = LogonForm.compcurr1 == "1";
						if (flag3)
						{
							this.prcid = this.dtsta.Rows[0]["prc_ry"].ToString();
						}
						else
						{
							bool flag4 = LogonForm.compcurr1 == "2";
							if (flag4)
							{
								this.prcid = this.dtsta.Rows[0]["prc_do"].ToString();
							}
							else
							{
								this.prcid = "0";
								MessageBox.Show("لايوجد اسعار لهذه المحطة");
							}
						}
					}
					else
					{
						bool flag5 = this.currid == "2";
						if (flag5)
						{
							this.prcid = this.dtsta.Rows[0]["prc_do"].ToString();
						}
						else
						{
							this.prcid = "0";
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

		private void button1_Click(object sender, EventArgs e)
		{
			bool flag = this.textBoxAmt.Text != "" && this.comboBoxCarNo.SelectedValue.ToString() != null && this.comboBoxProdType.SelectedValue.ToString() != null && this.comboBoxStation.SelectedValue.ToString() != null;
			if (flag)
			{
				this.CheckOrders();
			}
			else
			{
				MessageBox.Show("بيانات غير مكتملة!");
			}
		}

		private void CheckOrders()
		{
			DataTable dataTable = new DataTable();
			try
			{
				dataTable.Clear();
				dataTable.Clone();
				SouthTransForm.mAdatpter = new SqlDataAdapter(string.Concat(new string[]
				{
					"select CAST(ORDER_DATE AS DATE) ORDER_DATE from [dbo].[Trans_table] where [tyear]='",
					DateTime.Now.Year.ToString(),
					"' and [tmonth]='",
					DateTime.Now.Month.ToString(),
					"' and [bra_id]='",
					LogonForm.braid,
					"' and [comp_id]='",
					LogonForm.compid,
					"' and [prod_id]=@prodidd2 and [trans_type]='1' and flag in(0,1,2) and [car_id]='",
					this.carid,
					"' and [car_id_bra]='",
					this.carbra,
					"' and [car_id_comp]='",
					this.carcomp,
					"' and [car_no]='",
					this.carno,
					"'"
				}), SouthTransForm.mConnection);
				SouthTransForm.mAdatpter.SelectCommand.Parameters.AddWithValue("@prodidd2", this.comboBoxProdType.SelectedValue);
				SouthTransForm.mAdatpter.Fill(dataTable);
				MessageBox.Show(dataTable.Rows.Count.ToString());
				bool flag = dataTable.Rows.Count >= 1;
				if (flag)
				{
					string text = dataTable.Rows[0]["ORDER_DATE"].ToString();
					MessageBox.Show("يوجد طلب سابق للسيارة بتاريخ:" + ((IConvertible)text).ToDateTime(null).ToString("dd/MM/yyyy"));
				}
				else
				{
					MessageBox.Show(" اضافة الطلب" + this.carno);
					this.InsertOrder();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void InsertOrder()
		{
			try
			{
				bool flag = this.textBoxAmt.Text != "" && this.comboBoxCarNo.SelectedValue.ToString() != null && this.comboBoxProdType.SelectedValue.ToString() != null && this.comboBoxStation.SelectedValue.ToString() != null;
				if (flag)
				{
					this.mCommand = new SqlCommand(string.Concat(new object[]
					{
						"INSERT INTO [dbo].[Trans_table]([tyear],[tmonth],[bra_id],[comp_id],[sta_id],[prod_id],[trans_type],[order_date],[car_id],[car_id_bra],[car_id_comp],[car_no],[car_driver],[car_counter],[amt],curr_id,prc,[inuser_comp1],[pc_logs])VALUES('",
						DateTime.Now.Year.ToString(),
						"','",
						DateTime.Now.Month.ToString(),
						"','",
						LogonForm.braid,
						"','",
						LogonForm.compid,
						"',@sta,@prodid,'1','",
						DateTime.Now,
						"','",
						this.carid,
						"',@caridbra,'",
						this.carcomp,
						"','",
						this.carno,
						"',@driverr,@counterr,@amntt,'",
						LogonForm.compcurr1,
						"','",
						this.prcid,
						"','",
						LogonForm.userID,
						"','",
						Environment.MachineName,
						"-",
						Environment.UserName,
						"')"
					}), SouthTransForm.mConnection);
					SouthTransForm.mConnection.Open();
					this.mCommand.Parameters.AddWithValue("@driverr", this.textBoxdriver.Text);
					this.mCommand.Parameters.AddWithValue("@counterr", this.textBoxcounter.Text);
					this.mCommand.Parameters.AddWithValue("@amntt", this.textBoxAmt.Text);
					this.mCommand.Parameters.AddWithValue("@caridbra", this.comboBoxCarNo.SelectedValue);
					this.mCommand.Parameters.AddWithValue("@prodid", this.caroil);
					this.mCommand.Parameters.AddWithValue("@sta", this.comboBoxStation.SelectedValue);
					this.mCommand.ExecuteNonQuery();
					SouthTransForm.mConnection.Close();
					MessageBox.Show(" تم حفظ البيانات بنجاح");
					this.GetMaxOrder();
					this.textBoxdriver.Text = "";
					this.textBoxcounter.Text = "";
					this.textBoxAmt.Text = "";
				}
				else
				{
					MessageBox.Show("بيانات غير مكتملة!");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				SouthTransForm.mConnection.Close();
			}
		}

		private void GetMaxOrder()
		{
			DataTable dataTable = new DataTable();
			try
			{
				dataTable.Clear();
				dataTable.Clone();
				SouthTransForm.mAdatpter = new SqlDataAdapter(string.Concat(new string[]
				{
					"select max(trans_id) trans_id,max(trans_id_bra) trans_id_bra from [dbo].[Trans_table] where [tyear]='",
					DateTime.Now.Year.ToString(),
					"' and [tmonth]='",
					DateTime.Now.Month.ToString(),
					"' and [bra_id]='",
					LogonForm.braid,
					"' and [comp_id]='",
					LogonForm.compid,
					"' and [sta_id]=@staidd2 and [prod_id]=@prodidd2 and [trans_type]='1' and CAST(ORDER_DATE AS DATE)=CAST(GetDate() As date) and flag=0 and [car_id]='",
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
				}), SouthTransForm.mConnection);
				SouthTransForm.mAdatpter.SelectCommand.Parameters.AddWithValue("@staidd2", this.comboBoxStation.SelectedValue);
				SouthTransForm.mAdatpter.SelectCommand.Parameters.AddWithValue("@prodidd2", this.comboBoxProdType.SelectedValue);
				SouthTransForm.mAdatpter.Fill(dataTable);
				bool flag = dataTable.Rows.Count == 1;
				if (flag)
				{
					this.transid = dataTable.Rows[0]["trans_id"].ToString();
					this.transidbra = dataTable.Rows[0]["trans_id_bra"].ToString();
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

		private void GetCarInfo()
		{
			try
			{
				this.dtcars.Clear();
				this.dtcars.Clone();
				SouthTransForm.mAdatpter = new SqlDataAdapter("SELECT [ID],[car_id_comp],[car_id_bra],[car_desc],[car_no],[Sach_no],products.[prod_id],prod_name,[bra_id],[comp_id],[active],[driver_name],[notes] FROM [dbo].[Cars],products where [Cars].prod_id=products.prod_id and car_id_bra=@caridbra order by [car_id_bra] asc", SouthTransForm.mConnection);
				SouthTransForm.mAdatpter.SelectCommand.Parameters.AddWithValue("@caridbra", this.comboBoxCarNo.SelectedValue);
				SouthTransForm.mAdatpter.Fill(this.dtcars);
				bool flag = this.dtcars.Rows.Count == 1;
				if (flag)
				{
					this.carid = this.dtcars.Rows[0]["ID"].ToString();
					this.carcomp = this.dtcars.Rows[0]["car_id_comp"].ToString();
					this.carbra = this.dtcars.Rows[0]["car_id_bra"].ToString();
					this.caroil = this.dtcars.Rows[0]["prod_id"].ToString();
					this.cardriver = this.dtcars.Rows[0]["driver_name"].ToString();
					this.carno = this.dtcars.Rows[0]["car_no"].ToString();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Fill_Products()
		{
			try
			{
				SouthTransForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT [prod_id],[prod_name] FROM [dbo].[products] order by [prod_id] asc", SouthTransForm.mConnection);
				DataTable dataTable = new DataTable();
				sqlDataAdapter.Fill(dataTable);
				this.comboBoxProdType.DataSource = dataTable;
				this.comboBoxProdType.DisplayMember = "prod_name";
				this.comboBoxProdType.ValueMember = "prod_id";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
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

		private void Fill_Cars()
		{
			try
			{
				SouthTransForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(string.Concat(new string[]
				{
					"SELECT [car_id_bra],[car_no]+[car_desc] car_no,[ID],[car_id_comp] FROM [dbo].[Cars]  where comp_id='",
					LogonForm.compid,
					"' and bra_id='",
					LogonForm.braid,
					"' and active='1' order by [car_id_bra] asc"
				}), SouthTransForm.mConnection);
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

		private void Fill_Stations()
		{
			try
			{
				SouthTransForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT [sta_id],[sta_name]+' '+[curr_name] [sta_name],Stations.curr_id,[Stations].gov_id,Stations.prod_id,prc_ry,prc_do FROM Currency,[dbo].[Stations],Goverments,Products_prc where Currency.curr_id!=[Stations].curr_id and [Stations].[gov_id]=Goverments.[gov_id] and Products_prc.gov_id=Stations.[gov_id] and Stations.prod_id='" + this.caroil + "' and Products_prc.prod_id=Stations.[prod_id] and active='1' order by [sta_id] asc", SouthTransForm.mConnection);
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

		private void SouthTransForm_Load(object sender, EventArgs e)
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(SouthTransForm));
			this.textBoxOrdNo = new TextBox();
			this.label2 = new Label();
			this.textBoxOrddate = new TextBox();
			this.label1 = new Label();
			this.label3 = new Label();
			this.panel3 = new Panel();
			this.textBox_comp_id = new TextBox();
			this.pictureBoxQR = new PictureBox();
			this.groupBox1 = new GroupBox();
			this.radioActive = new RadioButton();
			this.radioStop = new RadioButton();
			this.panel5 = new Panel();
			this.button3 = new Button();
			this.button1 = new Button();
			this.button2 = new Button();
			this.textBoxcounter = new TextBox();
			this.label9 = new Label();
			this.textBoxAmt = new TextBox();
			this.panel4 = new Panel();
			this.label8 = new Label();
			this.comboBoxStation = new ComboBox();
			this.label7 = new Label();
			this.textBoxdriver = new TextBox();
			this.label6 = new Label();
			this.comboBoxProdType = new ComboBox();
			this.label5 = new Label();
			this.comboBoxCarNo = new ComboBox();
			this.label4 = new Label();
			this.panel1 = new Panel();
			this.pictureBox1 = new PictureBox();
			this.button4 = new Button();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.panel2 = new Panel();
			this.panel3.SuspendLayout();
			((ISupportInitialize)this.pictureBoxQR).BeginInit();
			this.groupBox1.SuspendLayout();
			this.panel5.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel1.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel2.SuspendLayout();
			base.SuspendLayout();
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
			this.label3.AutoSize = true;
			this.label3.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label3.Location = new Point(1180, 13);
			this.label3.Name = "label3";
			this.label3.Size = new Size(68, 29);
			this.label3.TabIndex = 88;
			this.label3.Text = "الشركة";
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
			this.panel3.Size = new Size(1572, 63);
			this.panel3.TabIndex = 0;
			this.textBox_comp_id.BackColor = SystemColors.ButtonHighlight;
			this.textBox_comp_id.Enabled = false;
			this.textBox_comp_id.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBox_comp_id.Location = new Point(877, 10);
			this.textBox_comp_id.Name = "textBox_comp_id";
			this.textBox_comp_id.ReadOnly = true;
			this.textBox_comp_id.Size = new Size(297, 33);
			this.textBox_comp_id.TabIndex = 89;
			this.textBox_comp_id.TextAlign = HorizontalAlignment.Center;
			this.pictureBoxQR.Image = (Image)componentResourceManager.GetObject("pictureBoxQR.Image");
			this.pictureBoxQR.Location = new Point(5, 202);
			this.pictureBoxQR.Name = "pictureBoxQR";
			this.pictureBoxQR.Size = new Size(200, 200);
			this.pictureBoxQR.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBoxQR.TabIndex = 147;
			this.pictureBoxQR.TabStop = false;
			this.groupBox1.BackColor = SystemColors.ButtonHighlight;
			this.groupBox1.Controls.Add(this.radioActive);
			this.groupBox1.Controls.Add(this.radioStop);
			this.groupBox1.Font = new Font("Arial", 10f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.groupBox1.Location = new Point(876, 137);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(404, 45);
			this.groupBox1.TabIndex = 146;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "نوع الصرف";
			this.groupBox1.Visible = false;
			this.radioActive.AutoSize = true;
			this.radioActive.Checked = true;
			this.radioActive.Font = new Font("Arial", 10f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.radioActive.Location = new Point(202, 14);
			this.radioActive.Name = "radioActive";
			this.radioActive.Size = new Size(89, 28);
			this.radioActive.TabIndex = 1;
			this.radioActive.TabStop = true;
			this.radioActive.Text = "مخصص";
			this.radioActive.UseVisualStyleBackColor = true;
			this.radioStop.AutoSize = true;
			this.radioStop.Font = new Font("Arial", 10f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.radioStop.Location = new Point(80, 14);
			this.radioStop.Name = "radioStop";
			this.radioStop.Size = new Size(101, 28);
			this.radioStop.TabIndex = 0;
			this.radioStop.TabStop = true;
			this.radioStop.Text = "مهمة سفر";
			this.radioStop.UseVisualStyleBackColor = true;
			this.panel5.BorderStyle = BorderStyle.FixedSingle;
			this.panel5.Controls.Add(this.button3);
			this.panel5.Controls.Add(this.button1);
			this.panel5.Controls.Add(this.button2);
			this.panel5.Dock = DockStyle.Bottom;
			this.panel5.Location = new Point(0, 416);
			this.panel5.Name = "panel5";
			this.panel5.Size = new Size(1363, 100);
			this.panel5.TabIndex = 101;
			this.button3.BackColor = Color.White;
			this.button3.Cursor = Cursors.Hand;
			this.button3.FlatStyle = FlatStyle.Flat;
			this.button3.Font = new Font("Arial", 16f, FontStyle.Bold);
			this.button3.ForeColor = Color.FromArgb(64, 64, 64);
			this.button3.Location = new Point(453, 27);
			this.button3.Name = "button3";
			this.button3.Size = new Size(146, 52);
			this.button3.TabIndex = 23;
			this.button3.Text = "ترحيل";
			this.button3.UseVisualStyleBackColor = false;
			this.button3.Click += new EventHandler(this.button3_Click);
			this.button1.BackColor = Color.White;
			this.button1.Cursor = Cursors.Hand;
			this.button1.FlatStyle = FlatStyle.Flat;
			this.button1.Font = new Font("Arial", 16f, FontStyle.Bold);
			this.button1.ForeColor = Color.FromArgb(64, 64, 64);
			this.button1.Location = new Point(618, 27);
			this.button1.Name = "button1";
			this.button1.Size = new Size(146, 52);
			this.button1.TabIndex = 22;
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
			this.button2.TabIndex = 21;
			this.button2.Text = "الغاء";
			this.button2.UseVisualStyleBackColor = false;
			this.textBoxcounter.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBoxcounter.Location = new Point(48, 86);
			this.textBoxcounter.MaxLength = 10;
			this.textBoxcounter.Name = "textBoxcounter";
			this.textBoxcounter.Size = new Size(279, 33);
			this.textBoxcounter.TabIndex = 100;
			this.textBoxcounter.TextAlign = HorizontalAlignment.Center;
			this.label9.AutoSize = true;
			this.label9.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label9.Location = new Point(336, 87);
			this.label9.Name = "label9";
			this.label9.Size = new Size(87, 29);
			this.label9.TabIndex = 99;
			this.label9.Text = "رقم العداد";
			this.textBoxAmt.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBoxAmt.Location = new Point(454, 85);
			this.textBoxAmt.MaxLength = 4;
			this.textBoxAmt.Name = "textBoxAmt";
			this.textBoxAmt.Size = new Size(283, 33);
			this.textBoxAmt.TabIndex = 98;
			this.textBoxAmt.TextAlign = HorizontalAlignment.Center;
			this.panel4.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
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
			this.panel4.Location = new Point(0, 63);
			this.panel4.Name = "panel4";
			this.panel4.Size = new Size(1367, 520);
			this.panel4.TabIndex = 1;
			this.label8.AutoSize = true;
			this.label8.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label8.Location = new Point(752, 85);
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
			this.comboBoxStation.Location = new Point(846, 82);
			this.comboBoxStation.Name = "comboBoxStation";
			this.comboBoxStation.Size = new Size(354, 37);
			this.comboBoxStation.TabIndex = 96;
			this.comboBoxStation.SelectedValueChanged += new EventHandler(this.comboBoxStation_SelectedValueChanged);
			this.label7.AutoSize = true;
			this.label7.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label7.Location = new Point(1199, 85);
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
			this.comboBoxProdType.Location = new Point(454, 25);
			this.comboBoxProdType.Name = "comboBoxProdType";
			this.comboBoxProdType.Size = new Size(283, 37);
			this.comboBoxProdType.TabIndex = 94;
			this.label5.AutoSize = true;
			this.label5.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label5.Location = new Point(742, 25);
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
			this.comboBoxCarNo.Location = new Point(846, 22);
			this.comboBoxCarNo.Name = "comboBoxCarNo";
			this.comboBoxCarNo.Size = new Size(354, 37);
			this.comboBoxCarNo.TabIndex = 92;
			this.comboBoxCarNo.SelectedValueChanged += new EventHandler(this.comboBoxCarNo_SelectedValueChanged);
			this.label4.AutoSize = true;
			this.label4.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label4.Location = new Point(1199, 25);
			this.label4.Name = "label4";
			this.label4.Size = new Size(103, 29);
			this.label4.TabIndex = 90;
			this.label4.Text = "رقم السيارة";
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Dock = DockStyle.Fill;
			this.panel1.Location = new Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(783, 59);
			this.panel1.TabIndex = 2;
			this.pictureBox1.BorderStyle = BorderStyle.FixedSingle;
			this.pictureBox1.Dock = DockStyle.Fill;
			this.pictureBox1.Image = Resources.waqood;
			this.pictureBox1.Location = new Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new Size(783, 59);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.button4.Dock = DockStyle.Fill;
			this.button4.Enabled = false;
			this.button4.FlatStyle = FlatStyle.Flat;
			this.button4.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.button4.Location = new Point(792, 3);
			this.button4.Name = "button4";
			this.button4.Size = new Size(783, 59);
			this.button4.TabIndex = 3;
			this.button4.Text = "طلب وقود - جنوب";
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
			this.tableLayoutPanel1.Size = new Size(1578, 654);
			this.tableLayoutPanel1.TabIndex = 2;
			this.tableLayoutPanel1.SetColumnSpan(this.panel2, 2);
			this.panel2.Controls.Add(this.panel4);
			this.panel2.Controls.Add(this.panel3);
			this.panel2.Dock = DockStyle.Fill;
			this.panel2.Location = new Point(3, 68);
			this.panel2.Name = "panel2";
			this.panel2.Size = new Size(1572, 583);
			this.panel2.TabIndex = 4;
			base.AutoScaleDimensions = new SizeF(9f, 19f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(1578, 654);
			base.Controls.Add(this.tableLayoutPanel1);
			base.Name = "SouthTransForm";
			this.RightToLeft = RightToLeft.Yes;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "طلب وقود - جنوب";
			base.Load += new EventHandler(this.SouthTransForm_Load);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			((ISupportInitialize)this.pictureBoxQR).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.panel5.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.panel1.ResumeLayout(false);
			((ISupportInitialize)this.pictureBox1).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}
