using System.Collections.Concurrent;
using DataProcessing.Application.Contracts;
using DataProcessing.Domain.Contracts;

namespace DataProcessing.Infrastructure.Persistence
{
    /// <summary>
    /// Thread-safe default implementation of a repository.
    /// </summary>
    /// <typeparam name="T">Any aggregate root.</typeparam>
    internal class InMemoryRepository<T> : IRepository<T> where T : IAggregateRoot
    {
        protected readonly ConcurrentDictionary<Guid, T> Items = new();

        public void AddOrUpdate(T device)
        {
            Items[device.Id] = device;
        }

        public T? Get(Guid id)
        {
            return Items.TryGetValue(id, out var item) ? item : default;
        }

        public IEnumerable<T> GetAll()
        {
            return Items.Values;
        }

        public bool Remove(Guid id)
        {
            return Items.TryRemove(id, out _);
        }
    }
}

