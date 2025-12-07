using System;
using System.Windows.Forms;

namespace MySCADA
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            // Căn giữa màn hình khi chạy cho đẹp
            this.StartPosition = FormStartPosition.CenterScreen;

            // Gán sự kiện Click cho nút Motor
            btnMotorSystem.Click += BtnMotorSystem_Click;

            // Gán sự kiện Click cho nút Meter
            btnMeterSystem.Click += BtnMeterSystem_Click;
        }

        // --- XỬ LÝ NÚT MOTOR SYSTEM ---
        private void BtnMotorSystem_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem class Form_Motor_Device có tồn tại và đúng tên không
            // (Dựa trên hình ảnh bạn gửi là Monitoring Pannel, tôi đoán tên class là Form_Motor_Device)
            try
            {
                Form_Motor_Device motorForm = new Form_Motor_Device();

                // Ẩn Form chính đi (tùy chọn, để nhìn cho gọn)
                this.Hide();

                // Hiện Form Motor lên. Dùng ShowDialog để khi tắt Form Motor mới quay lại đây được
                motorForm.ShowDialog();

                // Khi Form Motor tắt thì hiện lại Form chính
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở Form Motor: " + ex.Message);
                this.Show();
            }
        }

        // --- XỬ LÝ NÚT METER SYSTEM ---
        private void BtnMeterSystem_Click(object sender, EventArgs e)
        {
            try
            {
                // Mở Form Meter (Form tổng 15 cái mà bạn vừa làm xong)
                Form_Meter_devices meterForm = new Form_Meter_devices();

                this.Hide();
                meterForm.ShowDialog();
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở Form Meter: " + ex.Message);
                this.Show();
            }
        }
    }
}