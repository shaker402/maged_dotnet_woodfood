using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WAQOOD.Properties;

namespace WAQOOD
{
	public class ConfirmForm : Form
	{
		private int actv;

		private string carid = string.Empty;

		private string caramnt = string.Empty;

		private string caroil = string.Empty;

		private string ordDate = string.Empty;

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

		private DataTable dtorders;

		private IContainer components = null;

		private TextBox textBoxOrdNo;

		private Label label2;

		private TextBox textBoxOrddate;

		private Label label1;

		private Label label3;

		private Panel panel3;

		private TextBox textBox_comp_id;

		private PictureBox pictureBoxQR;

		private Panel panel5;

		private Button buttonUpdate;

		private Button button2;

		private Panel panel4;

		private Panel panel1;

		private PictureBox pictureBox1;

		private Button button4;

		private TableLayoutPanel tableLayoutPanel1;

		private Panel panel2;

		private DataGridView dataGridView1;

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

		private DataGridViewTextBoxColumn Column13;

		public ConfirmForm()
		{
			this.InitializeComponent();
			base.WindowState = FormWindowState.Maximized;
			ConfirmForm.mConnection = new SqlConnection(MainForm.str_conn);
			this.textBoxOrddate.Text = DateTime.Now.ToString();
			this.textBox_comp_id.Text = LogonForm.braname;
			this.dtorders = new DataTable();
			this.DisplayData();
		}

		public void DisplayData()
		{
			try
			{
				this.dtorders.Clear();
				this.dtorders.Clone();
				this.dataGridView1.Rows.Clear();
				this.dataGridView1.Refresh();
				ConfirmForm.mConnection = new SqlConnection(MainForm.str_conn);
				SqlConnection.ClearPool(ConfirmForm.mConnection);
				ConfirmForm.mAdatpter = new SqlDataAdapter(string.Concat(new string[]
				{
					"select prc,sta_name,Trans_table.sta_id,trans_id,trans_id_bra,[order_date],[car_id],[car_id_bra],car_id_comp,[car_no],[amt],Trans_table.[prod_id],prod_name,car_driver,Stations.curr_id,curr_name from [dbo].[Trans_table],Products,Stations,Currency where Currency.curr_id=Trans_table.curr_id and Trans_table.sta_id=Stations.sta_id and [trans_type] in(1,2,3) and flag='1' and [Trans_table].prod_id=Products.prod_id and [tyear]='",
					DateTime.Now.Year.ToString(),
					"' and [tmonth]='",
					DateTime.Now.Month.ToString(),
					"' and [bra_id]='",
					LogonForm.braid,
					"' and [comp_id]='",
					LogonForm.compid,
					"' order by [trans_id_bra] asc"
				}), ConfirmForm.mConnection);
				ConfirmForm.mAdatpter.Fill(this.dtorders);
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
						i + 1,
						this.dtorders.Rows[i]["trans_id_bra"].ToString(),
						this.dtorders.Rows[i]["car_id_bra"].ToString(),
						this.dtorders.Rows[i]["car_no"].ToString(),
						this.dtorders.Rows[i]["prod_name"].ToString(),
						this.dtorders.Rows[i]["sta_name"].ToString(),
						this.dtorders.Rows[i]["amt"].ToString(),
						this.dtorders.Rows[i]["car_driver"].ToString(),
						this.dtorders.Rows[i]["order_date"].ToString(),
						this.dtorders.Rows[i]["prod_id"].ToString(),
						this.dtorders.Rows[i]["car_id"].ToString(),
						this.dtorders.Rows[i]["car_id_comp"].ToString(),
						this.dtorders.Rows[i]["trans_id"].ToString(),
						this.dtorders.Rows[i]["curr_name"].ToString(),
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
				this.transidbra = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
				this.carbra = this.dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
				this.carno = this.dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
				this.caramnt = this.dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
				this.ordDate = this.dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
				this.caroil = this.dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
				this.carid = this.dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
				this.carcomp = this.dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
				this.transid = this.dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
				this.transid = this.dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
			}
		}

