using DataProcessing.Domain.Contracts;

namespace DataProcessing.Application.Contracts
{
    /// <summary>
    /// Base interface for entity repository.
    /// </summary>
    /// <typeparam name="T">Any aggregate root.</typeparam>
    public interface IRepository<T> where T : IAggregateRoot
    {
        /// <summary>
        /// Adds a new item to repository or replaces existing one.
        /// </summary>
        /// <param name="item">Item to add.</param>
        void AddOrUpdate(T item);

        /// <summary>
        /// Gets item by ID.
        /// </summary>
        /// <param name="id">Item ID.</param>
        /// <returns>Item if exists. Null otherwise.</returns>
        T? Get(Guid id);

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns>All items.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Tries to remove item.
        /// </summary>
        /// <param name="id">Item ID to remove.</param>
        /// <returns>True if removed. False if not present.</returns>
        bool Remove(Guid id);
    }
}
