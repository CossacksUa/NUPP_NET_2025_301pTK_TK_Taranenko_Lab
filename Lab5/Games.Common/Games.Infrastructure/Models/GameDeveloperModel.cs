using System;

namespace Games.Infrastructure.Models
{
    public class GameDeveloperModel
    {
        public Guid GameId { get; set; }
        public Guid DeveloperId { get; set; }

        public GameModel Game { get; set; }
        public DeveloperModel Developer { get; set; }
    }
}
