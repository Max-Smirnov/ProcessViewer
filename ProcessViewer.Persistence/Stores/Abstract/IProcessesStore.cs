using System.Collections.Generic;
using ProcessViewer.Core.Models.Abstract;

namespace ProcessViewer.Persistence.Stores.Abstract
{
    /// <summary>   Interface for processes store. </summary>
    public interface IProcessesStore
    {
        /// <summary>   Gets all items in this collection. </summary>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process all items in this collection.
        /// </returns>

        IEnumerable<IProcessModel> GetAll();

        /// <summary>   Refreshes the list of processes. </summary>
        void Refresh();
    }
}