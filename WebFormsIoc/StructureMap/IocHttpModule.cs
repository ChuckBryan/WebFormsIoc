using System.Web;
using WebFormsIoc.StructureMap;

[assembly: PreApplicationStartMethod(typeof(IocHttpModule), "Start")]

namespace WebFormsIoc.StructureMap
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.UI;
    using global::StructureMap;

    /// <summary>
    /// This Http Module attempts to provide a Constructor Injection for WebPages
    /// and UserControls. This requires both a default constructor on the WebPage and
    /// User Control. In addition, an additional constructor can be added with the services
    /// that have been registered with the IoC Container.
    /// </summary>
    public class IocHttpModule : IHttpModule
    {
        private static readonly IContainer Container;
        private IContainer _nestedContainer;
        private Page _page;

        static IocHttpModule()
        {
            Container = IoC.Container;
        }

        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += ConfigurePageCtorInjection;
            context.PostRequestHandlerExecute += DisposeNestedContainer;
        }

        public void Dispose()
        {
        }

        public static void Start()
        {
            HttpApplication.RegisterModule(typeof(IocHttpModule));
        }

        private void DisposeNestedContainer(object sender, EventArgs e)
        {
            if (!IsPageRequest()) return;
            
            _nestedContainer.Dispose();
        }


        private bool IsPageRequest()
        {
            _page = HttpContext.Current.CurrentHandler as Page;

            if (_page == null)
                return false;

            _page.InitComplete += ConfigureUserControlCtorInject;

            return true;
        }

        /// <summary>
        ///     Retrieve the Page and determine if it has a constructor that can be called.
        ///     Attempt to retrieve Services from a Nested Container in Structure Map.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigurePageCtorInjection(object sender, EventArgs e)
        {
            if (!IsPageRequest()) return;

            _nestedContainer = Container.GetNestedContainer();

            _page.Inject(_nestedContainer);
        }

        /// <summary>
        ///     Iterate over User Controls (.ascx) that are on the current page.
        ///     Determine if it has a constructor that can be called.
        ///     Attempt to retrieve Services and inject into constructor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigureUserControlCtorInject(object sender, EventArgs e)
        {
            if (!IsPageRequest()) return;

            // User Controls are Protected (non public) Fields defined in the Designer.
            var eFlags = BindingFlags.Instance | BindingFlags.NonPublic;

            // Get Non Public Fields of Type UserControl
            var userControlFields = _page.GetType().GetFields(eFlags)
                .Where(x => x.FieldType.BaseType == typeof(UserControl));

            foreach (var userControl in userControlFields)
            {
                var control = userControl.GetValue(_page) as UserControl;
                control.Inject(_nestedContainer);
            }
        }
    }
}