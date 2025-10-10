namespace Games.REST.Models
{
    public class GameModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public double Rating { get; set; }
    }
}
