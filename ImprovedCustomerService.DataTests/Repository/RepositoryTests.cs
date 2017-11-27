using NUnit.Framework;
using ImprovedCustomerService.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImprovedCustomerService.Data.Model;
using ImprovedCustomerService.Data.UnitOfWork;
using Moq;

namespace ImprovedCustomerService.Data.Repository.Tests
{
    [TestFixture()]
    public class RepositoryTests
    {
        private IUnitOfWork _unitOfWork;

        public void RepositoryTest()
        {
            var context = new CustomerServiceDbEntities();
            _unitOfWork = new UnitOfWork.UnitOfWork(context);
        }

        [Test()]
        public void GetByIdTest()
        {
            Mock<IRepository<Customer>> mock = new Mock<IRepository<Customer>>();
            mock.Setup(repo => repo);

            mock.Verify(r => r != null);
        }

        [Test()]
        public void GetTest()
        {

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

        [Test()]
        public void SaveTest()
        {

        }

        [Test()]
        public void DisposeTest()
        {

        }
    }
}