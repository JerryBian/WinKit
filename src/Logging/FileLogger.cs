using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Text;
using WinKit.Extensions;
using WinKit.Helper;
using WinKit.Options;

namespace WinKit.Logging
{
    public class FileLogger : IFileLogger
    {
        private readonly Task _mainTask;
        private readonly WinKitOption _option;
        private readonly ILogger<FileLogger> _logger;
        private readonly BlockingCollection<FileLog> _logBuffer;

        private string _logFile;

        public FileLogger(IOptions<WinKitOption> option, ILogger<FileLogger> logger)
        {
            _option = option.Value;
            _logger = logger;
            _logBuffer = [];
            _mainTask = Task.Run(async () =>
            {
                foreach (var item in _logBuffer.GetConsumingEnumerable())
                {
                    await WriteLogAsync(item);
                }
            });
        }

        public async ValueTask DisposeAsync()
        {
            _logBuffer.CompleteAdding();
            await _mainTask;
        }

        public async Task LogAsync(string message, Exception ex = null)
        {
            if (string.IsNullOrEmpty(_logFile))
            {
                Initialize(string.Empty);
            }

            var log = new FileLog
            {
                TimeStamp = DateTime.Now,
                Message = message,
                Error = ex
            };

            if (!_logBuffer.TryAdd(log))
            {
                await WriteLogAsync(log);
            }

            _logger.LogInformation($"Message={message}, Error={ex}");
        }

        public void Initialize(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = "default";
            }

            var dir = Path.Combine(_option.AppDataFolder, "logs");
            Directory.CreateDirectory(dir);
            _logFile = Path.Combine(dir, $"{fileName}.{DateTime.Now.ToDateString()}.{SystemHelper.ProcessId}.log");
            _logger.LogInformation($"Log file initialized at {_logFile}");
        }

        private async Task WriteLogAsync(FileLog log)
        {
            try
            {
                await File.AppendAllLinesAsync(_logFile, [$"{log.TimeStamp}: {log.Message}"], Encoding.UTF8);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to write log to file {_logFile}.");
            }
        }
    }
}
