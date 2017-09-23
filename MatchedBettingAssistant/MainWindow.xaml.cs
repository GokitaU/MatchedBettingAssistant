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
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.Model.Account;
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

            var account = new Bookmaker() { Name = "My Wallet", StartingBalance = 10 };

            var accountViewModel = new AccountViewModel(account);

            this.DataContext = accountViewModel;
        }
    }
}
