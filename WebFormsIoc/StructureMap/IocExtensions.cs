namespace WebFormsIoc.StructureMap
{
    using System;
    using System.Linq;
    using System.Web.UI;
    using global::StructureMap;

    /// <summary>
    /// These Extension methods use Reflection to Inject Constructor Parameters
    /// from the IoC Container.
    /// </summary>
    public static class IocExtensions
    {
        public static void Inject(this Page page, IContainer container)
        {
            page.GetType().BaseType.Inject(container, page);
        }

        public static void Inject(this UserControl control, IContainer container)
        {
            control.GetType().BaseType.Inject(container, control);
        }

        private static void Inject(this Type type, IContainer container, object obj)
        {
            // is there a constructor to inject?
            var ctor = (from c in type.GetConstructors()
                where c.GetParameters().Length > 0
                select c).FirstOrDefault();

            if (ctor == null) return;
            
            // resolve the parameters for the ctor
            var args = (from parm in ctor.GetParameters()
                select container.GetInstance(parm.ParameterType)).ToArray();

            // execute the constructor methods with the args resolved
            ctor.Invoke(obj, args);
        }
    }
}