using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class EditBankViewModel : ViewModelBase
    {
        private readonly IBank account;

        private IRepository repository;

        public EditBankViewModel(IBank account, IRepository repository)
        {
            this.account = account;
            this.repository = repository;
        }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name
        {
            get => this.account.Name;
            set
            {
                this.account.Name = value;
                RaisePropertyChanged(() => this.Name);
            }
        }

        /// <summary>
        /// Gets or sets the starting balance
        /// </summary>
        public double StartingBalance
        {
            get => this.account.StartingBalance;
            set
            {
                this.account.StartingBalance = value;
                RaisePropertyChanged(() => this.StartingBalance);
            }
        }

        public double PointValue
        {
            get => this.account.PointValue;
            set
            {
                if (this.account.PointValue == value)
                    return;

                this.account.PointValue = value;

                RaisePropertyChanged(() => this.PointValue);
            }
        }

        public double PointsBalance
        {
            get
            {
                var profit = this.account.Balance - this.StartingBalance;
                return profit != 0.00 ? profit / this.PointValue : 0;
            }
        }

        /// <summary>
        /// Gets the current balance
        /// </summary>
        public double Balance => this.account.Balance;

    }
}