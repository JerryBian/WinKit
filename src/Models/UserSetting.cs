using MessagePack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace WinKit.Models
{
    [MessagePackObject]
    public class UserSetting
    {
        #region AutoMouseMover

        [Key(100)]
        public bool AutoMouseMoverEnabled { get; set; }

        [Key(101)]
        public int AutoMouseMoverIntervalSeconds { get; set; }

        [Key(102)]
        public int AutoMouseMoverPixel { get; set; }

        #endregion

        public static UserSetting CreateDefault()
        {
            return new UserSetting
            {
                AutoMouseMoverEnabled = false,
                AutoMouseMoverIntervalSeconds = 30,
                AutoMouseMoverPixel = 50
            };
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = false });
        }
    }
}
