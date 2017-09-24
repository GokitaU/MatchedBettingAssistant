namespace MatchedBettingAssistant.Core
{
    public interface IRepository
    {
        IBookmakerRepository BookmakerRepository { get; }

        IWalletRepository WalletRepository { get; }
    }

}