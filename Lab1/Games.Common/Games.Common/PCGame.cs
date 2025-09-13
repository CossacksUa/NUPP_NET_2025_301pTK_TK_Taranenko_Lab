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
    }
}