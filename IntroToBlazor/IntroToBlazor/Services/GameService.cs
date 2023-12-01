using IntroToBlazor.Data;
using IntroToBlazor.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntroToBlazor.Services
{
    public class GameService : IGameService
    {
        // we are not using dtos here. We should implement
        private readonly DataContext _context;

        public GameService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Game>> GetAllGames()
        {
            await Task.Delay(1000);

            var games = await _context.Games.ToListAsync();
            return games;
        }

        public async Task<Game> AddGame(Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            return game;
        }
    }
}
