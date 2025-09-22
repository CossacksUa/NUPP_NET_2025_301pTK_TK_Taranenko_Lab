using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Games.Common
{
    public class CrudServiceAsync<T> : ICrudServiceAsync<T>
    {
        private readonly ConcurrentDictionary<Guid, T> _storage = new();

        // Асинхронне створення
        public Task<bool> CreateAsync(T element)
        {
            var idProp = typeof(T).GetProperty("Id");
            if (idProp == null) return Task.FromResult(false);

            var id = (Guid)idProp.GetValue(element);
            return Task.FromResult(_storage.TryAdd(id, element));
        }

        // Асинхронне читання по ID
        public Task<T> ReadAsync(Guid id)
        {
            _storage.TryGetValue(id, out var value);
            return Task.FromResult(value);
        }

        // Отримати всі елементи
        public Task<IEnumerable<T>> ReadAllAsync()
        {
            return Task.FromResult(_storage.Values.AsEnumerable());
        }

        // Пагінація
        public Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
        {
            return Task.FromResult(_storage.Values.Skip((page - 1) * amount).Take(amount));
        }

        // Оновлення
        public Task<bool> UpdateAsync(T element)
        {
            var idProp = typeof(T).GetProperty("Id");
            if (idProp == null) return Task.FromResult(false);

            var id = (Guid)idProp.GetValue(element);
            _storage[id] = element;
            return Task.FromResult(true);
        }

        // Видалення
        public Task<bool> RemoveAsync(T element)
        {
            var idProp = typeof(T).GetProperty("Id");
            if (idProp == null) return Task.FromResult(false);

            var id = (Guid)idProp.GetValue(element);
            return Task.FromResult(_storage.TryRemove(id, out _));
        }

        // Збереження у файл
        public async Task<bool> SaveAsync(string filePath)
        {
            try
            {
                var json = JsonSerializer.Serialize(_storage.Values, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(filePath, json);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Завантаження з файлу
        public async Task<bool> LoadAsync(string filePath)
        {
            if (!File.Exists(filePath)) return false;
            try
            {
                var json = await File.ReadAllTextAsync(filePath);
                var list = JsonSerializer.Deserialize<List<T>>(json);
                if (list == null) return false;

                _storage.Clear();
                foreach (var item in list)
                {
                    var idProp = typeof(T).GetProperty("Id");
                    if (idProp != null)
                    {
                        var id = (Guid)idProp.GetValue(item);
                        _storage.TryAdd(id, item);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        // Підтримка IEnumerable
        public IEnumerator<T> GetEnumerator() => _storage.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}