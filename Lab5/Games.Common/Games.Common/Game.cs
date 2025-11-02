using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Common
{
    public class Game
    {
        // Статичне поле
        public static int GameCount;

        // Конструктор
        public Game()
        {
            Id = Guid.NewGuid();
            GameCount++;
        }

        // Статичний конструктор
        static Game()
        {
            GameCount = 0;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }

        // Метод
        public void PrintInfo()
        {
            Console.WriteLine($"Game: {Name}, Price: {Price}, Rating: {Rating}");
        }

        // Подія
        public event Action<string> OnNameChanged;

        // Делегат
        public delegate void GameDelegate(string message);

        public void ChangeName(string newName)
        {
            Name = newName;
            OnNameChanged?.Invoke($"Назва гри змінена на {newName}");
        }
    }
}
