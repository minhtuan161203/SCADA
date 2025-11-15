namespace MySCADA
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.SimulatorTimer = new System.Windows.Forms.Timer(this.components);
            this.MonitorTimer = new System.Windows.Forms.Timer(this.components);
            this.btMotor_1 = new System.Windows.Forms.Button();
            this.btMotor_2 = new System.Windows.Forms.Button();
            this.btMotor_3 = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lbClock = new System.Windows.Forms.Label();
            this.lbHeader = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.timer_date = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelst_10 = new System.Windows.Forms.Label();
            this.labelst_9 = new System.Windows.Forms.Label();
            this.labelst_8 = new System.Windows.Forms.Label();
            this.labelst_7 = new System.Windows.Forms.Label();
            this.labelst_6 = new System.Windows.Forms.Label();
            this.labelst_5 = new System.Windows.Forms.Label();
            this.labelst_2 = new System.Windows.Forms.Label();
            this.btMotor_10 = new System.Windows.Forms.Button();
            this.btMotor_5 = new System.Windows.Forms.Button();
            this.btMotor_9 = new System.Windows.Forms.Button();
            this.btMotor_4 = new System.Windows.Forms.Button();
            this.btMotor_8 = new System.Windows.Forms.Button();
            this.btMotor_7 = new System.Windows.Forms.Button();
            this.btMotor_6 = new System.Windows.Forms.Button();
            this.labelst_3 = new System.Windows.Forms.Label();
            this.labelst_1 = new System.Windows.Forms.Label();
            this.labelst_4 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabControlMain = new System.Windows.Forms.TabPage();
            this.lvActions = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelStoppedCount = new System.Windows.Forms.Label();
            this.labelRunningCount = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // SimulatorTimer
            // 
            this.SimulatorTimer.Enabled = true;
            // 
            // MonitorTimer
            // 
            this.MonitorTimer.Enabled = true;
            this.MonitorTimer.Tick += new System.EventHandler(this.MonitorTimer_Tick);
            // 
            // btMotor_1
            // 
            this.btMotor_1.Location = new System.Drawing.Point(3, 2);
            this.btMotor_1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btMotor_1.Name = "btMotor_1";
            this.btMotor_1.Size = new System.Drawing.Size(161, 47);
            this.btMotor_1.TabIndex = 15;
            this.btMotor_1.Text = "Motor#1";
            this.btMotor_1.UseVisualStyleBackColor = true;
            this.btMotor_1.Click += new System.EventHandler(this.btMotor_1_Click);
            // 
            // btMotor_2
            // 
            this.btMotor_2.Location = new System.Drawing.Point(171, 2);
            this.btMotor_2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btMotor_2.Name = "btMotor_2";
            this.btMotor_2.Size = new System.Drawing.Size(172, 47);
            this.btMotor_2.TabIndex = 16;
            this.btMotor_2.Text = "Motor#2";
            this.btMotor_2.UseVisualStyleBackColor = true;
            this.btMotor_2.Click += new System.EventHandler(this.btMotor_2_Click);
            // 
            // btMotor_3
            // 
            this.btMotor_3.Location = new System.Drawing.Point(350, 2);
            this.btMotor_3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btMotor_3.Name = "btMotor_3";
            this.btMotor_3.Size = new System.Drawing.Size(174, 47);
            this.btMotor_3.TabIndex = 17;
            this.btMotor_3.Text = "Motor#3";
            this.btMotor_3.UseVisualStyleBackColor = true;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelHeader.Controls.Add(this.lbClock);
            this.panelHeader.Controls.Add(this.lbHeader);
            this.panelHeader.Controls.Add(this.picLogo);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(903, 79);
            this.panelHeader.TabIndex = 18;
            // 
            // lbClock
            // 
            this.lbClock.AutoSize = true;
            this.lbClock.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbClock.Location = new System.Drawing.Point(744, 9);
            this.lbClock.Name = "lbClock";
            this.lbClock.Size = new System.Drawing.Size(103, 29);
            this.lbClock.TabIndex = 20;
            this.lbClock.Text = "00:00:00";
            // 
            // lbHeader
            // 
            this.lbHeader.AutoSize = true;
            this.lbHeader.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeader.ForeColor = System.Drawing.Color.White;
            this.lbHeader.Location = new System.Drawing.Point(3, 17);
            this.lbHeader.Name = "lbHeader";
            this.lbHeader.Size = new System.Drawing.Size(543, 50);
            this.lbHeader.TabIndex = 20;
            this.lbHeader.Text = "MOTOR MONITORING SCADA";
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.Image = ((System.Drawing.Image)(resources.GetObject("picLogo.Image")));
            this.picLogo.Location = new System.Drawing.Point(577, -8);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(129, 107);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 19;
            this.picLogo.TabStop = false;
            // 
            // timer_date
            // 
            this.timer_date.Enabled = true;
            this.timer_date.Interval = 1000;
            this.timer_date.Tick += new System.EventHandler(this.timer_date_Tick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.77519F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 179F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 177F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            this.tableLayoutPanel1.Controls.Add(this.labelst_10, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelst_9, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelst_8, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelst_7, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelst_6, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelst_5, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelst_2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btMotor_10, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.btMotor_5, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.btMotor_9, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.btMotor_4, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btMotor_8, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.btMotor_1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btMotor_7, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btMotor_2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btMotor_6, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btMotor_3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelst_3, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelst_1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelst_4, 3, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 85);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(888, 189);
            this.tableLayoutPanel1.TabIndex = 20;
            // 
            // labelst_10
            // 
            this.labelst_10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelst_10.AutoSize = true;
            this.labelst_10.Location = new System.Drawing.Point(770, 161);
            this.labelst_10.Name = "labelst_10";
            this.labelst_10.Size = new System.Drawing.Size(51, 16);
            this.labelst_10.TabIndex = 36;
            this.labelst_10.Text = "label10";
            // 
            // labelst_9
            // 
            this.labelst_9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelst_9.AutoSize = true;
            this.labelst_9.Location = new System.Drawing.Point(593, 161);
            this.labelst_9.Name = "labelst_9";
            this.labelst_9.Size = new System.Drawing.Size(44, 16);
            this.labelst_9.TabIndex = 35;
            this.labelst_9.Text = "label9";
            // 
            // labelst_8
            // 
            this.labelst_8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelst_8.AutoSize = true;
            this.labelst_8.Location = new System.Drawing.Point(415, 161);
            this.labelst_8.Name = "labelst_8";
            this.labelst_8.Size = new System.Drawing.Size(44, 16);
            this.labelst_8.TabIndex = 34;
            this.labelst_8.Text = "label8";
            // 
            // labelst_7
            // 
            this.labelst_7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelst_7.AutoSize = true;
            this.labelst_7.Location = new System.Drawing.Point(235, 161);
            this.labelst_7.Name = "labelst_7";
            this.labelst_7.Size = new System.Drawing.Size(44, 16);
            this.labelst_7.TabIndex = 33;
            this.labelst_7.Text = "label7";
            // 
            // labelst_6
            // 
            this.labelst_6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelst_6.AutoSize = true;
            this.labelst_6.Location = new System.Drawing.Point(62, 161);
            this.labelst_6.Name = "labelst_6";
            this.labelst_6.Size = new System.Drawing.Size(44, 16);
            this.labelst_6.TabIndex = 32;
            this.labelst_6.Text = "label6";
            // 
            // labelst_5
            // 
            this.labelst_5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelst_5.AutoSize = true;
            this.labelst_5.Location = new System.Drawing.Point(774, 66);
            this.labelst_5.Name = "labelst_5";
            this.labelst_5.Size = new System.Drawing.Size(44, 16);
            this.labelst_5.TabIndex = 31;
            this.labelst_5.Text = "label5";
            // 
            // labelst_2
            // 
            this.labelst_2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelst_2.AutoSize = true;
            this.labelst_2.Location = new System.Drawing.Point(235, 66);
            this.labelst_2.Name = "labelst_2";
            this.labelst_2.Size = new System.Drawing.Size(44, 16);
            this.labelst_2.TabIndex = 30;
            this.labelst_2.Text = "label2";
            // 
            // btMotor_10
            // 
            this.btMotor_10.Location = new System.Drawing.Point(707, 98);
            this.btMotor_10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btMotor_10.Name = "btMotor_10";
            this.btMotor_10.Size = new System.Drawing.Size(178, 47);
            this.btMotor_10.TabIndex = 27;
            this.btMotor_10.Text = "Motor#10";
            this.btMotor_10.UseVisualStyleBackColor = true;
            // 
            // btMotor_5
            // 
            this.btMotor_5.Location = new System.Drawing.Point(707, 2);
            this.btMotor_5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btMotor_5.Name = "btMotor_5";
            this.btMotor_5.Size = new System.Drawing.Size(178, 47);
            this.btMotor_5.TabIndex = 22;
            this.btMotor_5.Text = "Motor#5";
            this.btMotor_5.UseVisualStyleBackColor = true;
            // 
            // btMotor_9
            // 
            this.btMotor_9.Location = new System.Drawing.Point(530, 98);
            this.btMotor_9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btMotor_9.Name = "btMotor_9";
            this.btMotor_9.Size = new System.Drawing.Size(171, 47);
            this.btMotor_9.TabIndex = 26;
            this.btMotor_9.Text = "Motor#9";
            this.btMotor_9.UseVisualStyleBackColor = true;
            // 
            // btMotor_4
            // 
            this.btMotor_4.Location = new System.Drawing.Point(530, 2);
            this.btMotor_4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btMotor_4.Name = "btMotor_4";
            this.btMotor_4.Size = new System.Drawing.Size(171, 47);
            this.btMotor_4.TabIndex = 21;
            this.btMotor_4.Text = "Motor#4";
            this.btMotor_4.UseVisualStyleBackColor = true;
            // 
            // btMotor_8
            // 
            this.btMotor_8.Location = new System.Drawing.Point(350, 98);
            this.btMotor_8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btMotor_8.Name = "btMotor_8";
            this.btMotor_8.Size = new System.Drawing.Size(171, 47);
            this.btMotor_8.TabIndex = 25;
            this.btMotor_8.Text = "Motor#8";
            this.btMotor_8.UseVisualStyleBackColor = true;
            // 
            // btMotor_7
            // 
            this.btMotor_7.Location = new System.Drawing.Point(171, 98);
            this.btMotor_7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btMotor_7.Name = "btMotor_7";
            this.btMotor_7.Size = new System.Drawing.Size(171, 47);
            this.btMotor_7.TabIndex = 24;
            this.btMotor_7.Text = "Motor#7";
            this.btMotor_7.UseVisualStyleBackColor = true;
            // 
            // btMotor_6
            // 
            this.btMotor_6.Location = new System.Drawing.Point(3, 98);
            this.btMotor_6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btMotor_6.Name = "btMotor_6";
            this.btMotor_6.Size = new System.Drawing.Size(161, 47);
            this.btMotor_6.TabIndex = 23;
            this.btMotor_6.Text = "Motor#6";
            this.btMotor_6.UseVisualStyleBackColor = true;
            // 
            // labelst_3
            // 
            this.labelst_3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelst_3.AutoSize = true;
            this.labelst_3.Location = new System.Drawing.Point(415, 66);
            this.labelst_3.Name = "labelst_3";
            this.labelst_3.Size = new System.Drawing.Size(44, 16);
            this.labelst_3.TabIndex = 28;
            this.labelst_3.Text = "label3";
            // 
            // labelst_1
            // 
            this.labelst_1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelst_1.AutoSize = true;
            this.labelst_1.Location = new System.Drawing.Point(62, 66);
            this.labelst_1.Name = "labelst_1";
            this.labelst_1.Size = new System.Drawing.Size(44, 16);
            this.labelst_1.TabIndex = 18;
            this.labelst_1.Text = "label1";
            // 
            // labelst_4
            // 
            this.labelst_4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelst_4.AutoSize = true;
            this.labelst_4.Location = new System.Drawing.Point(593, 66);
            this.labelst_4.Name = "labelst_4";
            this.labelst_4.Size = new System.Drawing.Size(44, 16);
            this.labelst_4.TabIndex = 29;
            this.labelst_4.Text = "label4";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabControlMain);
            this.tabControl1.Location = new System.Drawing.Point(7, 280);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(883, 288);
            this.tabControl1.TabIndex = 22;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.lvActions);
            this.tabControlMain.Controls.Add(this.labelStoppedCount);
            this.tabControlMain.Controls.Add(this.labelRunningCount);
            this.tabControlMain.Location = new System.Drawing.Point(4, 25);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabControlMain.Size = new System.Drawing.Size(875, 259);
            this.tabControlMain.TabIndex = 0;
            this.tabControlMain.Text = "General Info";
            this.tabControlMain.UseVisualStyleBackColor = true;
            // 
            // lvActions
            // 
            this.lvActions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lvActions.FullRowSelect = true;
            this.lvActions.GridLines = true;
            this.lvActions.HideSelection = false;
            this.lvActions.Location = new System.Drawing.Point(3, 50);
            this.lvActions.Name = "lvActions";
            this.lvActions.Size = new System.Drawing.Size(866, 203);
            this.lvActions.TabIndex = 25;
            this.lvActions.UseCompatibleStateImageBehavior = false;
            this.lvActions.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Date";
            this.columnHeader1.Width = 93;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Time";
            this.columnHeader2.Width = 73;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Motor";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Action";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Value";
            // 
            // labelStoppedCount
            // 
            this.labelStoppedCount.AutoSize = true;
            this.labelStoppedCount.Location = new System.Drawing.Point(5, 31);
            this.labelStoppedCount.Name = "labelStoppedCount";
            this.labelStoppedCount.Size = new System.Drawing.Size(72, 16);
            this.labelStoppedCount.TabIndex = 1;
            this.labelStoppedCount.Text = "Stopped: 0";
            // 
            // labelRunningCount
            // 
            this.labelRunningCount.AutoSize = true;
            this.labelRunningCount.Location = new System.Drawing.Point(5, 8);
            this.labelRunningCount.Name = "labelRunningCount";
            this.labelRunningCount.Size = new System.Drawing.Size(69, 16);
            this.labelRunningCount.TabIndex = 0;
            this.labelRunningCount.Text = "Running: 0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(903, 571);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Monitoring Pannel";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.tabControlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer SimulatorTimer;
        private System.Windows.Forms.Timer MonitorTimer;
        private System.Windows.Forms.Button btMotor_1;
        private System.Windows.Forms.Button btMotor_2;
        private System.Windows.Forms.Button btMotor_3;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lbHeader;
        private System.Windows.Forms.Label lbClock;
        private System.Windows.Forms.Timer timer_date;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelst_1;
        private System.Windows.Forms.Button btMotor_4;
        private System.Windows.Forms.Button btMotor_5;
        private System.Windows.Forms.Button btMotor_6;
        private System.Windows.Forms.Button btMotor_7;
        private System.Windows.Forms.Button btMotor_8;
        private System.Windows.Forms.Button btMotor_9;
        private System.Windows.Forms.Button btMotor_10;
        private System.Windows.Forms.Label labelst_10;
        private System.Windows.Forms.Label labelst_9;
        private System.Windows.Forms.Label labelst_8;
        private System.Windows.Forms.Label labelst_7;
        private System.Windows.Forms.Label labelst_6;
        private System.Windows.Forms.Label labelst_5;
        private System.Windows.Forms.Label labelst_2;
        private System.Windows.Forms.Label labelst_3;
        private System.Windows.Forms.Label labelst_4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabControlMain;
        private System.Windows.Forms.Label labelStoppedCount;
        private System.Windows.Forms.Label labelRunningCount;
        private System.Windows.Forms.ListView lvActions;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
    }
}

