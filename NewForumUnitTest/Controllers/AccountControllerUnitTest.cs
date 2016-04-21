using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewForumProject.Controllers;
using Moq;

namespace NewForoumUnitTest.Controllers
{
    using NewForumProject.Interfaces;
    using NewForumProject.Models;
    using  Ploeh.AutoFixture;
    [TestClass]
    public class AccountControllerUnitTest
    {
        private UsersController _controller;

        private Mock<IDataContextRepository> repository;

        private Fixture fixture = new Fixture();
        [TestInitialize]
        private void TestInitialize()
        {
            
            this.repository = this.fixture.Freeze<Mock<IDataContextRepository>>();
            this._controller = fixture.Create<UsersController>();
        }

        [TestMethod]
        public void LoginTest()
        {
            var user = this.fixture.Create<User>();
            
            this.repository.Setup(e => e.AddUser(user)).Returns(true);
            var result = this._controller.Create(user);
            this.repository.Verify(e=>e.AddUser(user),Times.Once);
        }
    }
}
