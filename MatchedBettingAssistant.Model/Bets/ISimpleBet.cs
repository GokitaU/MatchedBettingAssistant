using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.Model.Bets
{
    public interface ISimpleBet : IBet
    {
        new double Returns { get; set; }

        /// <summary>
        /// The transaction created by this bet
        /// </summary>
        ITransaction Transaction { get; }
    }
}