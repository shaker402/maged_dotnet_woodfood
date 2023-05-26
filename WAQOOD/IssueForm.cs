using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WAQOOD.Properties;

namespace WAQOOD
{
	public class IssueForm : Form
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

		private Button button4;

		private Panel panel2;

		private TableLayoutPanel tableLayoutPanel1;

		private Panel panel1;

		private PictureBox pictureBox1;

		private Panel panel4;

		private DataGridView dataGridView1;

		private Panel panel5;

		private Button buttonUpdate;

		private PictureBox pictureBoxQR;

		private Button button2;

		private Panel panel3;

		private TextBox textBoxCarNo;

		private Label label2;

		private TextBox textBoxOrddate;

		private Label label1;

		private Label label3;

		private ComboBox comboBoxSta_t;

		private Label label5;

		private TextBox textBoxAmt2;

		private Label label8;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn Column3;

		private DataGridViewTextBoxColumn Column5;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn Column11;

		private DataGridViewTextBoxColumn Column4;

		private DataGridViewTextBoxColumn Column2;

		private DataGridViewTextBoxColumn Column6;

		private DataGridViewTextBoxColumn Column7;

		private DataGridViewTextBoxColumn Column8;

		private DataGridViewTextBoxColumn Column9;

		private DataGridViewTextBoxColumn Column10;

		private DataGridViewTextBoxColumn Column12;

		private DataGridViewTextBoxColumn Column14;

		private DataGridViewTextBoxColumn Column15;

		private DataGridViewTextBoxColumn Column16;

		private DataGridViewTextBoxColumn Column17;

		private DataGridViewTextBoxColumn Column13;

		private ComboBox comboBoxStation;

		private void comboBoxStation_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxStation_SelectedValueChanged(object sender, EventArgs e)
		{
			bool flag = this.comboBoxStation.SelectedIndex != 0;
			if (flag)
			{
				this.DisplayData();
				MobileLogForm.staid2 = this.comboBoxStation.SelectedValue.ToString();
				this.Fill_Stations();
			}
		}

		public IssueForm()
		{
			this.InitializeComponent();
			base.WindowState = FormWindowState.Maximized;
			IssueForm.mConnection = new SqlConnection(MainForm.str_conn);
			this.textBoxOrddate.Text = DateTime.Now.ToString();
			this.dtorders = new DataTable();
			this.Fill_StationsMain();
		}

