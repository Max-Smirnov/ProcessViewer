using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ProcessViewer.Core.Adapters;
using ProcessViewer.Core.Adapters.Abstract;
using ProcessViewer.Core.Models;
using ProcessViewer.Core.Models.Abstract;
using ProcessViewer.Persistence.Repositories.Abstract;

namespace ProcessViewer.Persistence.Repositories
{
    /// <summary>   The process models repository. </summary>
    ///
    /// <seealso cref="T:ProcessViewer.Persistence.Repositories.Abstract.IRepository{ProcessViewer.Core.Models.Abstract.IProcessModel}"/>

    public class MacProcessModelsRepository : IRepository<IProcessModel>
    {
        /// <summary>   Gets all items running processes. </summary>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process all items in this collection.
        /// </returns>

        public IEnumerable<IProcessModel> GetAll()
        {
            return Process.GetProcesses().Where(p => !p.HasExited && p.Responding && p.WorkingSet64 > 0)
                .Select(p => new ProcessModel().GetProcessModel(new ProcessAdapter(p))).ToList();
        }
    }
}