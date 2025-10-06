using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Common
{
    public class ConsoleGame : Game
    {
        // Конструктор
        public ConsoleGame() : base() { }

        public string ConsoleType { get; set; }
        public bool PhysicalCopyAvailable { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}