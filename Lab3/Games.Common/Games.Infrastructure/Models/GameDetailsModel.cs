using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Infrastructure.Models
{
    public class GameDetailsModel
    {
        public Guid Id { get; set; } // PK = FK to GameModel
        public string Description { get; set; }
        public string AdditionalInfo { get; set; }
        public GameModel Game { get; set; }
    }
}