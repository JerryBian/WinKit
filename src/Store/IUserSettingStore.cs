using System;
using System.Collections.Generic;
using System.Text;
using WinKit.Models;

namespace WinKit.Store
{
    public interface IUserSettingStore
    {
        Task<UserSetting> GetUserSettingAsync();

        Task SaveAsync(UserSetting userSetting);
    }
}
