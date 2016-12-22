namespace Films.Views
{
    partial class EditForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.editNameTextBox = new System.Windows.Forms.TextBox();
            this.editCountryTextBox = new System.Windows.Forms.TextBox();
            this.editYearTextBox = new System.Windows.Forms.TextBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.saveButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Country";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Year";
            // 
            // editNameTextBox
            // 
            this.editNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editNameTextBox.Location = new System.Drawing.Point(73, 13);
            this.editNameTextBox.Name = "editNameTextBox";
            this.editNameTextBox.Size = new System.Drawing.Size(160, 20);
            this.editNameTextBox.TabIndex = 3;
            this.editNameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.editNameTextBox_Validating);
            this.editNameTextBox.Validated += new System.EventHandler(this.editNameTextBox_Validated);
            // 
            // editCountryTextBox
            // 
            this.editCountryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editCountryTextBox.Location = new System.Drawing.Point(73, 54);
            this.editCountryTextBox.Name = "editCountryTextBox";
            this.editCountryTextBox.Size = new System.Drawing.Size(160, 20);
            this.editCountryTextBox.TabIndex = 4;
            this.editCountryTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.editCountryTextBox_Validating);
            this.editCountryTextBox.Validated += new System.EventHandler(this.editCountryTextBox_Validated);
            // 
            // editYearTextBox
            // 
            this.editYearTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editYearTextBox.Location = new System.Drawing.Point(73, 102);
            this.editYearTextBox.Name = "editYearTextBox";
            this.editYearTextBox.Size = new System.Drawing.Size(160, 20);
            this.editYearTextBox.TabIndex = 5;
            this.editYearTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.editYearTextBox_Validating);
            this.editYearTextBox.Validated += new System.EventHandler(this.editYearTextBox_Validated);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(102, 226);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.editYearTextBox);
            this.Controls.Add(this.editCountryTextBox);
            this.Controls.Add(this.editNameTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "EditForm";
            this.Text = "EditForm";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox editNameTextBox;
        private System.Windows.Forms.TextBox editCountryTextBox;
        private System.Windows.Forms.TextBox editYearTextBox;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button saveButton;
    }
}