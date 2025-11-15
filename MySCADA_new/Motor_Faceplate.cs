using S7.Net;
using ScottPlot;
using ScottPlot.DataSources;
using ScottPlot.Plottables;
using Svg;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MySCADA.Motor;
using Color = System.Drawing.Color;


namespace MySCADA
{
    public partial class Motor_Faceplate : Form
    {
        private readonly Motor _motor;

        // SVG documents
        private SvgDocument svg1;
        private SvgDocument svg2;

        // Bitmaps for animation
        private Bitmap frame1;
        private Bitmap frame2;

        // Animation control
        private int animationCounter = 0;
        // Trend data buffer
        private readonly List<double> speedHistory = new List<double>();
        private readonly List<double> timeHistory = new List<double>();
        private System.Windows.Forms.Timer trendTimer = null;
        private DateTime startTime;

        public Motor_Faceplate(Motor parent)
        {
            _motor = parent ?? throw new ArgumentNullException(nameof(parent));
            InitializeComponent();

            // 🌀 Đăng ký cập nhật dữ liệu
            _motor.OnDataUpdated += Motor_OnDataUpdated;

            // 📈 Đăng ký trend nếu có
            _motor.OnTrendUpdated += Motor_OnTrendUpdated;

            // 🚨 Đăng ký alarm nếu có
            _motor.AlarmRaised += Motor_AlarmRaised;
            _motor.AlarmCleared += Motor_AlarmCleared;
            _motor.AlarmRaised += (m, info) =>
                Console.WriteLine($"⚡ UI RECEIVED ALARM: {m.Name} - {info.Message}");

        }

