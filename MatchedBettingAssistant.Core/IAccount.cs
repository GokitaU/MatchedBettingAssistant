namespace MatchedBettingAssistant.Core
{
    /// <summary>
    /// Interface for classes that represent methods of payment such as
    /// bank accounts and credit cards.
    /// </summary>
    public interface IAccount
    {
        int Id { get; set; }

        string Name { get; set; }

        double StartingBalance { get; set; }

        double Balance { get; }
    }
}