namespace MySCADA
{
    partial class Motor_Faceplate
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
            this.pbStatus = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbstatus = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnSetSpeed = new System.Windows.Forms.Button();
            this.input_speed_SP = new System.Windows.Forms.TextBox();
            this.track_speed = new System.Windows.Forms.TrackBar();
            this.pbFan = new System.Windows.Forms.PictureBox();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.formsPlotTrend = new ScottPlot.WinForms.FormsPlot();
            this.tabAlarm = new System.Windows.Forms.TabPage();
            this.lvAlarms = new System.Windows.Forms.ListView();
            this.Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Message = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Severity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.State = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Bar_speed = new VerticalProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.track_speed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFan)).BeginInit();
            this.tabControlMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabAlarm.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbStatus
            // 
            this.pbStatus.Location = new System.Drawing.Point(22, 22);
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(54, 16);
            this.pbStatus.TabIndex = 0;
            this.pbStatus.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbstatus
            // 
            this.lbstatus.AutoSize = true;
            this.lbstatus.Location = new System.Drawing.Point(67, -22);
            this.lbstatus.Name = "lbstatus";
            this.lbstatus.Size = new System.Drawing.Size(44, 16);
            this.lbstatus.TabIndex = 18;
            this.lbstatus.Text = "Status";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(17, 44);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(107, 49);
            this.btnStart.TabIndex = 19;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(17, 99);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(107, 49);
            this.btnStop.TabIndex = 20;
            this.btnStop.Text = "STOP";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnSetSpeed
            // 
            this.btnSetSpeed.Location = new System.Drawing.Point(17, 154);
            this.btnSetSpeed.Name = "btnSetSpeed";
            this.btnSetSpeed.Size = new System.Drawing.Size(107, 47);
            this.btnSetSpeed.TabIndex = 21;
            this.btnSetSpeed.Text = "SET";
            this.btnSetSpeed.UseVisualStyleBackColor = true;
            this.btnSetSpeed.Click += new System.EventHandler(this.btnSetSpeed_Click);
            // 
            // input_speed_SP
            // 
            this.input_speed_SP.Location = new System.Drawing.Point(143, 179);
            this.input_speed_SP.Name = "input_speed_SP";
            this.input_speed_SP.Size = new System.Drawing.Size(100, 22);
            this.input_speed_SP.TabIndex = 22;
            this.input_speed_SP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.input_speed_SP_KeyDown);
            // 
            // track_speed
            // 
            this.track_speed.Location = new System.Drawing.Point(237, 8);
            this.track_speed.Maximum = 1000;
            this.track_speed.Name = "track_speed";
            this.track_speed.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.track_speed.Size = new System.Drawing.Size(56, 396);
            this.track_speed.TabIndex = 23;
            this.track_speed.Scroll += new System.EventHandler(this.track_speed_Scroll);
            // 
            // pbFan
            // 
            this.pbFan.Location = new System.Drawing.Point(17, 207);
            this.pbFan.Name = "pbFan";
            this.pbFan.Size = new System.Drawing.Size(212, 186);
            this.pbFan.TabIndex = 24;
            this.pbFan.TabStop = false;
            // 
            // tabControlMain
            // 
            this.tabControlMain.AllowDrop = true;
            this.tabControlMain.Controls.Add(this.tabPage1);
            this.tabControlMain.Controls.Add(this.tabPage2);
            this.tabControlMain.Controls.Add(this.tabAlarm);
            this.tabControlMain.Location = new System.Drawing.Point(10, 12);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(635, 478);
            this.tabControlMain.TabIndex = 25;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.tabPage1.Controls.Add(this.pbStatus);
            this.tabPage1.Controls.Add(this.lbstatus);
            this.tabPage1.Controls.Add(this.pbFan);
            this.tabPage1.Controls.Add(this.Bar_speed);
            this.tabPage1.Controls.Add(this.btnStop);
            this.tabPage1.Controls.Add(this.track_speed);
            this.tabPage1.Controls.Add(this.input_speed_SP);
            this.tabPage1.Controls.Add(this.btnSetSpeed);
            this.tabPage1.Controls.Add(this.btnStart);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(627, 449);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Control";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.formsPlotTrend);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(627, 449);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Trend";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // formsPlotTrend
            // 
            this.formsPlotTrend.DisplayScale = 0F;
            this.formsPlotTrend.Location = new System.Drawing.Point(6, 6);
            this.formsPlotTrend.Name = "formsPlotTrend";
            this.formsPlotTrend.Size = new System.Drawing.Size(618, 447);
            this.formsPlotTrend.TabIndex = 0;
            // 
            // tabAlarm
            // 
            this.tabAlarm.AllowDrop = true;
            this.tabAlarm.Controls.Add(this.lvAlarms);
            this.tabAlarm.Location = new System.Drawing.Point(4, 25);
            this.tabAlarm.Name = "tabAlarm";
            this.tabAlarm.Padding = new System.Windows.Forms.Padding(3);
            this.tabAlarm.Size = new System.Drawing.Size(627, 449);
            this.tabAlarm.TabIndex = 2;
            this.tabAlarm.Text = "Alarm";
            this.tabAlarm.UseVisualStyleBackColor = true;
            // 
            // lvAlarms
            // 
            this.lvAlarms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Time,
            this.Message,
            this.Severity,
            this.State});
            this.lvAlarms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvAlarms.FullRowSelect = true;
            this.lvAlarms.GridLines = true;
            this.lvAlarms.HideSelection = false;
            this.lvAlarms.Location = new System.Drawing.Point(3, 3);
            this.lvAlarms.Name = "lvAlarms";
            this.lvAlarms.Size = new System.Drawing.Size(621, 443);
            this.lvAlarms.TabIndex = 0;
            this.lvAlarms.UseCompatibleStateImageBehavior = false;
            this.lvAlarms.View = System.Windows.Forms.View.Details;
            // 
            // Time
            // 
            this.Time.Text = "Time";
            this.Time.Width = 100;
            // 
            // Message
            // 
            this.Message.Text = "Message";
            this.Message.Width = 150;
            // 
            // Severity
            // 
            this.Severity.Text = "Severity";
            this.Severity.Width = 100;
            // 
            // State
            // 
            this.State.Text = "State";
            this.State.Width = 100;
            // 
            // Bar_speed
            // 
            this.Bar_speed.Location = new System.Drawing.Point(299, 23);
            this.Bar_speed.Maximum = 1000;
            this.Bar_speed.Name = "Bar_speed";
            this.Bar_speed.Size = new System.Drawing.Size(43, 367);
            this.Bar_speed.TabIndex = 14;
            // 
            // Motor_Faceplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PapayaWhip;
            this.ClientSize = new System.Drawing.Size(657, 510);
            this.Controls.Add(this.tabControlMain);
            this.Name = "Motor_Faceplate";
            this.Text = "Motor_Faceplate";
            this.Load += new System.EventHandler(this.Motor_Faceplate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.track_speed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFan)).EndInit();
            this.tabControlMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabAlarm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbStatus;
        private System.Windows.Forms.Timer timer1;
        private VerticalProgressBar Bar_speed;
        private System.Windows.Forms.Label lbstatus;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnSetSpeed;
        private System.Windows.Forms.TextBox input_speed_SP;
        private System.Windows.Forms.TrackBar track_speed;
        private System.Windows.Forms.PictureBox pbFan;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabAlarm;
        private ScottPlot.WinForms.FormsPlot formsPlotTrend;
        private System.Windows.Forms.ListView lvAlarms;
        private System.Windows.Forms.ColumnHeader Time;
        private System.Windows.Forms.ColumnHeader Message;
        private System.Windows.Forms.ColumnHeader Severity;
        private System.Windows.Forms.ColumnHeader State;
    }
}