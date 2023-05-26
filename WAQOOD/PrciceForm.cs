using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WAQOOD
{
	public class PrciceForm : Form
	{
		private string prodid = string.Empty;

		private string prodname = string.Empty;

		private string prcrold = string.Empty;

		private string prcdold = string.Empty;

		private SqlCommand mCommand = null;

		private static SqlDataAdapter mAdatpter = null;

		private static SqlConnection mConnection = new SqlConnection(MainForm.str_conn);

		private DataTable dtOrder;

		private IContainer components = null;

		private TableLayoutPanel tableLayoutPanel1;

		private DataGridView dataGridView2;

		private SplitContainer splitContainer1;

		private DataGridView dataGridView1;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn Column2;

		private Button buttonUpdate;

		private TextBox textBoxprcD;

		private Label label7;

		private TextBox textBoxprcR;

		private Label label5;

		private ComboBox comboBoxgov;

		private Label label6;

		private Button button1;

		private TextBox textBox1prodId;

		private Label label2;

		private TextBox textBox2proname;

		private Label label4;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn Column3;

		private DataGridViewTextBoxColumn Column4;

		private DataGridViewTextBoxColumn Column5;

		private DataGridViewTextBoxColumn Column6;

		private DataGridViewButtonColumn Column7;

		private Button button3;

		public PrciceForm()
		{
			this.InitializeComponent();
			base.WindowState = FormWindowState.Maximized;
			PrciceForm.mConnection = new SqlConnection(MainForm.str_conn);
			this.dtOrder = new DataTable();
			this.DisplayData();
			this.Fill_Goverments();
		}

		private void Fill_Goverments()
		{
			try
			{
				PrciceForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT [gov_id],[gov_name] FROM [dbo].[Goverments] order by gov_id asc", PrciceForm.mConnection);
				DataTable dataTable = new DataTable();
				sqlDataAdapter.Fill(dataTable);
				DataRow dataRow = dataTable.NewRow();
				dataRow[0] = 0;
				dataRow[1] = "-المحافظة-";
				dataTable.Rows.InsertAt(dataRow, 0);
				this.comboBoxgov.DataSource = dataTable;
				this.comboBoxgov.DisplayMember = "gov_name";
				this.comboBoxgov.ValueMember = "gov_id";
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
				PrciceForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlConnection.ClearPool(PrciceForm.mConnection);
				PrciceForm.mAdatpter = new SqlDataAdapter("SELECT [prod_id],[prod_name] FROM [dbo].[Products] order by prod_id asc ", PrciceForm.mConnection);
				PrciceForm.mAdatpter.Fill(this.dtOrder);
				bool flag = this.dtOrder.Rows.Count == 0;
				if (flag)
				{
					MessageBox.Show("لايوجد بيانات ");
				}
				else
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
						this.dtOrder.Rows[i]["prod_ID"].ToString(),
						this.dtOrder.Rows[i]["prod_NAME"].ToString()
					});
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void PrciceForm_Load(object sender, EventArgs e)
		{
		}

		private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			bool flag = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value != null;
			if (flag)
			{
				this.prodid = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
				this.prodname = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
				this.textBox1prodId.Text = this.prodid;
				this.textBox2proname.Text = this.prodname;
				this.DisplayDataPrice();
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
		}

		private void button1_Click(object sender, EventArgs e)
		{
		}

		private void ClearControls()
		{
			this.textBox1prodId.Text = "";
			this.textBox2proname.Text = "";
			this.comboBoxgov.Text = "";
			this.textBoxprcD.Text = "";
			this.textBoxprcR.Text = "";
		}

		public void DisplayDataPrice()
		{
			try
			{
				this.dtOrder.Clear();
				this.dtOrder.Clone();
				this.dataGridView2.Rows.Clear();
				this.dataGridView2.Refresh();
				PrciceForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlConnection.ClearPool(PrciceForm.mConnection);
				PrciceForm.mAdatpter = new SqlDataAdapter("SELECT [Products].[prod_id] prod_id,[prod_name],[prc_ry],[prc_do],[gov_name],goverments.gov_id gov_id,[lastdate] FROM [dbo].[Products_prc],[dbo].[Products],goverments where goverments.gov_id= [Products_prc].gov_id and [Products_prc].prod_id=[Products].prod_id and [Products_prc].prod_id=@prodid order by [Products].prod_id", PrciceForm.mConnection);
				PrciceForm.mAdatpter.SelectCommand.Parameters.AddWithValue("@prodid", this.prodid);
				PrciceForm.mAdatpter.Fill(this.dtOrder);
				bool flag = this.dtOrder.Rows.Count == 0;
				if (!flag)
				{
					this.add_coulmPrice();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.DisplayDataPrice();
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			try
			{
				bool flag = this.textBoxprcD.Text != "" && this.textBoxprcR.Text != "" && this.textBox1prodId.Text != "" && this.comboBoxgov.SelectedIndex.ToString() != "0";
				if (flag)
				{
					this.mCommand = new SqlCommand("INSERT INTO [dbo].[Products_prc]([prod_id],[prc_ry],[prc_do],[gov_id],[lastdate]) VALUES(@prodid,@prcr,@prcd,@gov,'" + DateTime.Now.ToString("yyyy-MM-dd") + "')", PrciceForm.mConnection);
					PrciceForm.mConnection.Open();
					this.mCommand.Parameters.AddWithValue("@prcr", this.textBoxprcR.Text);
					this.mCommand.Parameters.AddWithValue("@prcd", this.textBoxprcD.Text);
					this.mCommand.Parameters.AddWithValue("@gov", this.comboBoxgov.SelectedValue);
					this.mCommand.Parameters.AddWithValue("@prodid", this.textBox1prodId.Text);
					this.mCommand.ExecuteNonQuery();
					PrciceForm.mConnection.Close();
					MessageBox.Show(" تم حفظ البيانات بنجاح");
					this.DisplayDataPrice();
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
				PrciceForm.mConnection.Close();
			}
		}

		private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
		{
		}

		private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			bool flag = this.dataGridView2.Rows[e.RowIndex].Cells[0].Value != null;
			if (flag)
			{
				this.prodid = this.dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
				this.prodname = this.dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
				this.prcrold = this.dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
				this.prcdold = this.dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
				this.textBoxprcR.Text = this.dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
				this.textBoxprcD.Text = this.dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
				this.comboBoxgov.SelectedValue = this.dataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString();
				this.textBox1prodId.Text = this.prodid;
				this.textBox2proname.Text = this.prodname;
			}
		}

		private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			bool flag = this.dataGridView2.CurrentRow != null;
			if (flag)
			{
				bool flag2 = e.ColumnIndex == 6 && e.RowIndex >= 0;
				if (flag2)
				{
					this.dataGridView2.CommitEdit(DataGridViewDataErrorContexts.Commit);
					bool flag3 = this.dataGridView2.CurrentCell.Value != null;
					if (flag3)
					{
						this.dataGridView2.Rows[e.RowIndex].Selected = true;
						this.prodid = this.dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
						this.prodname = this.dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
						this.textBoxprcR.Text = this.dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
						this.textBoxprcD.Text = this.dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
						this.comboBoxgov.SelectedValue = this.dataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString();
						this.textBox1prodId.Text = this.prodid;
						this.textBox2proname.Text = this.prodname;
						this.buttonUpdate.Visible = true;
					}
				}
			}
		}

		private void buttonUpdate_Click(object sender, EventArgs e)
		{
			try
			{
				bool flag = this.textBoxprcD.Text != "" && this.textBoxprcR.Text != "" && this.textBox1prodId.Text != "" && this.comboBoxgov.SelectedIndex.ToString() != "0";
				if (flag)
				{
					this.mCommand = new SqlCommand("update Products_prc set prc_ry=@prcr,prc_do=@prcd where prod_id=@prodid and gov_id=@gov ", PrciceForm.mConnection);
					PrciceForm.mConnection.Open();
					this.mCommand.Parameters.AddWithValue("@prcr", this.textBoxprcR.Text);
					this.mCommand.Parameters.AddWithValue("@prcd", this.textBoxprcD.Text);
					this.mCommand.Parameters.AddWithValue("@gov", this.comboBoxgov.SelectedValue);
					this.mCommand.Parameters.AddWithValue("@prodid", this.textBox1prodId.Text);
					this.mCommand.ExecuteNonQuery();
					PrciceForm.mConnection.Close();
					MessageBox.Show("تم التعديل بنجاح");
					this.InsertPrcHist();
					this.DisplayDataPrice();
					this.ClearControls();
				}
				else
				{
					MessageBox.Show("بيانات غير مكتملة!");
				}
			}
			catch (Exception ex)
			{
				PrciceForm.mConnection.Close();
				MessageBox.Show(ex.Message);
			}
		}

		private void InsertPrcHist()
		{
			try
			{
				this.mCommand = new SqlCommand(string.Concat(new string[]
				{
					"INSERT INTO [dbo].[price_hist]([prod_id],[gov_id],[prc_r_old],[prc_r_new],[prc_d_old],[prc_d_new],[pc_log]) VALUES(@prodid,@gov,'",
					this.prcrold,
					"',@prcr,'",
					this.prcdold,
					"',@prcd,'",
					Environment.MachineName,
					"-",
					Environment.UserName,
					"')"
				}), PrciceForm.mConnection);
				PrciceForm.mConnection.Open();
				this.mCommand.Parameters.AddWithValue("@prcr", this.textBoxprcR.Text);
				this.mCommand.Parameters.AddWithValue("@prcd", this.textBoxprcD.Text);
				this.mCommand.Parameters.AddWithValue("@gov", this.comboBoxgov.SelectedValue);
				this.mCommand.Parameters.AddWithValue("@prodid", this.textBox1prodId.Text);
				this.mCommand.ExecuteNonQuery();
				PrciceForm.mConnection.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				PrciceForm.mConnection.Close();
			}
		}

		public void add_coulmPrice()
		{
			try
			{
				for (int i = 0; i < this.dtOrder.Rows.Count; i++)
				{
					this.dataGridView2.Rows.Add(new object[]
					{
						this.dtOrder.Rows[i]["prod_id"].ToString(),
						this.dtOrder.Rows[i]["prod_NAME"].ToString(),
						this.dtOrder.Rows[i]["gov_name"].ToString(),
						this.dtOrder.Rows[i]["prc_ry"].ToString(),
						this.dtOrder.Rows[i]["prc_do"].ToString(),
						this.dtOrder.Rows[i]["gov_id"].ToString()
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
			this.button3 = new Button();
			this.dataGridView2 = new DataGridView();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.Column3 = new DataGridViewTextBoxColumn();
			this.Column4 = new DataGridViewTextBoxColumn();
			this.Column5 = new DataGridViewTextBoxColumn();
			this.Column6 = new DataGridViewTextBoxColumn();
			this.Column7 = new DataGridViewButtonColumn();
			this.splitContainer1 = new SplitContainer();
			this.dataGridView1 = new DataGridView();
			this.Column1 = new DataGridViewTextBoxColumn();
			this.Column2 = new DataGridViewTextBoxColumn();
			this.buttonUpdate = new Button();
			this.textBoxprcD = new TextBox();
			this.label7 = new Label();
			this.textBoxprcR = new TextBox();
			this.label5 = new Label();
			this.comboBoxgov = new ComboBox();
			this.label6 = new Label();
			this.button1 = new Button();
			this.textBox1prodId = new TextBox();
			this.label2 = new Label();
			this.textBox2proname = new TextBox();
			this.label4 = new Label();
			this.tableLayoutPanel1.SuspendLayout();
			((ISupportInitialize)this.dataGridView2).BeginInit();
			((ISupportInitialize)this.splitContainer1).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			base.SuspendLayout();
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel1.Controls.Add(this.button3, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.dataGridView2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
			this.tableLayoutPanel1.Dock = DockStyle.Fill;
			this.tableLayoutPanel1.Location = new Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 90f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel1.Size = new Size(1576, 915);
			this.tableLayoutPanel1.TabIndex = 1;
			this.button3.Dock = DockStyle.Fill;
			this.button3.Enabled = false;
			this.button3.FlatStyle = FlatStyle.Flat;
			this.button3.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.button3.Location = new Point(791, 3);
			this.button3.Name = "button3";
			this.button3.Size = new Size(782, 85);
			this.button3.TabIndex = 5;
			this.button3.Text = "المحطات";
			this.button3.UseVisualStyleBackColor = true;
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
				this.Column3,
				this.Column4,
				this.Column5,
				this.Column6,
				this.Column7
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
			this.dataGridView2.Location = new Point(3, 94);
			this.dataGridView2.Name = "dataGridView2";
			this.dataGridView2.RowTemplate.Height = 29;
			this.dataGridView2.Size = new Size(782, 818);
			this.dataGridView2.TabIndex = 2;
			this.dataGridView2.CellContentClick += new DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
			this.dataGridView2.RowHeaderMouseClick += new DataGridViewCellMouseEventHandler(this.dataGridView2_RowHeaderMouseClick);
			this.dataGridViewTextBoxColumn1.HeaderText = "الرقم";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn2.HeaderText = "الصنف";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.Column3.HeaderText = "المحافظة";
			this.Column3.Name = "Column3";
			this.Column4.HeaderText = "السعر ريال";
			this.Column4.Name = "Column4";
			this.Column5.HeaderText = "السعر دولار";
			this.Column5.Name = "Column5";
			this.Column6.HeaderText = "رقم المحافظة";
			this.Column6.Name = "Column6";
			this.Column6.Visible = false;
			this.Column7.FlatStyle = FlatStyle.Flat;
			this.Column7.HeaderText = "تعديل";
			this.Column7.Name = "Column7";
			this.Column7.Text = "تعديل";
			this.Column7.ToolTipText = "تعديل";
			this.Column7.UseColumnTextForButtonValue = true;
			this.splitContainer1.BorderStyle = BorderStyle.FixedSingle;
			this.splitContainer1.Dock = DockStyle.Fill;
			this.splitContainer1.Location = new Point(791, 94);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = Orientation.Horizontal;
			this.splitContainer1.Panel1.BackColor = Color.LightSteelBlue;
			this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
			this.splitContainer1.Panel1.RightToLeft = RightToLeft.Yes;
			this.splitContainer1.Panel2.BackColor = Color.LightSteelBlue;
			this.splitContainer1.Panel2.Controls.Add(this.buttonUpdate);
			this.splitContainer1.Panel2.Controls.Add(this.textBoxprcD);
			this.splitContainer1.Panel2.Controls.Add(this.label7);
			this.splitContainer1.Panel2.Controls.Add(this.textBoxprcR);
			this.splitContainer1.Panel2.Controls.Add(this.label5);
			this.splitContainer1.Panel2.Controls.Add(this.comboBoxgov);
			this.splitContainer1.Panel2.Controls.Add(this.label6);
			this.splitContainer1.Panel2.Controls.Add(this.button1);
			this.splitContainer1.Panel2.Controls.Add(this.textBox1prodId);
			this.splitContainer1.Panel2.Controls.Add(this.label2);
			this.splitContainer1.Panel2.Controls.Add(this.textBox2proname);
			this.splitContainer1.Panel2.Controls.Add(this.label4);
			this.splitContainer1.Panel2.RightToLeft = RightToLeft.Yes;
			this.splitContainer1.Panel2.Paint += new PaintEventHandler(this.splitContainer1_Panel2_Paint);
			this.splitContainer1.Size = new Size(782, 818);
			this.splitContainer1.SplitterDistance = 301;
			this.splitContainer1.TabIndex = 0;
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
				this.Column2
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
			this.dataGridView1.Size = new Size(780, 299);
			this.dataGridView1.TabIndex = 1;
			this.dataGridView1.RowHeaderMouseClick += new DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
			this.Column1.HeaderText = "الرقم";
			this.Column1.Name = "Column1";
			this.Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.Column2.HeaderText = "الصنف";
			this.Column2.Name = "Column2";
			this.buttonUpdate.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 0);
			this.buttonUpdate.Font = new Font("Arial", 12f, FontStyle.Bold);
			this.buttonUpdate.Location = new Point(215, 264);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new Size(134, 37);
			this.buttonUpdate.TabIndex = 126;
			this.buttonUpdate.Text = "تحديث";
			this.buttonUpdate.UseVisualStyleBackColor = true;
			this.buttonUpdate.Visible = false;
			this.buttonUpdate.Click += new EventHandler(this.buttonUpdate_Click);
			this.textBoxprcD.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBoxprcD.Location = new Point(215, 225);
			this.textBoxprcD.Name = "textBoxprcD";
			this.textBoxprcD.Size = new Size(279, 33);
			this.textBoxprcD.TabIndex = 124;
			this.textBoxprcD.TextAlign = HorizontalAlignment.Center;
			this.label7.AutoSize = true;
			this.label7.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label7.Location = new Point(500, 226);
			this.label7.Name = "label7";
			this.label7.Size = new Size(106, 29);
			this.label7.TabIndex = 123;
			this.label7.Text = "السعر دولار";
			this.textBoxprcR.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBoxprcR.Location = new Point(215, 183);
			this.textBoxprcR.Name = "textBoxprcR";
			this.textBoxprcR.Size = new Size(279, 33);
			this.textBoxprcR.TabIndex = 123;
			this.textBoxprcR.TextAlign = HorizontalAlignment.Center;
			this.label5.AutoSize = true;
			this.label5.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label5.Location = new Point(500, 184);
			this.label5.Name = "label5";
			this.label5.Size = new Size(94, 29);
			this.label5.TabIndex = 119;
			this.label5.Text = "السعر ريال";
			this.comboBoxgov.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxgov.FlatStyle = FlatStyle.Popup;
			this.comboBoxgov.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.comboBoxgov.FormattingEnabled = true;
			this.comboBoxgov.Location = new Point(215, 139);
			this.comboBoxgov.Name = "comboBoxgov";
			this.comboBoxgov.Size = new Size(279, 37);
			this.comboBoxgov.TabIndex = 122;
			this.label6.AutoSize = true;
			this.label6.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label6.Location = new Point(500, 139);
			this.label6.Name = "label6";
			this.label6.Size = new Size(83, 29);
			this.label6.TabIndex = 117;
			this.label6.Text = "المحافظة";
			this.button1.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 0);
			this.button1.Font = new Font("Arial", 12f, FontStyle.Bold);
			this.button1.Location = new Point(360, 264);
			this.button1.Name = "button1";
			this.button1.Size = new Size(134, 37);
			this.button1.TabIndex = 125;
			this.button1.Text = "اضافة";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click_1);
			this.textBox1prodId.BackColor = SystemColors.ButtonHighlight;
			this.textBox1prodId.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBox1prodId.Location = new Point(215, 59);
			this.textBox1prodId.Name = "textBox1prodId";
			this.textBox1prodId.ReadOnly = true;
			this.textBox1prodId.Size = new Size(279, 33);
			this.textBox1prodId.TabIndex = 120;
			this.textBox1prodId.TextAlign = HorizontalAlignment.Center;
			this.label2.AutoSize = true;
			this.label2.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label2.Location = new Point(500, 63);
			this.label2.Name = "label2";
			this.label2.Size = new Size(101, 29);
			this.label2.TabIndex = 116;
			this.label2.Text = "رقم الصنف";
			this.textBox2proname.BackColor = SystemColors.ButtonHighlight;
			this.textBox2proname.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBox2proname.Location = new Point(215, 100);
			this.textBox2proname.Name = "textBox2proname";
			this.textBox2proname.ReadOnly = true;
			this.textBox2proname.Size = new Size(279, 33);
			this.textBox2proname.TabIndex = 121;
			this.textBox2proname.TextAlign = HorizontalAlignment.Center;
			this.label4.AutoSize = true;
			this.label4.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label4.Location = new Point(500, 101);
			this.label4.Name = "label4";
			this.label4.Size = new Size(104, 29);
			this.label4.TabIndex = 115;
			this.label4.Text = "اسم الصنف";
			base.AutoScaleDimensions = new SizeF(9f, 19f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(1576, 915);
			base.Controls.Add(this.tableLayoutPanel1);
			base.Name = "PrciceForm";
			this.RightToLeft = RightToLeft.Yes;
			this.Text = "اسعار المحروقات";
			base.Load += new EventHandler(this.PrciceForm_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			((ISupportInitialize)this.dataGridView2).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((ISupportInitialize)this.splitContainer1).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((ISupportInitialize)this.dataGridView1).EndInit();
			base.ResumeLayout(false);
		}
	}
}
