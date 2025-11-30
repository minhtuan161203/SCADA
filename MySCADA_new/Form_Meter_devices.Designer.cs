namespace MySCADA
{
    partial class Form_Meter_devices
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
            this.btnTest = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(422, 83);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(139, 23);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "read_data_meter";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(323, 83);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 38);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "btnLoad";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // Form_Meter_devices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnTest);
            this.Name = "Form_Meter_devices";
            this.Text = "Meter_devices";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnLoad;
    }
}