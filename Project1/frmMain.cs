using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Project1
{
    public partial class frmMain : Form
    {
        private readonly string _username;          

        public frmMain(string username)
        {
            InitializeComponent();
            _username = username;

            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblWelcome.ForeColor = Color.DarkGreen;
            this.lblWelcome.Location = new Point(50, 20);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new Size(200, 30);
            this.lblWelcome.Text = "Welcome, [User]";


 
            this.btnManageEmployees.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnManageEmployees.BackColor = Color.LightBlue;
            this.btnManageEmployees.Location = new Point(200, 100);
            this.btnManageEmployees.Name = "btnManageEmployees";
            this.btnManageEmployees.Size = new Size(200, 40);
            this.btnManageEmployees.Text = "Manage Employees";


            this.btnLogout.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnLogout.BackColor = Color.LightCoral;
            this.btnLogout.Location = new Point(250, 250);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new Size(100, 30);
            this.btnLogout.Text = "Logout";

 
            this.BackColor = Color.WhiteSmoke;
            this.ClientSize = new Size(600, 400);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.btnManageEmployees);
            this.Controls.Add(this.btnLogout);
            this.Name = "frmMain";
            this.Text = "Dashboard";
            

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnManageEmployees_Click(object sender, EventArgs e)
        {
            var manage = new frmEmployee();
            manage.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Exiting App...");
            this.Hide();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Welcome {_username}!";  
        }
    }
}
