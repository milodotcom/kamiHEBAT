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

namespace projectw
{
    public partial class Form2 : Form
    {

        private string connectionString = @"Data Source=YOUR_SERVER_NAME;Initial Catalog=GreenBinDB;Integrated Security=True";

        private List<DropOffItem> itemsList = new List<DropOffItem>();
        private decimal totalWeight = 0;
        private decimal totalAmount = 0;


        public Form2()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            LoadCustomers();
            LoadWasteTypes();
            LoadWeightOptions();
            ClearItemFields();
            UpdateTotals();
        }

        private void LoadCustomers()
        {
            try
            {
                comboBoxCustomer.Items.Clear();
                comboBoxCustomer.Items.Add("Select Customer");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT CustomerID, Name FROM Customers ORDER BY Name";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CustomerItem customer = new CustomerItem
                            {
                                CustomerID = Convert.ToInt32(reader["CustomerID"]),
                                Name = reader["Name"].ToString()
                            };
                            comboBoxCustomer.Items.Add(customer);
                        }
                    }
                }
                comboBoxCustomer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load waste types
        private void LoadWasteTypes()
        {
            comboBoxType.Items.Clear();
            comboBoxType.Items.Add("Select Waste Type");
            comboBoxType.Items.Add("Mobile Phones");
            comboBoxType.Items.Add("Laptops");
            comboBoxType.Items.Add("Desktop Computers");
            comboBoxType.Items.Add("Tablets");
            comboBoxType.Items.Add("Televisions");
            comboBoxType.Items.Add("Printers");
            comboBoxType.Items.Add("Keyboards & Mice");
            comboBoxType.Items.Add("Cables & Chargers");
            comboBoxType.Items.Add("Batteries");
            comboBoxType.Items.Add("Circuit Boards");
            comboBoxType.Items.Add("Other Electronics");
            comboBoxType.SelectedIndex = 0;
        }

        // Load weight options
        private void LoadWeightOptions()
        {
            comboBoxWeight.Items.Clear();
            comboBoxWeight.Items.Add("Select Weight");
            comboBoxWeight.Items.Add("0.1 kg");
            comboBoxWeight.Items.Add("0.5 kg");
            comboBoxWeight.Items.Add("1.0 kg");
            comboBoxWeight.Items.Add("2.0 kg");
            comboBoxWeight.Items.Add("5.0 kg");
            comboBoxWeight.Items.Add("10.0 kg");
            comboBoxWeight.Items.Add("Custom");
            comboBoxWeight.SelectedIndex = 0;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void labelItem_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBoxItems_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxWeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxWeight.SelectedItem.ToString() == "Custom")
            {
                // Enable custom weight input (you can add a TextBox for this)
                string customWeight = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter custom weight (kg):", "Custom Weight", "1.0");

                if (!string.IsNullOrEmpty(customWeight))
                {
                    comboBoxWeight.Items[comboBoxWeight.Items.Count - 1] = $"Custom: {customWeight} kg";
                    comboBoxWeight.SelectedIndex = comboBoxWeight.Items.Count - 1;
                }
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (ValidateItemInput())
            {
                try
                {
                    decimal weight = GetSelectedWeight();
                    decimal amount = CalculateAmount(comboBoxType.SelectedItem.ToString(), weight);

                    DropOffItem item = new DropOffItem
                    {
                        WasteType = comboBoxType.SelectedItem.ToString(),
                        Weight = weight,
                        Amount = amount
                    };

                    itemsList.Add(item);
                    UpdateItemsList();
                    UpdateTotals();
                    ClearItemFields();

                    MessageBox.Show("Item added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Get selected weight value
        private decimal GetSelectedWeight()
        {
            string weightText = comboBoxWeight.SelectedItem.ToString();

            if (weightText.Contains("Custom:"))
            {
                string[] parts = weightText.Split(':');
                string weightPart = parts[1].Replace("kg", "").Trim();
                return Convert.ToDecimal(weightPart);
            }
            else
            {
                return Convert.ToDecimal(weightText.Replace("kg", "").Trim());
            }
        }

        private decimal CalculateAmount(string wasteType, decimal weight)
        {
            decimal pricePerKg = 0;

            // Sample pricing structure
            switch (wasteType)
            {
                case "Mobile Phones":
                    pricePerKg = 50.00m;
                    break;
                case "Laptops":
                    pricePerKg = 30.00m;
                    break;
                case "Desktop Computers":
                    pricePerKg = 25.00m;
                    break;
                case "Tablets":
                    pricePerKg = 40.00m;
                    break;
                case "Televisions":
                    pricePerKg = 15.00m;
                    break;
                case "Printers":
                    pricePerKg = 20.00m;
                    break;
                case "Keyboards & Mice":
                    pricePerKg = 10.00m;
                    break;
                case "Cables & Chargers":
                    pricePerKg = 5.00m;
                    break;
                case "Batteries":
                    pricePerKg = 8.00m;
                    break;
                case "Circuit Boards":
                    pricePerKg = 100.00m;
                    break;
                default:
                    pricePerKg = 10.00m;
                    break;
            }

            return weight * pricePerKg;
        }

        private void UpdateItemsList()
        {
            // Create DataTable for DataGridView
            DataTable dt = new DataTable();
            dt.Columns.Add("Item No", typeof(int));
            dt.Columns.Add("Waste Type", typeof(string));
            dt.Columns.Add("Weight (kg)", typeof(decimal));
            dt.Columns.Add("Rate (RM/kg)", typeof(decimal));
            dt.Columns.Add("Amount (RM)", typeof(decimal));

            // Add items to DataTable
            for (int i = 0; i < itemsList.Count; i++)
            {
                var item = itemsList[i];
                decimal rate = item.Weight > 0 ? item.Amount / item.Weight : 0;

                dt.Rows.Add(
                    i + 1,
                    item.WasteType,
                    item.Weight,
                    rate,
                    item.Amount
                );
            }

            dataGridViewItems.DataSource = dt;

            // Format DataGridView columns
            if (dataGridViewItems.Columns.Count > 0)
            {
                dataGridViewItems.Columns["Item No"].Width = 60;
                dataGridViewItems.Columns["Waste Type"].Width = 150;
                dataGridViewItems.Columns["Weight (kg)"].Width = 80;
                dataGridViewItems.Columns["Rate (RM/kg)"].Width = 90;
                dataGridViewItems.Columns["Amount (RM)"].Width = 90;

                // Format decimal columns
                dataGridViewItems.Columns["Weight (kg)"].DefaultCellStyle.Format = "F2";
                dataGridViewItems.Columns["Rate (RM/kg)"].DefaultCellStyle.Format = "F2";
                dataGridViewItems.Columns["Amount (RM)"].DefaultCellStyle.Format = "F2";

                // Align numeric columns to right
                dataGridViewItems.Columns["Weight (kg)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridViewItems.Columns["Rate (RM/kg)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridViewItems.Columns["Amount (RM)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dataGridViewItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewItems.MultiSelect = false;
                dataGridViewItems.ReadOnly = true;
            }
        }

        // Update total weight and amount
        private void UpdateTotals()
        {
            totalWeight = itemsList.Sum(item => item.Weight);
            totalAmount = itemsList.Sum(item => item.Amount);

            textWeight.Text = totalWeight.ToString("F2");
            textAmount.Text = totalAmount.ToString("F2");
        }

        // Clear item input fields
        private void ClearItemFields()
        {
            comboBoxType.SelectedIndex = 0;
            comboBoxWeight.SelectedIndex = 0;
        }

        // Validate item input
        private bool ValidateItemInput()
        {
            if (comboBoxCustomer.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select a customer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (comboBoxType.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select a waste type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (comboBoxWeight.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select a weight.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (comboBoxCustomer.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select a customer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (itemsList.Count == 0)
            {
                MessageBox.Show("Please add at least one item.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SaveTransaction();
                MessageBox.Show("Transaction submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error submitting transaction: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Save transaction to database
        private void SaveTransaction()
        {
            CustomerItem selectedCustomer = (CustomerItem)comboBoxCustomer.SelectedItem;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Insert main transaction
                        string transactionQuery = @"INSERT INTO DropOffTransactions 
                                                   (CustomerID, TransactionDate, TotalWeight, TotalAmount, Status) 
                                                   VALUES (@CustomerID, @Date, @Weight, @Amount, @Status);
                                                   SELECT SCOPE_IDENTITY();";

                        int transactionId;
                        using (SqlCommand cmd = new SqlCommand(transactionQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@CustomerID", selectedCustomer.CustomerID);
                            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                            cmd.Parameters.AddWithValue("@Weight", totalWeight);
                            cmd.Parameters.AddWithValue("@Amount", totalAmount);
                            cmd.Parameters.AddWithValue("@Status", "Pending");

                            transactionId = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // Insert transaction items
                        foreach (var item in itemsList)
                        {
                            string itemQuery = @"INSERT INTO DropOffItems 
                                               (TransactionID, WasteType, Weight, Amount) 
                                               VALUES (@TransactionID, @WasteType, @Weight, @Amount)";

                            using (SqlCommand cmd = new SqlCommand(itemQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@TransactionID", transactionId);
                                cmd.Parameters.AddWithValue("@WasteType", item.WasteType);
                                cmd.Parameters.AddWithValue("@Weight", item.Weight);
                                cmd.Parameters.AddWithValue("@Amount", item.Amount);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private void ClearForm()
        {
            comboBoxCustomer.SelectedIndex = 0;
            itemsList.Clear();
            ClearItemFields();
            UpdateTotals();
        }

        private void textWeight_TextChanged(object sender, EventArgs e)
        {

        }

        private void textAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 customerForm = new Form1();
            customerForm.Show();
            this.Hide();
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Form3 pickupForm = new Form3();
            pickupForm.Show();
            this.Hide();
        }
    }
}
