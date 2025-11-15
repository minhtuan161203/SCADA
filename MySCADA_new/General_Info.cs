using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MySCADA
{
    public class ActionLogEntry
    {
        public DateTime Time { get; set; }
        public int MotorIndex { get; set; }
        public string MotorName { get; set; }
        public string Action { get; set; } // "Start", "Stop", "SetSpeed"
        public string Value { get; set; } // e.g. "500"
    }

    public class MotorStats
    {
        public int OnCountToday;
        public int OffCountToday;
    }

    public static class GeneralInfoManager
    {
        static readonly object _lock = new object();
        static List<ActionLogEntry> _actions = new List<ActionLogEntry>();
        static Dictionary<int, MotorStats> _stats = new Dictionary<int, MotorStats>();
        static DateTime _lastResetDate = DateTime.Today;

        public static void RecordOnOff(int motorIndex, string motorName, bool isOn)
        {
            EnsureDailyReset();

            lock (_lock)
            {
                if (!_stats.TryGetValue(motorIndex, out var s))
                {
                    s = new MotorStats();
                    _stats[motorIndex] = s;
                }

                if (isOn) s.OnCountToday++;
                else s.OffCountToday++;

                _actions.Add(new ActionLogEntry
                {
                    Time = DateTime.Now,
                    MotorIndex = motorIndex,
                    MotorName = motorName,
                    Action = isOn ? "Start" : "Stop",
                    Value = ""
                });

                // keep list bounded
                if (_actions.Count > 1000) _actions.RemoveRange(0, _actions.Count - 1000);
            }
        }

        public static void RecordSetSpeed(int motorIndex, string motorName, double setSpeed)
        {
            EnsureDailyReset();

            lock (_lock)
            {
                _actions.Add(new ActionLogEntry
                {
                    Time = DateTime.Now,
                    MotorIndex = motorIndex,
                    MotorName = motorName,
                    Action = "SetSpeed",
                    Value = setSpeed.ToString("0.0")
                });

                if (_actions.Count > 1000) _actions.RemoveRange(0, _actions.Count - 1000);
            }
        }

        static void EnsureDailyReset()
        {
            var today = DateTime.Today;
            if (today != _lastResetDate)
            {
                lock (_lock)
                {
                    if (today != _lastResetDate)
                    {
                        _stats.Clear();
                        _actions.Clear();
                        _lastResetDate = today;
                    }
                }
            }
        }

        public static List<ActionLogEntry> GetRecentActions(int limit = 100)
        {
            lock (_lock)
            {
                int take = Math.Min(limit, _actions.Count);
                return _actions.Skip(Math.Max(0, _actions.Count - take)).ToList();
            }
        }

        public static MotorStats GetMotorStats(int motorIndex)
        {
            lock (_lock)
            {
                return _stats.TryGetValue(motorIndex, out var s) ? s : new MotorStats();
            }
        }
    }
}