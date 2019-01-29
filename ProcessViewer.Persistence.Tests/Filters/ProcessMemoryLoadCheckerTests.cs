using ProcessViewer.Core.Models;
using ProcessViewer.Core.Models.Abstract;
using ProcessViewer.Persistence.Filters;
using ProcessViewer.Persistence.Filters.Abstract;
using ProcessViewer.Persistence.Repositories.Abstract;
using ProcessViewer.Persistence.Stores;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProcessViewer.Persistence.Tests.Filters
{
    public class ProcessMemoryLoadCheckerTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(5000, 0)]
        [TestCase(5, 4)]
        [TestCase(4.01, 4)]
        [TestCase(5, 5)]
        public void ProcessMemoryLoadChecker_GetAffectedProcesses_ShouldReturnEmptyCollectionIfLimitNotExceeded(double limit, long value)
        {
            var testProcs = new IProcessModel[] { new ProcessModel() { RamUsage = value * 1024 * 1024 } };
            var checker = new ProcessMemoryLoadChecker(limit);

            var messages = checker.GetAffectedProcesses(testProcs);

            Assert.That(messages.Count(), Is.EqualTo(0));
        }


        [Test]
        [TestCase(0, 1)]
        [TestCase(0, 1000000)]
        [TestCase(500, 501)]
        [TestCase(9, 10)]
        [TestCase(9.9, 10)]
        public void ProcessMemoryLoadChecker_GetAffectedProcesses_ShouldReturnProcessModelsCollectionIfLimitExceeded(double limit, long value)
        {
            var testProcs = new IProcessModel[] { new ProcessModel() { RamUsage = value * 1024 * 1024 } };
            var checker = new ProcessMemoryLoadChecker(limit);

            var messages = checker.GetAffectedProcesses(testProcs);

            Assert.That(messages.Count(), Is.EqualTo(1));
        }

        [Test]
        [TestCase(-1)]
        public void ProcessMemoryLoadChecker_GetAffectedProcesses_ShouldThrowExceptionWhenInvalidLimitValueProvided(double limit)
        {
            Assert.Throws<ArgumentException>(() => new ProcessMemoryLoadChecker(limit));
        }
    }
}