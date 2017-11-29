using System;
using System.Data.Entity;
using ImprovedCustomerService.Data.Model;
using ImprovedCustomerService.Data.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ImprovedCustomerService.Tests
{
    [TestClass]
    public class ImprovedCustomerServiceDataTests
    {
        #region internal fields
        private Mock<DbContext> _mockContext;
        private IUnitOfWork _mockUnitOfWork;
        #endregion

        #region constructor(s)

        //public UnitTest1(Mock<DbContext> mockContext)
        //{
        //    Initialize(mockContext);
        //}
        #endregion

        #region internal methods

        private void Initialize(Mock<DbContext> mockContext = null)
        {
            if(_mockContext == null)
                _mockContext = mockContext ?? new Mock<DbContext>();

            if(_mockUnitOfWork == null)
                _mockUnitOfWork = new UnitOfWork(_mockContext.Object);
        }
        #endregion

        #region public test methods

        [TestMethod]
        public void TestUnitOfWork()
        {
            if(_mockContext == null)
                Initialize();

            _mockUnitOfWork.SaveChanges();
            _mockContext.Verify(x => x.SaveChanges());
        }

        [TestMethod]
        public void TestGetById()
        {
            var newCustomer = new Customer
            {
                Address = null,
                Address_Id = null,
                Age = 16,
                CreatedOn = DateTime.UtcNow,
                Email = "test@test.com",
                FirstName = "Mock",
                LastName = "User",
                Id = 1
            };
            var unitOfWork = _mockContext.Object;

            _mockUnitOfWork.CustomerRepository.Create(newCustomer);
            _mockUnitOfWork.SaveChanges();

            var retrievedCustomer = _mockUnitOfWork.CustomerRepository.GetById(newCustomer.Id);

            Assert.IsNotNull(retrievedCustomer);
        }

        #endregion
    }
}
