using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.Model.Account;
using MatchedBettingAssistant.Model.Bets;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class SimpleMatchedBetViewModel : BetViewModel
    {
        private readonly SimpleMatchedBet matchedBet;
        private const double tolerance = 0.0001;
        private AccountLookupItem backAccount;
        private AccountLookupItem layAccount;
        private ObservableCollection<AccountLookupItem> bookmakers;
        private ObservableCollection<AccountLookupItem> exchanges;

        public SimpleMatchedBetViewModel(SimpleMatchedBet matchedBet, 
                                         IEnumerable<IBettingAccount> bookmakers, 
                                         IEnumerable<IBettingAccount> exchanges)
        {
            this.matchedBet = matchedBet;
            CreateBookmakers(bookmakers);
            CreateExchanges(exchanges);
        }

        public DateTime Date
        {
            get => this.matchedBet.Date;
            set
            {
                if (this.matchedBet.Date == value)
                    return;

                this.matchedBet.Date = value;

                RaisePropertyChanged(() => this.Date);
            }
        }

        public double BackReturns
        {
            get => this.matchedBet.BackReturns;
            set
            {
                if (Math.Abs(this.matchedBet.BackReturns - value) < tolerance)
                    return;

                this.matchedBet.BackReturns = value;

                RaisePropertiesChanged(() => this.BackReturns, ()=> Profit);
            }
        }

        public double LayReturns
        {
            get => this.matchedBet.LayReturns;
            set
            {
                if (Math.Abs(this.matchedBet.LayReturns - value) < tolerance)
                    return;

                this.matchedBet.LayReturns = value;

                RaisePropertiesChanged(() => this.LayReturns, ()=> this.Profit);
            }
        }

        public double Profit
        {
            get => this.matchedBet.LayReturns - this.matchedBet.BackReturns;
        }

        public ObservableCollection<AccountLookupItem> Bookmakers
        {
            get => bookmakers;
            set
            {
                this.bookmakers = value;
                this.RaisePropertyChanged(()=>Bookmakers);
            }
        }

        public ObservableCollection<AccountLookupItem> Exchanges
        {
            get => exchanges;
            private set
            {
                this.exchanges = value;
                this.RaisePropertyChanged(()=>Exchanges);
            }
        }

        public AccountLookupItem BackAccount
        {
            get { return backAccount; }
            set
            {
                backAccount = value;
                this.matchedBet.BackAccount = value?.Account as IBettingAccount;
                RaisePropertyChanged(()=>BackAccount);
            }
        }

        public AccountLookupItem LayAccount
        {
            get { return layAccount; }
            set
            {
                layAccount = value;
                this.matchedBet.LayAccount = value?.Account as IBettingAccount;
                RaisePropertyChanged(()=>LayAccount);
            }
        }


        public override void Commit()
        {
            this.matchedBet.Place();

            Messenger.Default.Send(new TransactionsUpdatedMessage());
        }

        private void CreateExchanges(IEnumerable<IBettingAccount> exchanges)
        {
            this.Exchanges = new ObservableCollection<AccountLookupItem>(exchanges.Select(x => new AccountLookupItem(x)));

            this.LayAccount =
                this.Exchanges.FirstOrDefault(x => ReferenceEquals(x.Account, this.matchedBet.LayAccount));
        }

        private void CreateBookmakers(IEnumerable<IBettingAccount> bookmakers)
        {
            this.Bookmakers = new ObservableCollection<AccountLookupItem>(bookmakers.Select(x => new AccountLookupItem(x)));

            this.BackAccount = this.Bookmakers.FirstOrDefault(x => ReferenceEquals(x.Account, this.matchedBet.BackAccount));
        }
    }
}