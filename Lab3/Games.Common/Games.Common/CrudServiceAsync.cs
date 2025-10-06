using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Games.Infrastructure.Repository;

namespace Games.Common
{
    public class CrudServiceAsync<T> : ICrudServiceAsync<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public CrudServiceAsync(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<bool> CreateAsync(T element)
        {
            await _repository.AddAsync(element);
            await _repository.SaveAsync();
            return true;
        }

        public async Task<T> ReadAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<T>> ReadAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
        {
            var all = await _repository.GetAllAsync();
            return all.Skip((page - 1) * amount).Take(amount);
        }

        public async Task<bool> UpdateAsync(T element)
        {
            await Task.Run(() => _repository.Update(element));
            await _repository.SaveAsync();
            return true;
        }

        public async Task<bool> RemoveAsync(T element)
        {
            await Task.Run(() => _repository.Delete(element));
            await _repository.SaveAsync();
            return true;
        }

        public async Task<bool> SaveAsync()
        {
            await _repository.SaveAsync();
            return true;
        }
    }
}
