namespace MatchedBettingAssistant.Model.Account
{
    public interface IAccountTransferTransaction : ITransaction
    {
        /// <summary>
        /// Gets or sets the source of thr transaction (where the money
        /// is coming from)
        /// </summary>
        IAccount Source { get; set; }

        /// <summary>
        /// Gets or sets the target of the transaction (where the money
        /// is going to)
        /// </summary>
        IAccount Target { get; set; }

    }


}