using ProcessViewer.Core.Models;
using ProcessViewer.Core.Models.Abstract;
using ProcessViewer.Persistence.Filters.Abstract;
using ProcessViewer.Persistence.Repositories.Abstract;
using ProcessViewer.Persistence.Stores;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ProcessViewer.Persistence.Tests.Stores
{
    public class ProcessesStoresTests
    {
        private Mock<IRepository<IProcessModel>> ProcessRepo { get; set; }
        private Mock<ILoadCheckerRunner> ProcessesFilter { get; set; }

        [SetUp]
        public void Setup()
        {
            ProcessRepo = new Mock<IRepository<IProcessModel>>();
            ProcessesFilter = new Mock<ILoadCheckerRunner>();
        }

        [Test]
        public void ProcessesStore_GetAll_ShouldReturnEmptyCollection()
        {
            var testProcs = new IProcessModel[] { new ProcessModel() };
            ProcessRepo.Setup(r => r.GetAll()).Returns(testProcs);

            var store = new ProcessesStore(ProcessRepo.Object);
            var procs = store.GetAll();

            Assert.That(procs, Is.InstanceOf(typeof(IEnumerable<IProcessModel>)));
            Assert.That(!procs.Any());
        }

        [Test]
        public void ProcessesStore_GetAll_ShouldReturnCollectionFromRepoAfterRefresh()
        {
            var testProcs = new IProcessModel[] { new ProcessModel() };
            ProcessRepo.Setup(r => r.GetAll()).Returns(testProcs);

            var store = new ProcessesStore(ProcessRepo.Object);
            store.Refresh();
            var procs = store.GetAll();

            Assert.That(procs, Is.EqualTo(testProcs));
        }

        [Test]
        public void ProcessesStore_Refresh_ShouldVisitRepoGetAllOnce()
        {
            var store = new ProcessesStore(ProcessRepo.Object);
            store.Refresh();

            ProcessRepo.Verify(r => r.GetAll(), Times.Once);
        }
    }
}