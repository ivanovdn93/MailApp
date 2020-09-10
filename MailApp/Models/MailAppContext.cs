using Microsoft.EntityFrameworkCore;

namespace MailApp.Models
{
    public class MailAppContext : DbContext
    {
        public MailAppContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Mail> Mails { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
    }
}
