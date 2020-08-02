using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P_M_App_SQLite1
{
    // TODO center this Form_SaveTOTable inside Form1
    public partial class Form_SaveToTable : Form
    {
        public Form_SaveToTable()
        {
            InitializeComponent();
            textBox3.PasswordChar = '*';
            textBox4.PasswordChar = '*';
        }

        private void button2_Click(object sender, EventArgs e)
        {//close button
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {//add button
            string url, username, password, password2;
            url = textBox1.Text;
            username = textBox2.Text;
            password = textBox3.Text;
            password2 = textBox4.Text;
            if (password.Equals(password2))
            {
                addtosql(url, username, password);
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                // TODO Reload DataGridView from Form1 window w/o closing this form
                Close();
            }
            else
            {
                MessageBox.Show("The Password dosn't match");
            }
            
        }
        public void addtosql(string url, string username, string password)
        {//stores data into the database
            UserInfo user = new UserInfo();
            user.url = url;
            user.userName = username;
            user.password = password;
            SQLiteDataAccess.SaveUser(user);
        }

    }
}
