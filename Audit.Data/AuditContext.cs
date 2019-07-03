using Microsoft.EntityFrameworkCore;

namespace Audit.Data
{
    public class AuditContext : DbContext
    {
        public AuditContext()
        {
        }

        public AuditContext(DbContextOptions<AuditContext> ConnectionStrings)
            : base(ConnectionStrings)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        
        public virtual DbSet<Audit_Notification> Audit_Notifications { get; set; }
        public virtual DbSet<Audit_User> Audit_Users { get; set; }
    }
}
