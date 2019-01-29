using System.Collections.Generic;

namespace ProcessViewer.Persistence.Filters.Abstract
{
    /// <summary>   Interface for load checker. </summary>
    ///
    /// <typeparam name="T">    Generic type parameter. </typeparam>

    public interface ILoadChecker<T>
    {
        /// <summary>   Gets the type of load checker. </summary>
        ///
        /// <value> The type of load checker. </value>

        string Type { get; }

        /// <summary>   Gets the limit. </summary>
        ///
        /// <value> The limit. </value>

        double Limit { get; }

        /// <summary>   Gets the processes that exceed the specified limit. </summary>
        ///
        /// <param name="processModels">    The collection of process models. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the affected processes in this
        /// collection.
        /// </returns>

        IEnumerable<T> GetAffectedProcesses(IEnumerable<T> processModels);
    }
}