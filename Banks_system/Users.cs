using System.ComponentModel.DataAnnotations;

namespace Banks_system
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
