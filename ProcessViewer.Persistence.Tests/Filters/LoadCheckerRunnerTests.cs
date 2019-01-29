using System.Collections.Generic;
using System.Linq;
using ProcessViewer.Core.Models;
using ProcessViewer.Core.Models.Abstract;
using ProcessViewer.Persistence.Filters;
using ProcessViewer.Persistence.Filters.Abstract;
using Moq;
using NUnit.Framework;

namespace ProcessViewer.Persistence.Tests.Filters
{
    public class LoadCheckerRunnerTests
    {
        private Mock<ILoadChecker<IProcessModel>> _loadChecker;

        [SetUp]
        public void Setup()
        {
            _loadChecker = new Mock<ILoadChecker<IProcessModel>>();
        }

        [Test]
        public void ProcessFilter_GetNotifications_ShouldGetMessageFromLoadChecker()
        {
            var testProcs = new IProcessModel[] { new ProcessModel() };
            _loadChecker.Setup(r => r.GetAffectedProcesses(testProcs)).Returns(testProcs);

            var filter = new LoadCheckerRunner(new [] {_loadChecker.Object});
            var messages = filter.GetNotifications(testProcs);

            Assert.That(messages.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ProcessFilter_GetNotifications_ShouldVisitLoadEachCheckerOnce()
        {
            var testProcs = new IProcessModel[] { new ProcessModel(), new ProcessModel() };
            _loadChecker.Setup(r => r.GetAffectedProcesses(testProcs)).Returns(testProcs);

            var filter = new LoadCheckerRunner(new[] { _loadChecker.Object, _loadChecker.Object });
            var messages = filter.GetNotifications(testProcs);

            Assert.That(messages.Count(), Is.EqualTo(2));
        }
    }
}