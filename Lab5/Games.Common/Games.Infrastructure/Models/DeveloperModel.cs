using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Infrastructure.Models
{
    public class DeveloperModel
    {
        public Guid Id { get; set; }
        public string DeveloperName { get; set; }
        public int FoundedYear { get; set; }

        public ICollection<GameDeveloperModel> GameDevelopers { get; set; }
    }
}