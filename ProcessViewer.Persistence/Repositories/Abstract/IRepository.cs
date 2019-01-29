using System.Collections.Generic;

namespace ProcessViewer.Persistence.Repositories.Abstract
{
    /// <summary>   Interface for repository. </summary>
    ///
    /// <typeparam name="T">    Generic type parameter. </typeparam>

    public interface IRepository<out T>
    {
        /// <summary>   Gets all items in this collection. </summary>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process all items in this collection.
        /// </returns>

        IEnumerable<T> GetAll();
    }
}