		private void Fill_StationsMain()
		{
			try
			{
				IssueForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT [sta_id],[sta_name],regn,[Stations].gov_id,Stations.prod_id,prc_ry,prc_do FROM [dbo].[Stations],Goverments,Products_prc where [Stations].[gov_id]=Goverments.[gov_id] and Products_prc.gov_id=Stations.[gov_id] and Stations.sta_id in(select sta_id from StationUsers where user_id='" + MobileLogForm.userID2 + "') and Products_prc.prod_id=Stations.[prod_id] and active='1' order by [sta_id] asc", IssueForm.mConnection);
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

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				IssueForm.mConnection.Close();
				bool flag = this.transidsta != "" && this.transid != "" && this.transidbra != "" && this.carid != "";
				if (flag)
				{
					this.mCommand = new SqlCommand(string.Concat(new object[]
					{
						"update [dbo].[Trans_table] set return_flag='1',flag='3',return_date='",
						DateTime.Now,
						"',inuser_sta='",
						MobileLogForm.userID2,
						"' where flag='2' and trans_id_bra='",
						this.transidbra,
						"' and trans_id='",
						this.transid,
						"' and [tyear]='",
						DateTime.Now.Year.ToString(),
						"' and [tmonth]='",
						DateTime.Now.Month.ToString(),
						"' and [bra_id]='",
						this.braid,
						"' and [comp_id]='",
						this.compid,
						"'and trans_id_sta='",
						this.transidsta,
						"' and sta_id='",
						MobileLogForm.staid2,
						"' and CAST(ORDER_DATE AS DATE)='",
						this.ordDate,
						"' and prod_id='",
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
					}), IssueForm.mConnection);
					IssueForm.mConnection.Open();
					int num = this.mCommand.ExecuteNonQuery();
					IssueForm.mConnection.Close();
					bool flag2 = num >= 1;
					if (flag2)
					{
						MessageBox.Show("تم الحفظ بنجاح" + num.ToString());
						this.DisplayData();
						this.textBoxAmt2.Text = "";
						this.comboBoxSta_t.ResetText();
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
				MessageBox.Show(ex.Message);
				IssueForm.mConnection.Close();
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
				IssueForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlConnection.ClearPool(IssueForm.mConnection);
				IssueForm.mAdatpter = new SqlDataAdapter("select [Trans_table].regn,trans_id_sta,prc,sta_name,Trans_table.sta_id,trans_id,trans_id_bra,CAST(ORDER_DATE AS DATE) ORDER_DATE,[car_id],[car_id_bra],car_id_comp,[car_no],[amt],Trans_table.[prod_id],prod_name,car_driver,Trans_table.curr_id,curr_name,Branchs.bra_id,Branchs.bra_name,Branchs.comp_id from [dbo].[Trans_table],Products,Stations,Currency,Branchs where Branchs.bra_id=Trans_table.bra_id and Branchs.comp_id=Trans_table.comp_id and Currency.curr_id=Trans_table.curr_id and Trans_table.sta_id=Stations.sta_id and [trans_type] in(1,2,3) and flag='2' and [Trans_table].prod_id=Products.prod_id and Trans_table.sta_id='" + this.comboBoxStation.SelectedValue + "' order by [trans_id_bra] asc", IssueForm.mConnection);
				IssueForm.mAdatpter.Fill(this.dtorders);
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
						this.dtorders.Rows[i]["trans_id_sta"].ToString(),
						this.dtorders.Rows[i]["trans_id_bra"].ToString(),
						this.dtorders.Rows[i]["car_id_bra"].ToString(),
						this.dtorders.Rows[i]["car_no"].ToString(),
						this.dtorders.Rows[i]["prod_name"].ToString(),
						this.dtorders.Rows[i]["bra_name"].ToString(),
						this.dtorders.Rows[i]["amt"].ToString(),
						this.dtorders.Rows[i]["car_driver"].ToString(),
						this.dtorders.Rows[i]["order_date"].ToString(),
						this.dtorders.Rows[i]["prod_id"].ToString(),
						this.dtorders.Rows[i]["car_id"].ToString(),
						this.dtorders.Rows[i]["car_id_comp"].ToString(),
						this.dtorders.Rows[i]["trans_id"].ToString(),
						this.dtorders.Rows[i]["curr_name"].ToString(),
						this.dtorders.Rows[i]["comp_id"].ToString(),
						this.dtorders.Rows[i]["bra_id"].ToString(),
						this.dtorders.Rows[i]["regn"].ToString(),
						this.dtorders.Rows[i]["curr_id"].ToString(),
						this.dtorders.Rows[i]["prc"].ToString()
					});
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			bool flag = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value != null;
			if (flag)
			{
				this.transidsta = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
				this.transidbra = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
				this.carbra = this.dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
				this.carno = this.dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
				this.caramnt = this.dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
				this.ordDate = this.dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
				this.caroil = this.dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
				this.carid = this.dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
				this.carcomp = this.dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
				this.transid = this.dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
				this.compid = this.dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
				this.braid = this.dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString();
				this.regnn = this.dataGridView1.Rows[e.RowIndex].Cells[16].Value.ToString();
				this.curridd = this.dataGridView1.Rows[e.RowIndex].Cells[17].Value.ToString();
			}
			else
			{
				this.transidsta = "";
				this.transidbra = "";
				this.carbra = "";
				this.carno = "";
				this.caramnt = "";
				this.ordDate = "";
				this.caroil = "";
				this.carid = "";
				this.transid = "";
				this.compid = "";
				this.braid = "";
				this.regnn = "";
			}
		}

		private void Fill_Stations()
		{
			try
			{
				IssueForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT [sta_t_id],[sta_t_name] FROM [dbo].[Station_t] where [Station_t].[sta_id]='" + this.comboBoxStation.SelectedValue + "'  and active='1' order by [sta_t_id] asc", IssueForm.mConnection);
				DataTable dataTable = new DataTable();
				sqlDataAdapter.Fill(dataTable);
				DataRow dataRow = dataTable.NewRow();
				dataRow[0] = 0;
				dataRow[1] = "-الطرمبة-";
				dataTable.Rows.InsertAt(dataRow, 0);
				this.comboBoxSta_t.DataSource = dataTable;
				this.comboBoxSta_t.DisplayMember = "sta_t_name";
				this.comboBoxSta_t.ValueMember = "sta_t_id";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}
		}

		private void GetMaxOrder()
		{
			DataTable dataTable = new DataTable();
			try
			{
				dataTable.Clear();
				dataTable.Clone();
				IssueForm.mAdatpter = new SqlDataAdapter(string.Concat(new string[]
				{
					"select max(trans_id_sta) trans_id,max(trans_id_bra) trans_id_bra from [dbo].[Trans_table] where [tyear]='",
					DateTime.Now.Year.ToString(),
					"' and [tmonth]='",
					DateTime.Now.Month.ToString(),
					"' and [bra_id]='",
					LogonForm.braid,
					"' and [comp_id]='",
					LogonForm.compid,
					"' and trans_id='",
					this.transid,
					"' and [sta_id]='",
					MobileLogForm.staid2,
					"' and [prod_id]='",
					this.caroil,
					"' and [trans_type] in(1,2,3) and CAST(trans_DATE AS DATE)=CAST(GetDate() As date) and flag=5 and [car_id]='",
					this.carid,
					"' and [car_id_bra]='",
					this.carbra,
					"' and [car_id_comp]='",
					this.carcomp,
					"' and [car_no]='",
					this.carno,
					"' and [amt2]='",
					this.textBoxAmt2.Text,
					"'"
				}), IssueForm.mConnection);
				IssueForm.mAdatpter.SelectCommand.Parameters.AddWithValue("@staidd2", this.comboBoxStation.SelectedValue);
				IssueForm.mAdatpter.Fill(dataTable);
				bool flag = dataTable.Rows.Count == 1;
				if (flag)
				{
					MessageBox.Show("رقم سند الصرف :\n" + dataTable.Rows[0]["trans_id"].ToString());
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

		private void GetPrice()
		{
			DataTable dataTable = new DataTable();
			try
			{
				dataTable.Clear();
				dataTable.Clone();
				IssueForm.mAdatpter = new SqlDataAdapter(string.Concat(new string[]
				{
					"select * from stations,Products_prc where stations.gov_id=Products_prc.gov_id and stations.prod_id=Products_prc.prod_id and stations.prod_id='",
					this.caroil,
					"' and stations.sta_id='",
					MobileLogForm.staid2,
					"'"
				}), IssueForm.mConnection);
				IssueForm.mAdatpter.Fill(dataTable);
				bool flag = dataTable.Rows.Count >= 1;
				if (flag)
				{
					bool flag2 = this.regnn == "1";
					if (flag2)
					{
						bool flag3 = this.curridd == "1";
						if (flag3)
						{
							this.prcid = dataTable.Rows[0]["prc_ry"].ToString();
						}
						else
						{
							bool flag4 = this.curridd == "2";
							if (flag4)
							{
								this.prcid = dataTable.Rows[0]["prc_do"].ToString();
							}
						}
					}
					else
					{
						bool flag5 = this.regnn == "2";
						if (flag5)
						{
							bool flag6 = this.curridd == "1";
							if (flag6)
							{
								this.prcid = dataTable.Rows[0]["prc_ry"].ToString();
							}
							else
							{
								bool flag7 = this.curridd == "2";
								if (flag7)
								{
									this.prcid = dataTable.Rows[0]["prc_do"].ToString();
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
					MessageBox.Show("لايوجد رقم طلب");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonUpdate_Click(object sender, EventArgs e)
		{
			try
			{
				bool flag = this.transid != "" && this.transidbra != "" && this.carid != "" && this.textBoxAmt2.Text != "" && this.comboBoxSta_t.SelectedIndex != 0;
				if (flag)
				{
					bool flag2 = short.Parse(this.textBoxAmt2.Text) > short.Parse(this.caramnt);
					if (flag2)
					{
						MessageBox.Show("الكمية الفعلي اكبر من كمية الطلب");
					}
					else
					{
						bool flag3 = short.Parse(this.textBoxAmt2.Text) <= 0;
						if (flag3)
						{
							MessageBox.Show("الكمية المدخلة اقل او تساوي 0 تاكد من الكمية");
						}
						else
						{
							bool flag4 = short.Parse(this.textBoxAmt2.Text) < short.Parse(this.caramnt);
							if (flag4)
							{
								DialogResult dialogResult = MessageBox.Show("الكمية المدخلة اقل من كمية الطلب هل تريد الاستمرار?", "Confirmation", MessageBoxButtons.YesNo);
								if (dialogResult == DialogResult.Yes)
								{
									this.ConfirmOrder();
								}
							}
							else
							{
								this.ConfirmOrder();
							}
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
				MessageBox.Show(ex.Message.ToString());
			}
		}

		private void ConfirmOrder()
		{
			try
			{
				this.GetPrice();
				bool flag = string.IsNullOrEmpty(this.prcid);
				if (flag)
				{
					MessageBox.Show("لايوجد سعر ...");
				}
				else
				{
					IssueForm.mConnection.Close();
					bool flag2 = short.Parse(this.textBoxAmt2.Text) <= short.Parse(this.caramnt) && this.comboBoxSta_t.SelectedIndex != 0;
					if (flag2)
					{
						this.mCommand = new SqlCommand(string.Concat(new object[]
						{
							"update [dbo].[Trans_table] set trans_id_sta=(select ISNULL(max(trans_id_sta),0)+1 from Trans_table where Trans_table.sta_id='",
							MobileLogForm.staid2,
							"' and Trans_table.tyear='",
							DateTime.Now.Year,
							"'),prc='",
							this.prcid,
							"',amt2='",
							this.textBoxAmt2.Text.Trim(),
							"',sta_t_id='",
							this.comboBoxSta_t.SelectedValue,
							"',flag='5',trans_date='",
							DateTime.Now.ToString("yyyy-MM-dd"),
							"',inuser_sta='",
							MobileLogForm.userID2,
							"' where flag='2' and trans_id_bra='",
							this.transidbra,
							"' and trans_id='",
							this.transid,
							"' and [tyear]='",
							DateTime.Now.Year.ToString(),
							"' and [tmonth]='",
							DateTime.Now.Month.ToString(),
							"' and [bra_id]='",
							this.braid,
							"' and [comp_id]='",
							this.compid,
							"'and sta_id='",
							MobileLogForm.staid2,
							"'  and prod_id='",
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
						}), IssueForm.mConnection);
						IssueForm.mConnection.Open();
						int num = this.mCommand.ExecuteNonQuery();
						IssueForm.mConnection.Close();
						bool flag3 = num >= 1;
						if (flag3)
						{
							MessageBox.Show("تم الحفظ بنجاح" + num.ToString());
							this.GetMaxOrder();
							this.DisplayData();
							this.textBoxAmt2.Text = "";
							this.comboBoxSta_t.ResetText();
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
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				IssueForm.mConnection.Close();
			}
		}

		private void IssueForm_Load(object sender, EventArgs e)
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(IssueForm));
			this.button4 = new Button();
			this.panel2 = new Panel();
			this.panel4 = new Panel();
			this.dataGridView1 = new DataGridView();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.Column3 = new DataGridViewTextBoxColumn();
			this.Column5 = new DataGridViewTextBoxColumn();
			this.Column1 = new DataGridViewTextBoxColumn();
			this.Column11 = new DataGridViewTextBoxColumn();
			this.Column4 = new DataGridViewTextBoxColumn();
			this.Column2 = new DataGridViewTextBoxColumn();
			this.Column6 = new DataGridViewTextBoxColumn();
			this.Column7 = new DataGridViewTextBoxColumn();
			this.Column8 = new DataGridViewTextBoxColumn();
			this.Column9 = new DataGridViewTextBoxColumn();
			this.Column10 = new DataGridViewTextBoxColumn();
			this.Column12 = new DataGridViewTextBoxColumn();
			this.Column14 = new DataGridViewTextBoxColumn();
			this.Column15 = new DataGridViewTextBoxColumn();
			this.Column16 = new DataGridViewTextBoxColumn();
			this.Column17 = new DataGridViewTextBoxColumn();
			this.Column13 = new DataGridViewTextBoxColumn();
			this.panel5 = new Panel();
			this.textBoxAmt2 = new TextBox();
			this.label8 = new Label();
			this.comboBoxSta_t = new ComboBox();
			this.label5 = new Label();
			this.buttonUpdate = new Button();
			this.pictureBoxQR = new PictureBox();
			this.button2 = new Button();
			this.panel3 = new Panel();
			this.comboBoxStation = new ComboBox();
			this.textBoxCarNo = new TextBox();
			this.label2 = new Label();
			this.textBoxOrddate = new TextBox();
			this.label1 = new Label();
			this.label3 = new Label();
			this.pictureBox1 = new PictureBox();
			this.panel1 = new Panel();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.panel2.SuspendLayout();
			this.panel4.SuspendLayout();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			this.panel5.SuspendLayout();
			((ISupportInitialize)this.pictureBoxQR).BeginInit();
			this.panel3.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			base.SuspendLayout();
			this.button4.Dock = DockStyle.Fill;
			this.button4.Enabled = false;
			this.button4.FlatStyle = FlatStyle.Flat;
			this.button4.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.button4.Location = new Point(670, 3);
			this.button4.Name = "button4";
			this.button4.Size = new Size(661, 54);
			this.button4.TabIndex = 3;
			this.button4.Text = "اعتماد طلبات الوقود";
			this.button4.UseVisualStyleBackColor = true;
			this.tableLayoutPanel1.SetColumnSpan(this.panel2, 2);
			this.panel2.Controls.Add(this.panel4);
			this.panel2.Controls.Add(this.panel3);
			this.panel2.Dock = DockStyle.Fill;
			this.panel2.Location = new Point(3, 63);
			this.panel2.Name = "panel2";
			this.panel2.Size = new Size(1328, 539);
			this.panel2.TabIndex = 4;
			this.panel4.BackColor = SystemColors.GradientActiveCaption;
			this.panel4.BorderStyle = BorderStyle.Fixed3D;
			this.panel4.Controls.Add(this.dataGridView1);
			this.panel4.Controls.Add(this.panel5);
			this.panel4.Dock = DockStyle.Fill;
			this.panel4.Location = new Point(0, 63);
			this.panel4.Name = "panel4";
			this.panel4.Size = new Size(1328, 476);
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
				this.dataGridViewTextBoxColumn1,
				this.dataGridViewTextBoxColumn2,
				this.Column3,
				this.Column5,
				this.Column1,
				this.Column11,
				this.Column4,
				this.Column2,
				this.Column6,
				this.Column7,
				this.Column8,
				this.Column9,
				this.Column10,
				this.Column12,
				this.Column14,
				this.Column15,
				this.Column16,
				this.Column17,
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
			this.dataGridView1.Size = new Size(1324, 320);
			this.dataGridView1.TabIndex = 148;
			this.dataGridView1.RowHeaderMouseClick += new DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
			this.dataGridViewTextBoxColumn1.HeaderText = "التسلسل";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn2.HeaderText = "رقم الطلب";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.Column3.HeaderText = "رقم السيارة";
			this.Column3.Name = "Column3";
			this.Column5.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.Column5.HeaderText = "رقم اللوحة";
			this.Column5.Name = "Column5";
			this.Column1.HeaderText = "الوقود";
			this.Column1.Name = "Column1";
			this.Column11.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.Column11.HeaderText = "الشركة";
			this.Column11.Name = "Column11";
			this.Column4.HeaderText = "الكمية";
			this.Column4.Name = "Column4";
			this.Column2.HeaderText = "السائق";
			this.Column2.Name = "Column2";
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
			this.Column10.HeaderText = "orderall";
			this.Column10.Name = "Column10";
			this.Column10.Visible = false;
			this.Column12.HeaderText = "العملة";
			this.Column12.Name = "Column12";
			this.Column14.HeaderText = "compid";
			this.Column14.Name = "Column14";
			this.Column14.Visible = false;
			this.Column15.HeaderText = "braid";
			this.Column15.Name = "Column15";
			this.Column15.Visible = false;
			this.Column16.HeaderText = "regn";
			this.Column16.Name = "Column16";
			this.Column16.Visible = false;
			this.Column17.HeaderText = "currid";
			this.Column17.Name = "Column17";
			this.Column17.Visible = false;
			this.Column13.HeaderText = "السعر";
			this.Column13.Name = "Column13";
			this.panel5.BorderStyle = BorderStyle.FixedSingle;
			this.panel5.Controls.Add(this.textBoxAmt2);
			this.panel5.Controls.Add(this.label8);
			this.panel5.Controls.Add(this.comboBoxSta_t);
			this.panel5.Controls.Add(this.label5);
			this.panel5.Controls.Add(this.buttonUpdate);
			this.panel5.Controls.Add(this.pictureBoxQR);
			this.panel5.Controls.Add(this.button2);
			this.panel5.Dock = DockStyle.Bottom;
			this.panel5.Location = new Point(0, 320);
			this.panel5.Name = "panel5";
			this.panel5.Size = new Size(1324, 152);
			this.panel5.TabIndex = 101;
			this.textBoxAmt2.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBoxAmt2.Location = new Point(907, 28);
			this.textBoxAmt2.MaxLength = 4;
			this.textBoxAmt2.Name = "textBoxAmt2";
			this.textBoxAmt2.Size = new Size(297, 33);
			this.textBoxAmt2.TabIndex = 151;
			this.textBoxAmt2.TextAlign = HorizontalAlignment.Center;
			this.label8.AutoSize = true;
			this.label8.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label8.Location = new Point(1210, 25);
			this.label8.Name = "label8";
			this.label8.Size = new Size(115, 29);
			this.label8.TabIndex = 150;
			this.label8.Text = "الكمية الفعلي";
			this.comboBoxSta_t.AutoCompleteMode = AutoCompleteMode.Suggest;
			this.comboBoxSta_t.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.comboBoxSta_t.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxSta_t.FlatStyle = FlatStyle.Flat;
			this.comboBoxSta_t.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.comboBoxSta_t.FormattingEnabled = true;
			this.comboBoxSta_t.Location = new Point(485, 24);
			this.comboBoxSta_t.Name = "comboBoxSta_t";
			this.comboBoxSta_t.Size = new Size(297, 37);
			this.comboBoxSta_t.TabIndex = 149;
			this.label5.AutoSize = true;
			this.label5.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label5.Location = new Point(802, 28);
			this.label5.Name = "label5";
			this.label5.Size = new Size(74, 29);
			this.label5.TabIndex = 148;
			this.label5.Text = "الطرمبة";
			this.buttonUpdate.BackColor = Color.White;
			this.buttonUpdate.Cursor = Cursors.Hand;
			this.buttonUpdate.FlatStyle = FlatStyle.Flat;
			this.buttonUpdate.Font = new Font("Arial", 12f, FontStyle.Bold);
			this.buttonUpdate.ForeColor = Color.FromArgb(64, 64, 64);
			this.buttonUpdate.Location = new Point(169, 19);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new Size(146, 41);
			this.buttonUpdate.TabIndex = 23;
			this.buttonUpdate.Text = "ترحيل";
			this.buttonUpdate.UseVisualStyleBackColor = false;
			this.buttonUpdate.Click += new EventHandler(this.buttonUpdate_Click);
			this.pictureBoxQR.Image = (Image)componentResourceManager.GetObject("pictureBoxQR.Image");
			this.pictureBoxQR.Location = new Point(3, -1);
			this.pictureBoxQR.Name = "pictureBoxQR";
			this.pictureBoxQR.Size = new Size(127, 96);
			this.pictureBoxQR.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBoxQR.TabIndex = 147;
			this.pictureBoxQR.TabStop = false;
			this.button2.BackColor = Color.White;
			this.button2.Cursor = Cursors.Hand;
			this.button2.FlatStyle = FlatStyle.Flat;
			this.button2.Font = new Font("Arial", 12f, FontStyle.Bold);
			this.button2.ForeColor = Color.FromArgb(64, 64, 64);
			this.button2.Location = new Point(321, 20);
			this.button2.Name = "button2";
			this.button2.Size = new Size(146, 41);
			this.button2.TabIndex = 21;
			this.button2.Text = "اعادة للشركة";
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.panel3.BackColor = SystemColors.ControlLight;
			this.panel3.BorderStyle = BorderStyle.FixedSingle;
			this.panel3.Controls.Add(this.comboBoxStation);
			this.panel3.Controls.Add(this.textBoxCarNo);
			this.panel3.Controls.Add(this.label2);
			this.panel3.Controls.Add(this.textBoxOrddate);
			this.panel3.Controls.Add(this.label1);
			this.panel3.Controls.Add(this.label3);
			this.panel3.Dock = DockStyle.Top;
			this.panel3.Location = new Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new Size(1328, 63);
			this.panel3.TabIndex = 0;
			this.comboBoxStation.AutoCompleteMode = AutoCompleteMode.Suggest;
			this.comboBoxStation.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.comboBoxStation.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxStation.FlatStyle = FlatStyle.Flat;
			this.comboBoxStation.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.comboBoxStation.FormattingEnabled = true;
			this.comboBoxStation.Location = new Point(877, 6);
			this.comboBoxStation.Name = "comboBoxStation";
			this.comboBoxStation.Size = new Size(377, 37);
			this.comboBoxStation.TabIndex = 95;
			this.comboBoxStation.SelectedIndexChanged += new EventHandler(this.comboBoxStation_SelectedIndexChanged);
			this.comboBoxStation.SelectedValueChanged += new EventHandler(this.comboBoxStation_SelectedValueChanged);
			this.textBoxCarNo.BackColor = SystemColors.ButtonHighlight;
			this.textBoxCarNo.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBoxCarNo.Location = new Point(42, 10);
			this.textBoxCarNo.Name = "textBoxCarNo";
			this.textBoxCarNo.Size = new Size(279, 33);
			this.textBoxCarNo.TabIndex = 89;
			this.textBoxCarNo.TextAlign = HorizontalAlignment.Center;
			this.label2.AutoSize = true;
			this.label2.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label2.Location = new Point(327, 10);
			this.label2.Name = "label2";
			this.label2.Size = new Size(147, 29);
			this.label2.TabIndex = 88;
			this.label2.Text = "بحث برقم اللوحة";
			this.textBoxOrddate.BackColor = SystemColors.ButtonHighlight;
			this.textBoxOrddate.Enabled = false;
			this.textBoxOrddate.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBoxOrddate.Location = new Point(487, 9);
			this.textBoxOrddate.Name = "textBoxOrddate";
			this.textBoxOrddate.ReadOnly = true;
			this.textBoxOrddate.Size = new Size(297, 33);
			this.textBoxOrddate.TabIndex = 91;
			this.textBoxOrddate.TextAlign = HorizontalAlignment.Center;
			this.label1.AutoSize = true;
			this.label1.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label1.Location = new Point(804, 13);
			this.label1.Name = "label1";
			this.label1.Size = new Size(67, 29);
			this.label1.TabIndex = 90;
			this.label1.Text = "التاريخ";
			this.label3.AutoSize = true;
			this.label3.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label3.Location = new Point(1253, 11);
			this.label3.Name = "label3";
			this.label3.Size = new Size(70, 29);
			this.label3.TabIndex = 88;
			this.label3.Text = "المحطة";
			this.pictureBox1.BorderStyle = BorderStyle.FixedSingle;
			this.pictureBox1.Dock = DockStyle.Fill;
			this.pictureBox1.Image = Resources.waqood;
			this.pictureBox1.Location = new Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new Size(661, 54);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Dock = DockStyle.Fill;
			this.panel1.Location = new Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(661, 54);
			this.panel1.TabIndex = 2;
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
			this.tableLayoutPanel1.Size = new Size(1334, 605);
			this.tableLayoutPanel1.TabIndex = 3;
			base.AutoScaleDimensions = new SizeF(9f, 19f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(1334, 605);
			base.Controls.Add(this.tableLayoutPanel1);
			base.Name = "IssueForm";
			this.RightToLeft = RightToLeft.Yes;
			this.Text = "صرف وقود";
			base.Load += new EventHandler(this.IssueForm_Load);
			this.panel2.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			((ISupportInitialize)this.dataGridView1).EndInit();
			this.panel5.ResumeLayout(false);
			this.panel5.PerformLayout();
			((ISupportInitialize)this.pictureBoxQR).EndInit();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			((ISupportInitialize)this.pictureBox1).EndInit();
			this.panel1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}
