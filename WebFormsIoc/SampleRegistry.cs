namespace WebFormsIoc
{
    using global::StructureMap;

    public class SampleRegistry : Registry
    {
        public SampleRegistry()
        {
            // This will be sure to use the same instance of ILogger throughout
            // the given request.
            For<ILogger>().Use<Logger>().Transient();

        }
    }
}