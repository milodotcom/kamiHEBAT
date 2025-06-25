using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projectw
{
    public partial class Form1 : Form
    {

        /// private string connectionString = @"Data Source=YOUR_SERVER_NAME;Initial Catalog=GreenBinDB;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
            LoadCustomerData();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ClearFields();
            LoadCustomerData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textName_TextChanged(object sender, EventArgs e)
        {

        }

        private void textAddress_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
