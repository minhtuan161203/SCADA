namespace MySCADA
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnMotorSystem = new System.Windows.Forms.Button();
            this.btnMeterSystem = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(294, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "System Monitoring Selection:";
            // 
            // btnMotorSystem
            // 
            this.btnMotorSystem.Location = new System.Drawing.Point(79, 45);
            this.btnMotorSystem.Name = "btnMotorSystem";
            this.btnMotorSystem.Size = new System.Drawing.Size(75, 61);
            this.btnMotorSystem.TabIndex = 2;
            this.btnMotorSystem.Text = "Motor System";
            this.btnMotorSystem.UseVisualStyleBackColor = true;
            // 
            // btnMeterSystem
            // 
            this.btnMeterSystem.Location = new System.Drawing.Point(207, 45);
            this.btnMeterSystem.Name = "btnMeterSystem";
            this.btnMeterSystem.Size = new System.Drawing.Size(75, 61);
            this.btnMeterSystem.TabIndex = 3;
            this.btnMeterSystem.Text = "Meter System";
            this.btnMeterSystem.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PeachPuff;
            this.ClientSize = new System.Drawing.Size(367, 118);
            this.Controls.Add(this.btnMeterSystem);
            this.Controls.Add(this.btnMotorSystem);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMotorSystem;
        private System.Windows.Forms.Button btnMeterSystem;
    }
}