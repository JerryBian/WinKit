using System;
using System.Collections.Generic;
using System.Text;

namespace WinKit.Extensions
{
    public static class TaskExtensions
    {
        public static async Task OkForCancelAsync(this Task task)
        {
            try
            {
                await task;
            }
            catch (OperationCanceledException)
            {
                // Ignore cancellation exceptions
            }
        }
    }
}
