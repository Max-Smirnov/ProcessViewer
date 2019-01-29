using System;
using ProcessViewer.Core.Models;
using ProcessViewer.Core.Models.Abstract;
using ProcessViewer.Persistence.Filters;
using ProcessViewer.Persistence.Filters.Abstract;
using ProcessViewer.Persistence.Repositories.Abstract;
using ProcessViewer.Persistence.Stores;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;

namespace ProcessViewer.Persistence.Tests.Filters
{
    public class ProcessCpuLoadCheckerTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(0.5, 0)]
        [TestCase(5, 4)]
        [TestCase(5, 5)]
        [TestCase(99.9, 99.9)]
        [TestCase(100, 100)]
        public void ProcessCpuLoadChecker_GetAffectedProcesses_ShouldReturnEmptyCollectionIfLimitNotExceeded(double limit, double value)
        {
            var testProcs = new IProcessModel[] { new ProcessModel() { CpuUsagePercent = value } };
            var checker = new ProcessCpuLoadChecker(limit);

            var messages = checker.GetAffectedProcesses(testProcs);

            Assert.That(messages.Count(), Is.EqualTo(0));
        }


        [Test]
        [TestCase(0, 0.1)]
        [TestCase(0, 10)]
        [TestCase(0.5, 1)]
        [TestCase(9.9, 10)]
        [TestCase(99.9, 100)]
        public void ProcessCpuLoadChecker_GetAffectedProcesses_ShouldReturnProcessModelsCollectionIfLimitExceeded(double limit, double value)
        {
            var testProcs = new IProcessModel[] { new ProcessModel() { CpuUsagePercent = value } };
            var checker = new ProcessCpuLoadChecker(limit);

            var messages = checker.GetAffectedProcesses(testProcs);

            Assert.That(messages.Count(), Is.EqualTo(1));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(101)]
        public void ProcessCpuLoadChecker_GetAffectedProcesses_ShouldThrowExceptionWhenInvalidLimitValueProvided(double limit)
        {
            Assert.Throws<ArgumentException>(() => new ProcessCpuLoadChecker(limit));
        }
    }
}