using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            UpdateCustomer();
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

        private void btnDel_Click(object sender, EventArgs e)
        {
            DeleteCustomer();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Form2 dropOffForm = new Form2();
            dropOffForm.Show();
            this.Hide();
        }
    }

    // Add new customer to database
        private void AddCustomer()
        {
            try
            {
                if (ValidateInput())
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "INSERT INTO Customers (Name, PhoneNumber, Address) VALUES (@Name, @Phone, @Address)";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Name", textName.Text.Trim());
                            cmd.Parameters.AddWithValue("@Phone", textBox2.Text.Trim());
                            cmd.Parameters.AddWithValue("@Address", textAddress.Text.Trim());

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Customer added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            ClearFields();
                            LoadCustomerData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding customer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Update existing customer
        private void UpdateCustomer()
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0 && ValidateInput())
                {
                    int customerId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["CustomerID"].Value);

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "UPDATE Customers SET Name = @Name, PhoneNumber = @Phone, Address = @Address WHERE CustomerID = @ID";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Name", textName.Text.Trim());
                            cmd.Parameters.AddWithValue("@Phone", textBox2.Text.Trim());
                            cmd.Parameters.AddWithValue("@Address", textAddress.Text.Trim());
                            cmd.Parameters.AddWithValue("@ID", customerId);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            ClearFields();
                            LoadCustomerData();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a customer to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating customer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Delete selected customer
        private void DeleteCustomer()
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this customer?",
                        "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        int customerId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["CustomerID"].Value);

                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();
                            string query = "DELETE FROM Customers WHERE CustomerID = @ID";

                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@ID", customerId);
                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Customer deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                ClearFields();
                                LoadCustomerData();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a customer to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting customer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load customer data into DataGridView
        private void LoadCustomerData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT CustomerID, Name, PhoneNumber as Phone, Address FROM Customers ORDER BY Name";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;

                    // Format the DataGridView
                    if (dataGridView1.Columns.Count > 0)
                    {
                        dataGridView1.Columns["CustomerID"].HeaderText = "Customer ID";
                        dataGridView1.Columns["CustomerID"].Width = 80;
                        dataGridView1.Columns["Name"].Width = 150;
                        dataGridView1.Columns["Phone"].Width = 120;
                        dataGridView1.Columns["Address"].Width = 200;

                        dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dataGridView1.MultiSelect = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customer data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Clear all input fields
        private void ClearFields()
        {
            textName.Clear(); // Name
            textBox2.Clear(); // Phone
            textAddress.Clear(); // Address
            textName.Focus();
        }

        // Validate user input
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(textName.Text))
            {
                MessageBox.Show("Please enter customer name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please enter phone number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textAddress.Text))
            {
                MessageBox.Show("Please enter address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textAddress.Focus();
                return false;
            }

            // Validate phone number format (basic validation)
            if (textBox2.Text.Trim().Length < 10)
            {
                MessageBox.Show("Please enter a valid phone number (at least 10 digits).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return false;
            }

            return true;
        }

        // DataGridView cell click event - populate fields when row is selected
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Rows[e.RowIndex].Cells[0].Value != null)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textName.Text = row.Cells["Name"].Value.ToString();
                textBox2.Text = row.Cells["Phone"].Value.ToString();
                textAddress.Text = row.Cells["Address"].Value.ToString();
            }
        }
    }
}
