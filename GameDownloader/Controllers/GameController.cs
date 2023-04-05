using GameDownloader.Domain.DTO;
using GameDownloader.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameDownloader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public IActionResult GetGameFileForDownload(int id)
        {
            try
            {
                var result = _gameService.GetGamebyId(id);
                return Ok(result);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/GetAllGames")]
        public IActionResult GetAllGames()
        {
            try
            {
                var result = _gameService.GetAllGames();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult InsertGame(string gameName, IFormFile gameFile)
        {
            try
            {
                bool retorno = _gameService.InsertNewGame(gameName, gameFile);
                return Ok($"{gameName} criado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
