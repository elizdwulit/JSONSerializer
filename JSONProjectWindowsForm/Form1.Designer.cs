namespace JSONProjectUI
{
    partial class Form1
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
            this.fileChooserTextBox = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.fileChooserButton = new System.Windows.Forms.Button();
            this.KeyTextBox = new System.Windows.Forms.TextBox();
            this.ValueTextBox = new System.Windows.Forms.TextBox();
            this.KeyLabel = new System.Windows.Forms.Label();
            this.ValueLabel = new System.Windows.Forms.Label();
            this.AddButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.ModifyButton = new System.Windows.Forms.Button();
            this.UndoButton = new System.Windows.Forms.Button();
            this.SaveToFileButton = new System.Windows.Forms.Button();
            this.FindKeyLabel = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.FindKeyTextBox = new System.Windows.Forms.TextBox();
            this.UploadButton = new System.Windows.Forms.Button();
            this.ResultTextBox = new System.Windows.Forms.TextBox();
            this.FindButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fileChooserTextBox
            // 
            this.fileChooserTextBox.Location = new System.Drawing.Point(208, 32);
            this.fileChooserTextBox.Multiline = true;
            this.fileChooserTextBox.Name = "fileChooserTextBox";
            this.fileChooserTextBox.Size = new System.Drawing.Size(1016, 34);
            this.fileChooserTextBox.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 35);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select Source File";
            // 
            // fileChooserButton
            // 
            this.fileChooserButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileChooserButton.Location = new System.Drawing.Point(1260, 30);
            this.fileChooserButton.Name = "fileChooserButton";
            this.fileChooserButton.Size = new System.Drawing.Size(136, 40);
            this.fileChooserButton.TabIndex = 3;
            this.fileChooserButton.Text = "Choose File";
            this.fileChooserButton.UseVisualStyleBackColor = true;
            this.fileChooserButton.Click += new System.EventHandler(this.fileChooserButton_Click);
            // 
            // KeyTextBox
            // 
            this.KeyTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyTextBox.Location = new System.Drawing.Point(1009, 221);
            this.KeyTextBox.Name = "KeyTextBox";
            this.KeyTextBox.Size = new System.Drawing.Size(392, 30);
            this.KeyTextBox.TabIndex = 5;
            // 
            // ValueTextBox
            // 
            this.ValueTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ValueTextBox.Location = new System.Drawing.Point(1009, 283);
            this.ValueTextBox.Multiline = true;
            this.ValueTextBox.Name = "ValueTextBox";
            this.ValueTextBox.Size = new System.Drawing.Size(392, 263);
            this.ValueTextBox.TabIndex = 6;
            // 
            // KeyLabel
            // 
            this.KeyLabel.AutoSize = true;
            this.KeyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyLabel.Location = new System.Drawing.Point(729, 221);
            this.KeyLabel.Name = "KeyLabel";
            this.KeyLabel.Size = new System.Drawing.Size(249, 25);
            this.KeyLabel.TabIndex = 7;
            this.KeyLabel.Text = "Key (to Add/Delete/Modify)";
            // 
            // ValueLabel
            // 
            this.ValueLabel.AutoSize = true;
            this.ValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ValueLabel.Location = new System.Drawing.Point(718, 283);
            this.ValueLabel.Name = "ValueLabel";
            this.ValueLabel.Size = new System.Drawing.Size(260, 25);
            this.ValueLabel.TabIndex = 8;
            this.ValueLabel.Text = "Value (Add and Modify only)";
            // 
            // AddButton
            // 
            this.AddButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddButton.Location = new System.Drawing.Point(729, 583);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(140, 43);
            this.AddButton.TabIndex = 9;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteButton.Location = new System.Drawing.Point(908, 583);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(140, 43);
            this.DeleteButton.TabIndex = 13;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // ModifyButton
            // 
            this.ModifyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ModifyButton.Location = new System.Drawing.Point(1073, 583);
            this.ModifyButton.Name = "ModifyButton";
            this.ModifyButton.Size = new System.Drawing.Size(151, 43);
            this.ModifyButton.TabIndex = 14;
            this.ModifyButton.Text = "Modify";
            this.ModifyButton.UseVisualStyleBackColor = true;
            this.ModifyButton.Click += new System.EventHandler(this.ModifyButton_Click);
            // 
            // UndoButton
            // 
            this.UndoButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UndoButton.Location = new System.Drawing.Point(809, 650);
            this.UndoButton.Name = "UndoButton";
            this.UndoButton.Size = new System.Drawing.Size(197, 49);
            this.UndoButton.TabIndex = 15;
            this.UndoButton.Text = "Undo Last Action";
            this.UndoButton.UseVisualStyleBackColor = true;
            this.UndoButton.Click += new System.EventHandler(this.UndoButton_Click);
            // 
            // SaveToFileButton
            // 
            this.SaveToFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveToFileButton.Location = new System.Drawing.Point(1042, 650);
            this.SaveToFileButton.Name = "SaveToFileButton";
            this.SaveToFileButton.Size = new System.Drawing.Size(263, 49);
            this.SaveToFileButton.TabIndex = 16;
            this.SaveToFileButton.Text = "Save Output to File";
            this.SaveToFileButton.UseVisualStyleBackColor = true;
            // 
            // FindKeyLabel
            // 
            this.FindKeyLabel.AutoSize = true;
            this.FindKeyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FindKeyLabel.Location = new System.Drawing.Point(724, 162);
            this.FindKeyLabel.Name = "FindKeyLabel";
            this.FindKeyLabel.Size = new System.Drawing.Size(254, 25);
            this.FindKeyLabel.TabIndex = 17;
            this.FindKeyLabel.Text = "Key to Insert Into (Add only)";
            // 
            // FindKeyTextBox
            // 
            this.FindKeyTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FindKeyTextBox.Location = new System.Drawing.Point(1009, 159);
            this.FindKeyTextBox.Name = "FindKeyTextBox";
            this.FindKeyTextBox.Size = new System.Drawing.Size(392, 30);
            this.FindKeyTextBox.TabIndex = 18;
            // 
            // UploadButton
            // 
            this.UploadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UploadButton.Location = new System.Drawing.Point(1260, 98);
            this.UploadButton.Name = "UploadButton";
            this.UploadButton.Size = new System.Drawing.Size(136, 38);
            this.UploadButton.TabIndex = 19;
            this.UploadButton.Text = "Upload";
            this.UploadButton.UseVisualStyleBackColor = true;
            this.UploadButton.Click += new System.EventHandler(this.UploadButton_Click);
            // 
            // ResultTextBox
            // 
            this.ResultTextBox.AcceptsReturn = true;
            this.ResultTextBox.Location = new System.Drawing.Point(35, 98);
            this.ResultTextBox.Multiline = true;
            this.ResultTextBox.Name = "ResultTextBox";
            this.ResultTextBox.ReadOnly = true;
            this.ResultTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ResultTextBox.Size = new System.Drawing.Size(637, 601);
            this.ResultTextBox.TabIndex = 20;
            // 
            // FindButton
            // 
            this.FindButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FindButton.Location = new System.Drawing.Point(1260, 583);
            this.FindButton.Name = "FindButton";
            this.FindButton.Size = new System.Drawing.Size(151, 43);
            this.FindButton.TabIndex = 21;
            this.FindButton.Text = "Find";
            this.FindButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1441, 737);
            this.Controls.Add(this.FindButton);
            this.Controls.Add(this.ResultTextBox);
            this.Controls.Add(this.UploadButton);
            this.Controls.Add(this.FindKeyTextBox);
            this.Controls.Add(this.FindKeyLabel);
            this.Controls.Add(this.SaveToFileButton);
            this.Controls.Add(this.UndoButton);
            this.Controls.Add(this.ModifyButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.ValueLabel);
            this.Controls.Add(this.KeyLabel);
            this.Controls.Add(this.ValueTextBox);
            this.Controls.Add(this.KeyTextBox);
            this.Controls.Add(this.fileChooserButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fileChooserTextBox);
            this.Name = "Form1";
            this.Text = "JSON Modifier";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox fileChooserTextBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button fileChooserButton;
        private System.Windows.Forms.TextBox KeyTextBox;
        private System.Windows.Forms.TextBox ValueTextBox;
        private System.Windows.Forms.Label KeyLabel;
        private System.Windows.Forms.Label ValueLabel;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button ModifyButton;
        private System.Windows.Forms.Button UndoButton;
        private System.Windows.Forms.Button SaveToFileButton;
        private System.Windows.Forms.Label FindKeyLabel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox FindKeyTextBox;
        private System.Windows.Forms.Button UploadButton;
        private System.Windows.Forms.TextBox ResultTextBox;
        private System.Windows.Forms.Button FindButton;
    }
}

