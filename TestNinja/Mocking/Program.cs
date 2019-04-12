using Ninject;
using System.Reflection;

namespace TestNinja.Mocking
{
    public class Program
    {
        public static void Main()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            var fileReader = kernel.Get<IFileReader>();
            var videoRepository = kernel.Get<IVideoRepository>();

            var service = new VideoService(fileReader, videoRepository);
            var title = service.ReadVideoTitle();
        }
    }
}
