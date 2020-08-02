using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//show all files
//Right Click include in porject
//App.config
//Right click Refrences + Manage NuGet Packages
//System.Data.SQLite.Core
//Dapper
namespace P_M_App_SQLite1
{
    public partial class Form1 : Form
    {
        List<UserInfo> listUserInfo = new List<UserInfo>();
        public Form1()
        {
            InitializeComponent();
            LoadUserInfoList();
            
        }

        public void LoadGridView()
        {//inserts the data from the List into the dataGrid Table
            
            if(listUserInfo.Count > 0)
            {//so the program wont crash if the dataGridView is empty
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
            }
            foreach (var list in listUserInfo)
            {
                dataGridView1.Rows.Add(list.url, list.userName, list.password, list.id );
            }
        }

        public void LoadUserInfoList()
        {//gets data from the database, adds it to the List<UserInfo>, reloads dataGrid Table
            listUserInfo.Clear();
            listUserInfo = SQLiteDataAccess.LoadUser();
            LoadGridView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //TODO add url, usename, password
            Form_SaveToTable EnterToDatabase = new Form_SaveToTable();
            EnterToDatabase.ShowDialog();
            LoadUserInfoList();
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            copyToClipboard("url_column");
            
        }
        private void button4_Click(object sender, EventArgs e)
        {
            copyToClipboard("userName_column");
            
        }
        private void button5_Click(object sender, EventArgs e)
        {
            copyToClipboard("password_column");
           
        }

        private void copyToClipboard(string columnName)
        {
            //gets the selected cell in the table and copies it to the clipboard
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int rowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[rowindex];
                string cellSelected = Convert.ToString(selectedRow.Cells[columnName].Value);
                Clipboard.SetText(cellSelected);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {//gets the selected cell id and deletes it from the database
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int rowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[rowindex];
                string cellSelected = Convert.ToString(selectedRow.Cells["id_column"].Value);
                SQLiteDataAccess.removeItemFromSQLite(cellSelected);
            }
            LoadUserInfoList();
        }

        private void button6_Click(object sender, EventArgs e)
        {//generate password
         //Membership.GeneratePassword(12, 1);
            textBox1.Clear();
            string psw = CreateRandomPassword();
            textBox1.Text = psw;

        }
        private static string CreateRandomPassword(int length = 15)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }
    }
}
