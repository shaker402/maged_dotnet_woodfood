using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WAQOOD
{
	public class StaPrivForm : Form
	{
		private string staid = string.Empty;

		private string staname = string.Empty;

		private SqlCommand mCommand = null;

		private static SqlDataAdapter mAdatpter = null;

		private static SqlConnection mConnection = new SqlConnection(MainForm.str_conn);

		private DataTable dtOrder;

		private IContainer components = null;

		private ComboBox comboBoxUsers;

		private TextBox textBox1staname;

		private Label label5;

		private Label label6;

		private Button button1;

		private SplitContainer splitContainer2;

		private DataGridView dataGridView2;

		private TableLayoutPanel tableLayoutPanel1;

		private DataGridView dataGridView1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewButtonColumn Column3;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn Column2;

		private DataGridViewTextBoxColumn Column5;

		private DataGridViewTextBoxColumn Column6;

		public StaPrivForm()
		{
			this.InitializeComponent();
			base.WindowState = FormWindowState.Maximized;
			StaPrivForm.mConnection = new SqlConnection(MainForm.str_conn);
			this.dtOrder = new DataTable();
			this.DisplayData();
		}

		public void DisplayData()
		{
			try
			{
				this.dtOrder.Clear();
				this.dtOrder.Clone();
				this.dataGridView1.Rows.Clear();
				this.dataGridView1.Refresh();
				StaPrivForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlConnection.ClearPool(StaPrivForm.mConnection);
				StaPrivForm.mAdatpter = new SqlDataAdapter("SELECT [sta_id],[sta_name],[gov_name],Stations.regn FROM [dbo].[Stations],Goverments where [Stations].[gov_id]=Goverments.[gov_id] order by [sta_id] asc", StaPrivForm.mConnection);
				StaPrivForm.mAdatpter.Fill(this.dtOrder);
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
						this.dtOrder.Rows[i]["gov_NAME"].ToString(),
						this.dtOrder.Rows[i]["regn"].ToString()
					});
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Fill_Users()
		{
			try
			{
				StaPrivForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT [user_id],[user_name] FROM [dbo].[Users_t] where user_type=2 order by user_id asc", StaPrivForm.mConnection);
				DataTable dataTable = new DataTable();
				sqlDataAdapter.Fill(dataTable);
				DataRow dataRow = dataTable.NewRow();
				dataRow[0] = 0;
				dataRow[1] = "-المستخدم-";
				dataTable.Rows.InsertAt(dataRow, 0);
				this.comboBoxUsers.DataSource = dataTable;
				this.comboBoxUsers.DisplayMember = "user_name";
				this.comboBoxUsers.ValueMember = "user_id";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}
		}

		private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			bool flag = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value != null;
			if (flag)
			{
				this.staid = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
				this.staname = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
				this.textBox1staname.Text = this.staname;
				this.Fill_Users();
				this.DisplayDataUsersPriv();
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				bool flag = this.comboBoxUsers.SelectedIndex.ToString() != "0" && this.staid.Length != 0 && this.staid != string.Empty;
				if (flag)
				{
					this.mCommand = new SqlCommand("INSERT INTO [dbo].[StationUsers](sta_id,[user_id]) VALUES(@sid,@uid)", StaPrivForm.mConnection);
					StaPrivForm.mConnection.Open();
					this.mCommand.Parameters.AddWithValue("@sid", this.staid);
					this.mCommand.Parameters.AddWithValue("@uid", this.comboBoxUsers.SelectedValue);
					this.mCommand.ExecuteNonQuery();
					StaPrivForm.mConnection.Close();
					MessageBox.Show(" تم حفظ البيانات بنجاح");
					this.DisplayDataUsersPriv();
				}
				else
				{
					MessageBox.Show("بيانات غير مكتملة!");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				StaPrivForm.mConnection.Close();
			}
		}

		public void DisplayDataUsersPriv()
		{
			try
			{
				this.dtOrder.Clear();
				this.dtOrder.Clone();
				this.dataGridView2.Rows.Clear();
				this.dataGridView2.Refresh();
				StaPrivForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlConnection.ClearPool(StaPrivForm.mConnection);
				StaPrivForm.mAdatpter = new SqlDataAdapter("SELECT Users_t.[user_id],[user_name] FROM [dbo].[Users_t],StationUsers where user_type='2' and [Users_t].user_id=StationUsers.user_id and StationUsers.sta_id='" + this.staid + "'   order by user_id asc", StaPrivForm.mConnection);
				StaPrivForm.mAdatpter.Fill(this.dtOrder);
				bool flag = this.dtOrder.Rows.Count == 0;
				if (!flag)
				{
					this.add_coulm2();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public void add_coulm2()
		{
			try
			{
				for (int i = 0; i < this.dtOrder.Rows.Count; i++)
				{
					this.dataGridView2.Rows.Add(new object[]
					{
						this.dtOrder.Rows[i]["user_ID"].ToString(),
						this.dtOrder.Rows[i]["user_NAME"].ToString()
					});
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void StaPrivForm_Load(object sender, EventArgs e)
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
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
			this.comboBoxUsers = new ComboBox();
			this.textBox1staname = new TextBox();
			this.label5 = new Label();
			this.label6 = new Label();
			this.button1 = new Button();
			this.splitContainer2 = new SplitContainer();
			this.dataGridView2 = new DataGridView();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.Column3 = new DataGridViewButtonColumn();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.dataGridView1 = new DataGridView();
			this.Column1 = new DataGridViewTextBoxColumn();
			this.Column2 = new DataGridViewTextBoxColumn();
			this.Column5 = new DataGridViewTextBoxColumn();
			this.Column6 = new DataGridViewTextBoxColumn();
			((ISupportInitialize)this.splitContainer2).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((ISupportInitialize)this.dataGridView2).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			base.SuspendLayout();
			this.comboBoxUsers.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxUsers.FlatStyle = FlatStyle.Flat;
			this.comboBoxUsers.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.comboBoxUsers.FormattingEnabled = true;
			this.comboBoxUsers.Location = new Point(161, 65);
			this.comboBoxUsers.Name = "comboBoxUsers";
			this.comboBoxUsers.Size = new Size(279, 37);
			this.comboBoxUsers.TabIndex = 123;
			this.textBox1staname.Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.textBox1staname.Location = new Point(161, 26);
			this.textBox1staname.Name = "textBox1staname";
			this.textBox1staname.ReadOnly = true;
			this.textBox1staname.Size = new Size(279, 33);
			this.textBox1staname.TabIndex = 114;
			this.textBox1staname.TextAlign = HorizontalAlignment.Center;
			this.label5.AutoSize = true;
			this.label5.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label5.Location = new Point(448, 68);
			this.label5.Name = "label5";
			this.label5.Size = new Size(86, 29);
			this.label5.TabIndex = 109;
			this.label5.Text = "المستخدم";
			this.label6.AutoSize = true;
			this.label6.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label6.Location = new Point(446, 30);
			this.label6.Name = "label6";
			this.label6.Size = new Size(70, 29);
			this.label6.TabIndex = 107;
			this.label6.Text = "المحطة";
			this.button1.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 0);
			this.button1.Font = new Font("Arial", 12f, FontStyle.Bold);
			this.button1.Location = new Point(229, 108);
			this.button1.Name = "button1";
			this.button1.Size = new Size(134, 37);
			this.button1.TabIndex = 112;
			this.button1.Text = "اضافة";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.splitContainer2.BorderStyle = BorderStyle.FixedSingle;
			this.splitContainer2.Dock = DockStyle.Fill;
			this.splitContainer2.Location = new Point(3, 67);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = Orientation.Horizontal;
			this.splitContainer2.Panel1.BackColor = Color.LightSteelBlue;
			this.splitContainer2.Panel1.Controls.Add(this.comboBoxUsers);
			this.splitContainer2.Panel1.Controls.Add(this.textBox1staname);
			this.splitContainer2.Panel1.Controls.Add(this.label5);
			this.splitContainer2.Panel1.Controls.Add(this.label6);
			this.splitContainer2.Panel1.Controls.Add(this.button1);
			this.splitContainer2.Panel1.RightToLeft = RightToLeft.Yes;
			this.splitContainer2.Panel2.Controls.Add(this.dataGridView2);
			this.splitContainer2.Panel2.RightToLeft = RightToLeft.Yes;
			this.splitContainer2.Size = new Size(715, 571);
			this.splitContainer2.SplitterDistance = 211;
			this.splitContainer2.TabIndex = 1;
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
			this.dataGridView2.Size = new Size(713, 354);
			this.dataGridView2.TabIndex = 1;
			this.dataGridViewTextBoxColumn1.HeaderText = "الرقم";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn2.HeaderText = "الاسم";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.Column3.FlatStyle = FlatStyle.Flat;
			this.Column3.HeaderText = "حذف";
			this.Column3.Name = "Column3";
			this.Column3.Text = "حذف";
			this.Column3.ToolTipText = "حذف";
			this.Column3.UseColumnTextForButtonValue = true;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.splitContainer2, 1, 1);
			this.tableLayoutPanel1.Dock = DockStyle.Fill;
			this.tableLayoutPanel1.Location = new Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 90f));
			this.tableLayoutPanel1.Size = new Size(1441, 641);
			this.tableLayoutPanel1.TabIndex = 2;
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
			this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle6;
			this.dataGridView1.Dock = DockStyle.Fill;
			this.dataGridView1.Location = new Point(724, 67);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowTemplate.Height = 29;
			this.dataGridView1.Size = new Size(714, 571);
			this.dataGridView1.TabIndex = 2;
			this.dataGridView1.RowHeaderMouseClick += new DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
			this.Column1.HeaderText = "الرقم";
			this.Column1.Name = "Column1";
			this.Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.Column2.HeaderText = "اسم المحطة";
			this.Column2.Name = "Column2";
			this.Column5.HeaderText = "المحافظة";
			this.Column5.Name = "Column5";
			this.Column6.HeaderText = "المنطقة";
			this.Column6.Name = "Column6";
			base.AutoScaleDimensions = new SizeF(9f, 19f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(1441, 641);
			base.Controls.Add(this.tableLayoutPanel1);
			base.Name = "StaPrivForm";
			this.RightToLeft = RightToLeft.Yes;
			this.Text = "ربط المستخدم بالمحطة";
			base.Load += new EventHandler(this.StaPrivForm_Load);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel1.PerformLayout();
			this.splitContainer2.Panel2.ResumeLayout(false);
			((ISupportInitialize)this.splitContainer2).EndInit();
			this.splitContainer2.ResumeLayout(false);
			((ISupportInitialize)this.dataGridView2).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			((ISupportInitialize)this.dataGridView1).EndInit();
			base.ResumeLayout(false);
		}
	}
}
