namespace ChessSystem.Application.Common.Interfaces
{
    using System.Threading.Tasks;
    using System.Threading;

    public interface IChessApplicationData
    {
        Task<int> SaveChanges(CancellationToken cancellationToken);
    }
}
