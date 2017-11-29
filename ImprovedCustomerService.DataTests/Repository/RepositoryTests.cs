using System;
using System.Data.Entity;
using ImprovedCustomerService.Data.Model;
using ImprovedCustomerService.Data.Repository;
using ImprovedCustomerService.Tests.MockRepository;
using Moq;
using NUnit.Framework;

namespace ImprovedCustomerService.Data.Repository.Tests
{
    [TestFixture]
    public class RepositoryTests
    {
        #region private fields

        private IRepository<Customer> _repository;
        #endregion

        [Test]
        public void GetByIdTest()
        {
            var mockRepository = new Mock<MockRepository<Customer>>();
            var customer = new Customer
            {
                Address = null,
                Age = 15,
                Email = "im@dabes.com",
                FirstName = "Im",
                LastName = "Dabes",
                Id = 1,
                CreatedOn = DateTime.UtcNow
            };

            mockRepository.Object.Create(customer);
            //mockRepository.S
            mockRepository.Setup(m => m.GetById(1)).Returns(customer);
        }

        [Test]
        public void GetTest()
        {

        }

        [Test]
        public void RemoveTest()
        {

        }

        [Test]
        public void CreateTest()
        {

        }

        [Test]
        public void UpdateTest()
        {

        }

        [Test]
        public void SaveTest()
        {

        }

        [Test]
        public void DisposeTest()
        {

        }
    }
}