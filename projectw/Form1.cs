using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace projectw
{
    public partial class Form1 : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\AMD\source\repos\kamiHEBAT\projectw\greenbid.mdf;Integrated Security=True;Connect Timeout=30";

        public Form1()
        {
            InitializeComponent();
            LoadCustomerData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ClearFields();
            LoadCustomerData();
        }

        private void button1_Click(object sender, EventArgs e) => AddCustomer();
        private void button3_Click(object sender, EventArgs e) => UpdateCustomer();
        private void btnDel_Click(object sender, EventArgs e) => DeleteCustomer();
        private void btnClear_Click(object sender, EventArgs e) => ClearFields();

        private void btnNext_Click(object sender, EventArgs e)
        {
            Form2 dropOffForm = new Form2();
            dropOffForm.Show();
            this.Hide();
        }

        private void AddCustomer()
        {
            try
            {
                if (ValidateInput())
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "INSERT INTO Customers (CustomerName, PhoneNumber, Address) VALUES (@CustomerName, @PhoneNumber, @Address)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@CustomerName", textName.Text.Trim());
                            cmd.Parameters.AddWithValue("@PhoneNumber", textBox2.Text.Trim());
                            cmd.Parameters.AddWithValue("@Address", textAddress.Text.Trim());
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Customer added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                    LoadCustomerData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding customer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCustomer()
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Get the selected row from the data source
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                    if (selectedRow.DataBoundItem != null)
                    {
                        DataRowView rowView = (DataRowView)selectedRow.DataBoundItem;

                        // Verify CustomerID exists in the data
                        if (rowView.Row.Table.Columns.Contains("CustomerID"))
                        {
                            int customerId = Convert.ToInt32(rowView["CustomerID"]);

                            // Validate input fields
                            if (ValidateInput())
                            {
                                using (SqlConnection conn = new SqlConnection(connectionString))
                                {
                                    conn.Open();
                                    string query = @"UPDATE Customers 
                                           SET CustomerName = @CustomerName, 
                                               PhoneNumber = @PhoneNumber, 
                                               Address = @Address 
                                           WHERE CustomerID = @CustomerID";

                                    using (SqlCommand cmd = new SqlCommand(query, conn))
                                    {
                                        cmd.Parameters.AddWithValue("@CustomerName", textName.Text.Trim());
                                        cmd.Parameters.AddWithValue("@PhoneNumber", textBox2.Text.Trim());
                                        cmd.Parameters.AddWithValue("@Address", textAddress.Text.Trim());
                                        cmd.Parameters.AddWithValue("@CustomerID", customerId);

                                        int rowsAffected = cmd.ExecuteNonQuery();

                                        if (rowsAffected > 0)
                                        {
                                            MessageBox.Show("Customer updated successfully!", "Success",
                                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            ClearFields();
                                            LoadCustomerData();
                                        }
                                        else
                                        {
                                            MessageBox.Show("No customer was updated.", "Warning",
                                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("CustomerID column not found in data.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a customer to update.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating customer: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteCustomer()
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                    // Check if CustomerID exists in the data source (more reliable than checking the grid columns)
                    if (selectedRow.DataBoundItem != null)
                    {
                        DataRowView rowView = (DataRowView)selectedRow.DataBoundItem;
                        if (rowView.Row.Table.Columns.Contains("CustomerID"))
                        {
                            int customerId = Convert.ToInt32(rowView["CustomerID"]);

                            DialogResult result = MessageBox.Show("Are you sure you want to delete this customer?",
                                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                using (SqlConnection conn = new SqlConnection(connectionString))
                                {
                                    conn.Open();
                                    string query = "DELETE FROM Customers WHERE CustomerID = @CustomerID";
                                    using (SqlCommand cmd = new SqlCommand(query, conn))
                                    {
                                        cmd.Parameters.AddWithValue("@CustomerID", customerId);
                                        cmd.ExecuteNonQuery();
                                    }
                                }

                                MessageBox.Show("Customer deleted successfully!", "Success",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ClearFields();
                                LoadCustomerData();
                            }
                        }
                        else
                        {
                            MessageBox.Show("CustomerID column not found in data.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a customer to delete.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting customer: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadCustomerData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT CustomerID, CustomerName, PhoneNumber, Address FROM Customers ORDER BY CustomerName";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.AutoGenerateColumns = true; // Ensure columns match DB
                    dataGridView1.DataSource = dt;

                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1.MultiSelect = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customer data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ClearFields()
        {
            textName.Clear();
            textBox2.Clear();
            textAddress.Clear();
            textName.Focus();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(textName.Text))
            {
                MessageBox.Show("Please enter customer name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please enter phone number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(textAddress.Text))
            {
                MessageBox.Show("Please enter address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (textBox2.Text.Trim().Length < 10)
            {
                MessageBox.Show("Please enter a valid phone number (at least 10 digits).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textName.Text = row.Cells["CustomerName"].Value?.ToString() ?? "";
                textBox2.Text = row.Cells["PhoneNumber"].Value?.ToString() ?? "";
                textAddress.Text = row.Cells["Address"].Value?.ToString() ?? "";
            }
        }



        // Unused designer events
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void textName_TextChanged(object sender, EventArgs e) { }
        private void textAddress_TextChanged(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) { }
    }
}
