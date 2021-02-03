using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VirtualMind.Domain.Entities;

namespace VirtualMind.Application.Interfaces
{
    public interface IVirtualMindDbContext
    {
        DbSet<Operation> Operations { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());        
    }
}
