using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;
using MatchedBettingAssistant.DataAccess;
using MatchedBettingAssistant.DataAccess.Repositories;
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.ViewModel.Account;


namespace MatchedBettingAssistant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : DXWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var db = new Repository();
            db.Create();

            var bookmakers = db.BookmakerRepository.GetAccounts();
            var wallets = db.WalletRepository.GetWallets();

            var account = new AccountViewModel(bookmakers.FirstOrDefault(), wallets);

            this.DataContext = account;
        }
    }
}
