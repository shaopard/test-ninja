using Ninject.Modules;
using TestNinja.Mocking;

namespace TestNinja.NinjectBinings
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IFileReader>().To<FileReader>();
            Bind<IVideoRepository>().To<VideoRepository>();
            Bind<IEmployeeRepository>().To<EmployeeRepository>();
        }
    }
}
