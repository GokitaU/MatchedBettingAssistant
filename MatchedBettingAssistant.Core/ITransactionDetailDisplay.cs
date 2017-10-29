namespace MatchedBettingAssistant.Core
{
    public interface ITransactionDetailDisplay
    {
        int Id { get; }

        string Description { get; }

        double Profit { get; }

        string BetType { get; }

        string OfferType { get; }

        string Sport { get;  }

        string Market { get; }

    }
}