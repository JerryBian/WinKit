using MessagePack;
using System.Text.Json;

namespace WinKit.Models
{
    [MessagePackObject]
    public class UserSetting
    {
        [Key(10)]
        public DateTime CreatedAt { get; set; }

        [Key(11)]
        public DateTime LastUpdatedAt { get; set; }

        #region AutoMouseMover

        [Key(100)]
        public bool AutoMouseMoverEnabled { get; set; }

        [Key(101)]
        public int AutoMouseMoverIntervalSeconds { get; set; }

        [Key(102)]
        public int AutoMouseMoverPixel { get; set; }

        [Key(103)]
        public int AutoMouseMoverDisableAfterInMinutes { get; set; }

        [Key(104)]
        public bool AutoMouseMoverClickEnabled { get; set; }

        #endregion

        #region AutoShutdownPC

        [Key(200)]
        public bool AutoShutdownPCEnabled { get; set; }

        [Key(201)]
        public int AutoShutdownPCAfterMinutes { get; set; }

        #endregion

        public static UserSetting CreateDefault()
        {
            return new UserSetting
            {
                CreatedAt = DateTime.Now,
                LastUpdatedAt = DateTime.Now,
                AutoMouseMoverEnabled = false,
                AutoMouseMoverIntervalSeconds = 30,
                AutoMouseMoverPixel = 5,
                AutoMouseMoverDisableAfterInMinutes = 0,
                AutoShutdownPCEnabled = false,
                AutoShutdownPCAfterMinutes = 60
            };
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = false });
        }
    }
}
