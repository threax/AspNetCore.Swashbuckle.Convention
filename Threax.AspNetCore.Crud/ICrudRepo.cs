using System.Threading.Tasks;
using Threax.AspNetCore.Halcyon.Ext;

namespace Threax.AspNetCore.Crud
{
    /// <summary>
    /// A base interface for any crud repos. These repos handle basic crud for one entity in your database.
    /// </summary>
    /// <typeparam name="Key">The key type for the entity.</typeparam>
    /// <typeparam name="Query">The query type for the entity.</typeparam>
    /// <typeparam name="InputModel">The entity's input model.</typeparam>
    /// <typeparam name="ViewModel">The entity's view model.</typeparam>
    /// <typeparam name="ViewModelCollection">The ViewModelColleciton type for the entity.</typeparam>
    public interface ICrudRepo<Key, Query, InputModel, ViewModel, ViewModelCollection> where Query : PagedCollectionQuery
    {
        /// <summary>
        /// Add a new item to the repository.
        /// </summary>
        /// <param name="value">The item to add.</param>
        /// <returns>The view of the newly added item.</returns>
        Task<ViewModel> Add(InputModel value);

        /// <summary>
        /// Delete the item specified by key.
        /// </summary>
        /// <param name="id">The id of the item to delete.</param>
        /// <returns>The item to delete.</returns>
        Task Delete(Key id);

        /// <summary>
        /// Get the entity specified by id or null if it does not exist.
        /// </summary>
        /// <param name="id">The id of the item to get.</param>
        /// <returns>The entity or null if it could not be found.</returns>
        Task<ViewModel> Get(Key id);

        /// <summary>
        /// List the entities that match the given query.
        /// </summary>
        /// <param name="query">The query to filter by.</param>
        /// <returns>The result collection.</returns>
        Task<ViewModelCollection> List(Query query);

        /// <summary>
        /// Update the entity specified by id with the value in value. Note that if you have not setup
        /// the InputModel -> Entity map to ignore ids the ids of the target model could be overwritten
        /// if id does not match the InputModel's id.
        /// </summary>
        /// <param name="id">The id of the entity to update.</param>
        /// <param name="value">The value to update.</param>
        /// <returns></returns>
        Task<ViewModel> Update(Key id, InputModel value);
    }
}
