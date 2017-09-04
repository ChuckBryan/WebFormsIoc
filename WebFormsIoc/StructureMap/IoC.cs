namespace WebFormsIoc.StructureMap
{
    using global::StructureMap;

    public class IoC
    {
        public static IContainer Container { get; set; }

        static IoC()
        {
            Container = new Container(new ScanningRegistry());
        }

        public static void BuildUp(object target)
        {
            Container.BuildUp(target);
        }
    }

}