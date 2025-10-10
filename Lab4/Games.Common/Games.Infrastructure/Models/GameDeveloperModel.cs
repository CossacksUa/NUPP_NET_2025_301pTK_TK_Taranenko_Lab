using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Infrastructure.Models
{
    public class GameDeveloperModel
    {
        public Guid GameId { get; set; }
        public GameModel Game { get; set; }

        public Guid DeveloperId { get; set; }
        public DeveloperModel Developer { get; set; }

        public string Role { get; set; }
    }
}