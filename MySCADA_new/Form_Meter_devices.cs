using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySCADA
{
    public partial class Form_Meter_devices : Form
    {
        // Danh sách quản lý 15 đồng hồ
        private List<MeterDevice> _meterList = new List<MeterDevice>();

        public Form_Meter_devices()
        {
            InitializeComponent(); // Hàm gọi code Designer

            // 1. Khởi tạo kết nối và sự kiện
            InitializeSystem();

            // 2. Cấu hình Timer
            tmrUpdate.Interval = 1000; // 1 giây quét 1 lần
            tmrUpdate.Tick += TmrUpdate_Tick; // Gán hàm xử lý
            tmrUpdate.Start();
        }

        private void InitializeSystem()
        {
            // CẤU HÌNH KẾT NỐI
            string ipSimulator = "192.168.0.111";
            int startPort = 502;

            // Vòng lặp tạo 15 đồng hồ
            for (int i = 0; i < 15; i++)
            {
                int id = i + 1;
                int port = startPort + i; // Port tăng dần: 502, 503...

                // Tạo đối tượng MeterDevice
                MeterDevice newMeter = new MeterDevice(id, $"Meter #{id}", ipSimulator, port);
                _meterList.Add(newMeter);

                // --- TỰ ĐỘNG GÁN SỰ KIỆN CLICK CHO NÚT DETAIL ---
                // Tìm nút có tên btnMeter1, btnMeter2...
                string btnName = $"btnMeter{id}";
                Control[] found = this.Controls.Find(btnName, true);

                if (found.Length > 0 && found[0] is Button btn)
                {
                    // Gán sự kiện: Khi nhấn nút -> Mở Faceplate
                    // Dùng biến localMeter để tránh lỗi Closure trong vòng lặp
                    MeterDevice localMeter = newMeter;
                    btn.Click += (s, e) => OpenFaceplate(localMeter);
                }
            }
        }

        // --- SỰ KIỆN TIMER: CẬP NHẬT GIAO DIỆN ---
        private async void TmrUpdate_Tick(object sender, EventArgs e)
        {
            // 1. Cập nhật đồng hồ thời gian thực
            lbClock.Text = DateTime.Now.ToString("HH:mm:ss");

            // 2. Quét dữ liệu 15 đồng hồ
            foreach (var meter in _meterList)
            {
                // Đọc dữ liệu (Chạy ngầm - Async)
                await Task.Run(() => meter.ReadData());

                int id = meter.Id;

                // --- CẬP NHẬT LABEL VOLTAGE (lblVolt1...) ---
                Control[] lblVolts = this.Controls.Find($"lblVolt{id}", true);
                if (lblVolts.Length > 0)
                {
                    if (meter.IsConnected)
                    {
                        lblVolts[0].Text = $"{meter.V:F0} V";
                        lblVolts[0].ForeColor = Color.DarkGreen;
                    }
                    else
                    {
                        lblVolts[0].Text = "---"; // Mất kết nối
                        lblVolts[0].ForeColor = Color.Gray;
                    }
                }

                // --- CẬP NHẬT LABEL POWER (lblPower1...) ---
                Control[] lblPowers = this.Controls.Find($"lblPower{id}", true);
                if (lblPowers.Length > 0)
                {
                    if (meter.IsConnected)
                    {
                        lblPowers[0].Text = $"{meter.P:F1} kW";
                        lblPowers[0].ForeColor = Color.Red;
                    }
                    else
                    {
                        lblPowers[0].Text = "---";
                    }
                }
            }
        }

        // --- HÀM MỞ FORM CHI TIẾT (FACEPLATE) ---
        private void OpenFaceplate(MeterDevice meter)
        {
            // Kiểm tra xem Form của đồng hồ đã mở chưa?
            foreach (Form f in Application.OpenForms)
            {
                if (f is frmMeterFaceplate faceplate && faceplate.Text == meter.Name)
                {
                    faceplate.Focus();
                    return;
                }
            }

            // If not, open this
            frmMeterFaceplate frm = new frmMeterFaceplate(meter);
            frm.Show();
        }

        // Stop timer when close form
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            tmrUpdate.Stop();
            base.OnFormClosing(e);
        }
    }
}