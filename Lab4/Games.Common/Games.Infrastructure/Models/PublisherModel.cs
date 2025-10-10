using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Infrastructure.Models
{
    public class PublisherModel
    {
        public Guid Id { get; set; }
        public string PublisherName { get; set; }
        public string Country { get; set; }
        public ICollection<GameModel> Games { get; set; }
    }
}