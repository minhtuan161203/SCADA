using System;
using System.Collections.Generic;
using System.Linq;

namespace MySCADA
{
    public static class IpHistoryManager
    {
        private const int MaxEntries = 5;  // tối đa 5 IP gần nhất
        private const char Sep = ';';       // ký tự ngăn cách

        public static List<string> GetHistory()
        {
            string raw = Properties.Settings.Default.IpHistory ?? string.Empty;

            if (string.IsNullOrWhiteSpace(raw))
                return new List<string>();

            // dùng Split với char và StringSplitOptions chuẩn
            return raw.Split(new char[] { Sep }, StringSplitOptions.RemoveEmptyEntries)
                      .Distinct(StringComparer.OrdinalIgnoreCase)
                      .ToList();
        }

        public static void AddToHistory(string ip)
        {
            if (string.IsNullOrWhiteSpace(ip))
                return;

            var list = GetHistory();

            // loại bỏ IP cũ nếu có và thêm mới vào đầu
            list = list.Where(x => !string.Equals(x, ip, StringComparison.OrdinalIgnoreCase)).ToList();
            list.Insert(0, ip);

            // giữ số lượng IP giới hạn
            if (list.Count > MaxEntries)
                list = list.Take(MaxEntries).ToList();

            // join lại thành string và lưu
            Properties.Settings.Default.IpHistory = string.Join(Sep.ToString(), list);
            Properties.Settings.Default.Save();
        }

        public static void ClearHistory()
        {
            Properties.Settings.Default.IpHistory = string.Empty;
            Properties.Settings.Default.Save();
        }
    }
}
