using Svg; // For loading SVG file
using Svg.Transforms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySCADA
{
    public partial class Form1 : Form
    {

        // Load SVG files
        
        // Load image files

        // Array to store references to motor status labels (labelst_1..labelst_10)
        public Label[] motorStatusLabels;
        public string _currentConnectedIp;

        // Variables for ListView update behavior
        private DateTime lvLastActionTime = DateTime.MinValue;
        private bool lvPauseUpdates = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void MonitorTimer_Tick(object sender, EventArgs e)
        {

            // Refresh ListView when not paused
            if (lvActions != null && !lvPauseUpdates)
            {
                LoadRecentActions(100);
            }
        }

        private void timer_date_Tick(object sender, EventArgs e)
        {
            // Show real time clock in label
            lbClock.Text = DateTime.Now.ToString("HH:mm:ss\ndd/MM/yyyy");
        }
        private async void connect_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // 🔹 Lấy PLC đã tạo sẵn trong Program.cs
                var plc = Program.Root.FindPLC("PLC_1");
                if (plc == null)
                {
                    MessageBox.Show("No PLC configured in the system.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 🔹 Chạy kết nối trong Task riêng
                await Task.Run(() => plc.Connect());

                if (plc.Connected)
                {
                    MessageBox.Show($"Connected to PLC", "Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Failed to connect to PLC.", "Connection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            connect_btn_Click(null, EventArgs.Empty);
            // Đăng ký event cập nhật dữ liệu cho tất cả motor

            // Gán mảng label
            motorStatusLabels = new Label[]
            {
        labelst_1, labelst_2, labelst_3, labelst_4, labelst_5,
        labelst_6, labelst_7, labelst_8, labelst_9, labelst_10
            };
            for (int i = 0; i < 10; i++)
            {
                var motor = Program.Root.FindMotor($"Motor_{i + 1}");
                if (motor != null)
                {
                    // Khi motor có dữ liệu mới → gọi cập nhật UI
                    motor.OnDataUpdated += Motor_OnDataUpdated;
                }
            }
            // Gọi cập nhật lần đầu
            UpdateAllMotorStatus();

        }
        private void Motor_OnDataUpdated(Motor motor)
        {
            // Đảm bảo update trên UI thread
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)(() => UpdateMotorLabel(motor)));
            }
            else
            {
                UpdateMotorLabel(motor);
            }
        }
        private void UpdateMotorLabel(Motor motor)
        {
            if (motor == null || motorStatusLabels == null) return;

            var index = int.Parse(motor.Name.Replace("Motor_", "")) - 1;
            if (index < 0 || index >= motorStatusLabels.Length) return;

            var label = motorStatusLabels[index];

            if (motor.Status)
            {
                label.Text = $"Running ({motor.Speed:0.0} rpm)";
                label.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                label.Text = "Stopped";
                label.ForeColor = System.Drawing.Color.Red;
            }

            // ✅ Cập nhật lại tổng số motor sau mỗi lần thay đổi
            UpdateRunningStoppedCount();
        }

        private void UpdateAllMotorStatus()
        {
            for (int i = 0; i < motorStatusLabels.Length; i++)
            {
                var motor = Program.Root.FindMotor($"Motor_{i + 1}");
                if (motor != null)
                {
                    UpdateMotorLabel(motor);
                }
            }
            // ✅ Gọi cập nhật tổng số motor đang chạy / dừng
            UpdateRunningStoppedCount();
        }
        private void UpdateRunningStoppedCount()
        {
            if (motorStatusLabels == null || motorStatusLabels.Length == 0) return;

            int runningCount = 0;
            int stoppedCount = 0;

            // Duyệt qua tất cả motor
            for (int i = 0; i < 10; i++)
            {
                var motor = Program.Root.FindMotor($"Motor_{i + 1}");
                if (motor == null) continue;

                if (motor.Status)
                    runningCount++;
                else
                    stoppedCount++;
            }

            // Cập nhật lên UI
            labelRunningCount.Text = $"Running: {runningCount}";
            labelStoppedCount.Text = $"Stopped: {stoppedCount}";

            // Đổi màu cho dễ nhìn (tùy bạn)
            labelRunningCount.ForeColor = System.Drawing.Color.Green;
            labelStoppedCount.ForeColor = System.Drawing.Color.Red;
        }



        private void LoadRecentActions(int limit = 100)
        {
            if (lvActions == null) return;

            // Remember the item at top (date|time|motor) so we can restore scroll position after refill
            string prevTopKey = null;
            try
            {
                if (lvActions.TopItem != null && lvActions.TopItem.SubItems.Count >= 3)
                    prevTopKey = $"{lvActions.TopItem.SubItems[0].Text}|{lvActions.TopItem.SubItems[1].Text}|{lvActions.TopItem.SubItems[2].Text}";
            }
            catch { /* ignore */ }

            var actions = GeneralInfoManager.GetRecentActions(limit);

            lvActions.BeginUpdate();
            lvActions.Items.Clear();

            // Insert each action at index 0 so newest ends up at the top
            foreach (var a in actions)
            {
                var lvi = new ListViewItem(a.Time.ToString("dd/MM/yyyy"));
                lvi.SubItems.Add(a.Time.ToString("HH:mm:ss"));
                lvi.SubItems.Add(a.MotorName);
                lvi.SubItems.Add(a.Action);
                lvi.SubItems.Add(a.Value);

                lvActions.Items.Insert(0, lvi);
            }

            lvActions.EndUpdate();

            // Restore scroll position: try to find previous top item and set it as TopItem.
            try
            {
                if (!string.IsNullOrEmpty(prevTopKey))
                {
                    for (int i = 0; i < lvActions.Items.Count; i++)
                    {
                        var it = lvActions.Items[i];
                        if (it.SubItems.Count >= 3)
                        {
                            var key = $"{it.SubItems[0].Text}|{it.SubItems[1].Text}|{it.SubItems[2].Text}";
                            if (key == prevTopKey)
                            {
                                lvActions.TopItem = it;
                                return;
                            }
                        }
                    }
                }

                // If previous top not found, keep newest visible (top)
                if (lvActions.Items.Count > 0)
                    lvActions.TopItem = lvActions.Items[0];
            }
            catch
            {
                // ignore restore errors
            }
        }

        // Motor faceplate buttons (kept unchanged)
        private void btMotor_1_Click(object sender, EventArgs e)
        { 
            Motor motor = Program.Root.FindMotor("Motor_1");
            if (motor != null)
            {
                new Motor_Faceplate(motor).Show();
            }

        }
        private void btMotor_2_Click(object sender, EventArgs e)
        {
            Motor motor = Program.Root.FindMotor("Motor_2");
            if (motor != null)
            {
                new Motor_Faceplate(motor).Show();  
            }

        }
        //private void btMotor_2_Click(object sender, EventArgs e) => new Motor_Faceplate(Program.Motors[1]).Show();
        //private void btMotor_3_Click(object sender, EventArgs e) => new Motor_Faceplate(Program.Motors[2]).Show();
        //private void btMotor_4_Click(object sender, EventArgs e) => new Motor_Faceplate(Program.Motors[3]).Show();
        //private void btMotor_5_Click(object sender, EventArgs e) => new Motor_Faceplate(Program.Motors[4]).Show();
        //private void btMotor_6_Click(object sender, EventArgs e) => new Motor_Faceplate(Program.Motors[5]).Show();
        //private void btMotor_7_Click(object sender, EventArgs e) => new Motor_Faceplate(Program.Motors[6]).Show();
        //private void btMotor_8_Click(object sender, EventArgs e) => new Motor_Faceplate(Program.Motors[7]).Show();
        //private void btMotor_9_Click(object sender, EventArgs e) => new Motor_Faceplate(Program.Motors[8]).Show();
        //private void btMotor_10_Click(object sender, EventArgs e) => new Motor_Faceplate(Program.Motors[9]).Show();
    }
}
