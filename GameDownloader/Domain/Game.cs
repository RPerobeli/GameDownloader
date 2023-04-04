namespace GameDownloader.Domain
{
    public class Game
    {
        public int idGame { get; set; }
        public string? GameName { get; set; }
        public byte[]? GameFile { get; set; }
    }
}