        private void Motor_Faceplate_Load(object sender, EventArgs e)
        {
            this.Text = _motor.Name;
            LoadSvgFrames();
            timer1.Interval = 200;
            timer1.Start();

            // Bắt đầu ghi trend từ Motor
            _motor.StartTrend();
            Console.WriteLine($"🧩 Faceplate linked to {_motor.Name}, InstanceHash={_motor.GetHashCode()}");

        }
        private void Motor_OnDataUpdated(Motor motor)
        {
            if (InvokeRequired)
                BeginInvoke((MethodInvoker)(() => UpdateFaceplateDisplay()));
            else
                UpdateFaceplateDisplay();
        }
        private void Motor_OnTrendUpdated(Motor motor)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)(() => UpdateTrendPlot(motor)));
            }
            else
            {
                UpdateTrendPlot(motor);
            }
        }
        private void UpdateTrendPlot(Motor motor)
        {
            formsPlotTrend.Plot.Clear();

            formsPlotTrend.Plot.Add.ScatterLine(
                xs: motor.TimeHistory.ToArray(),
                ys: motor.SpeedHistory.ToArray(),
                color: ScottPlot.Colors.Blue
            );
            formsPlotTrend.Plot.Axes.Left.Min = 0;
            formsPlotTrend.Plot.Axes.Left.Max = 1200; // assuming max speed is 1000 rpm

            formsPlotTrend.Plot.Axes.Left.Label.Text = "Speed (rpm)";
            formsPlotTrend.Plot.Axes.Bottom.Label.Text = "Time (s)";
            formsPlotTrend.Plot.Axes.AutoScaleX();
            formsPlotTrend.Plot.Title($"Speed Trend - {motor.Name}");
            formsPlotTrend.Refresh();
        }

        private void Motor_Faceplate_FormClosing(object sender, FormClosingEventArgs e)
        {
            _motor.StopTrend();
        }
        private void UpdateFaceplateDisplay()
        {
            if (_motor.Status)
            {
                pbStatus.BackColor = Color.Green;
                lbstatus.Text = $"Running {_motor.Speed:0.0} rpm";
                lbstatus.ForeColor = Color.Green;
                pbFan.Image = (animationCounter % 2 == 0) ? frame1 : frame2;
                animationCounter++;
            }
            else
            {
                pbStatus.BackColor = Color.Red;
                lbstatus.Text = "Stopped";
                lbstatus.ForeColor = Color.Red;
                pbFan.Image = frame1;
            }

            pbFan.SizeMode = PictureBoxSizeMode.StretchImage;

            if (Bar_speed != null)
            {
                int val = (int)Math.Max(Bar_speed.Minimum, Math.Min(Bar_speed.Maximum, _motor.Speed));
                Bar_speed.Value = val;
            }
        }

        private void LoadSvgFrames()
        {
            // Load SVG documents
            svg1 = SvgDocument.Open(@"images\Cool fan (animation frame 1).svg");
            svg2 = SvgDocument.Open(@"images\Cool fan (animation frame 2).svg");

            // Change fan blade color to blue
            if (_motor.Status)
            { // If motor is running
                ChangeFanColor(svg1, Color.Green);
                ChangeFanColor(svg2, Color.Green);
            }
            else
            {
                ChangeFanColor(svg1, Color.Gray);
                ChangeFanColor(svg2, Color.Gray);
            }
          
            
            // Render bitmaps
            frame1 = svg1.Draw();
            frame2 = svg2.Draw();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateFaceplateDisplay();
        }
        private void ChangeFanColor(SvgDocument svg, Color color)
        {
            foreach (var elem in svg.Children)
            {
                if (elem is SvgPath path)
                {
                    //  các path của cánh quạt có màu #b2b2b2
                    if (path.Fill is SvgColourServer fill && fill.Colour == ColorTranslator.FromHtml("#b2b2b2"))
                    {
                        path.Fill = new SvgColourServer(color);
                    }
                }
            }
        }

        private bool _isStarting = false;

        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (_isStarting) return; // tránh nhấn liên tục
            _isStarting = true;
            _motor.ShouldBeRunning = true;

            try
            {
                // Bật tín hiệu Start (nhấn nút)
                _motor.Start = true;
                _motor.Stop = false;
                await Task.Run(() => _motor.WriteToPLC());

                // Giữ trong 300–500ms để PLC kịp nhận xung
                await Task.Delay(1000);

                // Nhả tín hiệu Start (thả nút)
                _motor.Start = false;
                await Task.Run(() => _motor.WriteToPLC());

                // Ghi log bật motor
                GeneralInfoManager.RecordOnOff(_motor.Address, _motor.Name, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi gửi tín hiệu khởi động: {ex.Message}");
            }
            finally
            {
                _isStarting = false; // Cho phép nhấn lại
            }
        }


        private bool _isStopping = false;

        private async void btnStop_Click(object sender, EventArgs e)
        {
            if (_isStopping) return; // Ngăn spam click
            _isStopping = true;
            _motor.ShouldBeRunning = false;
            try
            {
                // Bật tín hiệu Stop (nhấn nút)
                _motor.Stop = true;
                await Task.Run(() => _motor.WriteToPLC());

                // Giữ trong 300–500ms để PLC kịp nhận xung
                await Task.Delay(1000);

                // Nhả tín hiệu Stop (thả nút)
                _motor.Stop = false;
                await Task.Run(() => _motor.WriteToPLC());

                // Ghi lại lịch sử thao tác
                GeneralInfoManager.RecordOnOff(_motor.Address, _motor.Name, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi gửi tín hiệu dừng: {ex.Message}");
            }
            finally
            {
                _isStopping = false; // Cho phép nhấn lại
            }
        }


        private async void btnSetSpeed_Click(object sender, EventArgs e)
        {
            string input = input_speed_SP?.Text?.Trim();
            if (!float.TryParse(input, out float sp) || sp < 0 || sp > 1000)
            {
                MessageBox.Show("Please enter a valid speed (0-1000).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                _motor.SetSpeed = sp;
                await Task.Run(() => _motor.WriteToPLC());
                GeneralInfoManager.RecordSetSpeed(_motor.Address, _motor.Name, sp);

                // update trackbar for new value
                if (track_speed != null)
                {
                    // limit range of trackbar
                    int val = (int)Math.Max(track_speed.Minimum, Math.Min(track_speed.Maximum, sp));
                    track_speed.Value = val;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to set speed.\nDetails: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void track_speed_Scroll(object sender, EventArgs e)
        {
            input_speed_SP.Text = track_speed.Value.ToString();
        }

        private void input_speed_SP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSetSpeed.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void Motor_AlarmRaised(Motor motor, AlarmInfo info)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)(() => AddAlarmRow(info)));
                return;
            }
            AddAlarmRow(info);
        }

        private void Motor_AlarmCleared(Motor motor, AlarmInfo info)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)(() => AddAlarmRow(info)));
                return;
            }
            AddAlarmRow(info);
        }

        private void AddAlarmRow(AlarmInfo info)
        {
            Console.WriteLine($"🟢 AddAlarmRow: {info.Message}");

            if (lvAlarms == null || lvAlarms.IsDisposed || !lvAlarms.IsHandleCreated)
            {
                Console.WriteLine("⚠️ lvAlarms not ready yet!");
                return;
            }

            var item = new ListViewItem(info.Time.ToString("HH:mm:ss"));
            item.SubItems.Add(info.Message);
            item.SubItems.Add(info.Severity.ToString());
            item.SubItems.Add(info.Active ? "Active" : "Cleared");

            // màu
            if (info.Active)
            {
                if (info.Severity == AlarmSeverity.Critical)
                    item.BackColor = Color.LightCoral;
                else if (info.Severity == AlarmSeverity.Warning)
                    item.BackColor = Color.Khaki;
                else
                    item.BackColor = Color.LightSteelBlue;
            }
            else
            {
                item.BackColor = Color.Honeydew;
                item.ForeColor = Color.Gray;
            }

            lvAlarms.Items.Insert(0, item);
            lvAlarms.Refresh();
        }


    }
}
