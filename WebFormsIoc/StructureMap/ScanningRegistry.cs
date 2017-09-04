namespace WebFormsIoc.StructureMap
{
    using global::StructureMap;

    /// <summary>
    /// Typical StructureMap Registry that will Scan the Calling Assembly
    /// and register Default Conventions. Additionally, it will also look
    /// for additional Registry objects and execute them.
    /// </summary>
    public class ScanningRegistry : Registry
    {
        public ScanningRegistry()
        {
            Scan(_ =>
            {
                _.TheCallingAssembly();
                _.WithDefaultConventions();
                _.LookForRegistries();
            });
        }
    }
}