namespace projectw
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.labelCustomer = new System.Windows.Forms.Label();
            this.comboBoxCustomer = new System.Windows.Forms.ComboBox();
            this.customersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.greenbidDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.greenbidDataSet = new projectw.greenbidDataSet();
            this.labelItem = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxWeight = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textWeight = new System.Windows.Forms.TextBox();
            this.textAmount = new System.Windows.Forms.TextBox();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.dataGridViewItems = new System.Windows.Forms.DataGridView();
            this.itemIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wasteTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.weightDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalWeightDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalAmountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customerIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemDetailsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.greenbidDataSet1 = new projectw.greenbidDataSet1();
            this.itemDetailsTableAdapter = new projectw.greenbidDataSet1TableAdapters.ItemDetailsTableAdapter();
            this.customersTableAdapter = new projectw.greenbidDataSetTableAdapters.CustomersTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.customersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenbidDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenbidDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemDetailsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenbidDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(419, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "E-WASTE DROP-OFF ENTRY";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelCustomer
            // 
            this.labelCustomer.AutoSize = true;
            this.labelCustomer.BackColor = System.Drawing.Color.Transparent;
            this.labelCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCustomer.Location = new System.Drawing.Point(15, 84);
            this.labelCustomer.Name = "labelCustomer";
            this.labelCustomer.Size = new System.Drawing.Size(102, 20);
            this.labelCustomer.TabIndex = 3;
            this.labelCustomer.Text = "Customer: ";
            // 
            // comboBoxCustomer
            // 
            this.comboBoxCustomer.DataSource = this.customersBindingSource;
            this.comboBoxCustomer.DisplayMember = "CustomerName";
            this.comboBoxCustomer.FormattingEnabled = true;
            this.comboBoxCustomer.Location = new System.Drawing.Point(123, 80);
            this.comboBoxCustomer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxCustomer.Name = "comboBoxCustomer";
            this.comboBoxCustomer.Size = new System.Drawing.Size(168, 24);
            this.comboBoxCustomer.TabIndex = 4;
            this.comboBoxCustomer.ValueMember = "CustomerName";
            this.comboBoxCustomer.SelectedIndexChanged += new System.EventHandler(this.comboBoxCustomer_SelectedIndexChanged);
            // 
            // customersBindingSource
            // 
            this.customersBindingSource.DataMember = "Customers";
            this.customersBindingSource.DataSource = this.greenbidDataSetBindingSource;
            // 
            // greenbidDataSetBindingSource
            // 
            this.greenbidDataSetBindingSource.DataSource = this.greenbidDataSet;
            this.greenbidDataSetBindingSource.Position = 0;
            // 
            // greenbidDataSet
            // 
            this.greenbidDataSet.DataSetName = "greenbidDataSet";
            this.greenbidDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // labelItem
            // 
            this.labelItem.AutoSize = true;
            this.labelItem.BackColor = System.Drawing.Color.Transparent;
            this.labelItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItem.Location = new System.Drawing.Point(355, 54);
            this.labelItem.Name = "labelItem";
            this.labelItem.Size = new System.Drawing.Size(138, 25);
            this.labelItem.TabIndex = 5;
            this.labelItem.Text = "Item Details: ";
            this.labelItem.Click += new System.EventHandler(this.labelItem_Click);
            // 
            // comboBoxType
            // 
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "Laptop",
            "Tablet",
            "Handphone",
            "Handheld Console",
            "Headset",
            "Mouse",
            "Keyboard",
            "Controller"});
            this.comboBoxType.Location = new System.Drawing.Point(513, 85);
            this.comboBoxType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(168, 24);
            this.comboBoxType.TabIndex = 7;
            this.comboBoxType.Text = "Select Waste Type";
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.comboBoxType_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(356, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Waste Type: ";
            // 
            // comboBoxWeight
            // 
            this.comboBoxWeight.FormattingEnabled = true;
            this.comboBoxWeight.Location = new System.Drawing.Point(513, 118);
            this.comboBoxWeight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxWeight.Name = "comboBoxWeight";
            this.comboBoxWeight.Size = new System.Drawing.Size(168, 24);
            this.comboBoxWeight.TabIndex = 9;
            this.comboBoxWeight.Text = "Select Weight";
            this.comboBoxWeight.SelectedIndexChanged += new System.EventHandler(this.comboBoxWeight_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(356, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Weight (kg): ";
            // 
            // btnAddItem
            // 
            this.btnAddItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddItem.Location = new System.Drawing.Point(892, 169);
            this.btnAddItem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(107, 23);
            this.btnAddItem.TabIndex = 10;
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(720, 123);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "Total Amount: ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(725, 84);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 20);
            this.label7.TabIndex = 11;
            this.label7.Text = "Total Weight: ";
            // 
            // textWeight
            // 
            this.textWeight.Location = new System.Drawing.Point(892, 80);
            this.textWeight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textWeight.Name = "textWeight";
            this.textWeight.Size = new System.Drawing.Size(100, 22);
            this.textWeight.TabIndex = 13;
            this.textWeight.TextChanged += new System.EventHandler(this.textWeight_TextChanged);
            // 
            // textAmount
            // 
            this.textAmount.Location = new System.Drawing.Point(892, 123);
            this.textAmount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textAmount.Name = "textAmount";
            this.textAmount.Size = new System.Drawing.Size(100, 22);
            this.textAmount.TabIndex = 14;
            this.textAmount.TextChanged += new System.EventHandler(this.textAmount_TextChanged);
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSubmit.Location = new System.Drawing.Point(881, 348);
            this.buttonSubmit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(149, 46);
            this.buttonSubmit.TabIndex = 16;
            this.buttonSubmit.Text = "Submit Transaction";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            this.buttonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(1060, 389);
            this.btnNext.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(93, 30);
            this.btnNext.TabIndex = 17;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnBack
            // 
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Location = new System.Drawing.Point(1116, 14);
            this.btnBack.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(93, 30);
            this.btnBack.TabIndex = 25;
            this.btnBack.Text = "Go Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // dataGridViewItems
            // 
            this.dataGridViewItems.AutoGenerateColumns = false;
            this.dataGridViewItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.itemIDDataGridViewTextBoxColumn,
            this.wasteTypeDataGridViewTextBoxColumn,
            this.weightDataGridViewTextBoxColumn,
            this.totalWeightDataGridViewTextBoxColumn,
            this.totalAmountDataGridViewTextBoxColumn,
            this.customerIDDataGridViewTextBoxColumn});
            this.dataGridViewItems.DataSource = this.itemDetailsBindingSource;
            this.dataGridViewItems.Location = new System.Drawing.Point(18, 209);
            this.dataGridViewItems.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewItems.Name = "dataGridViewItems";
            this.dataGridViewItems.RowHeadersWidth = 51;
            this.dataGridViewItems.Size = new System.Drawing.Size(811, 185);
            this.dataGridViewItems.TabIndex = 26;
            this.dataGridViewItems.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewItems_CellContentClick);
            // 
            // itemIDDataGridViewTextBoxColumn
            // 
            this.itemIDDataGridViewTextBoxColumn.DataPropertyName = "ItemID";
            this.itemIDDataGridViewTextBoxColumn.HeaderText = "ItemID";
            this.itemIDDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.itemIDDataGridViewTextBoxColumn.Name = "itemIDDataGridViewTextBoxColumn";
            this.itemIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.itemIDDataGridViewTextBoxColumn.Width = 125;
            // 
            // wasteTypeDataGridViewTextBoxColumn
            // 
            this.wasteTypeDataGridViewTextBoxColumn.DataPropertyName = "WasteType";
            this.wasteTypeDataGridViewTextBoxColumn.HeaderText = "WasteType";
            this.wasteTypeDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.wasteTypeDataGridViewTextBoxColumn.Name = "wasteTypeDataGridViewTextBoxColumn";
            this.wasteTypeDataGridViewTextBoxColumn.Width = 125;
            // 
            // weightDataGridViewTextBoxColumn
            // 
            this.weightDataGridViewTextBoxColumn.DataPropertyName = "Weight";
            this.weightDataGridViewTextBoxColumn.HeaderText = "Weight";
            this.weightDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.weightDataGridViewTextBoxColumn.Name = "weightDataGridViewTextBoxColumn";
            this.weightDataGridViewTextBoxColumn.Width = 125;
            // 
            // totalWeightDataGridViewTextBoxColumn
            // 
            this.totalWeightDataGridViewTextBoxColumn.DataPropertyName = "TotalWeight";
            this.totalWeightDataGridViewTextBoxColumn.HeaderText = "TotalWeight";
            this.totalWeightDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.totalWeightDataGridViewTextBoxColumn.Name = "totalWeightDataGridViewTextBoxColumn";
            this.totalWeightDataGridViewTextBoxColumn.Width = 125;
            // 
            // totalAmountDataGridViewTextBoxColumn
            // 
            this.totalAmountDataGridViewTextBoxColumn.DataPropertyName = "TotalAmount";
            this.totalAmountDataGridViewTextBoxColumn.HeaderText = "TotalAmount";
            this.totalAmountDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.totalAmountDataGridViewTextBoxColumn.Name = "totalAmountDataGridViewTextBoxColumn";
            this.totalAmountDataGridViewTextBoxColumn.Width = 125;
            // 
            // customerIDDataGridViewTextBoxColumn
            // 
            this.customerIDDataGridViewTextBoxColumn.DataPropertyName = "CustomerID";
            this.customerIDDataGridViewTextBoxColumn.HeaderText = "CustomerID";
            this.customerIDDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.customerIDDataGridViewTextBoxColumn.Name = "customerIDDataGridViewTextBoxColumn";
            this.customerIDDataGridViewTextBoxColumn.Width = 125;
            // 
            // itemDetailsBindingSource
            // 
            this.itemDetailsBindingSource.DataMember = "ItemDetails";
            this.itemDetailsBindingSource.DataSource = this.greenbidDataSet1;
            // 
            // greenbidDataSet1
            // 
            this.greenbidDataSet1.DataSetName = "greenbidDataSet1";
            this.greenbidDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // itemDetailsTableAdapter
            // 
            this.itemDetailsTableAdapter.ClearBeforeFill = true;
            // 
            // customersTableAdapter
            // 
            this.customersTableAdapter.ClearBeforeFill = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::projectw.Properties.Resources.output_onlinepngtools__2_;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1224, 450);
            this.Controls.Add(this.dataGridViewItems);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.textAmount);
            this.Controls.Add(this.textWeight);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnAddItem);
            this.Controls.Add(this.comboBoxWeight);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelItem);
            this.Controls.Add(this.comboBoxCustomer);
            this.Controls.Add(this.labelCustomer);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form2";
            this.Text = "GreenBin Drop-Off Entry";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.customersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenbidDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenbidDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemDetailsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenbidDataSet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelCustomer;
        private System.Windows.Forms.ComboBox comboBoxCustomer;
        private System.Windows.Forms.Label labelItem;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxWeight;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textWeight;
        private System.Windows.Forms.TextBox textAmount;
        private System.Windows.Forms.Button buttonSubmit;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.DataGridView dataGridViewItems;
        private greenbidDataSet greenbidDataSet;
        private System.Windows.Forms.BindingSource greenbidDataSetBindingSource;
        private greenbidDataSet1 greenbidDataSet1;
        private System.Windows.Forms.BindingSource itemDetailsBindingSource;
        private greenbidDataSet1TableAdapters.ItemDetailsTableAdapter itemDetailsTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn wasteTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn weightDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalWeightDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalAmountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn customerIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource customersBindingSource;
        private greenbidDataSetTableAdapters.CustomersTableAdapter customersTableAdapter;
    }
}