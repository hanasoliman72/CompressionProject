namespace CompressionClient
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
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.connect_button = new System.Windows.Forms.Button();
            this.browse_button = new System.Windows.Forms.Button();
            this.send_button = new System.Windows.Forms.Button();
            this.IPTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.receive_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(23, 100);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(738, 273);
            this.logBox.TabIndex = 0;
            this.logBox.Text = "";
            // 
            // connect_button
            // 
            this.connect_button.Location = new System.Drawing.Point(23, 395);
            this.connect_button.Name = "connect_button";
            this.connect_button.Size = new System.Drawing.Size(116, 43);
            this.connect_button.TabIndex = 1;
            this.connect_button.Text = "Connect";
            this.connect_button.UseVisualStyleBackColor = true;
            this.connect_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // browse_button
            // 
            this.browse_button.Location = new System.Drawing.Point(169, 395);
            this.browse_button.Name = "browse_button";
            this.browse_button.Size = new System.Drawing.Size(116, 43);
            this.browse_button.TabIndex = 2;
            this.browse_button.Text = "Browse";
            this.browse_button.UseVisualStyleBackColor = true;
            this.browse_button.Click += new System.EventHandler(this.browse_button_Click);
            // 
            // send_button
            // 
            this.send_button.Location = new System.Drawing.Point(325, 395);
            this.send_button.Name = "send_button";
            this.send_button.Size = new System.Drawing.Size(116, 43);
            this.send_button.TabIndex = 3;
            this.send_button.Text = "Send";
            this.send_button.UseVisualStyleBackColor = true;
            this.send_button.Click += new System.EventHandler(this.send_button_Click);
            // 
            // IPTextBox
            // 
            this.IPTextBox.Location = new System.Drawing.Point(90, 51);
            this.IPTextBox.Name = "IPTextBox";
            this.IPTextBox.Size = new System.Drawing.Size(133, 22);
            this.IPTextBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Server IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(298, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Server port:";
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(380, 57);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(133, 22);
            this.portTextBox.TabIndex = 8;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // receive_button
            // 
            this.receive_button.Location = new System.Drawing.Point(474, 395);
            this.receive_button.Name = "receive_button";
            this.receive_button.Size = new System.Drawing.Size(116, 43);
            this.receive_button.TabIndex = 9;
            this.receive_button.Text = "Receive";
            this.receive_button.UseVisualStyleBackColor = true;
            this.receive_button.Click += new System.EventHandler(this.receive_button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.receive_button);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IPTextBox);
            this.Controls.Add(this.send_button);
            this.Controls.Add(this.browse_button);
            this.Controls.Add(this.connect_button);
            this.Controls.Add(this.logBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox logBox;
        private System.Windows.Forms.Button connect_button;
        private System.Windows.Forms.Button browse_button;
        private System.Windows.Forms.Button send_button;
        private System.Windows.Forms.TextBox IPTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button receive_button;
    }
}

