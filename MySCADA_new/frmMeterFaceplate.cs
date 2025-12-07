using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySCADA
{
    public partial class frmMeterFaceplate : Form
    {
        private MeterDevice _meter;
        private Timer tmrUpdate;

        public frmMeterFaceplate(MeterDevice meterInput)
        {
            InitializeComponent();
            _meter = meterInput;
            this.Text = _meter.Name;

            // 1. TẠO TIMER
            tmrUpdate = new Timer();
            tmrUpdate.Interval = 1000;
            tmrUpdate.Tick += TmrUpdate_Tick;
            tmrUpdate.Start();

            // 2. GÁN SỰ KIỆN NÚT BẤM
            btnLoad1.Click += (s, e) => ToggleLoad(1, _meter.Load1Status);
            btnLoad2.Click += (s, e) => ToggleLoad(2, _meter.Load2Status);
            btnLoad3.Click += (s, e) => ToggleLoad(3, _meter.Load3Status);

            // 3. TRANG TRÍ MÀU SẮC
            ApplyVisualStyles();
        }

        private void ApplyVisualStyles()
        {
            Font boldFont = new Font("Segoe UI", 10, FontStyle.Bold);

            // Set màu và Font cho các nhóm
            Color colorVolt = Color.DarkGreen;
            SetStyle(lblV, colorVolt, boldFont);
            SetStyle(lblVab, colorVolt, boldFont);
            SetStyle(lblVbc, colorVolt, boldFont);
            SetStyle(lblVca, colorVolt, boldFont);

            Color colorAmp = Color.Navy;
            SetStyle(lblI, colorAmp, boldFont);
            SetStyle(lblIa, colorAmp, boldFont);
            SetStyle(lblIb, colorAmp, boldFont);
            SetStyle(lblIc, colorAmp, boldFont);

            Color colorPower = Color.Crimson;
            SetStyle(lblP, colorPower, new Font("Segoe UI", 12, FontStyle.Bold)); // P to nhất
            SetStyle(lblQ, colorPower, boldFont);

            Color colorEnergy = Color.DarkMagenta;
            SetStyle(lblPF, colorEnergy, boldFont);
            SetStyle(lblE, colorEnergy, boldFont);
        }

        // Hàm phụ
        private void SetStyle(Label lbl, Color c, Font f)
        {
            lbl.ForeColor = c;
            lbl.Font = f;
            lbl.TextAlign = ContentAlignment.MiddleLeft;
            lbl.AutoSize = true; // Tự giãn kích thước
        }

        private async void TmrUpdate_Tick(object sender, EventArgs e)
        {
            await Task.Run(() => _meter.ReadData());

            if (_meter.IsConnected)
            {
                lblConnect.Text = "Connected";
                lblConnect.ForeColor = Color.Green;

                // Nhóm Voltage (V, Vab...)
                lblV.Text = $"V:     {_meter.V:F1} V";
                lblVab.Text = $"Vab: {_meter.Vab:F1} V";
                lblVbc.Text = $"Vbc: {_meter.Vbc:F1} V";
                lblVca.Text = $"Vca: {_meter.Vca:F1} V";

                // Nhóm Current (I, Ia...)
                lblI.Text = $"I:      {_meter.I:F2} A";
                lblIa.Text = $"Ia:    {_meter.Ia:F2} A";
                lblIb.Text = $"Ib:    {_meter.Ib:F2} A";
                lblIc.Text = $"Ic:    {_meter.Ic:F2} A";

                // Nhóm Power (P, Q...)
                lblP.Text = $"P:   {_meter.P:F2} kW";
                lblQ.Text = $"Q:   {_meter.Q:F2} kVAR";
                lblPF.Text = $"PF: {_meter.PF:F2}";
                lblE.Text = $"E:   {_meter.E:F1} kWh";

                // Cập nhật nút
                UpdateButtonColor(btnLoad1, _meter.Load1Status);
                UpdateButtonColor(btnLoad2, _meter.Load2Status);
                UpdateButtonColor(btnLoad3, _meter.Load3Status);
            }
            else
            {
                lblConnect.Text = "Disconnected";
                lblConnect.ForeColor = Color.Red;
                lblV.Text = "V: ---";
                lblP.Text = "P: ---";
            }
        }

        private void UpdateButtonColor(Button btn, bool isOn)
        {
            if (isOn)
            {
                btn.BackColor = Color.Red;
                btn.ForeColor = Color.White;
                btn.Text = "ON";
            }
            else
            {
                btn.BackColor = Color.LightGray;
                btn.ForeColor = Color.Black;
                btn.Text = "OFF";
            }
        }

        private async void ToggleLoad(int index, bool currentStatus)
        {
            bool targetState = !currentStatus;
            await Task.Run(() =>
            {
                _meter.ControlLoad(index, targetState);
                _meter.ReadData();
            });
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            tmrUpdate.Stop();
            base.OnFormClosing(e);
        }
    }
}