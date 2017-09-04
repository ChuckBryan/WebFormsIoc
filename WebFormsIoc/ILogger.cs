namespace WebFormsIoc
{
    using System;

    public interface ILogger
    {
        Guid Id { get; }
        void LogMessage(string message);
    }

    public class Logger : ILogger
    {
        public Logger()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public void LogMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}