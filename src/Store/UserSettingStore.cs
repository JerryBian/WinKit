using MessagePack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using WinKit.Logging;
using WinKit.Models;
using WinKit.Options;

namespace WinKit.Store
{
    public class UserSettingStore(IOptions<WinKitOption> option, IFileLogger logger) : IUserSettingStore
    {
        private readonly WinKitOption _option = option.Value;
        private readonly IFileLogger _logger = logger;

        private UserSetting _userSetting;

        public async Task<UserSetting> GetUserSettingAsync()
        {
            if (_userSetting == null)
            {
                var file = GetUserSettingFile();
                try
                {
                    using (var stream = File.OpenRead(file))
                    {
                        _userSetting = await MessagePackSerializer.DeserializeAsync<UserSetting>(stream);
                    }
                }
                catch (Exception ex)
                {
                    _userSetting = UserSetting.CreateDefault();
                    await SaveAsync(_userSetting);
                    await _logger.LogAsync($"Load user setting failed from local file {file}, fallback to default.", ex);
                }
            }
            
            return _userSetting;
        }

        public async Task SaveAsync(UserSetting userSetting)
        {
            var file = GetUserSettingFile();
            using (var stream = File.Open(file, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await MessagePackSerializer.SerializeAsync(stream, userSetting);
            }

            _userSetting = userSetting;

            await _logger.LogAsync($"User settings saved to {file} => {JsonSerializer.Serialize(userSetting, new JsonSerializerOptions { WriteIndented = false })}");
        }

        private string GetUserSettingFile()
        {
            var file = Path.Combine(_option.AppDataFolder, "settings.bin");
            return file;
        }
    }
}
