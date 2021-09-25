using System.Threading;
using System.Threading.Tasks;
using PsaVideoGameDataProvider.IRepositories;

namespace PsaVideoGameDataProvider
{
    public interface IUnitOfWork
    {
        PsaContext Context { get; }

    //object ENTITYRepository { get; }

    public IVideoGameRepository VideoGameRepository { get; }

    int CommitAllChanges();
        Task<int> CommitAllChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
