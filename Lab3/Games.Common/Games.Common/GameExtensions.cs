using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Common
{
    public static class GameExtensions
    {
        // Метод розширення
        public static void Discount(this Game game, double percent)
        {
            game.Price -= game.Price * percent / 100;
        }
    }
}