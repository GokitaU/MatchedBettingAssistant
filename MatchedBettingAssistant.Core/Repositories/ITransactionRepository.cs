namespace MatchedBettingAssistant.Core.Repositories
{
    public interface ITransactionRepository
    {
        ITransaction New();

        ITransactionDetail NewDetail();
        void AddDetail(ITransactionDetail detail);
    }

    
}
