using System;

namespace MatchedBettingAssistant.Model
{
    public interface IBet
    {
        IBettingAccount Account { get; set; }
        DateTime Date { get; set; }
        double Odds { get; set; }
        double Stake { get; set; }

        double Returns { get; }

        void Lost();
        void Place();
        void Won();
    }
}