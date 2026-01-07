using MessagePack;
using Microsoft.Extensions.Options;
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
        private readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

        private UserSetting _userSetting;

        public async Task<UserSetting> GetUserSettingAsync()
        {
            if (_userSetting == null)
            {
                await _lock.WaitAsync();
                try
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
                            await SaveInternalAsync(_userSetting);
                            await _logger.LogAsync($"Load user setting failed from local file {file}, fallback to default.", ex);
                        }
                    }
                }
                finally
                {
                    _lock.Release();
                }
            }

            return _userSetting;
        }

        public async Task SaveAsync(UserSetting userSetting)
        {
            await _lock.WaitAsync();
            try
            {
                await SaveInternalAsync(userSetting);
            }
            finally
            {
                _lock.Release();
            }
        }

        private async Task SaveInternalAsync(UserSetting userSetting)
        {
            var file = GetUserSettingFile();
            userSetting.LastUpdatedAt = DateTime.Now;

            var tempFile = file + ".tmp";
            using (var stream = File.Open(tempFile, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await MessagePackSerializer.SerializeAsync(stream, userSetting);
            }

            if (File.Exists(file))
            {
                File.Replace(tempFile, file, null);
            }
            else
            {
                File.Move(tempFile, file);
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
