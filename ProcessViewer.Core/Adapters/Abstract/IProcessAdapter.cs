using System;

namespace ProcessViewer.Core.Adapters.Abstract
{
    /// <summary>   Interface for process adapter. </summary>
    public interface IProcessAdapter
    {
        /// <summary>   Gets the identifier. </summary>
        ///
        /// <value> The identifier. </value>

        int Id { get; }

        /// <summary>   Gets the name of the process. </summary>
        ///
        /// <value> The name of the process. </value>

        string ProcessName { get; }

        /// <summary>   Gets the working set 64. </summary>
        ///
        /// <value> The working set 64. </value>

        long WorkingSet64 { get; }

        /// <summary>   Gets the base priority. </summary>
        ///
        /// <value> The base priority. </value>

        int BasePriority { get; }

        /// <summary>   Gets the total number of processor time. </summary>
        ///
        /// <value> The total number of processor time. </value>

        TimeSpan TotalProcessorTime { get; }

        /// <summary>   Gets the start time. </summary>
        ///
        /// <value> The start time. </value>

        DateTime StartTime { get; }
    }
}