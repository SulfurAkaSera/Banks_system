using System.Data.Entity;
using System.Configuration;

namespace Banks_system
{
    public class DataBase : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DataBase() : base(ConfigurationManager.ConnectionStrings["TZ"].ConnectionString) { }

    }    
}
