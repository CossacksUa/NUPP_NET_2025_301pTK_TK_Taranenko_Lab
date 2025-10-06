using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Infrastructure.Models
{
    public class ConsoleGameModel : GameModel
    {
        public string ConsoleType { get; set; }
        public bool PhysicalCopyAvailable { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}