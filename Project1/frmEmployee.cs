using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
namespace Project1
{
    public partial class frmEmployee : Form
    {
        public frmEmployee()
        {
            InitializeComponent();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblPosition = new System.Windows.Forms.Label();
            this.txtPosition = new System.Windows.Forms.TextBox();
            this.lblSalary = new System.Windows.Forms.Label();
            this.txtSalary = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
             
            this.lblName.AutoSize = true;
            this.lblName.Font = new Font("Segoe UI", 10F);
            this.lblName.Location = new Point(50, 50);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(100, 20);
            this.lblName.Text = "Name:";
            
            this.txtName.Font = new Font("Segoe UI", 10F);
            this.txtName.Location = new Point(150, 50);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(200, 25);
            
            this.lblPosition.AutoSize = true;
            this.lblPosition.Font = new Font("Segoe UI", 10F);
            this.lblPosition.Location = new Point(50, 90);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new Size(100, 20);
            this.lblPosition.Text = "Position:";
             
            this.txtPosition.Font = new Font("Segoe UI", 10F);
            this.txtPosition.Location = new Point(150, 90);
            this.txtPosition.Name = "txtPosition";
            this.txtPosition.Size = new Size(200, 25);
             
            this.lblSalary.AutoSize = true;
            this.lblSalary.Font = new Font("Segoe UI", 10F);
            this.lblSalary.Location = new Point(50, 130);
            this.lblSalary.Name = "lblSalary";
            this.lblSalary.Size = new Size(100, 20);
            this.lblSalary.Text = "Salary:";
            
            this.txtSalary.Font = new Font("Segoe UI", 10F);
            this.txtSalary.Location = new Point(150, 130);
            this.txtSalary.Name = "txtSalary";
            this.txtSalary.Size = new Size(200, 25);
            
            this.btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnAdd.BackColor = Color.LightGreen;
            this.btnAdd.Location = new Point(50, 180);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(120, 30);
            this.btnAdd.Text = "Add Employee";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            
            this.btnEdit.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnEdit.BackColor = Color.LightBlue;
            this.btnEdit.Location = new Point(180, 180);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new Size(120, 30);
            this.btnEdit.Text = "Edit Employee";
            this.btnEdit.Click += new EventHandler(this.btnEdit_Click);
           
            this.btnDelete.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnDelete.BackColor = Color.LightCoral;
            this.btnDelete.Location = new Point(310, 180);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(120, 30);
            this.btnDelete.Text = "Delete Employee";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
          
            this.dataGridView1.Location = new Point(50, 250);
            this.dataGridView1.Name = "dgvEmployees";
            this.dataGridView1.Size = new Size(600, 200);
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.CellClick += new DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
          
            this.ClientSize = new System.Drawing.Size(700, 500);   
            this.BackColor = System.Drawing.Color.LightCyan;      
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblPosition);
            this.Controls.Add(this.txtPosition);
            this.Controls.Add(this.lblSalary);
            this.Controls.Add(this.txtSalary);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmEmployee";
            this.Text = "Employee Management";
            this.Load += new System.EventHandler(this.frmEmployee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private const string ConnStr =
    @"Server=DESKTOP-SJQQ6K9\SQLEXPRESS;
      Database=""Project 1"";
      Trusted_Connection=True;
      Encrypt=True;
      TrustServerCertificate=True";

        private void LoadEmployees()
        {
            const string sql =
                "SELECT EmployeeID, [Name], [Position], Salary FROM dbo.Employees";

            using (var con = new SqlConnection(ConnStr))
            using (var da = new SqlDataAdapter(sql, con))
            {
                var dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                if (dataGridView1.Columns.Count > 0)
                    dataGridView1.Columns["EmployeeID"].Visible = false;
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtSalary.Text, out decimal sal))
            {
                MessageBox.Show("Salary must be a number.");
                return;
            }

            const string sql = @"INSERT INTO dbo.Employees ([Name],[Position],Salary)
                         VALUES (@n,@p,@s)";

            using (var con = new SqlConnection(ConnStr))
            {
                con.Open();
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@n", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@p", txtPosition.Text.Trim());
                    cmd.Parameters.AddWithValue("@s", sal);
                    cmd.ExecuteNonQuery();

                    LoadEmployees();
                    ClearInputs();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Select a record first."); return;
            }
            if (!decimal.TryParse(txtSalary.Text, out decimal sal))
            {
                MessageBox.Show("Salary must be a number."); return;
            }

            int id = (int)dataGridView1.CurrentRow.Cells["EmployeeID"].Value;

            const string sql = @"UPDATE dbo.Employees
                         SET [Name]=@n, [Position]=@p, Salary=@s
                         WHERE EmployeeID=@id";

            using (var con = new SqlConnection(ConnStr))
            {


                con.Open();
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@n", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@p", txtPosition.Text.Trim());
                    cmd.Parameters.AddWithValue("@s", sal);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }

            LoadEmployees();
            ClearInputs();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Select a record first."); return;
            }
            int id = (int)dataGridView1.CurrentRow.Cells["EmployeeID"].Value;

            if (MessageBox.Show("Delete selected employee?", "Confirm",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                != DialogResult.Yes) return;

            const string sql = "DELETE FROM dbo.Employees WHERE EmployeeID=@id";

            using (var con = new SqlConnection(ConnStr))
            {
                con.Open();
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();

                    LoadEmployees();
                    ClearInputs();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;   

            var row = dataGridView1.Rows[e.RowIndex];
            txtName.Text = row.Cells["Name"].Value.ToString();
            txtPosition.Text = row.Cells["Position"].Value.ToString();
            txtSalary.Text = row.Cells["Salary"].Value.ToString();

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPosition_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSalary_TextChanged(object sender, EventArgs e)
        {

        }
        private void ClearInputs()
        {
            txtName.Clear();
            txtPosition.Clear();
            txtSalary.Clear();
            txtName.Focus();
        }

        private void frmEmployee_Load(object sender, EventArgs e)
        {

        }
    }
}
