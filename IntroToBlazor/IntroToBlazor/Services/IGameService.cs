using IntroToBlazor.Entities;

namespace IntroToBlazor.Services
{
    public interface IGameService
    {
        Task<List<Game>> GetAllGames();
        Task<Game> AddGame(Game game);
    }
}
