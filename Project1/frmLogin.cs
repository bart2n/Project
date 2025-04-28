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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.Navy;
            this.lblTitle.Location = new Point(110, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(180, 30);
            this.lblTitle.Text = "Login";

            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new Font("Segoe UI", 10F);
            this.lblUsername.Location = new Point(50, 80);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new Size(100, 20);
            this.lblUsername.Text = "Username:";

            this.txtUsername.Font = new Font("Segoe UI", 10F);
            this.txtUsername.Location = new Point(150, 80);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new Size(200, 25);
            

            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new Font("Segoe UI", 10F);
            this.lblPassword.Location = new Point(50, 120);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new Size(100, 20);
            this.lblPassword.Text = "Password:";


            this.txtPassword.Font = new Font("Segoe UI", 10F);
            this.txtPassword.Location = new Point(150, 120);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new Size(200, 25);
            this.txtPassword.UseSystemPasswordChar = true;

            this.btnLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnLogin.BackColor = Color.LightGreen;
            this.btnLogin.Location = new Point(50, 180);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new Size(100, 30);
            this.btnLogin.Text = "Login";

            this.btnRegister.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnRegister.BackColor = Color.LightBlue;
            this.btnRegister.Location = new Point(200, 180);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new Size(100, 30);
            this.btnRegister.Text = "Register";

            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.LightGray;
            this.ClientSize = new Size(400, 300);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnRegister);
            this.Name = "frmLogin";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        public static class Session
        {
            public static string CurrentUser { get; set; }
        }
        private const string ConnStr =
    @"Server=DESKTOP-SJQQ6K9\SQLEXPRESS;
      Database=""Project 1"";
      Trusted_Connection=True;
      Encrypt=True;
      TrustServerCertificate=True";
        private void button1_Click(object sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Please Enter Username and Password.");
                return;
            }

            const string sql = "SELECT COUNT(*) FROM dbo.Users " +
                               "WHERE UserName = @u AND [Password] = @p";

            try
            {
                using (var con = new SqlConnection(ConnStr))
                {
                    con.Open();
                    using (var cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@u", user);
                        cmd.Parameters.AddWithValue("@p", pass);

                        int match = (int)cmd.ExecuteScalar();

                        if (match == 1)         
                        {
                            var main = new frmMain(user);   
                            main.Show();
                            this.Hide();               
                        }
                        else
                        {
                            MessageBox.Show("Password or Username doesnt match any accounts");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var reg = new frmRegister();
            reg.Show();
            this.Hide();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
    }
