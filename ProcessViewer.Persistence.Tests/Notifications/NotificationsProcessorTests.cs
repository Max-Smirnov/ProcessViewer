using System.Collections.Generic;
using ProcessViewer.Core.Models.Abstract;
using ProcessViewer.Persistence.Filters.Abstract;
using ProcessViewer.Persistence.Notifications;
using ProcessViewer.Persistence.Stores.Abstract;
using Moq;
using NUnit.Framework;

namespace ProcessViewer.Persistence.Tests.Notifications
{
    public class NotificationsProcessorTests
    {
        private Mock<IProcessesStore> ProcessesStore { get; set; }
        private Mock<ILoadCheckerRunner> LoadCheckerRunner { get; set; }

        [SetUp]
        public void Setup()
        {
            ProcessesStore = new Mock<IProcessesStore>();
            LoadCheckerRunner = new Mock<ILoadCheckerRunner>();
        }

        [Test]
        public void ProcessesStore_SendNotifications_ShouldVisitFilterGetNotificationsOnce()
        {
            LoadCheckerRunner.Setup(r => r.GetNotifications(It.IsAny<IEnumerable<IProcessModel>>())).Returns(new string[1]);

            var monitor = new NotificationsProcessor(LoadCheckerRunner.Object, ProcessesStore.Object);
            monitor.SendNotifications();

            LoadCheckerRunner.Verify(r => r.GetNotifications(It.IsAny<IEnumerable<IProcessModel>>()), Times.Once);
        }
    }
}