using Games.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "pcgames.json";

            // Створюємо CRUD сервіс для PCGame
            CrudService<PCGame> pcService = new CrudService<PCGame>();

            // Створюємо об'єкти PCGame
            PCGame cyberpunk = new PCGame
            {
                Name = "Cyberpunk 2077",
                Price = 59.99,
                Rating = 7.5,
                MinimumRequirements = "8 GB RAM, GTX 1060",
                RecommendedRequirements = "16 GB RAM, RTX 2060"
            };

            PCGame witcher = new PCGame
            {
                Name = "The Witcher 3",
                Price = 39.99,
                Rating = 9.5,
                MinimumRequirements = "8 GB RAM, GTX 770",
                RecommendedRequirements = "16 GB RAM, GTX 1070"
            };

            // Створення (Create)
            pcService.Create(cyberpunk);
            pcService.Create(witcher);

            // Вивід усіх ігор (ReadAll)
            Console.WriteLine("=== Всі ПК ігри ===");
            foreach (var game in pcService.ReadAll())
            {
                game.PrintInfo();
            }

            // Метод розширення: знижка 20% на Cyberpunk
            cyberpunk.Discount(20);
            pcService.Update(cyberpunk);

            Console.WriteLine("\n=== Після знижки на Cyberpunk ===");
            foreach (var game in pcService.ReadAll())
            {
                game.PrintInfo();
            }

            // Видалення The Witcher 3
            pcService.Remove(witcher);

            Console.WriteLine("\n=== Після видалення The Witcher 3 ===");
            foreach (var game in pcService.ReadAll())
            {
                game.PrintInfo();
            }

            // Збереження даних у файл
            pcService.Save(filePath);
            Console.WriteLine($"\nДані збережено у файл: {filePath}");

            // Створюємо новий сервіс і завантажуємо дані
            CrudService<PCGame> pcServiceLoaded = new CrudService<PCGame>();
            pcServiceLoaded.Load(filePath);

            Console.WriteLine("\n=== Дані після завантаження з файлу ===");
            foreach (var game in pcServiceLoaded.ReadAll())
            {
                game.PrintInfo();
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}