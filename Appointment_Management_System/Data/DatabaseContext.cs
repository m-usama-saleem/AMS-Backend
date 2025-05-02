using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Appointment_Management_System.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Appointment_Management_System.Data
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<AppUsers> AppUsers { get; set; }
        public DbSet<AppointmentInfo> AppointmentInfo { get; set; }
        public DbSet<Translators> Translators { get; set; }
        public DbSet<Institutions> Institutions { get; set; }
        public DbSet<Audit> AuditLogs { get; set; }
        public DbSet<Finance> Finance { get; set; }

        public virtual int SaveChanges(string userId = null)
        {
            OnBeforeSaveChanges(userId);
            var result = base.SaveChanges();
            return result;
        }
        
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<AppUsers>()
        //        .HasKey(o => new { o.Name });

        //    modelBuilder.Entity<AppointmentInfo>()
        //        .HasKey(o => new { o.AppointmentId });

        //    modelBuilder.Entity<Translators>()
        //        .HasKey(o => new { o.Name, o.Language });

        //    modelBuilder.Entity<Institutions>()
        //        .HasKey(o => new { o.Name });
        //}

        private void OnBeforeSaveChanges(string userId)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntry.UserId = userId;
                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = Enums.AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            auditEntry.AuditType = Enums.AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = Enums.AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }
            foreach (var auditEntry in auditEntries)
            {
                AuditLogs.Add(auditEntry.ToAudit());
            }
        }
    }
}
