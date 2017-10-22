namespace MatchedBettingAssistant.Core
{
    public interface ITransactionDetail
    {
        double Profit { get; set; }

        IBetType BetType { get; set; }

        IOfferType OfferType { get; set; }

        ISport Sport { get; set; }

        IMarket Market { get; set; }

        double PaybackPercent { get; set; }

        double Payback { get; }

        void AddTransaction(ITransaction transaction);
    }
}