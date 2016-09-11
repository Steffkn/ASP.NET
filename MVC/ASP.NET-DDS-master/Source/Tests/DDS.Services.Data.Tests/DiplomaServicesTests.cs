namespace DDS.Services.Web.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Data.Interfaces;
    using DDS.Data.Common;
    using DDS.Data.Models;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class DiplomaServicesTests
    {
        private readonly IDbRepository<Diploma> DiplomasRepository;
        private readonly IDiplomasService DiplomasService;
        private IList<Diploma> diplomas;

        public DiplomaServicesTests()
        {
            // create some mock diplomas to play with
            this.diplomas = new List<Diploma>
            {
                new Diploma { Id = 1, Title = "First Diploma Title", Description = "First Diploma Description", ContentCSV = "Asp,.Net,C#" },
                new Diploma { Id = 2, Title = "Second Diploma Title", Description = "Second Diploma Description", ContentCSV = "This,Is,Fake" },
                new Diploma { Id = 3, Title = "Third Diploma Title", Description = "Third Diploma Description", ContentCSV = "Faker,for,Life" },
            };

            // Mock the Diplomas Repository using Moq
            var mockDiplomasRepository = new Mock<IDbRepository<Diploma>>();

            mockDiplomasRepository.Setup(mr => mr.All()).Returns(this.diplomas.Where(d => d.IsDeleted == false).AsQueryable());

            mockDiplomasRepository.Setup(mr => mr.AllWithDeleted()).Returns(this.diplomas.AsQueryable());

            mockDiplomasRepository.Setup(mr => mr.GetById(It.IsAny<int>())).Returns((int i) => this.diplomas.Where(x => x.Id == i).Single());

            mockDiplomasRepository.Setup(mr => mr.Add(It.IsAny<Diploma>())).Callback((Diploma diploma) =>
            {
                this.diplomas.Add(diploma);
            });

            mockDiplomasRepository.Setup(mr => mr.Delete(It.IsAny<Diploma>())).Callback((Diploma diploma) =>
            {
                diploma.IsDeleted = true;
                diploma.DeletedOn = DateTime.Today;
            });

            mockDiplomasRepository.Setup(mr => mr.HardDelete(It.IsAny<Diploma>())).Callback((Diploma diploma) =>
            {
                this.diplomas.Remove(diploma);
            });

            // Allows us to test saving a diploma
            mockDiplomasRepository.Setup(mr => mr.Save()).Verifiable();

            // Complete the setup of our Mock Diploma Repository
            this.DiplomasRepository = mockDiplomasRepository.Object;

            this.DiplomasService = new DiplomasService(mockDiplomasRepository.Object);
        }

        [Test]
        public void DiplomasServiceCreatesDiploma()
        {
            var newDiploma = new Diploma { Id = 4, Title = "Fourth Diploma Title", Description = "Fourth Diploma Description", ContentCSV = "rly,4th" };

            this.DiplomasService.Create(newDiploma);

            var testDiplomas = this.DiplomasService.GetAll().ToList();

            foreach (var item in testDiplomas)
            {
                if (item.IsDeleted)
                {
                    Assert.Fail("Diploma is set deleted when it shouldn't");
                }
            }

            Assert.IsNotNull(testDiplomas);
            Assert.AreEqual(4, testDiplomas.Count);

            this.DiplomasService.Delete(newDiploma);
            testDiplomas = this.DiplomasService.GetAll().ToList();
            Assert.AreEqual(3, testDiplomas.Count);
        }

        [Test]
        public void CanReturnDiplomaById()
        {
            var testDiploma = this.DiplomasRepository.GetById(2);

            Assert.IsNotNull(testDiploma);
            Assert.IsInstanceOf(typeof(Diploma), testDiploma);
            Assert.AreEqual("Second Diploma Title", testDiploma.Title);
        }

        [Test]
        public void CanReturnAllWhitoutDeletedDiplomas()
        {
            var newDiploma = new Diploma { Id = 4, Title = "Fourth Diploma Title", Description = "Fourth Diploma Description", ContentCSV = "rly,4th" };
            newDiploma.IsDeleted = true;

            this.DiplomasRepository.Add(newDiploma);

            var testDiplomas = this.DiplomasRepository.All().ToList();

            foreach (var item in testDiplomas)
            {
                if (item.IsDeleted)
                {
                    Assert.Fail("Diploma is set deleted when it shouldn't");
                }
            }

            Assert.IsNotNull(testDiplomas);
            Assert.AreEqual(3, testDiplomas.Count);

            this.DiplomasRepository.HardDelete(newDiploma);
            Assert.AreEqual(3, this.diplomas.Count);
        }

        [Test]
        public void CanReturnAllDiplomas()
        {
            var newDiploma = new Diploma { Id = 4, Title = "Fourth Diploma Title", Description = "Fourth Diploma Description", ContentCSV = "rly,4th" };
            newDiploma.IsDeleted = true;

            this.DiplomasRepository.Add(newDiploma);

            var testDiplomas = this.DiplomasRepository.AllWithDeleted().ToList();

            Assert.IsNotNull(testDiplomas);
            Assert.AreEqual(4, testDiplomas.Count);

            this.DiplomasRepository.HardDelete(newDiploma);
            Assert.AreEqual(3, this.diplomas.Count);
        }

        [Test]
        public void CanInsertDiploma()
        {
            var newDiploma = new Diploma { Id = 4, Title = "Fourth Diploma Title", Description = "Fourth Diploma Description", ContentCSV = "rly,4th" };

            int diplomasCount = this.DiplomasRepository.All().ToList().Count;
            Assert.AreEqual(3, diplomasCount);

            this.diplomas.Add(newDiploma);

            diplomasCount = this.DiplomasRepository.All().ToList().Count;
            Assert.AreEqual(4, diplomasCount);

            var testDiploma = this.DiplomasRepository.GetById(4);
            Assert.IsNotNull(testDiploma);
            Assert.IsInstanceOf(typeof(Diploma), testDiploma);
            Assert.AreEqual(4, testDiploma.Id);

            // Remove the last diploma
            this.DiplomasRepository.HardDelete(newDiploma);

            // recount (revert changes)
            diplomasCount = this.DiplomasRepository.All().ToList().Count;
            Assert.AreEqual(3, diplomasCount);
        }
    }
}
