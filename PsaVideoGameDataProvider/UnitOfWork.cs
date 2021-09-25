using System.Threading;
using System.Threading.Tasks;
using PsaVideoGameDataProvider.IRepositories;
using PsaVideoGameDataProvider.Repositories;

namespace PsaVideoGameDataProvider
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(PsaContext context)
        {
            Context = context;
        }
        public PsaContext Context { get; }



        public int CommitAllChanges()
        {
            return Context.SaveChanges();
        }
        public async Task<int> CommitAllChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Context.SaveChangesAsync(cancellationToken);
        }

        private IVideoGameRepository _videoGameRepository;
        public IVideoGameRepository VideoGameRepository => _videoGameRepository ??= new VideoGameRepository(Context);
     
       
  }

}
