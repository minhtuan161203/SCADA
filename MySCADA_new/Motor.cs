using System;
using System.Collections.Generic;
using System.Timers;

namespace MySCADA
{
    public class Motor
    {
        public string Name { get; }
        public int Address { get; }

        public bool Start { get; set; }
        public bool Stop { get; set; }
        public double SetSpeed { get; set; }

        public bool Status { get; private set; }
        public double Speed { get; private set; }
        public int Position { get; private set; }

        private readonly PLC _parentPLC;
        private readonly Timer _updateTimer;
        private readonly object _lockObj = new object();

        public event Action<Motor> OnDataUpdated;
        // --- Thêm phần trend ---
        private readonly List<double> speedHistory = new List<double>();
        private readonly List<double> timeHistory = new List<double>();
        private DateTime trendStart;
        private Timer trendTimer;

        public IReadOnlyList<double> SpeedHistory => speedHistory;
        public IReadOnlyList<double> TimeHistory => timeHistory;

        public event Action<Motor> OnTrendUpdated; // Sự kiện để Faceplate cập nhật đồ thị

        public void StartTrend()
        {
            trendStart = DateTime.Now;

            trendTimer = new Timer(200); // 200ms update 1 lần
            trendTimer.Elapsed += (s, e) => UpdateTrend();
            trendTimer.Start();
        }

        public void StopTrend()
        {
            trendTimer?.Stop();
        }
        public enum AlarmSeverity { Info, Warning, Critical }

        public enum AlarmId
        {
            HighSpeed,            // Speed vượt ngưỡng
            UnexpectedStop,       // Dừng bất thường
            PlcDisconnected       // Mất kết nối PLC (nếu bạn có cờ IsPlcConnected)
        }
        public event Action<Motor, AlarmInfo> AlarmRaised;
        public event Action<Motor, AlarmInfo> AlarmCleared;

        // Các ngưỡng/biến phục vụ đánh giá
        public double HighSpeedThreshold = 800;    // ví dụ 900 rpm
        public bool ShouldBeRunning = false;       // đặt true khi có lệnh/đang kỳ vọng chạy

        // Trạng thái trước đó để bắt cạnh
        private bool _prevStatus = false;

        // Lưu trạng thái kích hoạt để tránh trigger trùng
        private readonly Dictionary<AlarmId, bool> _activeAlarms = new Dictionary<AlarmId, bool>
        {
            { AlarmId.HighSpeed, false },
            { AlarmId.UnexpectedStop, false },
            { AlarmId.PlcDisconnected, false }
        };
        // Ghi nhớ message cuối cùng của mỗi alarm để tránh raise trùng
        private readonly Dictionary<AlarmId, string> _lastAlarmMessage = new Dictionary<AlarmId, string>();

        private void RaiseAlarm(AlarmId id, string message, AlarmSeverity sev)
        {
            bool isActive = _activeAlarms.TryGetValue(id, out var active) && active;

            // Nếu alarm đã active và message không đổi thì bỏ qua
            if (isActive && _lastAlarmMessage.TryGetValue(id, out var lastMsg) && lastMsg == message)
                return;

            // Lưu lại message hiện tại
            _lastAlarmMessage[id] = message;

            // Nếu chưa active thì mark active
            _activeAlarms[id] = true;

            var info = new AlarmInfo
            {
                Id = id,
                Message = message,
                Severity = sev,
                Time = DateTime.Now,
                Active = true,
                Acknowledged = false
            };

            AlarmRaised?.Invoke(this, info);
            Console.WriteLine($"🚨 Alarm Raised/Updated ({Name}): {message}");
        }

        private void ClearAlarm(AlarmId id, string message)
        {
            bool isActive;
            if (!_activeAlarms.TryGetValue(id, out isActive) || !isActive) return; // đang không active

            _activeAlarms[id] = false;
            var info = new AlarmInfo
            {
                Id = id,
                Message = message,
                Severity = AlarmSeverity.Info,
                Time = DateTime.Now,
                Active = false,
                Acknowledged = true // clear coi như đã kết thúc
            };
            var handler = AlarmCleared;
            if (handler != null) handler(this, info);
        }

