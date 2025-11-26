using System;
using System.Collections.Generic;
using System.Text;

namespace WinKit.Logging
{
    public interface IFileLogger : IAsyncDisposable
    {
        Task LogAsync(string message, Exception ex = null);

        void Initialize(string fileName);
    }
}
