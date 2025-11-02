using Games.Nosql.Models;
using Games.Nosql.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Games.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // 🔌 Підключення до MongoDB Atlas
            string connectionString = "mongodb+srv://user:qwerty123@test.eyeamvk.mongodb.net/";
            string databaseName = "GamesDB";

            var mongoRepo = new MongoRepository(connectionString, databaseName);

            // 🕹️ Створюємо ігри
            var cyberpunk = new GameDocument { Name = "Cyberpunk 2077", Genre = "RPG", Price = 59.99 };
            var stalker = new GameDocument { Name = "S.T.A.L.K.E.R. 2: Heart of Chornobyl", Genre = "Shooter", Price = 49.99 };

            await mongoRepo.AddAsync(cyberpunk);
            await mongoRepo.AddAsync(stalker);
            Console.WriteLine("✅ Ігри додано у MongoDB.");

            // Вивід усіх ігор
            var allGames = await mongoRepo.GetAllAsync();
            Console.WriteLine("\n=== 📜 Усі ігри ===");
            foreach (var g in allGames)
                Console.WriteLine($"{g.Id} | {g.Name} | {g.Genre} | ${g.Price}");

            // 🔍 Пошук гри за ID
            var first = allGames.FirstOrDefault();
            if (first != null)
            {
                var found = await mongoRepo.GetByIdAsync(first.Id);
                Console.WriteLine($"\n🔎 Знайдено гру: {found?.Name}");
            }

            // ✏️ Оновлення
            if (first != null)
            {
                first.Price = 39.99;
                await mongoRepo.UpdateAsync(first);
                Console.WriteLine($"\n💾 Оновлено ціну для {first.Name}");
            }

            // ❌ Видалення
            if (first != null)
            {
                await mongoRepo.DeleteAsync(first.Id);
                Console.WriteLine($"\n🗑️ Видалено гру {first.Name}");
            }

            Console.WriteLine("\n✅ Тест MongoDB завершено.");
        }
    }
}
