using System;
using ProcessViewer.Core.Models.Abstract;

namespace ProcessViewer.Core.Models
{
    /// <summary>   A real clock. </summary>
    ///
    /// <seealso cref="T:ProcessViewer.Core.Models.Abstract.IClock"/>

    public class RealClock : IClock
    {
        /// <summary>   Gets the current date and time. </summary>
        ///
        /// <value> Current value. </value>
        ///
        /// <seealso cref="P:ProcessViewer.Core.Models.Abstract.IClock.Now"/>

        public DateTime Now => DateTime.Now;
    }
}