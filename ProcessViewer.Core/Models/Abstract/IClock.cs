using System;

namespace ProcessViewer.Core.Models.Abstract
{
    /// <summary>   Interface for clock. </summary>
    public interface IClock
    {
        /// <summary>   Gets the current date and time. </summary>
        ///
        /// <value> Current value. </value>

        DateTime Now { get; }
    }
}