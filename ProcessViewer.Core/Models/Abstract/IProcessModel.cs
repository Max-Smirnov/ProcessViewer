using System;

namespace ProcessViewer.Core.Models.Abstract
{
    /// <summary>   Interface for process model. </summary>
    public interface IProcessModel
    {
        /// <summary>   Gets or sets the CPU usage percent. </summary>
        ///
        /// <value> The CPU usage percent. </value>

        double CpuUsagePercent { get; set; }

        /// <summary>   Gets or sets the CPU usage time. </summary>
        ///
        /// <value> The CPU usage time. </value>

        TimeSpan CpuUsageTime { get; set; }

        /// <summary>   Gets or sets the identifier. </summary>
        ///
        /// <value> The identifier. </value>

        int Id { get; set; }

        /// <summary>   Gets or sets the name. </summary>
        ///
        /// <value> The name. </value>

        string Name { get; set; }

        /// <summary>   Gets or sets the priority. </summary>
        ///
        /// <value> The priority. </value>

        int Priority { get; set; }

        /// <summary>   Gets or sets the ram usage. </summary>
        ///
        /// <value> The ram usage. </value>

        long RamUsage { get; set; }

        /// <summary>   Gets or sets the running time. </summary>
        ///
        /// <value> The running time. </value>

        TimeSpan RunningTime { get; set; }
    }
}