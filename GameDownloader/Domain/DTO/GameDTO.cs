using Microsoft.AspNetCore.Mvc;

namespace GameDownloader.Domain.DTO
{
    public class GameDTO
    {
        public int idGame { get; set; }
        public string? GameName { get; set; }
        public FileStreamResult? GameFile { get; set; }
    }

    public class GameWithoutFileDTO
    {
        public int idGame { get; set; }
        public string? GameName { get; set; }
    }

}
