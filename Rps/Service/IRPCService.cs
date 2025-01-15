using MyClassLibrary.Models;

namespace Rps.Service
{
    public interface IRPCService
    {
        void PlayGame();
        void ShowAllGames();
        void DeleteGameById();
        void UpdateGameById();
        MyClassLibrary.Models.Rpc FindGameById(int id);
    }
}
