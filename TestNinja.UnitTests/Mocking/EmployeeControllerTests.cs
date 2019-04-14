using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        private EmployeeController _employeeController;
        private Mock<IEmployeeRepository> _employeeRepository;

        [SetUp]
        public void SetUp()
        {
            _employeeRepository = new Mock<IEmployeeRepository>();
            _employeeController = new EmployeeController(_employeeRepository.Object);
        }

        [Test]
        public void DeleteEmployee_WhenCalled_DeleteEmployeeFromDb()
        {
            var result = _employeeController.DeleteEmployee(1);
            _employeeRepository.Verify(repository => repository.DeleteEmployee(1));

            Assert.That(result, Is.TypeOf<RedirectResult>());
        }
    }
}
