namespace ChessSystem.Application.Common.Interfaces
{
    using System.Threading.Tasks;

    using ChessSystem.Application.Common.Models;
    using ChessSystem.Application.Common.ServiceInterfaces;

    public interface IIdentity : IService
    {
        Task<string> GetUserNameAsync(string userId);

        string GetUserName(string userId);

        Task<(Result Result, string UserId)> CreateUser(string userName, string password);

        Task<Result> DeleteUser(string userId);
    }
}
