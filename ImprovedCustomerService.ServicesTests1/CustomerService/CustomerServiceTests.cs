using System.Collections.Generic;
using ImprovedCustomerService.Data.Dto;
using Moq;
using NUnit.Framework;
using ImprovedCustomerService.Data.Model;
using ImprovedCustomerService.Data.Repository;

namespace ImprovedCustomerService.Services.CustomerService.Tests
{
    [TestFixture()]
    public class CustomerServiceTests
    {
        [Test()]
        public void CustomerServiceTest()
        {
            var customerList = new List<Customer>();

            for (int a = 0; a < 10; a++)
            {
                customerList.Add(new Customer{Id = a});
            }

            var mockCustomerService = new Mock<ICustomerService>(MockBehavior.Default);
            Assert.NotNull(mockCustomerService.Object);

        }

        [Test()]
        public void GetByIdTest()
        {
            var mockCustomerService = new Mock<ICustomerService>();
            mockCustomerService.Setup(c => c.Create(new CustomerSaveDto
            {
                Id = 1,
                FirstName = "Test",
                LastName = "User",
                Age = 18,
                Email = "test@test.com"
            }));

            var testCustomer = mockCustomerService.Object.GetById(1);

            Assert.IsNotNull(testCustomer);
            Assert.IsEmpty(testCustomer.Errors);
            Assert.Equals(testCustomer.Message, "success");
        }

        [Test()]
        public void RemoveTest()
        {

        }

        [Test()]
        public void CreateTest()
        {

        }

        [Test()]
        public void UpdateTest()
        {

        }
    }
}