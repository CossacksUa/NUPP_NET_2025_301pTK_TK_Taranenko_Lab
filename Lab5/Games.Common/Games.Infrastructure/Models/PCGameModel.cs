using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Infrastructure.Models
{
    public class PCGameModel : GameModel
    {
        public string MinimumRequirements { get; set; }
        public string RecommendedRequirements { get; set; }
        public string Platform { get; set; } = "PC";
    }
}