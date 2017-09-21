using System;

namespace MatchedBettingAssistant.Model
{
    public interface IBet
    {
        IBettingAccount Account { get; set; }
        DateTime Date { get; set; }

        double Returns { get; }

        void Place();
    }

    public interface ISimpleBet : IBet
    {
        new double Returns { get; set; }
    }
}