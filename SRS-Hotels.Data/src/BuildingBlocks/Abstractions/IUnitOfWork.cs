using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SRS_Hotels.Data.src.BuildingBlocks.Abstractions
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}