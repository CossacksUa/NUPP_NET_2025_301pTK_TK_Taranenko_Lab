using Games.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Створюємо CRUD сервіс для PCGame
            CrudService<PCGame> pcGameService = new CrudService<PCGame>();

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

            // Create
            pcGameService.Create(cyberpunk);
            pcGameService.Create(witcher);

            // ReadAll
            Console.WriteLine("Всі ПК ігри:");
            foreach (var game in pcGameService.ReadAll())
            {
                game.PrintInfo();
            }

            // Update
            cyberpunk.Discount(20); // Метод розширення
            pcGameService.Update(cyberpunk);

            Console.WriteLine("\nПісля знижки на Cyberpunk:");
            foreach (var game in pcGameService.ReadAll())
            {
                game.PrintInfo();
            }

            // Remove
            pcGameService.Remove(witcher);

            Console.WriteLine("\nПісля видалення The Witcher 3:");
            foreach (var game in pcGameService.ReadAll())
            {
                game.PrintInfo();
            }
        }
    }
}