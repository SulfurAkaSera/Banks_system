using System.ComponentModel.DataAnnotations;

namespace Banks_system
{
    public class Card
    {
        [Key]
        public int Id { get; set; }
        public Bank Bank { get; set; }
        public string Num { get; set; }
        public int CVV { get; set; }
        public Users User { get; set; }
        public float Balance { get; set; }
    }
}
