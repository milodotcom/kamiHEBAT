using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace projectw
{
    public partial class Form2 : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\AMD\source\repos\kamiHEBAT\projectw\greenbid.mdf;Integrated Security=True;Connect Timeout=30";
        private List<WasteItem> wasteItems = new List<WasteItem>();
        private DataTable itemsDataTable = new DataTable();

        public Form2()
        {
            InitializeComponent();
            InitializeDataGridView();
            LoadCustomers();
            LoadWasteTypes();
            LoadWeightOptions();

            // Initialize items DataTable
            itemsDataTable.Columns.Add("ID", typeof(int));
            itemsDataTable.Columns.Add("Waste Type", typeof(string));
            itemsDataTable.Columns.Add("Weight (kg)", typeof(string));
            itemsDataTable.Columns.Add("Amount (RM)", typeof(string));

            // Ensure buttons are initially disabled
            buttonSubmit.Enabled = false;
            btnAddItem.Enabled = false;
        }

        private void InitializeDataGridView()
        {
            dataGridViewItems.AutoGenerateColumns = false;
            dataGridViewItems.AllowUserToAddRows = false;
            dataGridViewItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewItems.RowHeadersVisible = false;
            dataGridViewItems.ReadOnly = true;
            dataGridViewItems.DataSource = itemsDataTable;

            // Configure columns
            dataGridViewItems.Columns.Clear();
            dataGridViewItems.Columns.Add("colID", "ID");
            dataGridViewItems.Columns["colID"].DataPropertyName = "ID";
            dataGridViewItems.Columns.Add("colWasteType", "Waste Type");
            dataGridViewItems.Columns["colWasteType"].DataPropertyName = "Waste Type";
            dataGridViewItems.Columns.Add("colWeight", "Weight (kg)");
            dataGridViewItems.Columns["colWeight"].DataPropertyName = "Weight (kg)";
            dataGridViewItems.Columns.Add("colAmount", "Amount (RM)");
            dataGridViewItems.Columns["colAmount"].DataPropertyName = "Amount (RM)";
        }

        public class WasteItem
        {
            public int ItemID { get; set; }
            public string WasteType { get; set; }
            public decimal Weight { get; set; }
            public decimal Amount { get; set; }
        }

        private void LoadCustomers()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT CustomerID, CustomerName FROM Customers ORDER BY CustomerName";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    comboBoxCustomer.DataSource = dt;
                    comboBoxCustomer.DisplayMember = "CustomerName";
                    comboBoxCustomer.ValueMember = "CustomerID";
                    comboBoxCustomer.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadWasteTypes()
        {
            comboBoxType.Items.Clear();
            comboBoxType.Items.AddRange(new string[]
            {
                "Mobile Phones", "Laptops", "Tablets", "Desktop Computers",
                "Printers", "Televisions", "Batteries", "Cables", "Other Electronics"
            });
            comboBoxType.SelectedIndex = -1;
        }

        private void LoadWeightOptions()
        {
            comboBoxWeight.Items.Clear();
            for (decimal i = 0.1m; i <= 50.0m; i += 0.1m)
            {
                comboBoxWeight.Items.Add(i.ToString("0.0"));
            }
            comboBoxWeight.SelectedIndex = -1;
        }

        private void textWeight_TextChanged(object sender, EventArgs e)
        {
            // Add any logic you need when the weight text changes
            // For example, you might want to validate the input:
            if (!decimal.TryParse(textWeight.Text.Replace(" kg", ""), out _) && !string.IsNullOrEmpty(textWeight.Text))
            {
                MessageBox.Show("Please enter a valid weight", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textAmount_TextChanged(object sender, EventArgs e)
        {
            // Add any logic you need when the amount text changes
            // For example, you might want to validate the input:
            if (!decimal.TryParse(textAmount.Text.Replace("RM ", ""), out _) && !string.IsNullOrEmpty(textAmount.Text))
            {
                MessageBox.Show("Please enter a valid amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAddButtonState();
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAddButtonState();
            CalculateAmount();
        }

        private void comboBoxWeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAddButtonState();
            CalculateAmount();
        }

        private void UpdateAddButtonState()
        {
            btnAddItem.Enabled = comboBoxCustomer.SelectedIndex >= 0 &&
                               comboBoxType.SelectedIndex >= 0 &&
                               comboBoxWeight.SelectedIndex >= 0;
        }

        private void CalculateAmount()
        {
            if (comboBoxType.SelectedIndex >= 0 && comboBoxWeight.SelectedIndex >= 0)
            {
                try
                {
                    decimal weight = decimal.Parse(comboBoxWeight.SelectedItem.ToString());
                    decimal ratePerKg = GetRateForWasteType(comboBoxType.SelectedItem.ToString());
                    decimal amount = weight * ratePerKg;
                    textAmount.Text = amount.ToString("0.00");
                }
                catch
                {
                    textAmount.Text = "0.00";
                }
            }
            else
            {
                textAmount.Text = "0.00";
            }
        }

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
                default: return 0m;
            }
        }

        private bool ValidateItemInput()
        {
            if (comboBoxCustomer.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a customer", "Warning",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (comboBoxType.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a waste type", "Warning",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (comboBoxWeight.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a weight", "Warning",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (!ValidateItemInput()) return;

            try
            {
                decimal weight = decimal.Parse(comboBoxWeight.SelectedItem.ToString());
                decimal amount = decimal.Parse(textAmount.Text);

                WasteItem item = new WasteItem
                {
                    ItemID = wasteItems.Count + 1,
                    WasteType = comboBoxType.SelectedItem.ToString(),
                    Weight = weight,
                    Amount = amount
                };

                wasteItems.Add(item);

                // Add to DataTable
                itemsDataTable.Rows.Add(
                    item.ItemID,
                    item.WasteType,
                    item.Weight.ToString("0.00") + " kg",
                    "RM " + item.Amount.ToString("0.00")
                );

                UpdateTotalsDisplay();

                // Reset for next item
                comboBoxType.SelectedIndex = -1;
                comboBoxWeight.SelectedIndex = -1;
                textAmount.Text = "0.00";

                buttonSubmit.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding item: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateTotalsDisplay()
        {
            decimal totalWeight = wasteItems.Sum(i => i.Weight);
            decimal totalAmount = wasteItems.Sum(i => i.Amount);
            textWeight.Text = totalWeight.ToString("0.00") + " kg";
            textAmount.Text = "RM " + totalAmount.ToString("0.00");
        }

        public void PassDataToForm3(Form3 form3)
        {
            if (comboBoxCustomer.SelectedIndex >= 0 && wasteItems.Count > 0)
            {
                int customerId = Convert.ToInt32(comboBoxCustomer.SelectedValue);
                string customerName = comboBoxCustomer.Text;
                decimal totalWeight = wasteItems.Sum(i => i.Weight);
                decimal totalAmount = wasteItems.Sum(i => i.Amount);

                form3.SetCustomerInfo(customerName, customerId);
                form3.SetWasteItems(wasteItems);
                form3.UpdateTotals(totalWeight, totalAmount);
            }
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (wasteItems.Count == 0)
            {
                MessageBox.Show("Please add at least one waste item", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                            int customerId = Convert.ToInt32(comboBoxCustomer.SelectedValue);
                            decimal totalWeight = wasteItems.Sum(i => i.Weight);
                            decimal totalAmount = wasteItems.Sum(i => i.Amount);

                            foreach (var item in wasteItems)
                            {
                                string query = @"INSERT INTO ItemDetails 
                                               (WasteType, Weight, TotalWeight, TotalAmount, CustomerID) 
                                               VALUES (@WasteType, @Weight, @TotalWeight, @TotalAmount, @CustomerID)";

                                using (SqlCommand cmd = new SqlCommand(query, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@WasteType", item.WasteType);
                                    cmd.Parameters.AddWithValue("@Weight", item.Weight);
                                    cmd.Parameters.AddWithValue("@TotalWeight", totalWeight);
                                    cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                                    cmd.Parameters.AddWithValue("@CustomerID", customerId);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            MessageBox.Show("Transaction saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Pass data to Form3 and show it
                            Form3 pickupForm = new Form3();
                            PassDataToForm3(pickupForm);
                            pickupForm.Show();
                            this.Hide();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Error saving transaction: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database connection error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (wasteItems.Count > 0)
            {
                Form3 pickupForm = new Form3();
                PassDataToForm3(pickupForm);
                pickupForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please add at least one waste item before proceeding", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 customerForm = new Form1();
            customerForm.Show();
            this.Hide();
        }

        private void ClearForm()
        {
            wasteItems.Clear();
            itemsDataTable.Rows.Clear();
            comboBoxCustomer.SelectedIndex = -1;
            comboBoxType.SelectedIndex = -1;
            comboBoxWeight.SelectedIndex = -1;
            textWeight.Text = "";
            textAmount.Text = "";
            buttonSubmit.Enabled = false;
        }

        // Empty event handlers to maintain designer compatibility
        private void Form2_Load(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void pictureBox2_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void textTotalWeight_TextChanged(object sender, EventArgs e) { }
        private void textTotalAmount_TextChanged(object sender, EventArgs e) { }
        private void dataGridViewItems_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void labelItem_Click(object sender, EventArgs e) { }
    }
}