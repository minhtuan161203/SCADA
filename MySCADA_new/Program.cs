using System;
using System.Windows.Forms;

namespace MySCADA
{
    internal static class Program
    {
        public static SCADA Root { get; private set; }

        [STAThread]
        static void Main()
        {
            Root = new SCADA();

            PLC plc1 = new PLC("PLC_1", "192.168.0.1", motorCount: 10);
            Root.AddPLC(plc1);

            // Motor logic gắn với PLC_1
            Motor m1 = new Motor("Motor_1", plc1, 0, 500);
            Motor m2 = new Motor("Motor_2", plc1, 1, 500);

            plc1.AddMotor(m1);
            plc1.AddMotor(m2);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
