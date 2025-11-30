using EasyModbus;
using System;

public class MeterDevice
{
    // --- THÔNG TIN CƠ BẢN ---
    public int Id { get; set; }
    public string Name { get; set; }
    public string IpAddress { get; set; }
    public int Port { get; set; }

    // --- 12 THÔNG SỐ ĐO (Full Parameter) ---
    public float V { get; set; }     // Điện áp pha trung bình
    public float Vab { get; set; }   // Điện áp dây AB
    public float Vbc { get; set; }   // Điện áp dây BC
    public float Vca { get; set; }   // Điện áp dây CA
    public float I { get; set; }     // Dòng điện trung bình
    public float Ia { get; set; }    // Dòng pha A
    public float Ib { get; set; }    // Dòng pha B
    public float Ic { get; set; }    // Dòng pha C
    public float P { get; set; }     // Công suất tác dụng
    public float Q { get; set; }     // Công suất phản kháng
    public float PF { get; set; }    // Hệ số công suất
    public float E { get; set; }     // Điện năng
    // --- THÊM 3 BIẾN TRẠNG THÁI LOAD ---
    public bool Load1Status { get; set; }
    public bool Load2Status { get; set; }
    public bool Load3Status { get; set; }


    // --- TRẠNG THÁI ---
    public bool IsConnected { get; set; }
    public string StatusMessage { get; set; }

    private ModbusClient _modbusClient;

    public MeterDevice(int id, string name, string ip, int port)
    {
        Id = id;
        Name = name;
        IpAddress = ip;
        Port = port;
        _modbusClient = new ModbusClient(IpAddress, Port)
        {
            ConnectionTimeout = 1000
        };
    }

    // --- HÀM ĐỌC DỮ LIỆU ---
    public void ReadData()
    {
        try
        {
            if (!_modbusClient.Connected) _modbusClient.Connect();

            // Đọc 24 thanh ghi (12 thông số x 2 thanh ghi)
            int[] rawData = _modbusClient.ReadInputRegisters(0, 24);

            // Hàm chuyển đổi nội bộ
            float GetFloat(int startIndex)
            {
                // QUAY VỀ DÙNG HighLow NHƯ CŨ
                return ModbusClient.ConvertRegistersToFloat(
                    new int[] { rawData[startIndex], rawData[startIndex + 1] },
                    ModbusClient.RegisterOrder.HighLow);
            }

            // Gán dữ liệu (Index nhảy cóc 2 đơn vị)
            V = GetFloat(0);
            Vab = GetFloat(2);
            Vbc = GetFloat(4);
            Vca = GetFloat(6);
            I = GetFloat(8);
            Ia = GetFloat(10);
            Ib = GetFloat(12);
            Ic = GetFloat(14);
            P = GetFloat(16);
            Q = GetFloat(18);
            PF = GetFloat(20);
            E = GetFloat(22);

            // 2. ĐỌC TRẠNG THÁI LOAD (Coils) - MỚI THÊM VÀO
            // Đọc 3 Coil bắt đầu từ địa chỉ 0 (tương ứng Load 1, 2, 3)
            bool[] coils = _modbusClient.ReadCoils(0, 3);

            Load1Status = coils[0];
            Load2Status = coils[1];
            Load3Status = coils[2];

            IsConnected = true;
            StatusMessage = "OK";
        }
        catch (Exception ex)
        {
            IsConnected = false;
            StatusMessage = ex.Message;
            if (_modbusClient.Connected) _modbusClient.Disconnect();
        }
    }

    // --- HÀM ĐIỀU KHIỂN TẢI ---
    public void ControlLoad(int loadIndex, bool turnOn)
    {
        try
        {
            if (!_modbusClient.Connected) _modbusClient.Connect();
            _modbusClient.WriteSingleCoil(loadIndex - 1, turnOn);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi Control: " + ex.Message);
        }
    }
}