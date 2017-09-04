namespace WebFormsIoc
{
    using System;
    public class EmailService : IEmailService, IDisposable
    {
        private readonly ILogger _logger;

        public EmailService(ILogger logger)
        {
            _logger = logger;
        }
        public void SendMessage(string message)
        {
            // not really sending an email...but just logging to demonstrate IoC.
            _logger.LogMessage(message);
        }

        public void Dispose()
        {
            string stop = "Stop";
        }
    }

    public interface IEmailService
    {
        void SendMessage(string message);
    }
}