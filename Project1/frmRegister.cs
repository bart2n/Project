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
using System.Data;
using System.Diagnostics;

namespace Project1
{
    public partial class frmRegister : Form
    {
        public frmRegister()
        {
            InitializeComponent();
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.Navy;
            this.lblTitle.Location = new Point(110, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(180, 30);
            this.lblTitle.Text = "Register";
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
            this.lblConfirm.AutoSize = true;
            this.lblConfirm.Font = new Font("Segoe UI", 10F);
            this.lblConfirm.Location = new Point(50, 160);
            this.lblConfirm.Name = "lblConfirm";
            this.lblConfirm.Size = new Size(61, 19);
            this.lblConfirm.Text = "Confirm:";
            this.txtConfirm.Font = new Font("Segoe UI", 10F);
            this.txtConfirm.Location = new Point(150, 80);
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.Size = new Size(200, 25);
            this.txtConfirm.UseSystemPasswordChar = true;
            this.btnRegister.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnRegister.BackColor = Color.LightBlue;
            this.btnRegister.Location = new Point(200, 180);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new Size(100, 30);
            this.btnRegister.Text = "Register";
            this.btnRegister.Click += new EventHandler(this.button1_Click);
            this.BackColor = Color.LightYellow;
            this.ClientSize = new Size(400, 350);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblConfirm);
            this.Controls.Add(this.txtConfirm);
            this.Controls.Add(this.btnRegister);
            this.Name = "frmRegister";
            this.Text = "Register";
            this.Load += new EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        const string ConnStr =
  @"Server=DESKTOP-SJQQ6K9\SQLEXPRESS;Database=""Project 1"";Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True";
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text;
            string pass2 = txtConfirm.Text;

            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("You have to enter passoword and username!");
                return;
            }
            if (pass != pass2)
            {
                MessageBox.Show("The passwords you entered doesnt match!");
                return;
            }


            const string sql = "INSERT INTO dbo.Users(UserName, Password) VALUES (@u, @p)";

            try
            {

                using (var con = new SqlConnection(ConnStr))
                {
                    con.Open();
                    using (var cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@u", user);
                        cmd.Parameters.AddWithValue("@p", pass);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Registeration Successful!");
                var Login = new frmLogin();   
                Login.Show();
                this.Hide();
            }

            catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                MessageBox.Show("This username is already in use!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
