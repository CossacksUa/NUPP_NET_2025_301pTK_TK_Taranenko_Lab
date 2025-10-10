using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Common
{
    public class PCGame : Game
    {
        // Конструктор
        public PCGame() : base() { }

        public string MinimumRequirements { get; set; }
        public string RecommendedRequirements { get; set; }
        public string Platform { get; set; } = "PC";

        // Статичний метод
        public static void PrintPlatform()
        {
            Console.WriteLine("Це ПК гра");
        }

        //  Новий статичний метод для створення гри
        public static PCGame CreateNew(string name, double price, double rating,
                                       string minReq, string recReq)
        {
            return new PCGame
            {
                Id = Guid.NewGuid(),   // Унікальний ідентифікатор
                Name = name,
                Price = price,
                Rating = rating,
                MinimumRequirements = minReq,
                RecommendedRequirements = recReq
            };
        }


    }
}