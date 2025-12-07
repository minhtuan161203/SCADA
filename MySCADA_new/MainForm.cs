using System;
using System.Linq; // Cần thêm cái này để tìm Form nhanh hơn
using System.Windows.Forms;

namespace MySCADA
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            // Gán sự kiện
            btnMotorSystem.Click += BtnMotorSystem_Click;
            btnMeterSystem.Click += BtnMeterSystem_Click;
        }

        // --- HÀM XỬ LÝ CHUNG: MỞ HOẶC FOCUS FORM ---
        // Hàm này giúp tránh việc mở 10 cái cửa sổ giống nhau
        private void OpenFormOfType<T>() where T : Form, new()
        {
            // 1. Tìm xem trong danh sách các Form đang mở, có cái nào kiểu T chưa?
            T existingForm = Application.OpenForms.OfType<T>().FirstOrDefault();

            if (existingForm != null)
            {
                // 2. Nếu ĐÃ MỞ rồi:
                // Nếu đang bị minimize thì trả về bình thường
                if (existingForm.WindowState == FormWindowState.Minimized)
                {
                    existingForm.WindowState = FormWindowState.Normal;
                }
                // Đưa nó lên trên cùng (Focus)
                existingForm.Activate();
            }
            else
            {
                // 3. Nếu CHƯA MỞ: Tạo mới và Show
                T newForm = new T();
                newForm.Show(); // Dùng .Show() để mở song song
            }
        }

        // --- XỬ LÝ NÚT MOTOR SYSTEM ---
        private void BtnMotorSystem_Click(object sender, EventArgs e)
        {
            // Gọi hàm mở thông minh cho Form Motor
            OpenFormOfType<Form_Motor_Device>();
        }

        // --- XỬ LÝ NÚT METER SYSTEM ---
        private void BtnMeterSystem_Click(object sender, EventArgs e)
        {
            // Gọi hàm mở thông minh cho Form Meter
            OpenFormOfType<Form_Meter_devices>();
        }
    }
}