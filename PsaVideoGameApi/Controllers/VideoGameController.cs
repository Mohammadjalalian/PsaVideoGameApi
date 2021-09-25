using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PsaVideoGameCommon;
using PsaVideoGameDataProvider;

namespace PsaVideoGameApi.Controllers
{
 
  [ApiController]
  [Route("/api/v1/[controller]")]
  public class VideoGameController : PsaControllerBase<VideoGameController>
  {
    public VideoGameController(ILogger<VideoGameController> logger, IUnitOfWork unitOfWork) : base(logger, unitOfWork)
    {
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      IEnumerable<VideoGame> items = await UnitOfWork.VideoGameRepository.GetAllAsync();
      return Ok(items);
    }

    [Route("{id}")]
    [HttpGet]
    public async Task<IActionResult> GetById(int id)
    {
      var item = await UnitOfWork.VideoGameRepository.GetItemByIdAsync(id);
      return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create(VideoGame videoGame)
    {
      UnitOfWork.VideoGameRepository.Insert(videoGame);
      await UnitOfWork.CommitAllChangesAsync();
      return Ok(videoGame);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Edit(int id, VideoGame videoGame)
    {
      var vid = await UnitOfWork.VideoGameRepository.GetItemByIdAsync(id);
      vid.Name = videoGame.Name;
      vid.Genre = videoGame.Genre;
      vid.Description = videoGame.Description;
      UnitOfWork.VideoGameRepository.Update(vid);
      await UnitOfWork.CommitAllChangesAsync();
      return Ok(videoGame);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      UnitOfWork.VideoGameRepository.Delete(id);
      await UnitOfWork.CommitAllChangesAsync();
      return Ok(id);
    }
  }
}