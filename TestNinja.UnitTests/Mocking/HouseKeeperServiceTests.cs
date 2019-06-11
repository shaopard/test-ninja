using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class HouseKeeperServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailHandler> _emailHandler;
        private Mock<IXtraMessageBox> _messageBox;
        private HousekeeperService _houseKeeperService;
        private DateTime _statementDate = new DateTime(2017, 1, 1);
        private Housekeeper _houseKeeper;
        private string _statementFileName;

        [SetUp]
        public void SetUp()
        {
            _houseKeeper = new Housekeeper { Email = "a", FullName = "b", Old = 1, StatementEmailBody = "c" };

            _unitOfWork = new Mock<IUnitOfWork>();

            _statementFileName = "filename";
            _statementGenerator = new Mock<IStatementGenerator>();
            _statementGenerator
                .Setup(sg => sg.SaveStatement(_houseKeeper.Old, _houseKeeper.FullName, _statementDate))
                .Returns(() => _statementFileName);

            _emailHandler = new Mock<IEmailHandler>();
            _messageBox = new Mock<IXtraMessageBox>();

            _houseKeeperService = new HousekeeperService(
                _unitOfWork.Object, 
                _statementGenerator.Object, 
                _emailHandler.Object, 
                _messageBox.Object);

            _unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                _houseKeeper
            }.AsQueryable());
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _houseKeeperService.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg => sg.SaveStatement(_houseKeeper.Old, _houseKeeper.FullName, _statementDate));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_HouseKeeperEmailIsNull_ShouldNotGenerateStatements(string houseKeeperEmail)
        {
            _houseKeeper.Email = houseKeeperEmail;

            _houseKeeperService.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg => sg.SaveStatement(_houseKeeper.Old, _houseKeeper.FullName, _statementDate), Times.Never);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_NoStatementsGenerated_GenerateStatements(string generatedStatement)
        {
            _statementFileName = generatedStatement;

            _houseKeeperService.SendStatementEmails(_statementDate);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            _houseKeeperService.SendStatementEmails(_statementDate);

            VerifyEmailSent();
        }

        [Test]
        public void SendStatementEmails_EmailSendingFails_DisplayMessageBox()
        {
            _emailHandler.Setup(emailHandler => emailHandler.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()
            )).Throws<Exception>();

            _houseKeeperService.SendStatementEmails(_statementDate);

            _messageBox.Verify(msgbox => msgbox.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
        }


        private void VerifyEmailNotSent()
        {
            _emailHandler.Verify(emailHandler => emailHandler.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
                Times.Never);
        }

        private void VerifyEmailSent()
        {
            _emailHandler.Verify(emailHandler => emailHandler.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()));
        }
    }
}
