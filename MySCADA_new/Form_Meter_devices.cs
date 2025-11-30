using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySCADA
{
    public partial class Form_Meter_devices : Form
    {
        private MeterDevice meter1;
        public Form_Meter_devices()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            // Cấu hình cứng theo Simulator của bạn
            string ip = "192.168.0.111";
            int port = 502; // Port 502 là Meter 1, 503 là Meter 2...

            Console.WriteLine($"\n--- BẮT ĐẦU TEST KẾT NỐI ĐẾN {ip}:{port} ---");

            // 1. Khởi tạo
            if (meter1 == null)
            {
                meter1 = new MeterDevice(1, "Meter Test", ip, port);
            }

            // 2. Gọi hàm đọc
            meter1.ReadData();

            // 3. In kết quả ra Console
            if (meter1.IsConnected)
            {
                Console.WriteLine(">>> KẾT QUẢ: THÀNH CÔNG");
                Console.WriteLine($"Voltage: {meter1.V} V");
                Console.WriteLine($"Current: {meter1.I} A");
                Console.WriteLine($"Power:   {meter1.P} kW");
                Console.WriteLine($"Load1:   {meter1.Load1Status} ");
                Console.WriteLine($"Load2:   {meter1.Load2Status} ");
                Console.WriteLine($"Load3:   {meter1.Load3Status} ");
            }
            else
            {
                Console.WriteLine(">>> KẾT QUẢ: THẤT BẠI");
                Console.WriteLine($"Lỗi chi tiết: {meter1.StatusMessage}");
            }

            Console.WriteLine("------------------------------------------------");
        }

        private async void btnLoad_Click(object sender, EventArgs e)
        {
            // 1. Xác định trạng thái muốn set (Đảo ngược trạng thái hiện tại)
            // Nếu Load1Status đang là true (Bật) -> targetState sẽ là false (Tắt) và ngược lại
            bool targetState = !meter1.Load1Status;

            // 2. Gửi lệnh điều khiển xuống thiết bị
            // Dùng Task.Run để chạy ngầm, không làm đơ giao diện khi chờ mạng
            await Task.Run(() =>
            {
                // Tham số 1: Index của Load (1 tương ứng Load 1)
                // Tham số 2: Trạng thái muốn set (true/false)
                meter1.ControlLoad(1, targetState);
            });

            // LƯU Ý: 
            // Chúng ta KHÔNG đổi màu nút ở đây.
            // Màu nút sẽ tự đổi khi Timer (tmrUpdate) đọc được trạng thái mới từ Simulator gửi về.
            // Đây là nguyên tắc "Feedback" chuẩn trong SCADA.
        }
    }
}
