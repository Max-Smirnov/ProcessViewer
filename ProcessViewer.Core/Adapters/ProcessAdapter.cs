using System;
using System.Diagnostics;
using ProcessViewer.Core.Adapters.Abstract;

namespace ProcessViewer.Core.Adapters
{
    /// <summary>   The process adapter. </summary>
    ///
    /// <seealso cref="T:ProcessViewer.Core.Adapters.Abstract.IProcessAdapter"/>

    public class ProcessAdapter : IProcessAdapter
    {
        /// <summary>   The process. </summary>
        private Process _process;

        /// <summary>   Constructor. </summary>
        ///
        /// <param name="process">  The process. </param>

        public ProcessAdapter(Process process)
        {
            _process = process;
        }

        /// <summary>   Gets the identifier. </summary>
        ///
        /// <value> The identifier. </value>

        public int Id => _process.Id;

        /// <summary>   Gets the name of the process. </summary>
        ///
        /// <value> The name of the process. </value>

        public string ProcessName => _process.ProcessName;

        /// <summary>   Gets the working set 64. </summary>
        ///
        /// <value> The working set 64. </value>

        public long WorkingSet64 => _process.WorkingSet64;

        /// <summary>   Gets the base priority. </summary>
        ///
        /// <value> The base priority. </value>

        public int BasePriority => _process.BasePriority;

        /// <summary>   Gets the total number of processor time. </summary>
        ///
        /// <value> The total number of processor time. </value>

        public TimeSpan TotalProcessorTime => _process.TotalProcessorTime;

        /// <summary>   Gets the start time. </summary>
        ///
        /// <value> The start time. </value>

        public DateTime StartTime => _process.StartTime;
    }
}