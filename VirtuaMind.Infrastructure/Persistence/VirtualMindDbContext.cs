using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using VirtualMind.Application.Interfaces;
using VirtualMind.Domain.Entities;

namespace VirtuaMind.Infrastructure.Persistence
{
    public class VirtualMindDbContext : DbContext, IVirtualMindDbContext
    {
        public VirtualMindDbContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<Operation> Operations { get; set; }

        public DbSet<OperationCurrency> OperationCurrencies { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:                        
                        entry.Entity.Created = DateTime.Now;
                        break;                    
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);            

            return result;
        }
    }
}
