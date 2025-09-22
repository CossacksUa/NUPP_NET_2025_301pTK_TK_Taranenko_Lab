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

            CrudService<PCGame> pcService = new CrudService<PCGame>();

            PCGame cyberpunk = PCGame.CreateNew(
                "Cyberpunk 2077", 59.99, 7.5,
                "8 GB RAM, GTX 1060", "16 GB RAM, RTX 2060");

            PCGame witcher = PCGame.CreateNew(
                "The Witcher 3", 39.99, 9.5,
                "8 GB RAM, GTX 770", "16 GB RAM, GTX 1070");

            pcService.Create(cyberpunk);
            pcService.Create(witcher);

            Console.WriteLine("=== Всі ПК ігри ===");
            foreach (var game in pcService.ReadAll())
            {
                game.PrintInfo();
            }

            cyberpunk.Discount(20);
            pcService.Update(cyberpunk);

            Console.WriteLine("\n=== Після знижки на Cyberpunk ===");
            foreach (var game in pcService.ReadAll())
            {
                game.PrintInfo();
            }

            pcService.Remove(witcher);

            Console.WriteLine("\n=== Після видалення The Witcher 3 ===");
            foreach (var game in pcService.ReadAll())
            {
                game.PrintInfo();
            }

            pcService.Save(filePath);
            Console.WriteLine($"\nДані збережено у файл: {filePath}");

            CrudService<PCGame> pcServiceLoaded = new CrudService<PCGame>();
            pcServiceLoaded.Load(filePath);

            Console.WriteLine("\n=== Дані після завантаження з файлу ===");
            foreach (var game in pcServiceLoaded.ReadAll())
            {
                game.PrintInfo();
            }

            // Виклик демо синхронізації
            Console.WriteLine("\n=== Демонстрація примітивів синхронізації ===");
            SyncDemo.Run();

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}
