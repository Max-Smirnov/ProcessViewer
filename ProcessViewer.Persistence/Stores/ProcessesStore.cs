using System.Collections.Generic;
using ProcessViewer.Core.Models.Abstract;
using ProcessViewer.Persistence.Filters.Abstract;
using ProcessViewer.Persistence.Repositories.Abstract;
using ProcessViewer.Persistence.Stores.Abstract;

namespace ProcessViewer.Persistence.Stores
{
    /// <summary>   The processes store. </summary>
    ///
    /// <seealso cref="T:ProcessViewer.Persistence.Stores.Abstract.IProcessesStore"/>

    public class ProcessesStore : IProcessesStore
    {
        /// <summary>   Gets the process model repo. </summary>
        ///
        /// <value> The process model repo. </value>

        private IRepository<IProcessModel> ProcessModelsRepo { get; }

        /// <summary>   Gets or sets the process models. </summary>
        ///
        /// <value> The process models. </value>

        private IEnumerable<IProcessModel> ProcessModels { get; set; }

        /// <summary>   Process the high load notificationHandler. </summary>
        ///
        /// <param name="sender">   The sender. </param>
        /// <param name="args">     Process high load event information. </param>

        /// <summary>   Constructor. </summary>
        ///
        /// <param name="loadCheckerRunner">           Specifies the loadCheckerRunner. </param>
        /// <param name="processModelsRepo"> The process model repo. </param>

        public ProcessesStore(IRepository<IProcessModel> processModelsRepo)
        {
            ProcessModelsRepo = processModelsRepo;
            ProcessModels = new List<IProcessModel>();
        }

        /// <summary>   Gets all items in this collection. </summary>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process all items in this collection.
        /// </returns>

        public IEnumerable<IProcessModel> GetAll()
        {
            return ProcessModels;
        }

        /// <summary>   Refreshes the list of processes. </summary>
        ///
        /// <seealso cref="M:ProcessViewer.Persistence.Stores.Abstract.IProcessesStore.Refresh()"/>

        public void Refresh()
        {
            ProcessModels = ProcessModelsRepo.GetAll();
        }
    }
}
