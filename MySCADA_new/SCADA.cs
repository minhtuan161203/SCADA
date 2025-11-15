using System;
using System.Collections.Generic;
using System.Linq;

namespace MySCADA
{
    public class SCADA
    {
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
            return _plcs.SelectMany(p => p.Motors).FirstOrDefault(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
