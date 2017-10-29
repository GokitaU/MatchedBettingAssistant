using MatchedBettingAssistant.Core.Repositories;

namespace MatchedBettingAssistant.Core
{
    public interface IRepository
    {
        IBookmakerRepository BookmakerRepository { get; }

        IWalletRepository WalletRepository { get; }
        IBankRepository BankRepository { get; }

        IBetTypeRepository BetTypeRepository { get; }

        IOfferTypeRepository OfferTypeRepository { get; }

        ITransactionRepository TransactionRepository { get; }

        ISportRepository SportRepository { get; }

        IMarketRepository MarketRepository { get; }

        bool IsModified();

        void Create();
        void Save();

        void Undo();
    }

}