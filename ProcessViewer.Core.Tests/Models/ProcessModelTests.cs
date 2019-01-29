using ProcessViewer.Core.Adapters.Abstract;
using ProcessViewer.Core.Models;
using Moq;
using NUnit.Framework;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using ProcessViewer.Core.Models.Abstract;

namespace ProcessViewer.Core.Tests.Models
{
    public class ProcessModelTests
    {
        private Mock<IClock> _clock;
        private Mock<IProcessAdapter> _processAdapter;
        private DateTime _testDateTime;

        [SetUp]
        public void Setup()
        {
            _testDateTime = DateTime.Parse("2000-01-01");
            _clock = new Mock<IClock>();
            _clock.Setup(c => c.Now).Returns(_testDateTime);
            _processAdapter = new Mock<IProcessAdapter>();
            _processAdapter.Setup(p => p.Id).Returns(1);
            _processAdapter.Setup(p => p.ProcessName).Returns("TestName");
            _processAdapter.Setup(p => p.BasePriority).Returns(0);
            _processAdapter.Setup(p => p.WorkingSet64).Returns(1);
        }

        [Test]
        public void ProcessModel_GetProcessModel_ShouldReturnCorrectValues()
        {
            _processAdapter.Setup(p => p.TotalProcessorTime).Returns(TimeSpan.FromHours(1));
            _processAdapter.Setup(p => p.StartTime).Returns(_testDateTime.AddHours(-1));

            var processModel = new ProcessModel(_clock.Object);

            processModel.GetProcessModel(_processAdapter.Object);

            Assert.That(processModel.Id, Is.EqualTo(_processAdapter.Object.Id));
            Assert.That(processModel.Name, Is.EqualTo(_processAdapter.Object.ProcessName));
            Assert.That(processModel.RunningTime, Is.GreaterThanOrEqualTo(TimeSpan.Zero));
            Assert.That(processModel.CpuUsageTime, Is.GreaterThanOrEqualTo(TimeSpan.Zero));
            Assert.That(processModel.CpuUsagePercent, Is.GreaterThanOrEqualTo(0));
        }
    }
}