using System.Collections.Generic;
using System.Linq;
using ProcessViewer.Core.Models.Abstract;
using ProcessViewer.Persistence.Filters.Abstract;

namespace ProcessViewer.Persistence.Filters
{
    /// <summary>   A load checker runner. </summary>
    ///
    /// <seealso cref="T:ProcessViewer.Persistence.Filters.Abstract.ILoadCheckerRunner"/>

    public class LoadCheckerRunner : ILoadCheckerRunner
    {
        /// <summary>   Gets the list of load checkers. </summary>
        ///
        /// <value> The load checkers. </value>
        ///
        /// <seealso cref="P:ProcessViewer.Persistence.Filters.Abstract.ILoadCheckerRunner.LoadCheckers"/>

        public IEnumerable<ILoadChecker<IProcessModel>> LoadCheckers { get; }

        /// <summary>   Constructor. </summary>
        ///
        /// <param name="loadCheckers"> The load checkers. </param>

        public LoadCheckerRunner(IEnumerable<ILoadChecker<IProcessModel>> loadCheckers)
        {
            LoadCheckers = loadCheckers;
        }

        /// <summary>   Gets the notification messages of affected processes. </summary>
        ///
        /// <param name="processes">    The processes. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the notifications in this collection.
        /// </returns>
        ///
        /// <seealso cref="M:ProcessViewer.Persistence.Filters.Abstract.ILoadCheckerRunner.GetNotifications(IEnumerable{IProcessModel})"/>

        public IEnumerable<string> GetNotifications(IEnumerable<IProcessModel> processes)
        {
            return from checker in LoadCheckers
                let procs = checker.GetAffectedProcesses(processes).ToList()
                where procs.Any()
                select $"{string.Join(",", procs.Select(p => p.Name))} {checker.Type}";
        }
    }
}