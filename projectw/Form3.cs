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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'greenbidDataSet2.Pickups' table. You can move, or remove it, as needed.
            this.pickupsTableAdapter.Fill(this.greenbidDataSet2.Pickups);
            // TODO: This line of code loads data into the 'greenbidDataSet.Customers' table. You can move, or remove it, as needed.
            this.customersTableAdapter.Fill(this.greenbidDataSet.Customers);

        }

        private void btnBack_Click(object sender, EventArgs e)
        {

        }
    }
}
