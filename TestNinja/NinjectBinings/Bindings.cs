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
            Bind<IBookingRepository>().To<BookingRepository>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IStatementGenerator>().To<StatementGenerator>();
            Bind<IEmailHandler>().To<EmailHandler>();
        }
    }
}
