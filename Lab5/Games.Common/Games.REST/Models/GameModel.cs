namespace Games.REST.Models
{
    public class GameModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }   // ✅ стало decimal
        public decimal Rating { get; set; }  // ✅ стало decimal
    }

}
