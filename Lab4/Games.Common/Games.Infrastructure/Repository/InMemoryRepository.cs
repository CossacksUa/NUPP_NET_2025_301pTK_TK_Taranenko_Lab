using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Infrastructure.Repository
{
    public class InMemoryRepository<T> : IRepository<T> where T : class
    {
        private readonly List<T> _items = new();

        public Task AddAsync(T entity)
        {
            _items.Add(entity);
            return Task.CompletedTask;
        }

        public Task Delete(T entity)
        {
            _items.Remove(entity);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(_items.AsEnumerable());
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            var prop = typeof(T).GetProperty("Id");
            var item = _items.FirstOrDefault(x => (Guid)prop.GetValue(x)! == id);
            return Task.FromResult(item);
        }

        public Task Update(T entity)
        {
            var prop = typeof(T).GetProperty("Id");
            var id = (Guid)prop.GetValue(entity)!;
            var index = _items.FindIndex(x => (Guid)prop.GetValue(x)! == id);
            if (index >= 0)
                _items[index] = entity;
            return Task.CompletedTask;
        }

        public Task SaveAsync() => Task.CompletedTask;
    }
}