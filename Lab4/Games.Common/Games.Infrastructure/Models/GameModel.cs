using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Infrastructure.Models
{
    public class GameModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }

        // Один-до-одного
        public GameDetailsModel Details { get; set; }

        // Один-до-багатьох: Publisher -> Games
        public Guid? PublisherId { get; set; }
        public PublisherModel Publisher { get; set; }

        // many-to-many через join entity
        public ICollection<GameDeveloperModel> GameDevelopers { get; set; }
    }
}