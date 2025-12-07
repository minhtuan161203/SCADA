using System;
using System.Windows.Forms;

namespace MySCADA
{
    internal static class Program
    {
        // --- CHỈ CẦN 1 BIẾN TOÀN CỤC DUY NHẤT ---
        public static SCADA Root { get; private set; }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 1. KHỞI TẠO ĐỐI TƯỢNG SCADA TỔNG 
            Root = new SCADA();

            // --- A. CẤU HÌNH HỆ THỐNG MOTOR ---
            // Tạo PLC
            PLC plc1 = new PLC("PLC_Motor_Line1", "192.168.0.1", motorCount: 10);

            // Tạo Motor và gắn vào PLC (Ví dụ 2 cái, bạn có thể loop 10 cái nếu muốn)
            Motor m1 = new Motor("Motor_1", plc1, 0, 500);
            Motor m2 = new Motor("Motor_2", plc1, 1, 500);
            plc1.AddMotor(m1);
            plc1.AddMotor(m2);

            // [QUAN TRỌNG] Đưa PLC vào trong Root quản lý
            Root.AddPLC(plc1);


            // --- B. CẤU HÌNH HỆ THỐNG METER ---
            string ipSim = "192.168.0.111";
            int startPort = 502;

            for (int i = 0; i < 15; i++)
            {
                int id = i + 1;
                // Tạo đối tượng Meter
                MeterDevice meter = new MeterDevice(id, $"Meter #{id}", ipSim, startPort + i);

                // Đưa Meter vào trong Root quản lý
                Root.AddMeter(meter);
            }
            Application.Run(new MainForm());
        }
    }
}