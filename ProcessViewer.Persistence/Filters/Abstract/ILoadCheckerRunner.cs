using System.Collections.Generic;
using ProcessViewer.Core.Models.Abstract;

namespace ProcessViewer.Persistence.Filters.Abstract
{
    /// <summary>   Interface for load checker runner. </summary>
    public interface ILoadCheckerRunner
    {
        /// <summary>   Gets the list of load checkers. </summary>
        ///
        /// <value> The load checkers. </value>

        IEnumerable<ILoadChecker<IProcessModel>> LoadCheckers { get; }

        /// <summary>   Gets the notification messages of affected processes. </summary>
        ///
        /// <param name="processes">    The processes. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the notifications in this collection.
        /// </returns>

        IEnumerable<string> GetNotifications(IEnumerable<IProcessModel> processes);
    }
}