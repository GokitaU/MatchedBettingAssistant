namespace MatchedBettingAssistant.DataAccess.DataModel
{
    /// <summary>
    /// A bank keeps a record of bets made for a certain system or category to 
    /// keep track of its profitability
    /// </summary>
    public class Bank : Account
    {
        /// <summary>
        /// The amount each point is valued at
        /// </summary>
        public double PointValue { get; set; }
    }
}