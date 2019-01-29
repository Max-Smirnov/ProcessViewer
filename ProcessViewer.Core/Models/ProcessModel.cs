using ProcessViewer.Core.Models.Abstract;
using System;
using System.Diagnostics;
using ProcessViewer.Core.Adapters.Abstract;

namespace ProcessViewer.Core.Models
{
    /// <summary>   A data Model for the process. </summary>
    ///
    /// <seealso cref="T:ProcessViewer.Core.Models.Abstract.IProcessModel"/>

    public class ProcessModel : IProcessModel
    {
        /// <summary>   Gets the clock. </summary>
        ///
        /// <value> The clock. </value>

        private IClock Clock { get; }

        /// <summary>   Constructor. </summary>
        ///
        /// <param name="clock">    (Optional) The clock. </param>

        public ProcessModel(IClock clock = null)
        {
            if (clock == null)
            {
                clock = new RealClock();
            }

            Clock = clock;
        }

        /// <summary>   Gets process model. </summary>
        ///
        /// <param name="process">  The process. </param>
        ///
        /// <returns>   The process model. </returns>

        public IProcessModel GetProcessModel(IProcessAdapter process)
        {
            Id = process.Id;
            Name = process.ProcessName;
            RamUsage = process.WorkingSet64;
            Priority = process.BasePriority;
            CpuUsageTime = process.TotalProcessorTime;
            RunningTime = Clock.Now - process.StartTime;
            CpuUsagePercent = 100 * process.TotalProcessorTime.TotalMilliseconds / RunningTime.TotalMilliseconds;
            return this;
        }

        /// <summary>   Gets or sets the identifier. </summary>
        ///
        /// <value> The identifier. </value>
        ///
        /// <seealso cref="P:ProcessViewer.Core.Models.Abstract.IProcessModel.Id"/>

        public int Id { get; set; }

        /// <summary>   Gets or sets the name. </summary>
        ///
        /// <value> The name. </value>
        ///
        /// <seealso cref="P:ProcessViewer.Core.Models.Abstract.IProcessModel.Name"/>

        public string Name { get; set; }

        /// <summary>   Gets or sets the CPU usage percent. </summary>
        ///
        /// <value> The CPU usage percent. </value>
        ///
        /// <seealso cref="P:ProcessViewer.Core.Models.Abstract.IProcessModel.CpuUsagePercent"/>

        public double CpuUsagePercent { get; set; }

        /// <summary>   Gets or sets the CPU usage time. </summary>
        ///
        /// <value> The CPU usage time. </value>
        ///
        /// <seealso cref="P:ProcessViewer.Core.Models.Abstract.IProcessModel.CpuUsageTime"/>

        public TimeSpan CpuUsageTime { get; set; }

        /// <summary>   Gets or sets the ram usage. </summary>
        ///
        /// <value> The ram usage. </value>
        ///
        /// <seealso cref="P:ProcessViewer.Core.Models.Abstract.IProcessModel.RamUsage"/>

        public long RamUsage { get; set; }

        /// <summary>   Gets or sets the priority. </summary>
        ///
        /// <value> The priority. </value>
        ///
        /// <seealso cref="P:ProcessViewer.Core.Models.Abstract.IProcessModel.Priority"/>

        public int Priority { get; set; }

        /// <summary>   Gets or sets the running time. </summary>
        ///
        /// <value> The running time. </value>
        ///
        /// <seealso cref="P:ProcessViewer.Core.Models.Abstract.IProcessModel.RunningTime"/>

        public TimeSpan RunningTime { get; set; }
    }
}