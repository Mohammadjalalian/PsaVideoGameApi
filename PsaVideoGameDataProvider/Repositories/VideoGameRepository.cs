using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PsaVideoGameCommon;
using PsaVideoGameDataProvider.IRepositories;

namespace PsaVideoGameDataProvider.Repositories
{
  public class VideoGameRepository : PsaRepository<VideoGame>  ,IVideoGameRepository
  {
    public VideoGameRepository(PsaContext context) : base(context, context.VideoGames)
    {
    }
  }
}
