using Core.Context;
using Core.Context.Interfaces;
using Core.Interfaces;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class DI
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<GameContext>();
            services.AddScoped<IGameRepository, GameRepository>();

            services.AddScoped<IGameFactory, GameFactory>();
            services.AddScoped<IGameEngine, GameEngine>();
        }
    }
}
