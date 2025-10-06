using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;



namespace Games.Common
{
    public class CrudService<T> : ICrudService<T> where T : Game
    {
        private readonly List<T> _elements = new List<T>();

        public void Create(T element) => _elements.Add(element);

        public T Read(Guid id) => _elements.FirstOrDefault(e => e.Id == id);

        public IEnumerable<T> ReadAll() => _elements;

        public void Update(T element)
        {
            var existing = Read(element.Id);
            if (existing != null)
            {
                _elements.Remove(existing);
                _elements.Add(element);
            }
        }

        public void Remove(T element) => _elements.Remove(element);

        // Сохраняє дані у файл JSON
        public void Save(string FilePath)
        {
            var json = JsonSerializer.Serialize(_elements, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }

        // Завантажує дані з файлу JSON
        public void Load(string FilePath)
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                var elements = JsonSerializer.Deserialize<List<T>>(json);
                if (elements != null)
                {
                    _elements.Clear();
                    _elements.AddRange(elements);
                }
            }
        }
    }
}