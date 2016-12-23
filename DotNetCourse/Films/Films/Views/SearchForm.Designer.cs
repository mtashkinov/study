namespace Films.Views
{
    partial class SearchForm
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
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.searchNameTextBox = new System.Windows.Forms.TextBox();
            this.searchCountryTextBox = new System.Windows.Forms.TextBox();
            this.searchYearTextBox = new System.Windows.Forms.TextBox();
            this.searchDirectorTextBox = new System.Windows.Forms.TextBox();
            this.searchActorTextBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Year";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Country";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Director";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 186);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Actor";
            // 
            // searchNameTextBox
            // 
            this.searchNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchNameTextBox.Location = new System.Drawing.Point(63, 13);
            this.searchNameTextBox.Name = "searchNameTextBox";
            this.searchNameTextBox.Size = new System.Drawing.Size(187, 20);
            this.searchNameTextBox.TabIndex = 5;
            this.searchNameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.OnSearchNameTextBoxValidating);
            this.searchNameTextBox.Validated += new System.EventHandler(this.OnSearchNameTextBoxValidated);
            // 
            // searchCountryTextBox
            // 
            this.searchCountryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchCountryTextBox.Location = new System.Drawing.Point(63, 55);
            this.searchCountryTextBox.Name = "searchCountryTextBox";
            this.searchCountryTextBox.Size = new System.Drawing.Size(187, 20);
            this.searchCountryTextBox.TabIndex = 6;
            this.searchCountryTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.OnSearchCountryTextBoxValidating);
            this.searchCountryTextBox.Validated += new System.EventHandler(this.OnSearchCountryTextBoxValidated);
            // 
            // searchYearTextBox
            // 
            this.searchYearTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchYearTextBox.Location = new System.Drawing.Point(63, 95);
            this.searchYearTextBox.Name = "searchYearTextBox";
            this.searchYearTextBox.Size = new System.Drawing.Size(187, 20);
            this.searchYearTextBox.TabIndex = 7;
            this.searchYearTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.OnSearchYearTextBoxValidating);
            this.searchYearTextBox.Validated += new System.EventHandler(this.OnSearchYearTextBoxValidated);
            // 
            // searchDirectorTextBox
            // 
            this.searchDirectorTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchDirectorTextBox.Location = new System.Drawing.Point(63, 139);
            this.searchDirectorTextBox.Name = "searchDirectorTextBox";
            this.searchDirectorTextBox.Size = new System.Drawing.Size(187, 20);
            this.searchDirectorTextBox.TabIndex = 8;
            this.searchDirectorTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.OnSearchDirectorTextBoxValidating);
            this.searchDirectorTextBox.Validated += new System.EventHandler(this.OnSearchDirectorTextBoxValidated);
            // 
            // searchActorTextBox
            // 
            this.searchActorTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchActorTextBox.Location = new System.Drawing.Point(63, 183);
            this.searchActorTextBox.Name = "searchActorTextBox";
            this.searchActorTextBox.Size = new System.Drawing.Size(187, 20);
            this.searchActorTextBox.TabIndex = 9;
            this.searchActorTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.OnSearchActorTextBoxValidating);
            this.searchActorTextBox.Validated += new System.EventHandler(this.OnSearchActorTextBoxValidated);
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(175, 226);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 10;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.OnSearchButtonClicked);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(63, 226);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "Clear";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.OnClearButtonClicked);
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.searchActorTextBox);
            this.Controls.Add(this.searchDirectorTextBox);
            this.Controls.Add(this.searchYearTextBox);
            this.Controls.Add(this.searchCountryTextBox);
            this.Controls.Add(this.searchNameTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "SearchForm";
            this.Text = "Search";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox searchNameTextBox;
        private System.Windows.Forms.TextBox searchCountryTextBox;
        private System.Windows.Forms.TextBox searchYearTextBox;
        private System.Windows.Forms.TextBox searchDirectorTextBox;
        private System.Windows.Forms.TextBox searchActorTextBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button button2;
    }
}