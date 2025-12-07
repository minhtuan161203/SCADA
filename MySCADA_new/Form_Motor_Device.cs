using Svg;
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
    public partial class Form_Motor_Device : Form
    {
        // Mảng Label trạng thái
        public Label[] motorStatusLabels;

        // Biến cập nhật ListView
        private bool lvPauseUpdates = false;

        public Form_Motor_Device()
        {
            InitializeComponent();
        }

        // --- SỰ KIỆN LOAD FORM ---
        private void Form1_Load(object sender, EventArgs e)
        {
            // 1. Tự động kết nối khi mở Form
            connect_btn_Click(null, EventArgs.Empty);

            // 2. Gom nhóm các Label trạng thái vào mảng
            motorStatusLabels = new Label[]
            {
                labelst_1, labelst_2, labelst_3, labelst_4, labelst_5,
                labelst_6, labelst_7, labelst_8, labelst_9, labelst_10
            };

            // 3. Đăng ký sự kiện cập nhật dữ liệu cho 10 Motor
            // [UPDATE] Lấy danh sách Motor thông qua PLC trong Root
            var plc = Program.Root.FindPLC("PLC_Motor_Line1");

            if (plc != null && plc.Motors != null)
            {
                foreach (var motor in plc.Motors)
                {
                    // Khi motor có dữ liệu mới -> gọi hàm Motor_OnDataUpdated
                    motor.OnDataUpdated += Motor_OnDataUpdated;
                }
            }

            // 4. Cập nhật giao diện lần đầu
            UpdateAllMotorStatus();
        }

        // --- XỬ LÝ KẾT NỐI PLC ---
        private async void connect_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // [UPDATE] Tìm PLC trong Root
                var plc = Program.Root.FindPLC("PLC_Motor_Line1");

                if (plc == null)
                {
                    MessageBox.Show("Chưa khởi tạo PLC Motor trong Program.cs!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Chạy kết nối trong luồng riêng (Task)
                await Task.Run(() => plc.Connect());

                if (plc.Connected)
                {
                    // Update giao diện nếu cần
                }
                else
                {
                    MessageBox.Show("Không thể kết nối tới PLC Motor.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- CẬP NHẬT UI KHI CÓ DỮ LIỆU MỚI (EVENT) ---
        private void Motor_OnDataUpdated(Motor motor)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)(() => UpdateMotorLabel(motor)));
            }
            else
            {
                UpdateMotorLabel(motor);
            }
        }

        // --- CẬP NHẬT LABEL TRẠNG THÁI TỪNG MOTOR ---
        private void UpdateMotorLabel(Motor motor)
        {
            if (motor == null || motorStatusLabels == null) return;

            try
            {
                // Cắt chuỗi lấy số ID: "Motor_1" -> Index 0
                string idString = motor.Name.Replace("Motor_", "");
                int index = int.Parse(idString) - 1;

                if (index >= 0 && index < motorStatusLabels.Length)
                {
                    var label = motorStatusLabels[index];

                    if (motor.Status) // Đang chạy
                    {
                        label.Text = $"Running ({motor.Speed:0.0} rpm)";
                        label.ForeColor = Color.Green;
                    }
                    else // Đang dừng
                    {
                        label.Text = "Stopped";
                        label.ForeColor = Color.Red;
                    }
                }
            }
            catch { }

            UpdateRunningStoppedCount();
        }

        private void UpdateAllMotorStatus()
        {
            var plc = Program.Root.FindPLC("PLC_Motor_Line1");
            if (plc?.Motors == null) return;

            foreach (var motor in plc.Motors)
            {
                UpdateMotorLabel(motor);
            }
        }

        // --- ĐẾM SỐ LƯỢNG MOTOR CHẠY/DỪNG ---
        private void UpdateRunningStoppedCount()
        {
            var plc = Program.Root.FindPLC("PLC_Motor_Line1");
            if (plc?.Motors == null) return;

            int runningCount = plc.Motors.Count(m => m.Status);
            int stoppedCount = plc.Motors.Count(m => !m.Status);

            if (labelRunningCount != null)
            {
                labelRunningCount.Text = $"Running: {runningCount}";
                labelRunningCount.ForeColor = Color.Green;
            }

            if (labelStoppedCount != null)
            {
                labelStoppedCount.Text = $"Stopped: {stoppedCount}";
                labelStoppedCount.ForeColor = Color.Red;
            }
        }

        // --- TIMER UPDATE LISTVIEW LOG ---
        private void MonitorTimer_Tick(object sender, EventArgs e)
        {
            if (lvActions != null && !lvPauseUpdates)
            {
                LoadRecentActions(100);
            }
        }

        private void timer_date_Tick(object sender, EventArgs e)
        {
            lbClock.Text = DateTime.Now.ToString("HH:mm:ss\ndd/MM/yyyy");
        }

        // --- LOAD LISTVIEW ---
        private void LoadRecentActions(int limit = 100)
        {
            if (lvActions == null) return;

            var actions = GeneralInfoManager.GetRecentActions(limit);

            lvActions.BeginUpdate();
            lvActions.Items.Clear();

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
        }

        // --- CÁC SỰ KIỆN CLICK NÚT MỞ FACEPLATE ---
        private void OpenFaceplateFor(string motorName)
        {
            // [UPDATE] Tìm Motor thông qua Root
            var motor = Program.Root.FindMotor(motorName);

            if (motor != null)
            {
                new Motor_Faceplate(motor).Show();
            }
            else
            {
                MessageBox.Show($"Không tìm thấy {motorName}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btMotor_1_Click(object sender, EventArgs e) => OpenFaceplateFor("Motor_1");
        private void btMotor_2_Click(object sender, EventArgs e) => OpenFaceplateFor("Motor_2");
        private void btMotor_3_Click(object sender, EventArgs e) => OpenFaceplateFor("Motor_3");
        private void btMotor_4_Click(object sender, EventArgs e) => OpenFaceplateFor("Motor_4");
        private void btMotor_5_Click(object sender, EventArgs e) => OpenFaceplateFor("Motor_5");
        private void btMotor_6_Click(object sender, EventArgs e) => OpenFaceplateFor("Motor_6");
        private void btMotor_7_Click(object sender, EventArgs e) => OpenFaceplateFor("Motor_7");
        private void btMotor_8_Click(object sender, EventArgs e) => OpenFaceplateFor("Motor_8");
        private void btMotor_9_Click(object sender, EventArgs e) => OpenFaceplateFor("Motor_9");
        private void btMotor_10_Click(object sender, EventArgs e) => OpenFaceplateFor("Motor_10");
    }
}