        public void EvaluateAlarms()
        {
            // 1) High speed
            if (Speed > HighSpeedThreshold)
                RaiseAlarm(AlarmId.HighSpeed, string.Format("High speed: {0:0.0} rpm", Speed), AlarmSeverity.Warning);
            else
                ClearAlarm(AlarmId.HighSpeed, "High speed cleared");

            // 2) Unexpected stop: status rớt từ true -> false trong khi ShouldBeRunning
            if (_prevStatus && !Status && ShouldBeRunning)
                RaiseAlarm(AlarmId.UnexpectedStop, "Motor stopped unexpectedly", AlarmSeverity.Critical);
            // Clear khi đã chạy lại hoặc không còn kỳ vọng chạy
            if (!ShouldBeRunning || Status)
                ClearAlarm(AlarmId.UnexpectedStop, "Unexpected stop cleared");

            // 3) PLC Disconnected (nếu bạn có cờ IsPlcConnected)
            // if (!IsPlcConnected) RaiseAlarm(AlarmId.PlcDisconnected, "PLC disconnected", AlarmSeverity.Warning);
            // else ClearAlarm(AlarmId.PlcDisconnected, "PLC reconnected");

            // cập nhật prev
            _prevStatus = Status;
        }

        public class AlarmInfo
        {
            public AlarmId Id { get; set; }
            public string Message { get; set; }
            public AlarmSeverity Severity { get; set; }
            public DateTime Time { get; set; }
            public bool Active { get; set; }
            public bool Acknowledged { get; set; }
        }

        private readonly object _trendLock = new object();

        private void UpdateTrend()
        {
            lock (_trendLock)
            {
                double elapsed = (DateTime.Now - trendStart).TotalSeconds;

                // đảm bảo đồng bộ
                timeHistory.Add(elapsed);
                speedHistory.Add(Speed);

                // giới hạn buffer 60s
                while (timeHistory.Count > 60 || speedHistory.Count > 60)
                {
                    if (timeHistory.Count > 0) timeHistory.RemoveAt(0);
                    if (speedHistory.Count > 0) speedHistory.RemoveAt(0);
                }

                // gọi event
                try
                {
                    OnTrendUpdated?.Invoke(this);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ Trend update error: {ex.Message}");
                }
            }
        }

        public Motor(string name, PLC parentPLC, int address, int period = 1000)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            _parentPLC = parentPLC ?? throw new ArgumentNullException(nameof(parentPLC));
            Address = address;

            _updateTimer = new Timer(period);
            _updateTimer.Elapsed += UpdateTimerElapsed;
            _updateTimer.AutoReset = true;
            _updateTimer.Start();
        }

        /// <summary>
        /// Cập nhật dữ liệu motor từ PLC (dữ liệu đã được PLC đọc sẵn)
        /// </summary>
        private void UpdateTimerElapsed(object sender, ElapsedEventArgs e)
        {
            OnDataUpdated?.Invoke(this);
            EvaluateAlarms();
            if (!_parentPLC.Connected) return;
            if (Address < 0 || Address >= _parentPLC.MotorDataList.Count) return;

            lock (_lockObj)
            {
                try
                {
                    var data = _parentPLC.MotorDataList[Address];

                    // đồng bộ dữ liệu ra đối tượng Motor
                    Status = data.Status;
                    Speed = data.Speed;
                    SetSpeed = data.SetSpeed;
                    Position = data.Position;

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ {Name} update error: {ex.Message}");
                }
            }

        }

        // ===== GHI LÊN PLC =====
        public void WriteToPLC()
        {
            WriteToPLC(Start, Stop, SetSpeed);
        }

        public void WriteToPLC(bool start, bool stop, double setSpeed)
        {
            if (!_parentPLC.Connected) return;
            if (Address < 0 || Address >= _parentPLC.MotorDataList.Count) return;

            lock (_lockObj)
            {
                try
                {
                    var data = _parentPLC.MotorDataList[Address];
                    data.Start = start;
                    data.Stop = stop;
                    data.SetSpeed = (float)setSpeed;

                    _parentPLC.WriteMotor(Address);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ WriteToPLC error ({Name}): {ex.Message}");
                }
            }
        }
    }
}
