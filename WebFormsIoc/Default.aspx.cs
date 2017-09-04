namespace WebFormsIoc
{
    using System;
    using System.Web.UI;

    public partial class _Default : Page
    {
        private readonly IEmailService _emailService;
        private readonly ILogger _logger;

        public _Default(IEmailService emailService, ILogger logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        protected _Default()
        {
        } // Required by WebForms

        protected void Page_Load(object sender, EventArgs e)
        {
            _emailService.SendMessage("This is a Test");
            _logger.LogMessage("Blarg");

        }
    }
}