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
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\AMD\source\repos\kamiHEBAT\projectw\greenbid.mdf;Integrated Security=True;Connect Timeout=30";

        // List to store items before submitting
        private List<WasteItem> wasteItems = new List<WasteItem>();

        public Form2()
        {
            InitializeComponent();
            InitializeDataGridView();
            LoadCustomers();
            LoadWasteTypes();
            LoadWeightOptions();
        }

        // Initialize DataGridView
        private void InitializeDataGridView()
        {
            // Assuming your DataGridView is named dataGridViewItems
            if (dataGridViewItems != null)
            {
                dataGridViewItems.AutoGenerateColumns = false;
                dataGridViewItems.AllowUserToAddRows = false;
                dataGridViewItems.ReadOnly = true;
                dataGridViewItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // Clear existing columns
                dataGridViewItems.Columns.Clear();

                // Add columns to match your database structure
                dataGridViewItems.Columns.Add("ItemID", "ItemID");
                dataGridViewItems.Columns.Add("WasteType", "WasteType");
                dataGridViewItems.Columns.Add("Weight", "Weight");
                dataGridViewItems.Columns.Add("TotalWeight", "TotalWeight");
                dataGridViewItems.Columns.Add("TotalAmount", "TotalAmount");
                dataGridViewItems.Columns.Add("CustomerID", "CustomerID");

                // Set column widths
                dataGridViewItems.Columns["ItemID"].Width = 70;
                dataGridViewItems.Columns["WasteType"].Width = 120;
                dataGridViewItems.Columns["Weight"].Width = 80;
                dataGridViewItems.Columns["TotalWeight"].Width = 100;
                dataGridViewItems.Columns["TotalAmount"].Width = 100;
                dataGridViewItems.Columns["CustomerID"].Width = 90;
            }
        }

        // Class to represent a waste item
        public class WasteItem
        {
            public string WasteType { get; set; }
            public decimal Weight { get; set; }
            public decimal Amount { get; set; }
        }

        // Load customers into ComboBox
        private void LoadCustomers()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT CustomerID, CustomerName FROM Customers ORDER BY CustomerName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            comboBoxCustomer.Items.Clear();
                            comboBoxCustomer.Items.Add("Select Customer");

                            while (reader.Read())
                            {
                                comboBoxCustomer.Items.Add(new CustomerItem
                                {
                                    CustomerID = Convert.ToInt32(reader["CustomerID"]),
                                    CustomerName = reader["CustomerName"].ToString()
                                });
                            }
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

        // This is for the form's Load event
        private void Form2_Load(object sender, EventArgs e)
        {
            //
        }

        // This is for the DataGridView CellContentClick event
        private void dataGridViewItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // TODO: Add your DataGridView cell click handling logic here
        }

        // Load waste types into ComboBox
        private void LoadWasteTypes()
        {
            try
            {
                comboBoxType.Items.Clear();
                comboBoxType.Items.Add("Select Waste Type");
                comboBoxType.Items.Add("Mobile Phones");
                comboBoxType.Items.Add("Laptops");
                comboBoxType.Items.Add("Tablets");
                comboBoxType.Items.Add("Desktop Computers");
                comboBoxType.Items.Add("Printers");
                comboBoxType.Items.Add("Televisions");
                comboBoxType.Items.Add("Batteries");
                comboBoxType.Items.Add("Cables");
                comboBoxType.Items.Add("Other Electronics");

                comboBoxType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading waste types: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load weight options into ComboBox
        private void LoadWeightOptions()
        {
            try
            {
                comboBoxWeight.Items.Clear();
                comboBoxWeight.Items.Add("Select Weight");

                // Add weight options from 0.1 to 50 kg
                for (decimal i = 0.1m; i <= 50.0m; i += 0.1m)
                {
                    comboBoxWeight.Items.Add(i.ToString("0.0") + " kg");
                }

                comboBoxWeight.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading weight options: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Customer item class for ComboBox
        public class CustomerItem
        {
            public int CustomerID { get; set; }
            public string CustomerName { get; set; }

            public override string ToString()
            {
                return CustomerName;
            }
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
            // Enable/disable item selection based on customer selection
            bool customerSelected = comboBoxCustomer.SelectedItem != null;

            comboBoxType.Enabled = customerSelected;
            comboBoxWeight.Enabled = customerSelected;
            btnAddItem.Enabled = customerSelected &&
                                 comboBoxType.SelectedIndex >= 0 &&
                                 comboBoxWeight.SelectedIndex >= 0;
        }

        private void dataGridViewItems_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAddItemButton();
            CalculateAmount();
        }

        private void comboBoxWeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAddItemButton();
            CalculateAmount();
        }

        // Update Add Item button state
        private void UpdateAddItemButton()
        {
            btnAddItem.Enabled = comboBoxCustomer.SelectedIndex > 0 &&
                                comboBoxType.SelectedIndex > 0 &&
                                comboBoxWeight.SelectedIndex > 0;
        }

        // Calculate amount based on weight and waste type
        private void CalculateAmount()
        {
            if (comboBoxType.SelectedIndex > 0 && comboBoxWeight.SelectedIndex > 0)
            {
                try
                {
                    // Extract weight value from selection
                    string weightText = comboBoxWeight.SelectedItem.ToString();
                    decimal weight = decimal.Parse(weightText.Replace(" kg", ""));

                    // Define rates per kg for different waste types (you can adjust these)
                    decimal ratePerKg = GetRateForWasteType(comboBoxType.SelectedItem.ToString());

                    decimal amount = weight * ratePerKg;

                    // Update the amount textbox
                    if (textAmount != null)
                        textAmount.Text = "RM " + amount.ToString("0.00");
                }
                catch (Exception ex)
                {
                    if (textAmount != null)
                        textAmount.Text = "RM 0.00";
                }
            }
            else
            {
                if (textAmount != null)
                    textAmount.Text = "RM 0.00";
            }
        }

        // Get rate per kg for different waste types
        private decimal GetRateForWasteType(string wasteType)
        {
            switch (wasteType)
            {
                case "Mobile Phones": return 15.00m;
                case "Laptops": return 12.00m;
                case "Tablets": return 10.00m;
                case "Desktop Computers": return 8.00m;
                case "Printers": return 5.00m;
                case "Televisions": return 6.00m;
                case "Batteries": return 20.00m;
                case "Cables": return 3.00m;
                case "Other Electronics": return 4.00m;
                default: return 5.00m;
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxType.SelectedIndex > 0 && comboBoxWeight.SelectedIndex > 0 && comboBoxCustomer.SelectedIndex > 0)
                {
                    // Extract weight value
                    string weightText = comboBoxWeight.SelectedItem.ToString();
                    decimal weight = decimal.Parse(weightText.Replace(" kg", ""));
                    decimal ratePerKg = GetRateForWasteType(comboBoxType.SelectedItem.ToString());
                    decimal amount = weight * ratePerKg;

                    CustomerItem selectedCustomer = (CustomerItem)comboBoxCustomer.SelectedItem;

                    // Create waste item
                    WasteItem item = new WasteItem
                    {
                        WasteType = comboBoxType.SelectedItem.ToString(),
                        Weight = weight,
                        Amount = amount
                    };

                    // Add to list
                    wasteItems.Add(item);

                    // Calculate totals
                    decimal totalWeight = wasteItems.Sum(i => i.Weight);
                    decimal totalAmount = wasteItems.Sum(i => i.Amount);

                    // Add to DataGridView
                    if (dataGridViewItems != null)
                    {
                        int rowIndex = dataGridViewItems.Rows.Add();
                        DataGridViewRow row = dataGridViewItems.Rows[rowIndex];

                        row.Cells["ItemID"].Value = wasteItems.Count; // Temporary ID
                        row.Cells["WasteType"].Value = item.WasteType;
                        row.Cells["Weight"].Value = item.Weight.ToString("0.00") + " kg";
                        row.Cells["TotalWeight"].Value = totalWeight.ToString("0.00") + " kg";
                        row.Cells["TotalAmount"].Value = "RM " + totalAmount.ToString("0.00");
                        row.Cells["CustomerID"].Value = selectedCustomer.CustomerID;
                    }

                    // Update totals in textboxes
                    UpdateTotals();

                    // Reset item selection
                    comboBoxType.SelectedIndex = 0;
                    comboBoxWeight.SelectedIndex = 0;

                    // Enable submit button if items exist
                    buttonSubmit.Enabled = wasteItems.Count > 0;

                    MessageBox.Show("Item added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Please select customer, waste type, and weight.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Update total weight and amount
        private void UpdateTotals()
        {
            decimal totalWeight = wasteItems.Sum(item => item.Weight);
            decimal totalAmount = wasteItems.Sum(item => item.Amount);

            // Update the total weight textbox
            if (textWeight != null)
                textWeight.Text = totalWeight.ToString("0.00") + " kg";

            // Update the total amount textbox
            if (textAmount != null)
                textAmount.Text = "RM " + totalAmount.ToString("0.00");
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            SaveTransaction();
        }

        // Save transaction to database
        private void SaveTransaction()
        {
            if (comboBoxCustomer.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select a customer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (wasteItems.Count == 0)
            {
                MessageBox.Show("Please add at least one item.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            CustomerItem selectedCustomer = (CustomerItem)comboBoxCustomer.SelectedItem;
                            decimal totalWeight = wasteItems.Sum(item => item.Weight);
                            decimal totalAmount = wasteItems.Sum(item => item.Amount);

                            // Insert each waste item
                            foreach (WasteItem item in wasteItems)
                            {
                                string insertQuery = @"INSERT INTO WasteItems (WasteType, Weight, TotalWeight, TotalAmount, CustomerID) 
                                                     VALUES (@WasteType, @Weight, @TotalWeight, @TotalAmount, @CustomerID)";

                                using (SqlCommand command = new SqlCommand(insertQuery, connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@WasteType", item.WasteType);
                                    command.Parameters.AddWithValue("@Weight", item.Weight);
                                    command.Parameters.AddWithValue("@TotalWeight", totalWeight);
                                    command.Parameters.AddWithValue("@TotalAmount", totalAmount);
                                    command.Parameters.AddWithValue("@CustomerID", selectedCustomer.CustomerID);

                                    command.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();

                            MessageBox.Show("Transaction saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Clear form
                            ClearForm();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving transaction: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Clear form after successful submission
        private void ClearForm()
        {
            comboBoxCustomer.SelectedIndex = 0;
            comboBoxType.SelectedIndex = 0;
            comboBoxWeight.SelectedIndex = 0;

            // Clear DataGridView
            if (dataGridViewItems != null)
                dataGridViewItems.Rows.Clear();

            // Clear textboxes
            if (textWeight != null)
                textWeight.Text = "";
            if (textAmount != null)
                textAmount.Text = "";

            wasteItems.Clear();
            buttonSubmit.Enabled = false;
        }

        private void textWeight_TextChanged(object sender, EventArgs e)
        {
        }

        private void textAmount_TextChanged(object sender, EventArgs e)
        {
        }

        // Add event handler for DataGridView selection changed
        private void dataGridViewItems_SelectionChanged(object sender, EventArgs e)
        {
            // You can add functionality here if needed when user selects a row
        }

        // Add event handler for removing selected item (optional)
        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewItems != null && dataGridViewItems.SelectedRows.Count > 0)
                {
                    int selectedIndex = dataGridViewItems.SelectedRows[0].Index;

                    if (selectedIndex >= 0 && selectedIndex < wasteItems.Count)
                    {
                        // Remove from list and DataGridView
                        wasteItems.RemoveAt(selectedIndex);
                        dataGridViewItems.Rows.RemoveAt(selectedIndex);

                        // Update totals
                        UpdateTotals();

                        // Update all rows in DataGridView with new totals
                        RefreshDataGridView();

                        // Disable submit if no items
                        buttonSubmit.Enabled = wasteItems.Count > 0;

                        MessageBox.Show("Item removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please select an item to remove.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error removing item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Refresh DataGridView with updated totals
        private void RefreshDataGridView()
        {
            if (dataGridViewItems != null && wasteItems.Count > 0)
            {
                CustomerItem selectedCustomer = (CustomerItem)comboBoxCustomer.SelectedItem;
                decimal totalWeight = wasteItems.Sum(item => item.Weight);
                decimal totalAmount = wasteItems.Sum(item => item.Amount);

                for (int i = 0; i < dataGridViewItems.Rows.Count; i++)
                {
                    dataGridViewItems.Rows[i].Cells["ItemID"].Value = i + 1;
                    dataGridViewItems.Rows[i].Cells["TotalWeight"].Value = totalWeight.ToString("0.00") + " kg";
                    dataGridViewItems.Rows[i].Cells["TotalAmount"].Value = "RM " + totalAmount.ToString("0.00");
                    dataGridViewItems.Rows[i].Cells["CustomerID"].Value = selectedCustomer?.CustomerID ?? 0;
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 customerForm = new Form1();
            customerForm.Show();
            this.Hide();
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            SaveTransaction();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            // Check if there's a valid transaction before proceeding
            if (wasteItems.Count > 0 && comboBoxCustomer.SelectedIndex > 0)
            {
                Form3 pickupForm = new Form3();
                pickupForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please complete the transaction before proceeding.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}