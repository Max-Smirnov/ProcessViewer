using ProcessViewer.Core.Models.Abstract;
using ProcessViewer.Persistence.Filters.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProcessViewer.Persistence.Filters
{
    /// <summary>   The process CPU load checker. </summary>
    ///
    /// <seealso cref="T:ProcessViewer.Persistence.Filters.Abstract.ILoadChecker{ProcessViewer.Core.Models.Abstract.IProcessModel}"/>

    public class ProcessCpuLoadChecker : ILoadChecker<IProcessModel>
    {
        /// <summary>   Gets the type of load checker. </summary>
        ///
        /// <value> The type of load checker. </value>
        ///
        /// <seealso cref="P:ProcessViewer.Persistence.Filters.Abstract.ILoadChecker{ProcessViewer.Core.Models.Abstract.IProcessModel}.Type"/>

        public string Type => $"Exceeded Cpu load limit of {Limit:F}%";

        /// <summary>   Gets the limit in %. </summary>
        ///
        /// <value> The limit in %. </value>
        ///
        /// <seealso cref="P:ProcessViewer.Persistence.Filters.Abstract.ILoadChecker{ProcessViewer.Core.Models.Abstract.IProcessModel}.Limit"/>

        public double Limit { get; }

        /// <summary>   Constructor. </summary>
        ///
        /// <exception cref="ArgumentException">    Thrown when limit is less than zero or greater than 100%. </exception>
        ///
        /// <param name="limit">    The limit in %. </param>

        public ProcessCpuLoadChecker(double limit)
        {
            if (limit < 0)
            {
                throw new ArgumentException("Limit value cannot be less than zero");
            }
            else if (limit > 100)
            {
                throw new ArgumentException("Limit value cannot be greater than 100%");
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
            return processModels.Where(p => p.CpuUsagePercent > Limit);
        }
    }
}