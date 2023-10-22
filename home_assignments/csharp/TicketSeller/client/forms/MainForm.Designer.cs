using System.ComponentModel;

namespace client.forms;

partial class MainForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

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
        this.dataGridView1 = new System.Windows.Forms.DataGridView();
        this.dataGridView2 = new System.Windows.Forms.DataGridView();
        this.allFestivalsLabel = new System.Windows.Forms.Label();
        this.festivalsOnDateLabel = new System.Windows.Forms.Label();
        this.refreshButton = new System.Windows.Forms.Button();
        this.logoutButton = new System.Windows.Forms.Button();
        this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
        this.searchButton = new System.Windows.Forms.Button();
        this.buyTicketsLabel = new System.Windows.Forms.Label();
        this.buyerNameLabel = new System.Windows.Forms.Label();
        this.numberOfSpotsLabel = new System.Windows.Forms.Label();
        this.buyerNameTextBox = new System.Windows.Forms.TextBox();
        this.spotsTextBox = new System.Windows.Forms.TextBox();
        this.allRadioButton = new System.Windows.Forms.RadioButton();
        this.onDateRadioButton = new System.Windows.Forms.RadioButton();
        this.buyButton = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize) (this.dataGridView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize) (this.dataGridView2)).BeginInit();
        this.SuspendLayout();
        // 
        // dataGridView1
        // 
        this.dataGridView1.AllowUserToAddRows = false;
        this.dataGridView1.AllowUserToDeleteRows = false;
        this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridView1.Location = new System.Drawing.Point(12, 49);
        this.dataGridView1.MultiSelect = false;
        this.dataGridView1.Name = "dataGridView1";
        this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.dataGridView1.Size = new System.Drawing.Size(310, 199);
        this.dataGridView1.TabIndex = 0;
        // 
        // dataGridView2
        // 
        this.dataGridView2.AllowUserToAddRows = false;
        this.dataGridView2.AllowUserToDeleteRows = false;
        this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridView2.Location = new System.Drawing.Point(502, 49);
        this.dataGridView2.Name = "dataGridView2";
        this.dataGridView2.ReadOnly = true;
        this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.dataGridView2.Size = new System.Drawing.Size(286, 192);
        this.dataGridView2.TabIndex = 1;
        // 
        // allFestivalsLabel
        // 
        this.allFestivalsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
        this.allFestivalsLabel.Location = new System.Drawing.Point(30, 14);
        this.allFestivalsLabel.Name = "allFestivalsLabel";
        this.allFestivalsLabel.Size = new System.Drawing.Size(189, 26);
        this.allFestivalsLabel.TabIndex = 2;
        this.allFestivalsLabel.Text = "All Festivals";
        // 
        // festivalsOnDateLabel
        // 
        this.festivalsOnDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
        this.festivalsOnDateLabel.Location = new System.Drawing.Point(513, 14);
        this.festivalsOnDateLabel.Name = "festivalsOnDateLabel";
        this.festivalsOnDateLabel.Size = new System.Drawing.Size(275, 29);
        this.festivalsOnDateLabel.TabIndex = 3;
        this.festivalsOnDateLabel.Text = "Festivals On Date";
        // 
        // refreshButton
        // 
        this.refreshButton.Location = new System.Drawing.Point(29, 278);
        this.refreshButton.Name = "refreshButton";
        this.refreshButton.Size = new System.Drawing.Size(78, 27);
        this.refreshButton.TabIndex = 4;
        this.refreshButton.Text = "Refresh";
        this.refreshButton.UseVisualStyleBackColor = true;
        this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
        // 
        // logoutButton
        // 
        this.logoutButton.Location = new System.Drawing.Point(29, 397);
        this.logoutButton.Name = "logoutButton";
        this.logoutButton.Size = new System.Drawing.Size(77, 31);
        this.logoutButton.TabIndex = 5;
        this.logoutButton.Text = "Logout";
        this.logoutButton.UseVisualStyleBackColor = true;
        this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
        // 
        // dateTimePicker
        // 
        this.dateTimePicker.Location = new System.Drawing.Point(513, 274);
        this.dateTimePicker.Name = "dateTimePicker";
        this.dateTimePicker.Size = new System.Drawing.Size(214, 20);
        this.dateTimePicker.TabIndex = 6;
        // 
        // searchButton
        // 
        this.searchButton.Location = new System.Drawing.Point(551, 332);
        this.searchButton.Name = "searchButton";
        this.searchButton.Size = new System.Drawing.Size(115, 30);
        this.searchButton.TabIndex = 7;
        this.searchButton.Text = "Search";
        this.searchButton.UseVisualStyleBackColor = true;
        this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
        // 
        // buyTicketsLabel
        // 
        this.buyTicketsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
        this.buyTicketsLabel.Location = new System.Drawing.Point(332, 14);
        this.buyTicketsLabel.Name = "buyTicketsLabel";
        this.buyTicketsLabel.Size = new System.Drawing.Size(162, 27);
        this.buyTicketsLabel.TabIndex = 8;
        this.buyTicketsLabel.Text = "Buy Tickets";
        // 
        // buyerNameLabel
        // 
        this.buyerNameLabel.Location = new System.Drawing.Point(353, 67);
        this.buyerNameLabel.Name = "buyerNameLabel";
        this.buyerNameLabel.Size = new System.Drawing.Size(109, 25);
        this.buyerNameLabel.TabIndex = 9;
        this.buyerNameLabel.Text = "Buyer Name:";
        // 
        // numberOfSpotsLabel
        // 
        this.numberOfSpotsLabel.Location = new System.Drawing.Point(353, 132);
        this.numberOfSpotsLabel.Name = "numberOfSpotsLabel";
        this.numberOfSpotsLabel.Size = new System.Drawing.Size(104, 27);
        this.numberOfSpotsLabel.TabIndex = 10;
        this.numberOfSpotsLabel.Text = "Number Of Spots:";
        // 
        // buyerNameTextBox
        // 
        this.buyerNameTextBox.Location = new System.Drawing.Point(353, 95);
        this.buyerNameTextBox.Name = "buyerNameTextBox";
        this.buyerNameTextBox.Size = new System.Drawing.Size(104, 20);
        this.buyerNameTextBox.TabIndex = 11;
        // 
        // spotsTextBox
        // 
        this.spotsTextBox.Location = new System.Drawing.Point(353, 162);
        this.spotsTextBox.Name = "spotsTextBox";
        this.spotsTextBox.Size = new System.Drawing.Size(103, 20);
        this.spotsTextBox.TabIndex = 12;
        // 
        // allRadioButton
        // 
        this.allRadioButton.Location = new System.Drawing.Point(344, 216);
        this.allRadioButton.Name = "allRadioButton";
        this.allRadioButton.Size = new System.Drawing.Size(103, 25);
        this.allRadioButton.TabIndex = 13;
        this.allRadioButton.TabStop = true;
        this.allRadioButton.Text = "from All Festivals";
        this.allRadioButton.UseVisualStyleBackColor = true;
        // 
        // onDateRadioButton
        // 
        this.onDateRadioButton.Location = new System.Drawing.Point(332, 266);
        this.onDateRadioButton.Name = "onDateRadioButton";
        this.onDateRadioButton.Size = new System.Drawing.Size(141, 28);
        this.onDateRadioButton.TabIndex = 14;
        this.onDateRadioButton.TabStop = true;
        this.onDateRadioButton.Text = "from Festivals On Date";
        this.onDateRadioButton.UseVisualStyleBackColor = true;
        // 
        // buyButton
        // 
        this.buyButton.Location = new System.Drawing.Point(344, 334);
        this.buyButton.Name = "buyButton";
        this.buyButton.Size = new System.Drawing.Size(123, 26);
        this.buyButton.TabIndex = 15;
        this.buyButton.Text = "Buy";
        this.buyButton.UseVisualStyleBackColor = true;
        this.buyButton.Click += new System.EventHandler(this.buyButton_Click);
        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Controls.Add(this.buyButton);
        this.Controls.Add(this.onDateRadioButton);
        this.Controls.Add(this.allRadioButton);
        this.Controls.Add(this.spotsTextBox);
        this.Controls.Add(this.buyerNameTextBox);
        this.Controls.Add(this.numberOfSpotsLabel);
        this.Controls.Add(this.buyerNameLabel);
        this.Controls.Add(this.buyTicketsLabel);
        this.Controls.Add(this.searchButton);
        this.Controls.Add(this.dateTimePicker);
        this.Controls.Add(this.logoutButton);
        this.Controls.Add(this.refreshButton);
        this.Controls.Add(this.festivalsOnDateLabel);
        this.Controls.Add(this.allFestivalsLabel);
        this.Controls.Add(this.dataGridView2);
        this.Controls.Add(this.dataGridView1);
        this.Name = "MainForm";
        this.Text = "MainForm";
        this.Activated += new System.EventHandler(this.MainForm_Activated);
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
        ((System.ComponentModel.ISupportInitialize) (this.dataGridView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize) (this.dataGridView2)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.Button buyButton;

    private System.Windows.Forms.RadioButton allRadioButton;

    private System.Windows.Forms.RadioButton onDateRadioButton;

    private System.Windows.Forms.TextBox spotsTextBox;

    private System.Windows.Forms.TextBox buyerNameTextBox;

    private System.Windows.Forms.Label buyerNameLabel;
    private System.Windows.Forms.Label numberOfSpotsLabel;

    private System.Windows.Forms.Label buyTicketsLabel;

    private System.Windows.Forms.DateTimePicker dateTimePicker;
    private System.Windows.Forms.Button searchButton;

    private System.Windows.Forms.Button logoutButton;

    private System.Windows.Forms.Button refreshButton;

    private System.Windows.Forms.Label allFestivalsLabel;
    private System.Windows.Forms.Label festivalsOnDateLabel;

    private System.Windows.Forms.DataGridView dataGridView2;

    private System.Windows.Forms.DataGridView dataGridView1;

    #endregion
}