		private void buttonUpdate_Click(object sender, EventArgs e)
		{
			try
			{
				ConfirmForm.mConnection.Close();
				bool flag = this.transidbra.Length != 0 && this.transid.Length != 0 && this.carid.Length != 0 && this.carno.Length != 0;
				if (flag)
				{
					this.mCommand = new SqlCommand(string.Concat(new object[]
					{
						"update [dbo].[Trans_table] set flag='2',inuser_comp2='",
						LogonForm.userID,
						"' ,confirm_date='",
						DateTime.Now.ToString("yyyy-MM-dd"),
						"' where flag='1' and trans_id_bra='",
						this.transidbra,
						"' and trans_id='",
						this.transid,
						"' and [tyear]='",
						DateTime.Now.Year,
						"' and [tmonth]='",
						DateTime.Now.Month,
						"' and [bra_id]='",
						LogonForm.braid,
						"' and [comp_id]='",
						LogonForm.compid,
						"'and  amt='",
						this.caramnt,
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
					}), ConfirmForm.mConnection);
					ConfirmForm.mConnection.Open();
					int num = this.mCommand.ExecuteNonQuery();
					ConfirmForm.mConnection.Close();
					bool flag2 = num >= 1;
					if (flag2)
					{
						MessageBox.Show("تم التثبيت بنجاح" + num.ToString());
						this.DisplayData();
					}
					else
					{
						MessageBox.Show("خطأ في حفظ الملف" + num.ToString());
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
				ConfirmForm.mConnection.Close();
			}
		}

		private void ConfirmForm_Load(object sender, EventArgs e)
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ConfirmForm));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			this.textBoxOrdNo = new TextBox();
			this.label2 = new Label();
			this.textBoxOrddate = new TextBox();
			this.label1 = new Label();
			this.label3 = new Label();
			this.panel3 = new Panel();
			this.textBox_comp_id = new TextBox();
			this.pictureBoxQR = new PictureBox();
			this.panel5 = new Panel();
			this.buttonUpdate = new Button();
			this.button2 = new Button();
			this.panel4 = new Panel();
			this.dataGridView1 = new DataGridView();
			this.panel1 = new Panel();
			this.pictureBox1 = new PictureBox();
			this.button4 = new Button();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.panel2 = new Panel();
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
			this.Column13 = new DataGridViewTextBoxColumn();
			this.panel3.SuspendLayout();
			((ISupportInitialize)this.pictureBoxQR).BeginInit();
			this.panel5.SuspendLayout();
			this.panel4.SuspendLayout();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			this.panel1.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel2.SuspendLayout();
			base.SuspendLayout();
			this.textBoxOrdNo.BackColor = SystemColors.ButtonHighlight;
			this.textBoxOrdNo.Enabled = false;
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
			this.panel3.Size = new Size(1543, 63);
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
			this.pictureBoxQR.Location = new Point(-3, -1);
			this.pictureBoxQR.Name = "pictureBoxQR";
			this.pictureBoxQR.Size = new Size(200, 96);
			this.pictureBoxQR.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBoxQR.TabIndex = 147;
			this.pictureBoxQR.TabStop = false;
			this.panel5.BorderStyle = BorderStyle.FixedSingle;
			this.panel5.Controls.Add(this.buttonUpdate);
			this.panel5.Controls.Add(this.pictureBoxQR);
			this.panel5.Controls.Add(this.button2);
			this.panel5.Dock = DockStyle.Bottom;
			this.panel5.Location = new Point(0, 472);
			this.panel5.Name = "panel5";
			this.panel5.Size = new Size(1539, 100);
			this.panel5.TabIndex = 101;
			this.buttonUpdate.BackColor = Color.White;
			this.buttonUpdate.Cursor = Cursors.Hand;
			this.buttonUpdate.FlatStyle = FlatStyle.Flat;
			this.buttonUpdate.Font = new Font("Arial", 16f, FontStyle.Bold);
			this.buttonUpdate.ForeColor = Color.FromArgb(64, 64, 64);
			this.buttonUpdate.Location = new Point(453, 27);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new Size(146, 52);
			this.buttonUpdate.TabIndex = 23;
			this.buttonUpdate.Text = "ترحيل";
			this.buttonUpdate.UseVisualStyleBackColor = false;
			this.buttonUpdate.Click += new EventHandler(this.buttonUpdate_Click);
			this.button2.BackColor = Color.White;
			this.button2.Cursor = Cursors.Hand;
			this.button2.FlatStyle = FlatStyle.Flat;
			this.button2.Font = new Font("Arial", 16f, FontStyle.Bold);
			this.button2.ForeColor = Color.FromArgb(64, 64, 64);
			this.button2.Location = new Point(618, 27);
			this.button2.Name = "button2";
			this.button2.Size = new Size(146, 52);
			this.button2.TabIndex = 21;
			this.button2.Text = "الغاء";
			this.button2.UseVisualStyleBackColor = false;
			this.panel4.BackColor = SystemColors.GradientActiveCaption;
			this.panel4.BorderStyle = BorderStyle.Fixed3D;
			this.panel4.Controls.Add(this.dataGridView1);
			this.panel4.Controls.Add(this.panel5);
			this.panel4.Dock = DockStyle.Fill;
			this.panel4.Location = new Point(0, 63);
			this.panel4.Name = "panel4";
			this.panel4.Size = new Size(1543, 576);
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
			this.dataGridView1.Size = new Size(1539, 472);
			this.dataGridView1.TabIndex = 148;
			this.dataGridView1.RowHeaderMouseClick += new DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Dock = DockStyle.Fill;
			this.panel1.Location = new Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(769, 65);
			this.panel1.TabIndex = 2;
			this.pictureBox1.BorderStyle = BorderStyle.FixedSingle;
			this.pictureBox1.Dock = DockStyle.Fill;
			this.pictureBox1.Image = Resources.waqood;
			this.pictureBox1.Location = new Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new Size(769, 65);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.button4.Dock = DockStyle.Fill;
			this.button4.Enabled = false;
			this.button4.FlatStyle = FlatStyle.Flat;
			this.button4.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.button4.Location = new Point(778, 3);
			this.button4.Name = "button4";
			this.button4.Size = new Size(768, 65);
			this.button4.TabIndex = 3;
			this.button4.Text = "اعتماد طلبات الوقود";
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
			this.tableLayoutPanel1.Size = new Size(1549, 716);
			this.tableLayoutPanel1.TabIndex = 2;
			this.tableLayoutPanel1.SetColumnSpan(this.panel2, 2);
			this.panel2.Controls.Add(this.panel4);
			this.panel2.Controls.Add(this.panel3);
			this.panel2.Dock = DockStyle.Fill;
			this.panel2.Location = new Point(3, 74);
			this.panel2.Name = "panel2";
			this.panel2.Size = new Size(1543, 639);
			this.panel2.TabIndex = 4;
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
			this.Column11.HeaderText = "المحطة";
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
			this.Column13.HeaderText = "السعر";
			this.Column13.Name = "Column13";
			base.AutoScaleDimensions = new SizeF(9f, 19f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(1549, 716);
			base.Controls.Add(this.tableLayoutPanel1);
			base.Name = "ConfirmForm";
			this.RightToLeft = RightToLeft.Yes;
			this.Text = "اعتماد طلب وقود";
			base.Load += new EventHandler(this.ConfirmForm_Load);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			((ISupportInitialize)this.pictureBoxQR).EndInit();
			this.panel5.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			((ISupportInitialize)this.dataGridView1).EndInit();
			this.panel1.ResumeLayout(false);
			((ISupportInitialize)this.pictureBox1).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}
