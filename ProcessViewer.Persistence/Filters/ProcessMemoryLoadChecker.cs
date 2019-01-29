using System;
using System.Collections.Generic;
using System.Linq;
using ProcessViewer.Core.Models.Abstract;
using ProcessViewer.Persistence.Filters.Abstract;

namespace ProcessViewer.Persistence.Filters
{
    /// <summary>   The process memory load checker. </summary>
    ///
    /// <seealso cref="T:ProcessViewer.Persistence.Filters.Abstract.ILoadChecker{ProcessViewer.Core.Models.Abstract.IProcessModel}"/>

    public class ProcessMemoryLoadChecker : ILoadChecker<IProcessModel>
    {
        /// <summary>   Gets the type of load checker. </summary>
        ///
        /// <value> The type of load checker. </value>
        ///
        /// <seealso cref="P:ProcessViewer.Persistence.Filters.Abstract.ILoadChecker{ProcessViewer.Core.Models.Abstract.IProcessModel}.Type"/>

        public string Type => $"Exceeded Memory load limit of {Limit:F} Mb";

        /// <summary>   Gets the limit in Mb. </summary>
        ///
        /// <value> The limit in Mb. </value>
        ///
        /// <seealso cref="P:ProcessViewer.Persistence.Filters.Abstract.ILoadChecker{ProcessViewer.Core.Models.Abstract.IProcessModel}.Limit"/>

        public double Limit { get; }

        /// <summary>   Constructor. </summary>
        ///
        ///  <exception cref="ArgumentException">    Thrown when limit is less than zero. </exception>
        ///
        /// <param name="limit">    The limit in Mb. </param>

        public ProcessMemoryLoadChecker(double limit)
        {
            if (limit < 0)
            {
                throw new ArgumentException("Limit value cannot be less than zero");
            }


            Limit = limit;
        }

        /// <summary>   Gets the processes that exceed the specified limit. </summary>
        ///
        /// <param name="processModels">    The collection of process models. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the affected processes in this
        /// collection.
        /// </returns>
        ///
        /// <seealso cref="M:ProcessViewer.Persistence.Filters.Abstract.ILoadChecker{ProcessViewer.Core.Models.Abstract.IProcessModel}.GetAffectedProcesses(IEnumerable{IProcessModel})"/>

        public IEnumerable<IProcessModel> GetAffectedProcesses(IEnumerable<IProcessModel> processModels)
        {
            return processModels.Where(p => p.RamUsage > Limit * 1024 * 1024);
        }
    }
}