using GameDownloader.Domain;
using GameDownloader.Domain.DTO;
using GameDownloader.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;

namespace GameDownloader.Services
{
    public interface IGameService
    {
        public GameDTO GetGamebyId(int id);
        public IList<GameWithoutFileDTO> GetAllGames();
        public bool InsertNewGame(string gameName, IFormFile gameFile);
    }
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private IWebHostEnvironment _env;
        public GameService(IGameRepository gameRepository, IWebHostEnvironment env)
        {
            _gameRepository = gameRepository;
            _env = env;
        }
        public IList<GameWithoutFileDTO> GetAllGames()
        {
            IList<Game> listGames = _gameRepository.GetAllGames();
            IList<GameWithoutFileDTO>  listGamesDTO = MapToDTO(listGames);
            return listGamesDTO;
        }


        public GameDTO GetGamebyId(int id)
        {
            Game game = _gameRepository.GetGameById(id);
            GameDTO gameDTO = new GameDTO();
            gameDTO.idGame = game.idGame;
            gameDTO.GameName = game.GameName;

            if(game.GameFile != null)
            {
                string fileName = $"{game.GameName}.zip";
                string contentType = "";

                new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);
                if (string.IsNullOrWhiteSpace(_env.WebRootPath))
                {
                    _env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory());
                }
                string path = Path.Combine(_env.WebRootPath) + "\\Files";

                FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                gameDTO.GameFile = new FileStreamResult(fileStream, contentType);
            }
            return gameDTO;
        }



        private static IList<GameWithoutFileDTO> MapToDTO(IList<Game> listGames)
        {
            IList <GameWithoutFileDTO> listGamesDTO = new List<GameWithoutFileDTO>();
            foreach (var game in listGames)
            {
                GameWithoutFileDTO gameDTO = new GameWithoutFileDTO();
                gameDTO.idGame = game.idGame;
                gameDTO.GameName = game.GameName;

                listGamesDTO.Add(gameDTO);
            }
            return listGamesDTO;
        }

        public bool InsertNewGame(string gameName, IFormFile gameFile)
        {
            #region map de classe de game
            Game newGame = new Game();
            newGame.GameName = gameName;
            using (MemoryStream ms = new MemoryStream())
            {
                gameFile.CopyTo(ms);
                byte[] fileBytes = ms.ToArray();
                newGame.GameFile = fileBytes;
            }
            #endregion

            bool insertResult = _gameRepository.InsertGame(newGame);
            if(insertResult)
            {
                return insertResult;
            }
            else
            {
                throw new Exception("Falha na inserção do arquivo de jogo.");
            }
        }
    }
}
