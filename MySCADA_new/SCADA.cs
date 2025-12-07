using System;
using System.Collections.Generic;
using System.Linq;

namespace MySCADA
{
    public class SCADA
    {
        // --- 1. QUẢN LÝ PLC & MOTOR  ---
        private readonly List<PLC> _plcs = new List<PLC>();
        public IReadOnlyList<PLC> PLCs => _plcs.AsReadOnly();

        public void AddPLC(PLC plc)
        {
            if (plc == null) throw new ArgumentNullException(nameof(plc));
            _plcs.Add(plc);
        }

        public PLC FindPLC(string name)
        {
            return _plcs.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Motor> GetAllMotors()
        {
            return _plcs.SelectMany(p => p.Motors);
        }

        public Motor FindMotor(string name)
        {
            return _plcs.SelectMany(p => p.Motors)
                        .FirstOrDefault(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        // --- 2. QUẢN LÝ METER  ---

        // List chứa các đồng hồ
        private readonly List<MeterDevice> _meters = new List<MeterDevice>();

        // Property để các Form khác lấy danh sách đồng hồ read only
        public IReadOnlyList<MeterDevice> Meters => _meters.AsReadOnly();

        // Hàm thêm đồng hồ
        public void AddMeter(MeterDevice meter)
        {
            if (meter == null) throw new ArgumentNullException(nameof(meter));
            _meters.Add(meter);
        }

        // Hàm tìm kiếm đồng hồ theo Tên
        public MeterDevice FindMeter(string name)
        {
            return _meters.FirstOrDefault(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        // Hàm tìm kiếm đồng hồ theo ID
        public MeterDevice FindMeterById(int id)
        {
            return _meters.FirstOrDefault(m => m.Id == id);
        }
    }
}