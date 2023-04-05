using GameDownloader.Domain;
using GameDownloader.Repository.Context;

namespace GameDownloader.Repository
{
    public interface IGameRepository
    {
        public Game GetGameById(int id);
        public IList<Game> GetAllGames();
        public bool InsertGame(Game game);
        public void Commit();
    }
    public class GameRepository : IGameRepository
    {
        private readonly GameDownloaderContext _context;

        public GameRepository(GameDownloaderContext gameDownloaderContext)
        {
            this._context = gameDownloaderContext;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public IList<Game> GetAllGames()
        {
            IList<Game> gameList = _context.Games.ToList();
            return gameList;
        }

        public Game GetGameById(int id)
        {
            Game? game = _context.Games.Where(w => w.idGame == id).First();
            return game;
        }

        public bool InsertGame(Game game)
        {
            try
            {
                _context.Games.Add(game);
                Commit();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
