using System.ComponentModel.DataAnnotations;

namespace Banks_system
{
    public class Transactions
    {
        [Key]
        public int Id { get; set; } 
        public Users Sender { get; set; }
        public Users Recipient { get; set; }
        public decimal Money { get; set; }
        public Card Card { get; set; }
    }
}
