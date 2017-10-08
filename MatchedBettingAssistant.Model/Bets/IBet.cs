using System;
using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.Model.Bets
{
    public interface IBet
    {
        IBettingAccount Account { get; set; }
        DateTime Date { get; set; }

        double Returns { get; }

        void Place();

        void Complete();
    }
}