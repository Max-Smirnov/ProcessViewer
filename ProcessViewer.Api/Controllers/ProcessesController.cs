using System.Collections.Generic;
using System.Linq;
using ProcessViewer.Core.Models.Abstract;
using ProcessViewer.Persistence.Repositories.Abstract;
using ProcessViewer.Persistence.Stores.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ProcessViewer.Api.Controllers
{
    /// <summary>   A controller for handling processes. </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessesController : ControllerBase
    {
        /// <summary>   Gets the processes store. </summary>
        private IProcessesStore ProcessesStore { get; }

        /// <summary>   Constructor. </summary>
        ///
        /// <param name="processesStore">  The processes store. </param>

        public ProcessesController(IProcessesStore processesStore)
        {
            ProcessesStore = processesStore;
        }



        /// <summary>   Gets the the list of the running processes. </summary>
        ///
        /// <returns>   An ActionResult&lt;IEnumerable&lt;IProcessModel&gt;&gt; </returns>

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<IProcessModel>> Get()
        {
            return ProcessesStore.GetAll().ToArray();
        }
    }
}
