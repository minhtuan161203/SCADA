using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySCADA
{
    public partial class Form_Meter_devices : Form
    {
        public Form_Meter_devices()
        {
            InitializeComponent();

            // 1. CẤU HÌNH NÚT BẤM
            SetupButtons();

            // 2. Cấu hình Timer
            tmrUpdate.Interval = 1000;
            tmrUpdate.Tick += TmrUpdate_Tick;
            tmrUpdate.Start();
        }

        private void SetupButtons()
        {
            // Kiểm tra xem danh sách toàn cục có dữ liệu chưa
            if (Program.Root.Meters == null || Program.Root.Meters.Count == 0) return;

            // Duyệt qua 15 đồng hồ
            foreach (var meter in Program.Root.Meters)
            {
                // Tìm nút tương ứng trên giao diện (btnMeter1, btnMeter2...)
                string btnName = $"btnMeter{meter.Id}";
                Control[] found = this.Controls.Find(btnName, true);

                if (found.Length > 0 && found[0] is Button btn)
                {
                    // Gỡ sự kiện cũ và gán sự kiện mới
                    btn.Click -= null;

                    // gán object meter vào nút
                    MeterDevice localMeter = meter;
                    btn.Click += (s, e) => OpenFaceplate(localMeter);
                }
            }
        }

        // --- SỰ KIỆN TIMER: CẬP NHẬT GIAO DIỆN ---
        private async void TmrUpdate_Tick(object sender, EventArgs e)
        {
            // 1. Cập nhật đồng hồ thời gian thực
            if (lbClock != null) lbClock.Text = DateTime.Now.ToString("HH:mm:ss");

            // 2. Quét dữ liệu từ danh sách
            if (Program.Root.Meters == null) return;

            foreach (var meter in Program.Root.Meters)
            {
                // Gọi hàm đọc
                await Task.Run(() => meter.ReadData());

                int id = meter.Id;

                // --- CẬP NHẬT LABEL VOLTAGE ---
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
                        lblVolts[0].Text = "---";
                        lblVolts[0].ForeColor = Color.Gray;
                    }
                }

                // --- CẬP NHẬT LABEL POWER ---
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
        private void OpenFaceplate(MeterDevice meter)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is frmMeterFaceplate faceplate && faceplate.Text == meter.Name)
                {
                    faceplate.Focus();
                    return;
                }
            }

            // Nếu chưa mở thì mở mới, truyền meter toàn cục
            frmMeterFaceplate frm = new frmMeterFaceplate(meter);
            frm.Show();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            tmrUpdate.Stop();
            base.OnFormClosing(e);
        }
    }
}