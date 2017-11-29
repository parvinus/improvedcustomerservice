using NUnit.Framework;
using System.Data.Entity;
using Moq;

namespace ImprovedCustomerService.Data.UnitOfWork.Tests
{
    [TestFixture]
    public class UnitOfWorkTests
    {
        #region internal fields
        private Mock<DbContext> _mockContext;
        #endregion

        #region constructor(s)
        #endregion

        #region internal methods

        private void Initialize(Mock<DbContext> mockContext = null)
        {
            if (_mockContext == null)
                _mockContext = mockContext ?? new Mock<DbContext>();
        }
        #endregion

        [Test]
        public void SaveChangesTest()
        {
            Initialize();

            var unitOfWork = new UnitOfWork(_mockContext.Object);
            unitOfWork.SaveChanges();

            _mockContext.Verify(u => u.SaveChanges());
        }
    }
}