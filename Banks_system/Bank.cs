using System.ComponentModel.DataAnnotations;

namespace Banks_system
{
    public class Bank
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
