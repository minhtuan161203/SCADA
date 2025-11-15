using S7.Net;
using System;
using System.Collections.Generic;
using System.Timers;

namespace MySCADA
{
    /// <summary>
    /// Đại diện cho 1 PLC thật trong hệ thống SCADA.
    /// Tự động đọc dữ liệu Motor_Data từ nhiều DB riêng biệt (DB1 → DB10).
    /// </summary>
    public class PLC : IDisposable
    {
        // ========== THUỘC TÍNH ==========
        public string Name { get; }
        public string IPAddress { get; }
        public bool Connected => _plc != null && _plc.IsConnected;

        // ========== BIẾN NỘI BỘ ==========
        private readonly Plc _plc;                        // Kết nối thật tới PLC
        private readonly List<Motor_Data> _motorDataList; // Lưu dữ liệu DB
        private readonly List<Motor> _motors;             // Danh sách motor logic (OOP)
        private readonly Timer _updateTimer;
        private readonly object _lockObj = new object();

        // ========== TRUY CẬP CÔNG KHAI ==========
        public IReadOnlyList<Motor_Data> MotorDataList => _motorDataList.AsReadOnly();
        public IReadOnlyList<Motor> Motors => _motors.AsReadOnly();

        // ========== CONSTRUCTOR ==========
        public PLC(string name, string ip, int motorCount = 10)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            IPAddress = ip ?? throw new ArgumentNullException(nameof(ip));

            _plc = new Plc(CpuType.S71500, IPAddress, 0, 1);
            _motorDataList = new List<Motor_Data>();
            _motors = new List<Motor>();

            for (int i = 0; i < motorCount; i++)
                _motorDataList.Add(new Motor_Data());

            _updateTimer = new Timer(1000);
            _updateTimer.Elapsed += UpdateTimer_Elapsed;
        }

        // ========== KẾT NỐI / NGẮT KẾT NỐI ==========
        public void Connect()
        {
            try
            {
                _plc.Open();
                if (_plc.IsConnected)
                {
                    Console.WriteLine($"✅ {Name} connected at {IPAddress}");
                    _updateTimer.Start();
                }
                else
                {
                    Console.WriteLine($"⚠️ {Name} connection failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ PLC {Name} connect error: {ex.Message}");
            }
        }

        public void Disconnect()
        {
            try
            {
                _updateTimer.Stop();
                if (_plc.IsConnected)
                    _plc.Close();

                Console.WriteLine($"🔌 {Name} disconnected.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Disconnect error: {ex.Message}");
            }
        }

        // ========== ĐỌC DỮ LIỆU TỪ PLC ==========
        private void UpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_plc.IsConnected) return;

            lock (_lockObj)
            {
                try
                {
                    for (int i = 0; i < _motorDataList.Count; i++)
                    {
                        int dbNumber = i + 1; // DB1, DB2, DB3...
                        _plc.ReadClass(_motorDataList[i], dbNumber);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ PLC {Name} read error: {ex.Message}");
                }
            }
        }

        // ========== GHI DỮ LIỆU LÊN PLC ==========
        public void WriteMotor(int index)
        {
            if (!_plc.IsConnected) return;
            if (index < 0 || index >= _motorDataList.Count) return;

            try
            {
                int dbNumber = index + 1;
                _plc.WriteClass(_motorDataList[index], dbNumber);
                Console.WriteLine($"➡️ Write to DB{dbNumber} successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ PLC {Name} write error: {ex.Message}");
            }
        }

        // ========== THÊM MOTOR LOGIC ==========
        public void AddMotor(Motor motor)
        {
            if (motor == null) throw new ArgumentNullException(nameof(motor));
            _motors.Add(motor);
        }

        // ========== GIẢI PHÓNG ==========
        public void Dispose()
        {
            _updateTimer?.Stop();
            _updateTimer?.Dispose();
            if (_plc.IsConnected) _plc.Close();
        }
    }

    // ======= CẤU TRÚC MOTOR_DATA KHỚP VỚI DB TRONG ẢNH CỦA BẠN =======
    public class Motor_Data
    {
        public bool Start { get; set; }      // offset 0.0
        public bool Stop { get; set; }       // offset 0.1
        public float SetSpeed { get; set; }  // offset 2.0
        public bool Status { get; set; }     // offset 6.0
        public float Speed { get; set; }     // offset 8.0
        public short Position { get; set; }  // offset 12.0
    }
}
