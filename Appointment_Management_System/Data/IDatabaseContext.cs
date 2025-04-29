using System;
using Appointment_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Appointment_Management_System.Data;

public interface IDatabaseContext : IDisposable
{
    DbSet<AppUsers> AppUsers { get; set; }
    DbSet<AppointmentInfo> AppointmentInfo { get; set; }
    DbSet<Translators> Translators { get; set; }
    DbSet<Institutions> Institutions { get; set; }
    DbSet<Audit> AuditLogs { get; set; }
    DbSet<Finance> Finance { get; set; }
    int SaveChanges(string userId = null);
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}