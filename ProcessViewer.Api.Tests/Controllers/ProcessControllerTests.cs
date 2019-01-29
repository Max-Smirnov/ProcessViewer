using ProcessViewer.Api.Controllers;
using ProcessViewer.Core.Models;
using ProcessViewer.Core.Models.Abstract;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ProcessViewer.Persistence.Stores.Abstract;

namespace ProcessViewer.Api.Tests.Controllers
{
    public class ProcessControllerTests
    {
        private Mock<IProcessesStore> ProcessRepo { get; set; }

        [SetUp]
        public void Setup()
        {
            ProcessRepo = new Mock<IProcessesStore>();
            ProcessRepo.Setup(p => p.GetAll()).Returns(new IProcessModel[] { new ProcessModel() });
        }

        [Test]
        public void ProcessController_Get_ShouldReturnIEnumerableOfProcessModel()
        {
            var controller = new ProcessesController(ProcessRepo.Object);

            var processes = controller.Get();

            Assert.That(processes.Value.Any());
        }

        [Test]
        public void ProcessController_Get_ShouldCallGetAllMethodOfRepo()
        {
            var controller = new ProcessesController(ProcessRepo.Object);

            var processes = controller.Get();

            ProcessRepo.Verify(repo => repo.GetAll(), Times.Once);
        }
    }
}