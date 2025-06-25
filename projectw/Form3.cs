using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace projectw
{
    public partial class Form3 : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\AMD\source\repos\kamiHEBAT\projectw\greenbid.mdf;Integrated Security=True;Connect Timeout=30";
        private int customerId;
        private string customerName;
        private List<Form2.WasteItem> wasteItems;
        private decimal totalWeight;
        private decimal totalAmount;

        public Form3()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Set minimum date to today
            dateTimePicker1.MinDate = DateTime.Today;

            // Initialize status options
            comboBoxCustomer.Items.AddRange(new string[] { "Scheduled", "Pending", "Completed", "Cancelled" });
            comboBoxCustomer.SelectedIndex = 0;

            // Disable controls until data is loaded
            textTotalWeight.Enabled = false;
            textTotalAmount.Enabled = false;
            comboBoxCustomer.Enabled = false;
        }

        public void SetCustomerInfo(string name, int id)
        {
            customerName = name;
            customerId = id;
            comboBoxCustomer.Text = name;
        }

        public void SetWasteItems(List<Form2.WasteItem> items)
        {
            wasteItems = items;
            UpdateDataGridView();
        }

        public void UpdateTotals(decimal weight, decimal amount)
        {
            totalWeight = weight;
            totalAmount = amount;
            textTotalWeight.Text = weight.ToString("0.00");
            textTotalAmount.Text = "RM " + amount.ToString("0.00");
        }

        private void UpdateDataGridView()
        {
            dataGridView1.Rows.Clear();
            if (wasteItems != null)
            {
                foreach (var item in wasteItems)
                {
                    dataGridView1.Rows.Add(
                        item.ItemID,
                        item.WasteType,
                        item.Weight.ToString("0.00") + " kg",
                        "RM " + item.Amount.ToString("0.00")
                    );
                }
            }
        }

        private bool IsDateAvailable(DateTime selectedDate)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM PickupSchedules WHERE CONVERT(DATE, PickupDate) = @PickupDate";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@PickupDate", selectedDate.Date);
                    int count = (int)cmd.ExecuteScalar();
                    return count == 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking date availability: " + ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnCheckStatus_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value < DateTime.Today)
            {
                MessageBox.Show("Please select a future date",
                              "Warning",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                return;
            }

            bool isAvailable = IsDateAvailable(dateTimePicker1.Value);
            MessageBox.Show(isAvailable ? "Date is available for pickup!" : "Date is not available. Please choose another date.",
                          isAvailable ? "Available" : "Not Available",
                          MessageBoxButtons.OK,
                          isAvailable ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
        }

        private void btnSchedulePickup_Click(object sender, EventArgs e)
        {
            if (customerId == 0 || wasteItems == null || wasteItems.Count == 0)
            {
                MessageBox.Show("No waste items to schedule pickup for",
                              "Warning",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                return;
            }

            if (!IsDateAvailable(dateTimePicker1.Value))
            {
                MessageBox.Show("Selected date is not available. Please check availability first.",
                              "Warning",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"INSERT INTO PickupSchedules 
                                   (CustomerID, PickupDate, TotalWeight, TotalAmount, Status) 
                                   VALUES (@CustomerID, @PickupDate, @TotalWeight, @TotalAmount, @Status);
                                   SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerId);
                        cmd.Parameters.AddWithValue("@PickupDate", dateTimePicker1.Value);
                        cmd.Parameters.AddWithValue("@TotalWeight", totalWeight);
                        cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                        cmd.Parameters.AddWithValue("@Status", comboBoxCustomer.SelectedItem.ToString());

                        int pickupId = Convert.ToInt32(cmd.ExecuteScalar());
                        MessageBox.Show($"Pickup scheduled successfully! Pickup ID: {pickupId}",
                                      "Success",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Information);

                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error scheduling pickup: " + ex.Message,
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form2 dropOffForm = new Form2();
            dropOffForm.Show();
            this.Hide();
        }

        // Empty event handlers to prevent designer errors
        private void Form3_Load(object sender, EventArgs e) { }
        private void comboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e) { }
        private void dataGridViewItems_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}