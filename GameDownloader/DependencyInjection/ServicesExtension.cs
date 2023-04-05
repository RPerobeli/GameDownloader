using GameDownloader.Repository;
using GameDownloader.Services;

namespace GameDownloader.DependencyInjection
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddGameModules(this IServiceCollection input)
        {
            input
                .AddScoped<IGameRepository, GameRepository>()
                .AddScoped<IGameService, GameService>()
                ;
            return input;
        }
    }
}
