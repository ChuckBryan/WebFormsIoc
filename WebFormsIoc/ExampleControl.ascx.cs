namespace WebFormsIoc
{
    using System;
    using System.Web.UI;

    public partial class ExampleControl : UserControl
    {
        private readonly ILogger _logger;

        /// <summary>
        /// This constructor is called by the Structure Map Http Module.
        /// </summary>
        /// <param name="logger"></param>
        public ExampleControl(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// This constructor is required by ASP.NET WebForms. 
        /// Do not add any code here.
        /// </summary>
        protected ExampleControl()
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _logger.LogMessage("From the User Control");
        }
    }
}