using System;

namespace WebApi.Services
{
    public class ConsoleLogger : ILoggerService
    {
        public void Log(string message)
        {
            Console.WriteLine($"ConsoleLogger : {message}");
        }
    }
}
